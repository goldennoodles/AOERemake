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


/** 
  * Non functional. Needs to be updated and improved.!-- Poss re-write.
  *
**/
public class Camera_Controller : MonoBehaviour
{
    //Zoom Clamp
    [Header("Scroll Zoom")]

    public float subSteps = 1000f;
    public float clampMin = -10f;
    public float clampMax = -1f;
    public float scrollFactor = 0.1f;// input default
    public float hrSpeed = 30.0f;
    public float vrSpeed = 30.0f;
    Vector3 controlAxisHorizontal;
    Vector3 controlAxisVertical;

    //Current Zoom Velocity
    Vector3 velocity;

    void Start () {
        controlAxisHorizontal = new Vector3();
        controlAxisVertical = new Vector3();
        velocity  = new Vector3(0, 0, 10);
    }

    void Update()
    {
        //Horizontal Camera Rotation
        if (Input.GetKey(KeyCode.Q))
        {
            controlAxisHorizontal.y = -1; //Left
            this.transform.RotateAround(transform.localPosition, controlAxisHorizontal, hrSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            controlAxisHorizontal.y = 1; //Right
            this.transform.RotateAround(transform.localPosition, controlAxisHorizontal, hrSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Q) == false && Input.GetKey(KeyCode.E) == false)
        {
            controlAxisHorizontal.y = 0;
        }

        //Vertical Camera Rotation
        if (Input.GetKey(KeyCode.R))
        {
            controlAxisVertical.x = 1; //Up
            this.transform.Rotate(controlAxisVertical, vrSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.F))
        {
            controlAxisVertical.x = -1; //Down
            this.transform.Rotate(controlAxisVertical, vrSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.R) == false && Input.GetKey(KeyCode.F) == false)
        {
            controlAxisVertical.x = 0;
        }

        //Scroll wheel zoom
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            scroll(Input.GetAxis("Mouse ScrollWheel"));
        }
    }

    void scroll(float zFactor)
    {
        Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
        //Scroll Intensity
        float var = this.transform.localPosition.z + Input.GetAxis("Mouse ScrollWheel") * scrollFactor;

        //Temp Clamped
        Vector3 zoomVector = new Vector3(0, 0, Mathf.Clamp(var, clampMin, clampMax));

        //Smooth Ramp
        float ramp = Mathf.Sin(Time.deltaTime) / 2 + .5f;

        //Damp
        Vector3 damp = Vector3.SmoothDamp(zoomVector, zoomVector, ref velocity, Mathf.Round(ramp));

        //Transform
        this.transform.localPosition = damp;

    }
}