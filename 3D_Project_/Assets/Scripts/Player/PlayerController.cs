using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody _rb;
    private bool _isGrounded;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * _speed;
        float moveVertical = Input.GetAxis("Vertical") * _speed;

        Vector3 movement = new(moveHorizontal, _rb.velocity.y, moveVertical);
        _rb.velocity = movement;

        _isGrounded = Physics.CheckSphere(transform.position, 0.1f, _groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }
}
