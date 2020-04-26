using UnityEngine;

[RequireComponent( typeof( Rigidbody ) )]
public class PlayerController :MonoBehaviour {

    private Vector3 _velocity;
    private Rigidbody _rigidbody;

    private void Start () {
        this._rigidbody = GetComponent<Rigidbody>();
    }

    public void Move( Vector3 desiredVelocity ) {
        _velocity = desiredVelocity;
    }

    public void FixedUpdate () {
        this._rigidbody.MovePosition( this._rigidbody.position + this._velocity * Time.fixedDeltaTime );
    }

    public void LookAt( Vector3 pointToLook ) {
        Vector3 finalPointToLook = new Vector3( pointToLook.x, this.transform.position.y, pointToLook.z );
        this.transform.LookAt( finalPointToLook );
    }
}
