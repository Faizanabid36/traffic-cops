using UnityEngine;

[System.Serializable]
public class GuardRotationList
{
    [Range(-360f, 360f)]
    public float angle;

    public float duration;
    public float rotationSpeed = 5f;
}
