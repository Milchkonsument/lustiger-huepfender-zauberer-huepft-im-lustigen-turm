using UnityEngine;

public class Moving : MonoBehaviour
{
    public bool IsVertical;
    public float MaxExtent;
    private float _maxExtent;
    private Vector3 _initPos;
    private float _initTimeOffset;

    void Awake()
    {
        _initPos = transform.position;
        _maxExtent = Random.Range(-MaxExtent, MaxExtent);
        _initTimeOffset = Random.Range(0f, 1f);
    }

    void Update()
    {
        var p = transform.position;
        var t = Mathf.Sin(Time.time + _initTimeOffset);

        if (IsVertical)
        {
            transform.position = new Vector3(_initPos.x + (_maxExtent * t), p.y, p.z);
        }
        else
        {
            transform.position = new Vector3(p.x, _initPos.y + (_maxExtent * t), p.z);
        }
    }
}
