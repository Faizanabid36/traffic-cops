using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
     public WheelCollider wheelB;
    public WheelCollider wheelF;
 
    public Transform wheelFTrans;
    public Transform wheelBTrans;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        wheelFTrans.Rotate (0,  0,wheelF.rpm / 60 * 360 * Time.deltaTime);
        wheelBTrans.Rotate (0, 0 ,wheelB.rpm / 60 * 360 * Time.deltaTime);
        
    }
}
