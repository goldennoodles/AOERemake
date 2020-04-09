// DL FROM UNITY DOCS
//////////////////////////////////////////////////////////////
// This script moves the character controller forward
// and sideways based on the arrow keys.
// It also jumps when pressing space.
// Make sure to attach a character controller to the same game object.
// It is recommended that you make only one call to Move or SimpleMove per frame.
//////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    //Debug
    public bool DebugEnabled = false;
    [Space(10)]


    [Header("Character to Control")]
    public CharacterController characterController;
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    Vector3 moveDirection;


    //The camera
    [Header("Camera to Control")]
    public Transform Camera;

    //This is for added functionality of movement around the player
    [Header("Gyros")]
    public Transform CameraGyroA;
    public Transform CameraGyroB;

    //Camera rotation axis direction
    [Header("Q/E Result")]
    Vector3 controlAxisHorizontal;
    Vector3 controlAxisVertical;

    //Speed of the cameras rotation
    [Header("Rotation Speeds")]
    public float hrSpeed = 30.0f;
    public float vrSpeed = 30.0f;

    public AnimationCurve curve;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    //Zoom Clamp
    [Header("Scroll Zoom")]

    public float subSteps = 1000f;
    public float clampMin = -10f;
    public float clampMax = -1f;
    public float scrollFactor = 0.1f;// input default


    //Current Zoom Velocity
    Vector3 velocity = new Vector3(0, 0, 10);

    void Update()
    {
        //Horizontal Camera Rotation
        if (Input.GetKey(KeyCode.Q))
        {
            controlAxisHorizontal.y = -1; //Left
            CameraGyroB.RotateAround(transform.localPosition, controlAxisHorizontal, hrSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            controlAxisHorizontal.y = 1; //Right
            CameraGyroB.RotateAround(transform.localPosition, controlAxisHorizontal, hrSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q) == false && Input.GetKey(KeyCode.E) == false)
        {
            controlAxisHorizontal.y = 0;
        }

        //Vertical Camera Rotation
        if (Input.GetKey(KeyCode.R))
        {
            controlAxisVertical.x = 1; //Up
            CameraGyroB.Rotate(controlAxisVertical, vrSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.F))
        {
            controlAxisVertical.x = -1; //Down
            CameraGyroB.Rotate(controlAxisVertical, vrSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.R) == false && Input.GetKey(KeyCode.F) == false)
        {
            controlAxisVertical.x = 0;
        }

        //Horizontal Player Rotation
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0,-1,0),Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0,1,0),Space.World);
        }

        //Scroll wheel zoom
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            scroll(Input.GetAxis("Mouse ScrollWheel"));
        }


        //Player Movement + Jump
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void scroll(float zFactor)
    {
        //Need to solve this (Need to smoothly damp to new local z vector)
        Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        //Scroll Intensity
        float var = CameraGyroA.localPosition.z + Input.GetAxis("Mouse ScrollWheel") * scrollFactor;
        //if (DebugEnabled) { Debug.Log("Var: " + var); }

        //Temp Clamped
        Vector3 zoomVector = new Vector3(0, 0, Mathf.Clamp(var, clampMin, clampMax));
        //if (DebugEnabled) { Debug.Log("ZoomVector: " + zoomVector); }

        //Smooth Ramp
        float ramp = Mathf.Sin(Time.deltaTime) / 2 + .5f;
        //if (DebugEnabled) { Debug.Log("Ramp: " + ramp); }

        //Damp
        Vector3 damp = Vector3.SmoothDamp(zoomVector, zoomVector, ref velocity, Mathf.Round(ramp));
        //if (DebugEnabled) { Debug.Log("Damp: " + damp); }
        //Transform
        CameraGyroA.localPosition = damp;
        //if (DebugEnabled) { Debug.Log("Position: " + CameraGyroA.localPosition); }

    }
}