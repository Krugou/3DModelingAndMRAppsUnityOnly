using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI elements
using UnityEngine.SceneManagement; // Required for scene management

public class KeepMoving : MonoBehaviour
{
    public GameObject ballKing; // Reference to the BallKing GameObject
    private Vector3 lastPosition; // The last recorded position of the BallKing
    private float timeSinceLastMove; // The time since the BallKing last moved
    public Text gameOverText; // Reference to the Text component

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the last position to the current position of the BallKing
        lastPosition = ballKing.transform.position;
        gameOverText.text = ""; // Ensure the game over text is empty at the start
        StartCoroutine(SlowUpdate());
    }

    // Update is called once per frame
    IEnumerator SlowUpdate()
    {
        while (true)
        {
            // Check if the BallKing has moved
            if (ballKing.transform.position != lastPosition)
            {
                // If the BallKing has moved, reset the timer and update the last position
                timeSinceLastMove = 0;
                lastPosition = ballKing.transform.position;
            }
            else
            {
                // If the BallKing hasn't moved, increment the timer
                timeSinceLastMove += Time.deltaTime;

                // If the BallKing hasn't moved for 10 seconds, display a "Game Over" message
                if (timeSinceLastMove >= 10)
                {
                    gameOverText.text = "Game Over";
                    timeSinceLastMove = 0; // Reset the timer to prevent the message from being displayed repeatedly
                }
            }

            yield return new WaitForSeconds(0.5f); // wait for half a second before the next update
        }
    }
}