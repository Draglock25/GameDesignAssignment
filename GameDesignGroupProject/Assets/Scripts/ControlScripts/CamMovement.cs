using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CamMovement : MonoBehaviour
{
    private Inputs controls;
    private float mouseSensitivity = 100;
    private Vector2 mousePosition;
    private float xRotation;
    [SerializeField] private Transform playerBody;

    void Awake()
    {
        playerBody = transform.parent;

        controls = new Inputs();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Look();
    }

    private void Look()
    {
        mousePosition = controls.Player.ControlCamera.ReadValue<Vector2>();

        float mouseX = mousePosition.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mousePosition.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void OnEnable()
    {
        controls.Enable(); 
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
