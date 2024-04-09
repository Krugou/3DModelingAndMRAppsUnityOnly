using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraRayController : MonoBehaviour
{
    Text infoText;

    Slider delaySlider;
    float delayTimer = 0f;
    float delayMax = 10;

    void Start()
    {
        infoText = GameObject.Find("InfoText").GetComponent<Text>();
        infoText.enabled = false;
        delaySlider = GameObject.Find("DelaySlider").GetComponent<Slider>();
        delaySlider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayDirection = transform.TransformDirection(Vector3.forward) * 20;
        Debug.DrawRay(transform.position, rayDirection, Color.green);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, rayDirection, out hit, 50))
        {

            Debug.Log("hitting something: " + hit.collider.gameObject.name);
            if (hit.collider.CompareTag("InfoSpot"))
            {
                DisplayInfoText(hit.collider.gameObject.name);
            }
            if (hit.collider.CompareTag("TeleportSpot"))
            {
                StartCoroutine(MoveToScene(hit.collider.gameObject.name));
            }
        }
        else
        {
            ResetHit();
        }
    }

    void DisplayInfoText(string gameObjectName)
    {
        infoText.enabled = true;
        if (gameObjectName == "InfoCapsule")
        {
            infoText.text = "This is a plushie";
        }
        else if (gameObjectName == "WhiteBoard")
        {
            infoText.text = "This is a whiteboard";
        }
        else if (gameObjectName == "Chair")
        {
            infoText.text = "This is a chair";
        }
        else if (gameObjectName == "Ball")
        {
            infoText.text = "This is a ball";
        }
        else
        {
            infoText.text = "You are looking at " + gameObjectName + " object.";
        }
    }

    IEnumerator MoveToScene(string sceneName)
    {
        delaySlider.gameObject.SetActive(true);
        Camera mainCamera = Camera.main; // Get the main camera
        float initialFOV = mainCamera.fieldOfView; // Store the initial FOV

        while (delayTimer < 2)
        {
            delayTimer += Time.deltaTime;
            delaySlider.value = delayTimer;

            // Zoom in by decreasing the FOV
            mainCamera.fieldOfView = Mathf.Lerp(initialFOV, 0, delayTimer / delayMax); // Adjust this value to match the duration of the transition

            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }

    void ResetHit()
    {
        infoText.enabled = false;
        delaySlider.gameObject.SetActive(false);
        delayTimer = 0f;
    }

}