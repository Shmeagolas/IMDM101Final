
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : NetworkBehaviour
{
	[SerializeField] private float speed, turnSpeed;
	[SerializeField] private Vector2 minMaxRotationX;
    [SerializeField] private Transform camTransform;
	[SerializeField] private Camera camPrefab;

	private CharacterController characterController;
    private PlayerControls playerControls;
	private float cameraAngle;
	private Items myItem;



	public override void OnNetworkSpawn()
	{
		base.OnNetworkSpawn();

		if (IsLocalPlayer) {
			Camera cam = Instantiate(camPrefab);
			cam.transform.position = new Vector3(0f, 0.5f, 0f);
			cam.transform.parent = camTransform;

		}
	}



	void Start()
    {
		if (IsLocalPlayer) {
			characterController = GetComponent<CharacterController>();
			playerControls = new PlayerControls();
			playerControls.Enable();
			myItem = new Items();
		}
        

        //Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
		float _dt = Time.deltaTime;
		if (IsLocalPlayer)
		{
			Debug.Log("locale");
			if (playerControls.Default.Movement.inProgress)
			{
				Vector2 movementInput = playerControls.Default.Movement.ReadValue<Vector2>();
				Vector3 move = movementInput.x * camTransform.right + movementInput.y * camTransform.forward;

				move.y = 0;
				move.Normalize();
				characterController.Move(move * speed * _dt);
			}

			if (playerControls.Default.Look.inProgress)
			{
				Vector2 lookInput = playerControls.Default.Look.ReadValue<Vector2>();
				transform.RotateAround(transform.position, transform.up, lookInput.x * turnSpeed * _dt);
				RotateCamera(lookInput.y);
			}

			if (playerControls.Default.LeftClick.inProgress) {
				myItem.Use();
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
