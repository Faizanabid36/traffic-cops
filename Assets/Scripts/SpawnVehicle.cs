using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class SpawnVehicle : MonoBehaviour
{
    public GameObject[] Vehicles;

     private float minX=-1.6f;
    private float maxX=1.6f;
    public float force=100f;


    void Start()
    {
        StartCoroutine (SpawnVehicles());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // new Vector3(Random.Range(minX,maxX),transform.position.y,transform.position.z)
    IEnumerator SpawnVehicles(){
        yield return new WaitForSeconds(Random.Range(1,6));
        GameObject thisobject=Instantiate(Vehicles[(Random.Range(0,Vehicles.Length))],transform.position,Quaternion.identity) as GameObject ;
        // thisobject.GetComponent<Rigidbody>().AddForce(0,0 ,force * Time.deltaTime);

        // if (pathCreator != null && !GameManager.gameOver )
        // {
        //     distanceTravelled += force * Time.deltaTime;
        //     transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        //     transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        // }
        // transform.position += transform.forward * Time.deltaTime * force;
        StartCoroutine (SpawnVehicles());
    }
}
