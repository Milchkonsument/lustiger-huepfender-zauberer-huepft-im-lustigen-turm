using UnityEngine;

public class Spinning : MonoBehaviour
{
    public float MaxSpinSpeed = 1f;
    private float _spinspeed = 1f;

    void Awake()
    {
        _spinspeed = Random.Range(-MaxSpinSpeed, MaxSpinSpeed);
    }

    void Update()
    {
        transform.Rotate(0f, 0f, Time.deltaTime * _spinspeed * 100f);
    }
}
