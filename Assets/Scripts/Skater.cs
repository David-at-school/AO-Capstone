using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Skater : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    [SerializeField] float turnRate = 1.0f;
    [SerializeField] float jumpForce = 5.0f;
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2.0f;
    [SerializeField] Rigidbody rb;
    //[SerializeField] GameObject rocketPrefab;
    PlayerInput playerInput;
    Vector3 input = Vector3.zero;
    //private Vector2 fireDirection = Vector2.zero;
    //private float threshold = 1;

    RaycastHit groundHit;
    Vector3 upDir;

    bool grounded = true;
    bool jumping = false;
    float uprightTorque = 5.0f;

    public int avgFrameRate;
    public TextMeshProUGUI display_Text;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        //Debug.Log(playerInput.currentControlScheme);
        //Debug.Log(playerInput.currentActionMap);
    }

    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            jumping = true;
        }

        if (Physics.Raycast(rb.position, -rb.transform.up, out groundHit, 1.5f))
        {
            upDir = groundHit.normal;
        }

        float current = 0;
        current = (int)(1f / Time.fixedUnscaledDeltaTime);
        avgFrameRate = (int)current;
        //display_Text.text = avgFrameRate.ToString() + " FPS";
    }

    private void FixedUpdate()
    {
        //Debug.DrawRay(transform.position, Vector3.down, Color.red, 1.0f);
        if (Physics.Raycast(transform.position, Vector3.down, 0.9f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (rb.velocity.y < 0)
        {
            //rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            rb.AddForce(Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime, ForceMode.Impulse);
            
        }
        else if (rb.velocity.y > 0 && !jumping)
        {
            //rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            rb.AddForce(Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime, ForceMode.Impulse);
        }

        //movement
        rb.AddTorque(transform.up * input.x * turnRate * Time.fixedDeltaTime, ForceMode.Impulse);
        rb.AddForce(transform.forward * input.z * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

        upDir = Vector3.up;

        //get normal of ground
        if (Physics.Raycast(rb.position, -rb.transform.up, out groundHit, 1.5f))
        {
            upDir = groundHit.normal;
        }

        // Rotates character to up direction
        Quaternion deltaQuat = Quaternion.FromToRotation(rb.transform.up, upDir);
        Vector3 axis;
        float angle;
        deltaQuat.ToAngleAxis(out angle, out axis);

        float dampen = 2.0f;
        rb.AddTorque(-rb.angularVelocity * dampen, ForceMode.Acceleration);
        float adjust = 2.0f;
        rb.AddTorque(axis.normalized * angle * adjust, ForceMode.Acceleration);
    }

    public void OnMove(InputValue inputValue)
    {
        input.x = inputValue.Get<Vector2>().x;
        input.z = inputValue.Get<Vector2>().y;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        input.x = context.ReadValue<Vector2>().x;
        input.z = context.ReadValue<Vector2>().y;
    }

    public void OnJump(InputValue inputValue)
    {
        if (inputValue.isPressed && grounded)
        {
            grounded = false;
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void OnLook(InputValue inputValue)
    {

    }

}
