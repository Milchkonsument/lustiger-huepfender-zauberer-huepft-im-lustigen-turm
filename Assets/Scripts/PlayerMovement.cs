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

    public enum FacingDirection
    {
        Left,
        Right
    }

    public enum MovemenState
    {
        Idle,
        Running,
        Jumping,
        WallHanging
    }

    public FacingDirection facingDirection = FacingDirection.Right;
    public MovemenState movementState = MovemenState.Idle;

    [SerializeField] protected Animator animator;

    // TODO put into scriptable object library

    [SerializeField] protected AnimationClip animIdleLeft;
    [SerializeField] protected AnimationClip animRunLeft;
    [SerializeField] protected AnimationClip animJumpLeft;
    [SerializeField] protected AnimationClip animJumpIdleLeft;
    [SerializeField] protected AnimationClip animJumpToWallLeft;
    [SerializeField] protected AnimationClip animJumpFromWallLeft;
    [SerializeField] protected AnimationClip animHangWallIdleLeft;

    [SerializeField] protected AnimationClip animIdleRight;
    [SerializeField] protected AnimationClip animRunRight;
    [SerializeField] protected AnimationClip animJumpRight;
    [SerializeField] protected AnimationClip animJumpIdleRight;
    [SerializeField] protected AnimationClip animJumpToWallRight;
    [SerializeField] protected AnimationClip animJumpFromWallRight;
    [SerializeField] protected AnimationClip animHangWallIdleRight;



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

        UpdateMovementState();
        UpdateMoveAnimation(); 
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
                PlayJumpAnimation();
            }
            else if (_isTouchingLeftWall())
            {
                animator.Play(animJumpFromWallLeft.name);
                facingDirection = FacingDirection.Right;
                _rb.linearVelocity = new Vector2(wallJumpForce.x, wallJumpForce.y);
                _wallJumpLockTimer = wallJumpLockDuration;
            }
            else if (_isTouchingRightWall())
            {
                animator.Play(animJumpFromWallRight.name);
                facingDirection = FacingDirection.Left;
                _rb.linearVelocity = new Vector2(-wallJumpForce.x, wallJumpForce.y);
                _wallJumpLockTimer = wallJumpLockDuration;
            }

            _jumping = false;
        }
    }

    // Determine Movment State and Direction based on location and input
    protected void UpdateMovementState()
    {
        if (_isGrounded())
        {
            if (_inputX > 0)
            {
                facingDirection = FacingDirection.Right;
                movementState = MovemenState.Running;
            }
            else if (_inputX < 0)
            {
                facingDirection = FacingDirection.Left;
                movementState = MovemenState.Running;
            }
            else
            {
                movementState = MovemenState.Idle;

            }
        }
        else
        {
            if (_isTouchingRightWall())
            {
                movementState = MovemenState.WallHanging;
            }
            else
            {
                movementState = MovemenState.Jumping;
            }
        }

        if (jumpAction.triggered)
        {
            _jumping = true;
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

    // ANIMATION

    // Play animation base don Movement State and Direction
    private void UpdateMoveAnimation()
    {
        switch (movementState)
        {
            case MovemenState.Idle:

                if (facingDirection == FacingDirection.Right)
                {
                    animator.Play(animIdleRight.name);
                }
                else
                {
                    animator.Play(animIdleLeft.name);
                }
                break;

            case MovemenState.Running:

                if (facingDirection == FacingDirection.Right)
                {
                    animator.Play(animRunRight.name);
                }
                else
                {
                    animator.Play(animRunLeft.name);
                }
                break;

            case MovemenState.Jumping:

                // Do nothing basically

                break;
            case MovemenState.WallHanging:
                if (facingDirection == FacingDirection.Right)
                {
                    animator.Play(animHangWallIdleRight.name);
                }
                else
                {
                    animator.Play(animHangWallIdleLeft.name);
                }
                break;
        }
    }

    private void PlayJumpAnimation()
    {
        if (facingDirection == FacingDirection.Right)
        {
            animator.Play(animJumpRight.name);
        }
        else
        {
            animator.Play(animJumpLeft.name);
        }
    }
}
