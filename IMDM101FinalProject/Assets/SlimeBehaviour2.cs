using System.Collections;
using System.Collections.Generic;
using UnityEngine;




//BY KATOON STUDIO
//MODIFIED BY EP



public class SlimeBehaviour2 : MonoBehaviour {
    public Transform Player;
    float MoveSpeed = 2;
    float MaxDist = 101;
    float MinDist = 5;


    public float health = 50f;  // code to add



    private void Start() {
    }


    void FixedUpdate() {



        transform.LookAt(Player);

        if (Vector3.Distance(transform.position, Player.position) <= MaxDist) {
            if (Vector3.Distance(transform.position, Player.position) <= MinDist) {
            } else {
                transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            }

        }

    }
    // code to add
    public void takeDamage(float amount) {
        health -= amount;
        if (health <= 0f) {
            Die();
        }
    }
    void Die() {
        Destroy(transform.gameObject);

    }

    // code to add



}