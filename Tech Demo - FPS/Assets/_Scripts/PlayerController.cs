using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float move_speed;

    private Rigidbody _rb;

    private float move_X;
    private float move_Z;
    private Vector3 movement = new Vector3();

    public float Mouse_Sensitivity_X = 100f;
    public float Mouse_Sensitivity_Y = 100f;

    public float clampAngle = 80.0f;

    private Transform _cam;

    private float mouseX;
    private float mouseY;
    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis

    // Use this for initialization
    void Start () {
        _rb = GetComponent<Rigidbody>();
        _cam = transform.Find("Camera");

        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }
	
	// Update is called once per frame
	void Update () {
        HandleKeyInput();
        ControlCameraWithMouse();
    }

    private void FixedUpdate()
    {
    }

    private void HandleKeyInput()
    {
        move_X = Input.GetAxis("Horizontal");
        move_Z = Input.GetAxis("Vertical");

        Debug.Log(move_X + " , " + move_Z);
        
        movement.x = move_X;
        movement.y = 0f;
        movement.z = move_Z;

        //Transform to base the movement direction on the camera's transform
        movement = _cam.TransformDirection(movement);
        movement.y = 0f;

        _rb.velocity = movement * move_speed;        
    }

    private void ControlCameraWithMouse()
    {
        mouseY = Input.GetAxis("Mouse Y");
        rotX += mouseY * Mouse_Sensitivity_X * Time.deltaTime;

        mouseX = -Input.GetAxis("Mouse X");
        rotY += mouseX * Mouse_Sensitivity_Y * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(-rotX, -rotY, 0f);
        _cam.rotation = localRotation;
    }
}
