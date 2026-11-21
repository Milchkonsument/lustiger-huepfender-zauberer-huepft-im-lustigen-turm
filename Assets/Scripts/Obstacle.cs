using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public String Name;
    public int Difficulty;

    private List<BoxCollider2D> _hitboxes;

    void Awake()
    {
        _hitboxes = GetComponents<BoxCollider2D>().ToList();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("bomba");
        }

        if (collision.gameObject.name == "Deadzone")
        {
            Debug.Log("Destroy");
            Destroy(this.gameObject);
        }
    }
}
