using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerStates
    {
        IDLE,
        RUNNING,
        JUMPING
    }

    public StateMachine<PlayerStates> playerStateMachine;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody _rb;
    private bool _isGrounded;

    void Start()
    {
        playerStateMachine = new StateMachine<PlayerStates>();
        playerStateMachine.Init();
        playerStateMachine.RegisterStates(PlayerStates.IDLE, new StateBase());
        playerStateMachine.RegisterStates(PlayerStates.RUNNING, new StateBase());
        playerStateMachine.RegisterStates(PlayerStates.JUMPING, new StateBase());

        playerStateMachine.SwitchState(PlayerStates.IDLE);
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();        
    }

    private void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * _speed;
        float moveVertical = Input.GetAxis("Vertical") * _speed;

        if (moveHorizontal == 0 && moveVertical == 0)
        {
            playerStateMachine.SwitchState(PlayerStates.IDLE);
        }
        else
        {
            playerStateMachine.SwitchState(PlayerStates.RUNNING);
        }
        

        Vector3 movement = new(moveHorizontal, _rb.velocity.y, moveVertical);
        _rb.velocity = movement;
    }

    private void HandleJump()
    {
        _isGrounded = Physics.CheckSphere(transform.position, 0.5f, _groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            playerStateMachine.SwitchState(PlayerStates.JUMPING);
        }
    }
}
