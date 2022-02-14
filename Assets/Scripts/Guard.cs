using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public Transform pathHolder;

    public float speed = 5f, waitTime = 1.5f, rotationSpeed = 90f;
    public float viewDistance;
    public LayerMask viewMask;
    public Color spotlightColor;

    private Light spotlight;
    private float viewAngle;
    private Transform player;

    private void Start()
    {
        spotlight = GetComponent<Light>();
        spotlightColor = spotlight.color;
        viewAngle = spotlight.spotAngle;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
            waypoints[i] = pathHolder.GetChild(i).position;

        StartCoroutine(TraversePath(waypoints));
    }

    private void Update()
    {
        if (Helpers.IsPlayerInRange(transform, player, viewDistance, viewAngle, viewMask))
        {
            spotlight.color = Color.red;
            GameManager.gameOver = true;
        }
        else
            spotlight.color = spotlightColor;
    }

    IEnumerator TurnToAngle(Vector3 target)
    {
        Vector3 directionToLook = (target - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(directionToLook.z, directionToLook.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    IEnumerator TraversePath(Vector3[] wayPoints)
    {
        transform.position = wayPoints[0];
        int targetIndex = 1;

        Vector3 targetWayPoint = wayPoints[targetIndex];
        transform.LookAt(targetWayPoint);
        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWayPoint, speed * Time.deltaTime);
            if (transform.position == targetWayPoint)
            {
                targetIndex = (targetIndex + 1) % wayPoints.Length;
                targetWayPoint = wayPoints[targetIndex];
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(TurnToAngle(targetWayPoint));
            }
            yield return null;
        }
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
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }
}
