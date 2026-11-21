using UnityEngine;
using UnityEngine.LowLevelPhysics;

public class SelfDestroy : MonoBehaviour
{
    private GameObject _player;
    public float _selfDestroyDistance = 999f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (_player.transform.position.y - transform.position.y > _selfDestroyDistance)
        {
            Destroy(gameObject);
        }

    }

}
