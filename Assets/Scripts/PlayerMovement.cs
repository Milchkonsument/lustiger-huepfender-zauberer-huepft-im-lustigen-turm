using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.LowLevelPhysics2D;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private Rigidbody2D _rb;
    private InputAction moveAction;
    private float _inputY;
    private float _inputX;
    [SerializeField] private bool _isGrounded;
    private bool _isTouchingWall;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Player/Movement");
    }

    private void FixedUpdate()
    {
        _inputX = moveAction.ReadValue<Vector2>().x;
        _inputY = moveAction.ReadValue<Vector2>().y;
        _rb.linearVelocity = new Vector2(_inputX * speed,0);
       /* if (_rb.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            _rb.AddForce(new Vector2(0, inputY * jumpForce), ForceMode2D.Impulse);
        }*/
       if (_isGrounded && _isTouchingWall.Equals(false))
       {
           _rb.AddForce(new Vector2(0, _inputY * jumpForce), ForceMode2D.Impulse);
       }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        print("collision at: " + other.contacts[0].normal);
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && other.contacts[0].normal.y > 0)
        {
            _isGrounded = true;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") && other.contacts[0].normal.x != 0)
        {
            _isTouchingWall = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground") )
        {
            _isGrounded = false;
        }
    }
}
