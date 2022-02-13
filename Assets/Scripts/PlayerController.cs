using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6;

    private Rigidbody rigidbody;
    private Camera camera;
    private Vector3 velocity;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        camera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 mousePosition = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camera.transform.position.y));
        transform.LookAt(mousePosition + Vector3.up * transform.position.y);
        velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * speed;
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + velocity * Time.deltaTime);
    }
}
