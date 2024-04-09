using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce = 5f; // Set the jump force
    public float jumpSpeed = 1f; // Set the jump speed
    public Transform orientation;
    private float xInput;
    private float yInput;
    private Vector3 movingDirection;
    private Rigidbody rb;
    public GameObject plane;
    public TextMeshProUGUI TargetText;

    public GameObject secondplane;

    public GameObject securityWalls;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PlayerController.Start()");
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        securityWalls.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Read input (keyboard)
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Calculate correct direction
        movingDirection = orientation.forward * yInput + orientation.right * xInput;
        // Move to correct direction
        rb.AddForce(movingDirection.normalized * speed, ForceMode.Force);
        // Do not exceed maximum speed
        Vector3 groundVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (groundVelocity.magnitude > speed)
        {
            Vector3 maxVelocity = groundVelocity.normalized * speed;
            rb.velocity = new Vector3(maxVelocity.x, rb.velocity.y, maxVelocity.z);
        }

        // Jump when the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }

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
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == secondplane)
        {
            securityWalls.SetActive(true);
        }
    }
}