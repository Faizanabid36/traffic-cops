using UnityEngine;

public class WpPatrol : MonoBehaviour
{
    public float movementSpeed = 6f, turningSpeed = 1f;
    [HideInInspector]
    public string waypointTag;

    bool canMove, wpReached;
    string targetWpToGo;
    int currentWpNumber;
    float maxSpeed = 0f;
    GameObject startingPoint, wpToGo;
    Rigidbody rb;
    Transform waypoints;

    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        GameManager.levelCompleted = false; 
        Invoke("PatrolNow", 1f);
        AudioManager.Instance.Play("Traffic");
    }

    public void PatrolNow()
    {
        waypoints = GameObject.FindGameObjectWithTag(waypointTag).transform.parent.transform;
        rb = GetComponent<Rigidbody>();
        if (!IsVehiclePlayer())
        {
            transform.position = waypoints.GetChild(0).position;
            transform.LookAt(waypoints.GetChild(1));
        }
        startingPoint = waypoints.GetChild(0).gameObject;
        targetWpToGo = startingPoint.gameObject.name;
        currentWpNumber = int.Parse(targetWpToGo.Split(char.Parse("-"))[1]);
        wpToGo = waypoints.GetChild(currentWpNumber).gameObject;
        canMove = true;
    }

    private bool IsVehiclePlayer()
    {
        return gameObject.tag == "Player";
    }

    private void MoveVehicle()
    {

        Vector3 lookPos = wpToGo.transform.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        // if (IsVehiclePlayer())
        //     transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * maxTurningSpeed);
        // else
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turningSpeed);
        float speed = IsVehiclePlayer() ? maxSpeed : movementSpeed;
        transform.position += transform.forward * Time.deltaTime * speed;

        if (!IsVehiclePlayer() && currentWpNumber == 0)
            PatrolNow();

    }

    private void Update()
    {
        if (GameManager.gameOver)
            maxSpeed = 0f;
        if (GameManager.levelCompleted)
            maxSpeed = 0f;
        if (Input.GetButton("Fire1"))
        {
            if (maxSpeed < movementSpeed)
            {
                maxSpeed += 10f * Time.deltaTime;
            }
        }
        else
        {
            if (maxSpeed >= 0.5f)
                maxSpeed -= 20f * Time.deltaTime;
            else
                maxSpeed = 0f;
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            if (IsVehiclePlayer() && !GameManager.gameOver && !GameManager.levelCompleted)
                MoveVehicle();
            else
                MoveVehicle();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == waypointTag && !wpReached)
        {
            wpReached = true;
            if (GameObject.Find(waypointTag + "-" + (currentWpNumber + 1)) != null)
                currentWpNumber += 1;
            else
                currentWpNumber = 0;
            wpToGo = waypoints.GetChild(currentWpNumber).gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == waypointTag && wpReached)
            wpReached = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (IsVehiclePlayer() && collision.gameObject.CompareTag("Cars"))
        {
            AudioManager.Instance.PlayOneShot("Crash");
            GameManager.gameOver = true;
        }
    }
}