using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStartScreen : MonoBehaviour
{
    [Range(0, 30)]
    public float rotationSpeed;

    private Camera mainCamera;
    private Grid grid;

    // Start is called before the first frame update

    private void Awake() {
        mainCamera = this.gameObject.GetComponent<Camera>();    
        grid = FindObjectOfType<Grid>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(grid.GetCenterPointOnGrid(), Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
