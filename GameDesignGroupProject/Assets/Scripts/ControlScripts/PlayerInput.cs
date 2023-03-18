using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Inputs controls;
    private float moveSpeed = 6f;
    private Vector3 velocity;
    private float gravity = -9.81f;
    private float jumpHeight = 2.4f;
    private CharacterController controller;
    private Transform mainCam;

    [SerializeField]
    private Transform model;

    public Transform ground;
    public float distanceToGround = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;
    public int number = 0;

    [SerializeField]
    private float rotationSpeed = 4f;

    public CinemachineFreeLook freeLook;
    public GameObject inventPanel;
    public bool panelBool;

    void Awake()
    {
        controls = new Inputs();
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        panelBool = false;
    }

    void Start()
    {
        mainCam = Camera.main.transform;
    }

    void Update()
    {
        
        if (panelBool == false)
        {
            PlayerMovement();
            Jump();
        }
        Gravity();
        item();
        openInvent();

       
    }

    private void Gravity()
    {
        isGrounded = Physics.CheckSphere(ground.position, distanceToGround, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        } 

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void PlayerMovement()
    {
        Vector2 moveCtrl = controls.Player.Move.ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveCtrl.x, 0, moveCtrl.y);
        movement = mainCam.forward * movement.z + mainCam.right * movement.x;
        movement.y = 0f;
        controller.Move(movement * moveSpeed * Time.deltaTime);

        if(moveCtrl != Vector2.zero)
        {
            float targetAngel = Mathf.Atan2(moveCtrl.x, moveCtrl.y) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngel, 0f);
            model.rotation = Quaternion.Lerp(model.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void Jump()
    {
        if (controls.Player.Jump.triggered && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void item()
    {
        if (controls.Player.Item1.triggered)
        {
            number = 0;
            returnNumber();
        }else if (controls.Player.Item2.triggered)
        {
            number = 1;
            returnNumber();
        }else if (controls.Player.Item3.triggered)
        {
            number = 2;
            returnNumber();
        }
        else if (controls.Player.Item4.triggered)
        {
            number = 3;
            returnNumber();
        }
        else if (controls.Player.Item5.triggered)
        {
            number = 4;
            returnNumber();
        }
        else if (controls.Player.Item6.triggered)
        {
            number = 5;
            returnNumber();
        }
        else if (controls.Player.Item7.triggered)
        {
            number = 6;
            returnNumber();
        }

    }

    public int returnNumber()
    {
        return number;
    }

    private void openInvent()
    {
        if (controls.Player.openInventory.triggered)
        {
            if (panelBool)
            {
                inventPanel.SetActive(false);
                setPanel(false);
                Cursor.lockState = CursorLockMode.Locked;
                freeLook.m_YAxis.m_MaxSpeed = 1;
                freeLook.m_XAxis.m_MaxSpeed = 150;
            }
            else
            {
                inventPanel.SetActive(true);
                setPanel(true);
                Cursor.lockState = CursorLockMode.None;
                freeLook.m_YAxis.m_MaxSpeed = 0;
                freeLook.m_XAxis.m_MaxSpeed = 0;
            }
        }
    }

    public void setPanel(bool b)
    {
        panelBool = b;
    }

    public bool getpanel()
    {
        return panelBool;
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
