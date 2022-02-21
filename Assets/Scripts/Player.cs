using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform[] waypoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            transform.position = waypoints[0].position;
            int targetIndex = 1;

            Vector3 targetWayPoint = waypoints[targetIndex].position;
            transform.LookAt(targetWayPoint);
            transform.position = Vector3.MoveTowards(transform.position, targetWayPoint, 100 * Time.deltaTime);

            if (transform.position == targetWayPoint)
            {
                targetIndex = (targetIndex + 1) % waypoints.Length;
                targetWayPoint = waypoints[targetIndex].position;
            }
        }
    }
}
