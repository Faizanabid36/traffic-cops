using UnityEngine;

public class WpPatrol : MonoBehaviour
{
    public float movementSpeed = 6f, turningSpeed = 1f;
    public string waypointTag;

    bool canMove, wpReached, receivedInput;
    string targetWpToGo;
    int currentWpNumber;
    [SerializeField]
    float maxSpeed = 0f;
    GameObject startingPoint, wpToGo;
    Rigidbody rb;
    Transform waypoints;

    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        Invoke("PatrolNow", 1f);
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
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turningSpeed);
        float speed = IsVehiclePlayer() ? maxSpeed : movementSpeed;
        transform.position += transform.forward * Time.deltaTime * speed;

        if (!IsVehiclePlayer() && currentWpNumber == 0)
        {
            PatrolNow();
        }
    }

    private void Update()
    {

        receivedInput = Input.GetButton("Fire1");
        if (receivedInput)
        {
            if (maxSpeed < movementSpeed)
            {
                maxSpeed += 7.5f * Time.deltaTime;
            }
        }
        else
        {
            if (maxSpeed >= 0.5f)
                maxSpeed -= 17.5f * Time.deltaTime;
            //maxSpeed = maxSpeed - 0.5f;
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            if (IsVehiclePlayer() && !GameManager.gameOver)
            {
                    MoveVehicle();
            }
            else
            {
                MoveVehicle();
            }
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
        //if (collision.gameObject.CompareTag("Cars"))
        //{
        //    GameManager.gameOver = true;
        //    Debug.Log("Car collided!!!!!");
        //}
    }
}