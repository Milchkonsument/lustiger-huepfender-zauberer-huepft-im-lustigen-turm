using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    public float offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, player.transform.position.y + offset, this.transform.position.z);
    }
}
