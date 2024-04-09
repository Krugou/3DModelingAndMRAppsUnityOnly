using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ballKingMovement : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float jumpForce = 2.0f; // You can adjust this value to get the desired jump height
    public float speed = 0; // You can adjust this value to get the desired speed
    public GameObject plane; // Reference to the plane GameObject

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        // Get the size of the plane
        Vector3 planeSize = plane.GetComponent<Renderer>().bounds.size;

        // Define an offset to make the boundaries tighter
        float offset = 1.0f; // You can adjust this value to get the desired tightness

        // Clamp the position of the BallKing within the boundaries of the plane
        Vector3 position = rb.position;
        position.x = Mathf.Clamp(position.x, -planeSize.x / 2 + offset, planeSize.x / 2 - offset);
        position.z = Mathf.Clamp(position.z, -planeSize.z / 2 + offset, planeSize.z / 2 - offset);
        rb.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
    }

    // move the ball king
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
}