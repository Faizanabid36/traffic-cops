using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    public WheelCollider wheelBack;
    public WheelCollider wheelFront;
 
    public Transform wheelBackTrans;
    public Transform wheelFrontTrans;
 
    void Start () {
 
    }
 
    void Update ()
 
    {
        wheelFLTrans.Rotate (0, wheelFL.rpm / 60 * 360 * Time.deltaTime, 0);
        wheelFRTrans.Rotate (0, wheelFR.rpm / 60 * 360 * Time.deltaTime, 0);
        wheelBLTrans.Rotate (0, wheelBL.rpm / 60 * 360 * Time.deltaTime, 0);
        wheelBRTrans.Rotate (0, wheelBR.rpm / 60 * 360 * Time.deltaTime, 0);
    }
 
}
