using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public Transform pathHolder;

    public float speed = 5f, waitTime = 1.5f, rotationSpeed = 60f;
    public float viewDistance, rotationPrecision = 0.05f, angleToTakeTurnFrom = 0f;
    public LayerMask viewMask;
    public Color spotlightColor;
    public string armToRaise;


    private Light spotlight;
    private float viewAngle;
    private Transform player;
    private Animator animator;

    private void Start()
    {
        spotlight = GetComponent<Light>();
        spotlightColor = spotlight.color;
        viewAngle = spotlight.spotAngle;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
            waypoints[i] = pathHolder.GetChild(i).position;

        StartCoroutine(TraversePath(waypoints));
    }

    private void Update()
    {
        if (Helpers.IsPlayerInRange(transform, player, viewDistance, viewAngle, viewMask))
        {
            if (armToRaise == "left")
            {
                animator.SetBool("raiseLeftArm", true);
            }
            else if (armToRaise == "right")
            {
                animator.SetBool("raiseRightArm", true);
            }
            spotlight.color = Color.red;
            GameManager.gameOver = true;
            UIManager.Instance.PlayerWasCaught();
        }
        else
            spotlight.color = spotlightColor;
    }

    IEnumerator TurnToAngle(Vector3 target)
    {
        Vector3 directionToLook = (target - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(directionToLook.z, directionToLook.x) * Mathf.Rad2Deg;
        if (targetAngle < angleToTakeTurnFrom)
        {
            animator.SetBool("isTurningRight", true);
            animator.SetBool("isTurningLeft", false);
        }
        else
        {
            animator.SetBool("isTurningLeft", true);
            animator.SetBool("isTurningRight", false);
        }

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > rotationPrecision)
        {
            //float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
            //transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
        animator.SetBool("isTurningRight", false);
        animator.SetBool("isTurningLeft", false);
    }

    IEnumerator TraversePath(Vector3[] wayPoints)
    {
        transform.position = wayPoints[0];
        int targetIndex = 1;

        Vector3 targetWayPoint = wayPoints[targetIndex];
        transform.LookAt(targetWayPoint);
        while (true)
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, targetWayPoint, speed * Time.deltaTime);

            if (transform.position == targetWayPoint)
            {
                targetIndex = (targetIndex + 1) % wayPoints.Length;
                targetWayPoint = wayPoints[targetIndex];
                animator.SetBool("isWalking", false);
                animator.SetBool("isIdle", true);
                yield return new WaitForSeconds(waitTime);

                //Starts Rotation
                yield return StartCoroutine(TurnToAngle(targetWayPoint));
                //Stop Rotation
                //animator.SetBool("isTurningRight", false);
                //animator.SetBool("isTurningLeft", false);
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
