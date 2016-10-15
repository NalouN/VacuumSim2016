using UnityEngine;
using System.Collections;
using System;

public class VacuumBotController : MonoBehaviour
{
    public float MovementSpeed = 0.1f;
    public float RotationSpeed = 1;
    public float JumpForce = 5;
    public int MaxJumps = 0;
    public int Size = 0;
    public float ScaleSizePerLevel = 1.2f;
    public bool IsGrounded { get { return isGrounded; } }
    public Camera PlayerCamera;
    public Transform CameraAnchor;
    public bool HasControl = true;

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
        if (HasControl)
        {
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
        //Adjust camera position
        if ((PlayerCamera != null) && (CameraAnchor != null))
        {
            PlayerCamera.transform.position = CameraAnchor.position;
            PlayerCamera.transform.rotation = CameraAnchor.rotation;
        }
    }

    void OnCollisionEnter(Collision hit)
    {
        Suckable suckableScript;
        //If player touched the floor
        if (hit.gameObject.tag == "Floor")
        {
            //Set it to grounded
            isGrounded = true;
        }else if (hit.gameObject.tag == "Battery")
        {
            //Add a level of jump/jetpack and deactivate the battery
            hit.gameObject.SetActive(false);
            MaxJumps++;
        }
        //If the touched object has the Suckable script component attached and check if it is edible for the player
        else if ((suckableScript = hit.gameObject.GetComponent<Suckable>()) != null && (Size >= suckableScript.Size))
        {
            if(hit.gameObject.tag == "Cat")
                //If it was the cat, do stuff!
                catWasKilled();
            //If it is a suckable item, deactivate it and increase the player's size, both property and model scale
            hit.gameObject.SetActive(false);
            Size++;

            Transform transformModel = transform.FindChild("Model");
            Vector3 scaledScale = new Vector3(transformModel.localScale.x * ScaleSizePerLevel, transformModel.localScale.y * ScaleSizePerLevel, transformModel.localScale.z * ScaleSizePerLevel);

            transformModel.localScale = new Vector3(scaledScale.x, scaledScale.y, scaledScale.z);
        }
    }

    void catWasKilled()
    {
        //TODO:
    }
}