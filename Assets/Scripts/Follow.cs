using UnityEngine;

class Follow : MonoBehaviour
{
    public float AggroRadius = 3f;
    public float Speed = 0.5f;

    private PlayerMovement _player;

    void Start()
    {
        _player = Object.FindFirstObjectByType<PlayerMovement>();
    }

    void Update()
    {
        if (Vector3.Distance(_player.transform.position, transform.position) <= AggroRadius)
        {
            transform.LookAt(_player.transform);
            transform.position += transform.forward * Time.deltaTime * Speed;
        }
    }
}