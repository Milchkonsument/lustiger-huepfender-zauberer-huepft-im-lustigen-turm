using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;
    //sobald NeedleController gestartet wird, setzt es needle
    public GameObject needle;
    private PlayerMovement _playerMovement;
    public float offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _playerMovement = player.GetComponent<PlayerMovement>();
    }

    void LateUpdate()
    {
        if (_playerMovement.isUsingAbility)
        {
            this.transform.position = new Vector3(this.transform.position.x, needle.transform.position.y + offset, this.transform.position.z);
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, player.transform.position.y + offset,
                this.transform.position.z);
        }
    }
}
