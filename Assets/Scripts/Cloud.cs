using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    public Vector3 currPos;

    public float speed = .45f;

    // Start is called before the first frame update
    void Start()
    {
        currPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currPos += Vector3.forward * (speed * Time.deltaTime);
        this.transform.position = currPos;
    }
}
