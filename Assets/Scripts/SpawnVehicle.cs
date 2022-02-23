using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class SpawnVehicle : MonoBehaviour
{
    public float force = 100f;
    public Spawn[] spawns;

    private int currentSpawn = 0;


    void Start()
    {
        if (currentSpawn < spawns.Length)
        {
            StartCoroutine(SpawnVehicles());
        }
    }

    IEnumerator SpawnVehicles()
    {
        yield return new WaitForSeconds(spawns[currentSpawn].spawnAfterTime);
        WpPatrol wp = spawns[currentSpawn].prefab.GetComponent<WpPatrol>();
        wp.waypointTag = spawns[currentSpawn].name;
        wp.movementSpeed = spawns[currentSpawn].speed;
        spawns[currentSpawn].prefab.GetComponent<WpPatrol>().waypointTag = spawns[currentSpawn].name;
        Instantiate(spawns[currentSpawn].prefab, transform.position, Quaternion.identity);
        ++currentSpawn;

        if (currentSpawn < spawns.Length)
        {
            StartCoroutine(SpawnVehicles());
        }
        else
        {
            StopCoroutine(SpawnVehicles());
        }
    }
}
