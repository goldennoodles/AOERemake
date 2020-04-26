using UnityEngine;

public class CameraController :MonoBehaviour {

    [SerializeField] private Transform target;
    [SerializeField] Vector3 offset = new Vector3( 0f, 12f, -12f );

    private void Start () {

        if (target == null) {
            target = FindObjectOfType<Player>().transform;
        }

    }

    private void LateUpdate () {
        this.transform.position = target.position + offset;
        this.transform.LookAt( target );
    }

}
