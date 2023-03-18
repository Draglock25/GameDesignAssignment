using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(Rigidbody))]
public class SpaceShip : MonoBehaviour
{
    [Header("=== Ship Movement Setting ===")]
    [SerializeField]
    private float yawTorque = 500f;
    [SerializeField]
    private float pitchTorque = 1000f;
    [SerializeField]
    private float rollTorque = 1000f;
    [SerializeField]
    private float thrust = 100f;
    [SerializeField]
    private float upThrust = 50f;
    [SerializeField]
    private float strafeThrust = 50f;

    [Header("=== Boost Setting ===")]
    [SerializeField]
    private float maxBoostAmount = 2f;
    [SerializeField]
    private float boostDeprecationRate = 0.25f;
    [SerializeField]
    private float boostRechargeRate = 0.5f;
    [SerializeField]
    private float boostMultiplier = 5f;
    public bool boosting = false;
    public float currantBoostAmount;

    [SerializeField, Range(0.001f, 0.999f)]
    private float thrustGlideReduction = 0.999f;
    [SerializeField, Range(0.001f, 0.999f)]
    private float upDownGlideReduction = 0.111f;
    [SerializeField, Range(0.001f, 0.999f)]
    private float leftRightGlideReduction = 0.111f;
    float glide, verticalGlide, horizontalGlide = 0f;


    Rigidbody rb;

    //Input Values
    private float thrust1D;
    private float upDown1D;
    private float strafe1D;
    private float roll1D;
    private Vector2 pitchYaw;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currantBoostAmount = maxBoostAmount;
    }

    void FixedUpdate()
    {
        HandleBoosting();
        HandleMovement();
    }

    void HandleBoosting()
    {
        if (boosting && currantBoostAmount > 0)
        {
            currantBoostAmount -= boostDeprecationRate;
            if(currantBoostAmount <= 0)
            {
                boosting = false;
            }
            else
            {
                if(currantBoostAmount < maxBoostAmount)
                {
                    currantBoostAmount += boostRechargeRate;
                }
            }
        }
    }

    void HandleMovement()
    {
        //roll
        rb.AddRelativeTorque(Vector3.back * roll1D * rollTorque * Time.deltaTime);
        //pitch
        rb.AddRelativeTorque(Vector3.right * Mathf.Clamp(-pitchYaw.y, -1f, 1f) * pitchTorque * Time.deltaTime);
        //yaw
        rb.AddRelativeTorque(Vector3.up * Mathf.Clamp(pitchYaw.x, -1f, 1f) * yawTorque * Time.deltaTime);

        //thrust
        if (thrust1D > 0.1f || thrust1D < -0.1f)
        {
            float currantThrust;

            if (boosting)
            {
                currantThrust = thrust * boostMultiplier;
            }
            else
            {
                currantThrust = thrust;
            }

            rb.AddRelativeForce(Vector3.forward * thrust1D * currantThrust * Time.deltaTime);
            glide = thrust;
        }
        else
        {
            rb.AddRelativeForce(Vector3.forward * glide * Time.deltaTime);
            glide *= thrustGlideReduction;
        }

        //up/down
        if (upDown1D > 0.1f || upDown1D < -0.1f)
        {
            float currantThrust = thrust;

            rb.AddRelativeForce(Vector3.up * upDown1D * upThrust * Time.fixedDeltaTime);
            verticalGlide = upDown1D * upThrust;
        }
        else
        {
            rb.AddRelativeForce(Vector3.up * verticalGlide * Time.fixedDeltaTime);
            verticalGlide = upDownGlideReduction;
        }

        //strafing
        if (upDown1D > 0.1f || upDown1D < -0.1f)
        {
            

            rb.AddRelativeForce(Vector3.right * strafe1D * upThrust * Time.fixedDeltaTime);
            horizontalGlide = strafe1D * strafeThrust;
        }
        else
        {
            rb.AddRelativeForce(Vector3.right * horizontalGlide * Time.fixedDeltaTime);
            horizontalGlide *= leftRightGlideReduction;
        }
    }

    #region Input Methods
    public void OnThrust(InputAction.CallbackContext context)
    {
        thrust1D = context.ReadValue<float>();
    }
    public void OnStrafe(InputAction.CallbackContext context)
    {
        strafe1D = context.ReadValue<float>();
    }
    public void OnUpDown(InputAction.CallbackContext context)
    {
        upDown1D = context.ReadValue<float>();
    }
    public void OnRoll(InputAction.CallbackContext context)
    {
        roll1D = context.ReadValue<float>();
    }
    public void OnPitchYaw(InputAction.CallbackContext context)
    {
        pitchYaw = context.ReadValue<Vector2>();
    }

    public void OnBoost(InputAction.CallbackContext context)
    {
        boosting = context.performed;
    }
    #endregion
}