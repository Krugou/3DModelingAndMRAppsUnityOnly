using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject knightPrefab; // Assign your BlackCubeKnightMovement Prefab in the inspector
    public float spawnDelay = 5.0f; // Delay in seconds before spawning the knight
    public GameObject plane; // Reference to the plane GameObject

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnKnights", spawnDelay);
    }

    void SpawnKnights()
    {
        // Get the size of the plane
        Vector3 planeSize = plane.GetComponent<MeshRenderer>().bounds.size;

        // Generate a random position within the spawn range
        Vector3 spawnPosition = new Vector3(
          Random.Range(-planeSize.x / 2, planeSize.x / 2),
          knightPrefab.transform.position.y, // Use the Y position from the prefab
          Random.Range(-planeSize.z / 2, planeSize.z / 2)
        );

        // Instantiate a new knight at the spawn position
        Instantiate(knightPrefab, spawnPosition, Quaternion.identity);
    }
}