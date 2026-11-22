using UnityEngine;
using UnityEngine.Audio;
using static PlayerMovement;

public class HatItem : MonoBehaviour
{

    public HatColor hatColor = HatColor.Purple;

    [SerializeField] protected Sprite purpleHatSprite;
    [SerializeField] protected Sprite redHatSprite;
    [SerializeField] protected Sprite blueHatSprite;

    [SerializeField] protected SpriteRenderer hatSpriteRenderer;

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player == null)
            return;

        player.hatController.AddHat();
        AudioManager.I.PlaySFX("CollectHat");

        // TODO Animation?
        Destroy(gameObject);
    }
    void Start()
    {
        SetColor((HatColor)Random.Range(0, 4));
    }

    protected void SetColor(HatColor color)
    {
        switch (color)
        {
            case HatColor.Purple:
                hatSpriteRenderer.sprite = purpleHatSprite;
                break;
            case HatColor.Red:
                hatSpriteRenderer.sprite = redHatSprite;
                break;
            case HatColor.Blue:
                hatSpriteRenderer.sprite = blueHatSprite;
                break;
        }
        hatColor = color;
    }
}
