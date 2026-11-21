using UnityEngine;
using static PlayerMovement;

public class ScissorsEnemyController : MonoBehaviour
{
    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player == null)
            return;

        if (player.hatController.hatColor == HatColor.None)
        {
            GameManager.I.EndGame();
        }
        else
        {
            player.hatController.PlayDissovleAnimation();
        }

        Destroy(this);
    }
}
