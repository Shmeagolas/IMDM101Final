using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


public class NetworkMoveComponent : NetworkBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed, turnSpeed;
    [SerializeField] private Transform camTransform;
    [SerializeField] private Camera camPrefab;

    private int tick = 0;
    private float tickRate = 1f / 128f;
    private float tickDeltaTime = 0f;

    private const int BUFFER_SIZE = 1024;
    private InputState[] inputStates = new InputState[BUFFER_SIZE];
    private TransformState[] transformStates = new TransformState[BUFFER_SIZE];

    public NetworkVariable<TransformState> serverTransformState = new NetworkVariable<TransformState>();
    public TransformState previousTransformState;

    private Items item;

	private void onEnable()
	{
        serverTransformState.OnValueChanged += OnServerStateChanged;
	}

	public override void OnNetworkSpawn()
	{
        base.OnNetworkSpawn();
		if (IsLocalPlayer && camTransform != null && camPrefab != null) {
            Camera cam = Instantiate(camPrefab);
            cam.transform.parent = camTransform;

            Transform equipment = transform.Find("CameraSocket").Find("Equipment");
            List<Renderer[]> weaponList = new List<Renderer[]>();
            List<Animator> animList = new List<Animator>();
			foreach (Transform weapon in equipment.transform) {
                animList.Add(weapon.gameObject.GetComponent<Animator>());
                Renderer[] parts = new Renderer[weapon.childCount];
                int i = 0;
                foreach(Transform part in weapon.transform) {
                    parts[i] = part.GetComponent<Renderer>();
                    i++;
				}
                weaponList.Add(parts);

			}


            item = new Items(weaponList, animList);
		}
	}

	private void OnServerStateChanged(TransformState previous, TransformState current)
	{
        previousTransformState = previous;
	}

    public void ProcessLocalPlayerMovement(Vector2 moveInput, Vector2 lookInput, bool clicking)
	{
        tickDeltaTime += Time.deltaTime;
        if(tickDeltaTime > tickRate)
		{
            int bufferIndex = tick % BUFFER_SIZE;
			if (!IsServer)
			{
                MovePlayerServerRPC(tick, moveInput, lookInput);
                MovePlayer(moveInput);
                RotatePlayer(lookInput);
                UsePlayer(clicking);
			}
			else
			{
                MovePlayer(moveInput);
                RotatePlayer(lookInput);
                UsePlayer(clicking);
                TransformState state = new TransformState()
                {
                    Tick = tick,
                    position = transform.position,
                    rotation = transform.rotation,
                    hasStartedMoving = true

                };

                previousTransformState = serverTransformState.Value;
                serverTransformState.Value = state;

                
            }

            InputState inputState = new InputState()
            {
                Tick = tick,
                MoveInput = moveInput,
                LookInput = lookInput
            };

            TransformState transformState = new TransformState()
            {
                Tick = tick,
                position = transform.position,
                rotation = transform.rotation,
                hasStartedMoving = true

            };

            inputStates[bufferIndex] = inputState;
            transformStates[bufferIndex] = transformState;

            tickDeltaTime -= tickRate;
            tick++;


        }

        
	}


    public void ProcessSimulatedPlayerMovement()
	{
        tickDeltaTime += Time.deltaTime;
        if(tickDeltaTime > tickRate)
		{
			if (serverTransformState.Value.hasStartedMoving)
			{
                transform.position = serverTransformState.Value.position;
                transform.rotation = serverTransformState.Value.rotation;
			}
            tickDeltaTime -= tickRate;
            tick++;
		}
	}

    private void MovePlayer(Vector2 moveInput)
	{
        Vector3 move = moveInput.x * camTransform.right + moveInput.y * camTransform.forward;
        move.y = 0f;
        move.Normalize();

		if (!characterController.isGrounded)
		{
            move.y = -9.81f;
		}

        characterController.Move(move * speed * tickRate);
	}


    private void RotatePlayer(Vector2 lookInput)
	{
        camTransform.RotateAround(transform.position, camTransform.right, -1f * lookInput.y * turnSpeed * tickRate);
        transform.RotateAround(transform.position, transform.up, lookInput.x * turnSpeed * tickRate);
	}

    private void UsePlayer(bool clickInput) {
        if (clickInput) {
            item.Use();
        }
	}



    [ServerRpc]
	private void MovePlayerServerRPC(int tick, Vector2 moveInput, Vector2 lookInput)
	{



        MovePlayer(moveInput);
        RotatePlayer(lookInput);

        TransformState state = new TransformState()
        {
            Tick = tick,
            position = transform.position,
            rotation = transform.rotation,
            hasStartedMoving = true

        };

        previousTransformState = serverTransformState.Value;
        serverTransformState.Value = state;
	}
}
