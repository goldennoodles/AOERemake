using UnityEngine;
using System.Collections;

/***
  *   Place holder for now just to checkout the terrain and move on with the project.
  *   This will be revisited at a later date.
  *   @author : Rus Kuzmin
***/
enum MovementDirection {
    Forward,
    Backwards,
    Left,
    Right,
    Stationary,
    Jump
}

public class MainPlayer : MonoBehaviour {

    private float speed = 20f;
    private float gravity = 20f;

    private Vector3 playerPos;
    private Character builder;
    private GameObject playerObject;

    private void Start () {

        // Adding this for now but no current plan or design implemented. I will come back to this.
        builder = new Character(Character.characterType.Builder, this.gameObject);

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
            default:
                // Something Something Something //
            break;
        }

        Debug.Log(movementDirection);
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
            } else {
                return MovementDirection.Stationary;
            }
        }
    } 

    private bool isPlayerGrounded {
        get {
            RaycastHit hit;
            if(Physics.Raycast(this.transform.position, Vector3.down, out hit, 1f)) {
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