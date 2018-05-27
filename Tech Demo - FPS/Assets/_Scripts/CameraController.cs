using UnityEngine;

public class CameraController : MonoBehaviour {

    public float Mouse_Sensitivity = 100f;
    public float clampAngle = 80.0f;

    private Camera _cam;

    private float mouseX;
    private float mouseY;
    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis

    // Use this for initialization
    void Start () {
        _cam = GetComponent<Camera>();

        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }
	
	// Update is called once per frame
	void Update () {
        ControlCameraWithMouse();
	}

    private void ControlCameraWithMouse()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * Mouse_Sensitivity * Time.deltaTime;
        rotX += mouseY * Mouse_Sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }
}
