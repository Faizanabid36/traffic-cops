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
        if (Helpers.IsPlayerInRange(transform, player, viewDistance, viewAngle, viewMask))
        {
            spotlight.color = Color.red;
            GameManager.gameOver = true;
        }
        else
            spotlight.color = spotlightColor;
    }
}
