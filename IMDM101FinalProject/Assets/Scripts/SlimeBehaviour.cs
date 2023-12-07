using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    private CharacterController characterController;
    private GameObject target;
    private float checkTimer = 0f;
    private Quaternion baseRot;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        checkTimer = 1f;
        baseRot = characterController.transform.rotation;

    }


    void Update()
    {
        checkTimer +=  Time.deltaTime;
        if(checkTimer > 1f) {
            float distance = Mathf.Infinity;
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players) {
				if (distance > Vector3.Distance(player.transform.position, characterController.transform.position)) {
                    distance = Vector3.Distance(player.transform.position, characterController.transform.position);
                    target = player;
                }


            }
            checkTimer = 0f;
        }

        if(target != null) {
            Vector3 dir = characterController.transform.position - target.transform.position;
            Quaternion rot = Quaternion.LookRotation(dir, Vector3.up);
            characterController.transform.rotation = rot;
            characterController.transform.forward = new Vector3(1f, 0f, 0f) * speed * Time.deltaTime;
            transform.Find("slimeModel").rotation = baseRot;
        }
        

    }
}
