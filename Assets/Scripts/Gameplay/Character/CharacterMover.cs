using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;

    private PlayerInput _playerInput;
    private CharacterController _controller;
    
    private void Awake()
    {
        _playerInput = new PlayerInput();
        _controller = GetComponent<CharacterController>();
    }
    
    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var movementInput = _playerInput.Player.Move.ReadValue<Vector2>();
        var move = new Vector3(movementInput.x, 0f, movementInput.y);

        _controller.Move(move * (Time.deltaTime * moveSpeed));
        if (move != Vector3.zero)
            transform.forward = move;
    }
}
