using System.Collections;
using UnityEngine;

public class GuardRotation : MonoBehaviour
{

    public GuardRotationList[] rotationList;
    public float viewDistance;
    public LayerMask viewMask;
    public Color spotlightColor;

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

        StartCoroutine(RotateGuard(rotationList));
    }

    IEnumerator RotateGuard(GuardRotationList[] rotations)
    {
        int currentIndex = 0;
        while (true)
        {
            float t = 0.0f;
            if (rotations[currentIndex].duration > 0)
            {
                animator.SetBool("isTurningLeft", true);
            }
            else
            {
                animator.SetBool("isTurningRight", true);
            }
            while (t < Mathf.Abs(rotations[currentIndex].duration))
            {
                t += Time.deltaTime;
                //float yRotation = Mathf.Lerp(startRotation, endRotation, t / rotations[currentIndex].duration) % 360.0f;
                //transform.eulerAngles = new Vector3(transform.eulerAngles.x, yRotation, transform.eulerAngles.z);
                yield return null;
            }

            animator.SetBool("isTurningLeft", false);
            animator.SetBool("isTurningRight", false);
            yield return new WaitForSeconds(rotations[currentIndex].waitAfterRotation);
            currentIndex = (currentIndex + 1) % rotations.Length;
        }
    }

    private void Update()
    {
        if (Helpers.IsPlayerInRange(transform, player, viewDistance, viewAngle, viewMask))
        {
            spotlight.color = Color.red;
            GameManager.gameOver = true;
            UIManager.Instance.PlayerWasCaught();
        }
        else
            spotlight.color = spotlightColor;
    }
}
