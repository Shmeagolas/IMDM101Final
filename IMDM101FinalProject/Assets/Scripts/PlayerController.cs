using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : NetworkBehaviour
{
	[SerializeField] private float speed, turnSpeed;
	[SerializeField] private Vector2 minMaxRotationX;
    [SerializeField] private Transform camTransform;

    private CharacterController characterController;
    private PlayerControls playerControls;

    private float cameraAngle;
	private Items myItem;


	public override void OnNetworkSpawn()
	{
		base.OnNetworkSpawn();

        CinemachineVirtualCamera cvm = camTransform.gameObject.GetComponent<CinemachineVirtualCamera>();

		if (IsOwner)
		{
            cvm.Priority = 1;
		}
		else
		{
            cvm.Priority = 0;
		}
	}



	void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerControls.Enable();
		myItem = new Items();

        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
		float _dt = Time.deltaTime;
		if (IsLocalPlayer)
		{
			if (playerControls.Default.Movement.inProgress)
			{
				Vector2 movementInput = playerControls.Default.Movement.ReadValue<Vector2>();
				Vector3 move = movementInput.x * camTransform.right + movementInput.y * camTransform.forward;

				move.y = 0;

				characterController.Move(move * speed * _dt);
			}

			if (playerControls.Default.Look.inProgress)
			{
				Vector2 lookInput = playerControls.Default.Look.ReadValue<Vector2>();
				transform.RotateAround(transform.position, transform.up, lookInput.x * turnSpeed * _dt);
				RotateCamera(lookInput.y);
			}

			if (playerControls.Default.LeftClick.inProgress) {
				myItem.use();
			}
		}
    }

	private void RotateCamera(float lookInputY)
	{
		float _dt = Time.deltaTime;

		cameraAngle = Vector3.SignedAngle(transform.forward, camTransform.forward, camTransform.right);
		float cameraRotationAmount = lookInputY * turnSpeed * _dt;
		float newCameraAngle = cameraAngle - cameraRotationAmount;

		if(newCameraAngle <= minMaxRotationX.x && newCameraAngle >= minMaxRotationX.y)
		{
			camTransform.RotateAround(camTransform.position, camTransform.right, lookInputY * - 1f * turnSpeed * _dt);
		}
	}
}
