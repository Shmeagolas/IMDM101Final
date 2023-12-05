using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{
	private PlayerControls playerControls;


    private CharacterController characterController;
    private Vector3 move;

    private Vector3 ascend;
    private readonly float GRAVITY = -9.8f;
    [SerializeField] float height = 2;
    private float groundDistance = .4f;
    [SerializeField] LayerMask groundMask;

    private bool isGrounded;
    private readonly float speed = 4f;
    private float xSpeed;
    private float zSpeed;
    [SerializeField] float acceleration = 2f;
    [SerializeField] float decceleration = 5f;
    private bool lastRight;
    private bool lastUp;


	private void Start()
	{
		if (IsLocalPlayer)
		{
            characterController = GetComponent<CharacterController>();
            lastRight = true;
            lastUp = true;

            playerControls = GetComponent<PlayerControls>();
            playerControls.Enable();
        }
	}


	private void OnNetworkDestroy()
    {
        playerControls.Disable();
    }


    /*public override void OnNetworkSpawn()
    {

        base.OnNetworkSpawn();
        if (IsOwner)
        {
            
        }
    }*/


    // Update is called once per frame
    void Update()
    {
		if (!IsLocalPlayer)
		{
            return;
		}

        float deltaTime = Time.deltaTime;
        isGrounded = Physics.CheckSphere(characterController.transform.position - new Vector3(0f, 0f, height / 2f), groundDistance, groundMask);

        if (isGrounded && ascend.y < 0)
        {
            ascend.y = -2f;
        }

        ascend.y += GRAVITY * deltaTime;
        move = new Vector3(playerControls.Default.Movement.ReadValue<Vector2>().x, 0, playerControls.Default.Movement.ReadValue<Vector2>().y);
        if (move.x > .1f)
        {
            if (xSpeed < 0f && !lastRight)
            {
                xSpeed = -.2f;
                lastRight = true;
            }
            xSpeed += acceleration * deltaTime;
        }
        else if (move.x < -.1f)
        {
            if (xSpeed > 0f && lastRight)
            {
                xSpeed = .2f;
                lastRight = false;
            }
            xSpeed -= acceleration * deltaTime;
        }
        else
        {
            if (xSpeed > .1f)
            {
                xSpeed -= decceleration * deltaTime;
            }
            else if (xSpeed < -.1f)
            {
                xSpeed += decceleration * deltaTime;
            }
            else
            {
                xSpeed = 0;
            }
        }
        xSpeed = Mathf.Clamp(xSpeed, -1f, 1f);
        move.x = xSpeed;

        if (move.z > .1f)
        {
            if (zSpeed < 0f && !lastUp)
            {
                zSpeed = -.2f;
                lastUp = true;
            }
            zSpeed += acceleration * deltaTime;
        }
        else if (move.z < -.1f)
        {
            if (zSpeed > 0f && lastUp)
            {
                zSpeed = .2f;
                lastUp = false;
            }
            zSpeed -= acceleration * deltaTime;
        }
        else
        {
            if (zSpeed > .1f)
            {
                zSpeed -= decceleration * deltaTime;
            }
            else if (zSpeed < -.1f)
            {
                zSpeed += decceleration * deltaTime;
            }
            else
            {
                zSpeed = 0;
            }
        }
        zSpeed = Mathf.Clamp(zSpeed, -1f, 1f);
        move.z = zSpeed;

        move = transform.TransformDirection(new Vector3(move.x, 0f, move.z));

        if (move.magnitude > 1)
        {
            move.Normalize();
        }
        characterController.Move(move * speed * deltaTime);
        characterController.Move(ascend * deltaTime);


    }

}
