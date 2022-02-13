using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    private float viewRadius = 10;
    private float viewAngle = 30;

    public Vector3 DirectionFromAngle(float angle)
    {
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) * 0, Mathf.Cos(angle * Mathf.Deg2Rad)); 
    }
}
