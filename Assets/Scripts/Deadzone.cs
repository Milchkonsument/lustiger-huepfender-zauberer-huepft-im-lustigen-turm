using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Deadzone : MonoBehaviour
{
    public String _restartScene;
    private GameObject _player;
    public float deadZoneSpeed;
    

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
   private void OnCollisionEnter2D(Collision2D other)
   {
       if (other.transform.CompareTag("Player"))
       {
           //Set Game State to Dead
            AudioManager.I.PlaySFX("Fall");
           GameManager.I.EndGame();
       }
   }

   private void Update()
   {
       transform.position = new Vector3(transform.position.x, transform.position.y * deadZoneSpeed + Time.deltaTime,
           transform.position.z);
   }
}
