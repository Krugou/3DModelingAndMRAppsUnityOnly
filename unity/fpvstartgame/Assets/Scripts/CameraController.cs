using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public Transform cameraPosition;
  public float sensitivyX;
  public float sensitivyY;
  public Transform orientation;
  private float xRotation;
  private float yRotation;

  // Start is called before the first frame update
  void Start()
  {
    // lock & hide hardware pointer
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  // Update is called once per frame
  void Update()
  {
    // move camera according the player
    transform.position = cameraPosition.position;

    // read mouse input
    float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivyX;
    float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivyY;
    // update rotation values
    yRotation = yRotation + mouseX;
    xRotation = xRotation + mouseY;
    // limit tilting
    xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    // rotate camera & orientation of object
    transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    orientation.rotation = Quaternion.Euler(0, yRotation, 0);
  }
}