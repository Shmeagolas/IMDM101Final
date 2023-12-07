using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : MonoBehaviour
{
    [SerializeField] private float speed, distance, jump;
    private CharacterController characterController;
    private GameObject target;
    private float checkTimer = 0f, verticalVelocity, health = 100f;
    private bool isGrounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        checkTimer = 1f;
      

    }


    void Update()
    {
        checkTimer +=  Time.deltaTime;
        if(checkTimer > 1f) {
            float distance = Mathf.Infinity;
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players) {
				if (distance > Vector3.Distance(player.transform.position, gameObject.transform.position)) {
                    distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
                    target = player;
                }


            }
            checkTimer = 0f;
        }

        isGrounded = characterController.isGrounded;

        if (characterController.isGrounded) {
            verticalVelocity = -.1f * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space)) {
                verticalVelocity = jump;
            }
        } else {
            verticalVelocity -= 3f * Time.deltaTime;
        }

        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
        characterController.Move(moveVector * Time.deltaTime);




        if (target != null) {

            transform.LookAt(target.transform);


            if (Vector3.Distance(transform.position, target.transform.position) >= distance) {

                characterController.Move(transform.forward * speed * Time.deltaTime);



                if (Vector3.Distance(transform.position, target.transform.position) <= distance) {
                    attack();
                }

            }
        }
        

    }

    private void attack() {
      //  Debug.Log("attack!");
	}

    public void Damage(float damage) {
        health -= damage;
        if(health <= 0) {
            Destroy(gameObject);
		}
	}
}
