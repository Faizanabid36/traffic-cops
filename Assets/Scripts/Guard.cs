using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public Transform pathHolder;

    private Vector3 currentPosition;
    private int currentPositionIndex;
    private Vector3[] waypoints;

    private void Start()
    {
        waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
            waypoints[i] = pathHolder.GetChild(i).position;
        currentPosition = waypoints[0];
        currentPositionIndex = 0;
        transform.position = currentPosition;
    }

    private void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;
        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, 0.5f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);
    }

    private void Update()
    {
        gameObject.transform.Translate(waypoints[(++currentPositionIndex) % waypoints.Length] * .5f);
        //gameObject.transform.position = waypoints[];
    }
}
