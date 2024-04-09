using UnityEngine;

public class CameraMouseControl : MonoBehaviour
{
    public float sensitivyX;
    public float sensitivyY;
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
    }
}