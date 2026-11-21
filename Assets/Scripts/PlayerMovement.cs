using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.LowLevelPhysics2D;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public Vector2 wallJumpForce = new Vector2(10f, 13f);
    public float wallJumpLockDuration = 0.15f;
    
    private Rigidbody2D _rb;
    private InputAction moveAction;
    private InputAction jumpAction;
    private float _inputX;
    private bool _jumping;
    private float _wallJumpLockTimer;
    
    private LayerMask _groundLayer;
    private Transform _groundChecker;
    private Transform _leftWallChecker;
    private Transform _rightWallChecker;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        _groundChecker = transform.Find("GroundChecker");
        _leftWallChecker = transform.Find("LeftWallChecker");
        _rightWallChecker = transform.Find("RightWallChecker");
        
        moveAction = InputSystem.actions.FindAction("Player/Movement");
        jumpAction = InputSystem.actions.FindAction("Player/Jump");
        
        _groundLayer = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        _inputX = moveAction.ReadValue<Vector2>().x;
        if (jumpAction.triggered)
        {
            _jumping = true;
        }
    }

    private void FixedUpdate()
    {
        if (_wallJumpLockTimer > 0)
        {
            _wallJumpLockTimer -= Time.fixedDeltaTime;
        }
        else
        {
            //left to right movement
            _rb.linearVelocity = new Vector2(_inputX * speed, _rb.linearVelocity.y);
        }




        //jumping
        if (_jumping)
        {
            if (_isGrounded())
            {
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, _rb.linearVelocity.y);
                _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else if (_isTouchingLeftWall())
            {
                _rb.linearVelocity = new Vector2(wallJumpForce.x, wallJumpForce.y);
                _wallJumpLockTimer = wallJumpLockDuration;
            }
            else if (_isTouchingRightWall())
            {
                _rb.linearVelocity = new Vector2(-wallJumpForce.x, wallJumpForce.y);
                _wallJumpLockTimer = wallJumpLockDuration;
            }

            _jumping = false;
        }
    }


    private bool _isGrounded()
    {
        return Physics2D.OverlapCircle(_groundChecker.position, 0.2f, _groundLayer);
    }
    
    private bool _isTouchingLeftWall()
    {
        return Physics2D.OverlapCircle(_leftWallChecker.position, 0.2f, _groundLayer);
    }

    private bool _isTouchingRightWall()
    {
        return Physics2D.OverlapCircle(_rightWallChecker.position, 0.2f, _groundLayer);
    }
    
}
