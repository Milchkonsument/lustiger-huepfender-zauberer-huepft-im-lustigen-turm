using UnityEngine;
using UnityEngine.LowLevelPhysics;

public class SelfDestroy : MonoBehaviour
{
    
    //This Class destroys Everything below the Deadzone
    private GameObject _deadzone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _deadzone = GameObject.FindGameObjectWithTag("Deadzone");
    }

    // Update is called once per frame
    void Update()
    {

        if (_deadzone.transform.position.y > transform.position.y)
        {
            Destroy(gameObject);
        }

    }

}
