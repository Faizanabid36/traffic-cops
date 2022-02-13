using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public Transform pathHolder;


    public float speed = 5f;
    public float waitTime = 1.5f;

    private void Start()
    {
        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
            waypoints[i] = pathHolder.GetChild(i).position;

        StartCoroutine(TraversePath(waypoints));
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

    IEnumerator TraversePath(Vector3[] wayPoints)
    {
        transform.position = wayPoints[0];
        int targetIndex = 1;

        Vector3 targetWayPoint = wayPoints[targetIndex];
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWayPoint, speed * Time.deltaTime);
            if(transform.position == targetWayPoint)
            {
                targetIndex = (targetIndex + 1) % wayPoints.Length;
                targetWayPoint = wayPoints[targetIndex];
                yield return new WaitForSeconds(waitTime);
            }
            yield return null;
        }
    }
}
