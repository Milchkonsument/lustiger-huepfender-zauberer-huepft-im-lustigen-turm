using UnityEngine;

public class MirrorPosition : MonoBehaviour
{
    public GameObject tracking;
    public bool mirrorY = true;
    public bool mirrorX = false;
    public bool mirrorZ = false;

    void LateUpdate()
    {
        if (tracking != null)
        {
            var p = transform.position;
            transform.position = new Vector3(
                mirrorX ? tracking.transform.position.x : p.x,
                mirrorY ? tracking.transform.position.y : p.y,
                mirrorZ ? tracking.transform.position.z : p.z
            );
        }
    }
}
