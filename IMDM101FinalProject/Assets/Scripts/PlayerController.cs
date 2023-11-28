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
	[SerializeField] private Vector2 minMaxRotationX;
    [SerializeField] private Transform camTransform;
	[SerializeField] private NetworkMoveComponent playerMovement;

    private CharacterController characterController;
    private PlayerControls playerControls;

    private float cameraAngle;


	public override void OnNetworkSpawn()
	{
		//base.OnNetworkSpawn();

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

        //Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {

		Vector2 lookInput = playerControls.Default.Look.ReadValue<Vector2>();
		Vector2 moveInput = playerControls.Default.Movement.ReadValue<Vector2>();

		if (IsClient && IsLocalPlayer)
		{
			playerMovement.ProcessLocalPlayerMovement(moveInput, lookInput);
		}
		else
		{
			playerMovement.ProcessSimulatedPlayerMovement();
		}
    }

	/*private void RotateY(float lookInputY)
	{
		float _dt = Time.deltaTime;

		cameraAngle = Vector3.SignedAngle(transform.forward, camTransform.forward, camTransform.right);
		float cameraRotationAmount = lookInputY * turnSpeed * _dt;
		float newCameraAngle = cameraAngle - cameraRotationAmount;

		if(newCameraAngle < minMaxRotationX.x && newCameraAngle > minMaxRotationX.y)
		{
			camTransform.RotateAround(camTransform.position, camTransform.right, lookInputY * -1f * turnSpeed * _dt);
		}
	}*/

}
