using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class SpawnVehicleMove : MonoBehaviour
{
   public float speed = 40;


    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    float distanceTravelled;

    void Start()
    {
        GameObject gm=GameObject.Find("SpawnPath");
        pathCreator = gm.GetComponent<PathCreator>();
        if (pathCreator != null)
        {
            pathCreator.pathUpdated += OnPathChanged;
        }
    }
    void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }

    void Update()
    {
        if (pathCreator != null && !GameManager.gameOver )
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }
    }

    // private void OnDestroy()
    // {
    //     pathCreator.pathUpdated -= OnPathChanged;
    // }
}
