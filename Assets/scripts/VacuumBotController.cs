using UnityEngine;
using System.Collections;
using System;

public class VacuumBotController : MonoBehaviour
{
    public float MovementSpeed = 0.1f;
    public float RotationSpeed = 1;
    public float JumpForce = 5;
    public int MaxJumps = 1;
    public static bool control = true;
    
    private Rigidbody rb;
    private int jumpsAvailable;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpsAvailable = MaxJumps;
        isGrounded = false;
    }

    void Update()
    {
        if (control) { 
        //Get axis
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");
        //Apply movement on transform        
        transform.Translate(0, 0, vAxis * MovementSpeed);
        transform.Rotate(0, hAxis * RotationSpeed, 0);
        //Apply jump
        if (Input.GetButtonDown("Jump") && jumpsAvailable >= 1)
        {
            //rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
            rb.velocity = new Vector3(0, JumpForce, 0);
            isGrounded = false;
            jumpsAvailable--;
        }
        //Reinitialize the jump count
        if (isGrounded)
            jumpsAvailable = MaxJumps;
        }
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }
    }

    public bool GetGround()
    {
        return isGrounded;
    }
}