using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{

    private float rateIncrease;
    [SerializeField] private float spawnRate;
    [SerializeField] private GameObject slimePrefab;
    private float spawnTimer;

    void Start()
    {
        spawnTimer = 0f;
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > 10f / spawnRate) {
            Instantiate(slimePrefab, new Vector3(Random.Range(-15f,15f),0f,Random.Range(-15f,15f)), Quaternion.identity);
            spawnTimer = 0f;
		}
    }
}
