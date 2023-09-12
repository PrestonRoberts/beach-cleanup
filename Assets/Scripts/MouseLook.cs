using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    // Sensitivity
    [SerializeField] private Slider sensitivitySlider;
    private float maxSensitivity = 1000f;

    public float mouseSensitivity = 1000f;
    public Transform playerBody;

    private float xRotation = 0f;

    private bool lookEnabled;

    // Start is called before the first frame update
    void Start()
    {
        // Enable look
        lookEnabled = true;

        // Hide cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Set sensitivity
        SetSensitivity();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if look is enabled
        if (!lookEnabled) return;

        // Get mouse x and y
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate camera
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate player body
        playerBody.Rotate(Vector3.up * mouseX);
    }

    // Set sensitivity
    public void SetSensitivity()
    {
        mouseSensitivity = sensitivitySlider.value * maxSensitivity;
    }

    // Enable look
    public void EnableLook()
    {
        lookEnabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Disable look
    public void DisableLook()
    {
        lookEnabled = false;
        Cursor.lockState = CursorLockMode.None;
    }
}
