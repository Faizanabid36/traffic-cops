using PathCreation;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 40;


    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    float distanceTravelled;

    void Start()
    {
        if (pathCreator != null)
        {
            pathCreator.pathUpdated += OnPathChanged;
        }
    }
    void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }

    void Update()
    {
        if (pathCreator != null && !GameManager.gameOver && Input.GetButton("Fire1"))
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }
    }

    private void OnDestroy()
    {
        pathCreator.pathUpdated -= OnPathChanged;
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag=="Cars"){
            Debug.Log("Car collided!!!!!");
        }
    }

}