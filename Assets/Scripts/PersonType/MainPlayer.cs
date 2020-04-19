using UnityEngine;
using System.Collections;

enum MovementDirection {
    Forward,
    Backwards,
    Left,
    Right,
    Stationary,
    Jump
}

//[RequireComponent(typeof(Rigidbody))]
public class MainPlayer : MonoBehaviour {

    private float speed = 20f;
    private float gravity = 20f;
    private Vector3 playerPos;
    private GameObject playerObject;
    private void Start () {
        playerObject = this.gameObject;
        playerPos = this.transform.position;
    }

    private void Update () {

        switch(movementDirection) {
            case MovementDirection.Forward:
                playerPos += Vector3.forward * (speed * Time.deltaTime);
                Debug.Log("Moving forwards");
            break;
            case MovementDirection.Backwards:
                playerPos += Vector3.back * (speed * Time.deltaTime);
            break;
            case MovementDirection.Left:
                playerPos += Vector3.left * (speed * Time.deltaTime);
            break;
            case MovementDirection.Right:
                playerPos += Vector3.right * (speed * Time.deltaTime);
            break;
            case MovementDirection.Jump:
                int jumpForce = 5;
                if(isPlayerGrounded && !isPlayerJumping){
                    playerPos.y = jumpForce;
                } else {
                    playerPos.y -= gravity * Time.deltaTime;
                }
            break;
        }

        //Debug.Log(movementDirection);
        this.transform.position = playerPos;

    }

    private MovementDirection movementDirection{
        get {
            if(Input.GetKey(KeyCode.W)) {
                return MovementDirection.Forward;
            } else if (Input.GetKey(KeyCode.A)) {
                return MovementDirection.Left;
            } else if (Input.GetKey(KeyCode.D)) {
                return MovementDirection.Right;
            } else if (Input.GetKey(KeyCode.S)){
                return MovementDirection.Backwards;
            } else if (Input.GetKeyDown(KeyCode.Space)) {
                return MovementDirection.Jump;
            } else {
                return MovementDirection.Stationary;
            }
        }
    } 

    private bool isPlayerGrounded {
        get {
            RaycastHit hit;
            if(Physics.Raycast(this.transform.position, Vector3.down, out hit)) {
                Debug.Log("Player is grounded");
                return true;
            }
            return false;
        }
    }

    private bool isPlayerJumping {
        get {
            if(!isPlayerGrounded) {
                return true;
            } else {
                return false;
            }
        }
    }
}