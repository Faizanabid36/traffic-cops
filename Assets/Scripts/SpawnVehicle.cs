using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class SpawnVehicle : MonoBehaviour
{
    public GameObject[] Vehicles;
    private int i=-1, c=0;
    public float force=100f;


    void Start()
    {
        if(c<=Vehicles.Length){
            c++;
        StartCoroutine (SpawnVehicles());
        
        }
        else{
            StopCoroutine(SpawnVehicles());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // new Vector3(Random.Range(minX,maxX),transform.position.y,transform.position.z)
    IEnumerator SpawnVehicles(){
        yield return new WaitForSeconds(Random.Range(1,4));
        GameObject thisobject=Instantiate(Vehicles[(Random.Range(0,Vehicles.Length))],transform.position,Quaternion.identity) as GameObject ;
        
        
        if(c<=Vehicles.Length){
            c++;
        StartCoroutine (SpawnVehicles());
        }
        else{
            StopCoroutine(SpawnVehicles());
        }
    }
}
