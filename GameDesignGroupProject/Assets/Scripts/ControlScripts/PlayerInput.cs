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
    private Vector2 move;
    private float jumpHeight = 2.4f;
    private CharacterController controller;
    private Transform mainCam;

    public Transform ground;
    public float distanceToGround = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;
    public int number = 0;

    void Awake()
    {
        controls = new Inputs();
        controller = GetComponent<CharacterController>();
        mainCam = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Gravity();
        PlayerMovement();
        Jump();
        item();
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
        move = controls.Player.Move.ReadValue<Vector2>();

        Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
        movement = mainCam.forward * movement.z + mainCam.right * movement.x;
        movement.y = 0f;
        controller.Move(movement * moveSpeed * Time.deltaTime);
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


    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
