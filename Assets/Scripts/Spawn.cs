using UnityEngine;

[System.Serializable]
public class Spawn
{
    public string name;
    public float spawnAfterTime;
    public float speed;
    public GameObject prefab;
    
    public float turningSpeed = 3.5f;
}
