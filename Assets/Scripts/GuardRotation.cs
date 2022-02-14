using System.Collections;
using UnityEngine;

public class GuardRotation : MonoBehaviour
{

    public GuardRotationList[] rotationList;

    public float rotationSpeed = 90f;
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

        StartCoroutine(RotateGuard(rotationList));
    }

    IEnumerator RotateGuard(GuardRotationList[] rotations)
    {
        int currentIndex = 0;
        while (true)
        {
            float startRotation = transform.eulerAngles.y;
            float endRotation = startRotation + rotations[currentIndex].angle;
            float t = 0.0f;
            while (t < rotations[currentIndex].duration)
            {
                t += Time.deltaTime;
                float yRotation = Mathf.Lerp(startRotation, endRotation, t / rotations[currentIndex].duration) % 360.0f;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
                yield return null;
            }

            currentIndex = (currentIndex + 1) % rotations.Length;
            yield return new WaitForSeconds(rotations[currentIndex].duration);
        }   
    }

    private void Update()
    {
        if (IsPlayerInRange())
            spotlight.color = Color.red;
        else
            spotlight.color = spotlightColor;
    }

    private bool IsPlayerInRange()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position, viewMask))
                    return true;
            }
        }
        return false;
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

    //IEnumerator TraversePath(Vector3[] wayPoints)
    //{
    //    transform.position = wayPoints[0];
    //    int targetIndex = 1;

    //    Vector3 targetWayPoint = wayPoints[targetIndex];
    //    transform.LookAt(targetWayPoint);
    //    while (true)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint, speed * Time.deltaTime);
    //        if (transform.position == targetWayPoint)
    //        {
    //            targetIndex = (targetIndex + 1) % wayPoints.Length;
    //            targetWayPoint = wayPoints[targetIndex];
    //            yield return new WaitForSeconds(waitTime);
    //            yield return StartCoroutine(TurnToAngle(targetWayPoint));
    //        }
    //        yield return null;
    //    }
    //}
}
