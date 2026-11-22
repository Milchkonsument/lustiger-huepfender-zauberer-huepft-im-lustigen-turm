using System;
using UnityEngine;

public class SheepMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    private GameObject _player;

    private bool toogleCollision;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
     _player = GameObject.FindGameObjectWithTag("Player");
     _rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            toogleCollision = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            toogleCollision = false;
        }
    }

    private void FixedUpdate()
    {
        if (toogleCollision)
        {
            _rb.linearVelocityX = _player.transform.position.x - transform.position.x;
            
        }
    }
}
