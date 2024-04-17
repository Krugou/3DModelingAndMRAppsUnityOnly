using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCubeKnightMovement : MonoBehaviour
{
    public float speed = 0.5f; // Speed of movement
    public GameObject ballKing; // Reference to the BallKing GameObject
    public float separationDistance = 1.0f; // The minimum distance the objects will keep from each other

    // Update is called once per frame
    void Update()
    {
        // Calculate the direction towards the BallKing
        Vector3 direction = (ballKing.transform.position - transform.position);
        direction.y = 0; // Ignore Y axis
        direction = direction.normalized;

        // Rotate towards the BallKing
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);

        // Move towards the BallKing
        transform.position += direction * speed * Time.deltaTime;

        // Apply separation force
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj == gameObject) continue; // Skip self

            BlackCubeKnightMovement other = obj.GetComponent<BlackCubeKnightMovement>();
            if (other != null)
            {
                float distance = Vector3.Distance(transform.position, other.transform.position);
                if (distance < separationDistance)
                {
                    Vector3 awayFromOther = (transform.position - other.transform.position);
                    awayFromOther.y = 0; // Ignore Y axis
                    awayFromOther = awayFromOther.normalized;
                    transform.position += awayFromOther * speed * Time.deltaTime;
                }
            }
        }
    }
}