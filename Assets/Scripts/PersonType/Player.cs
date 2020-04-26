using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player :MonoBehaviour {

    [SerializeField] private float _moveSpeed = 7f;
    private PlayerController _playerController;
    private Camera _camera;

    private void Start () {
        _playerController = GetComponent<PlayerController>();
        _camera = Camera.main;
    }

    private void Update () {
        Vector3 moveInput = new Vector3( Input.GetAxisRaw( "Horizontal" ), 0, Input.GetAxisRaw( "Vertical" ) );
        Vector3 moveVelocity = moveInput.normalized * _moveSpeed;
        _playerController.Move(moveVelocity);

        Vector3 mousePosition3D = Utils.GetMousePosition3D();
        _playerController.LookAt( mousePosition3D );
    }
}
