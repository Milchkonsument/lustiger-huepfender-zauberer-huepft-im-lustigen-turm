using UnityEngine;
using UnityEngine.Audio;
using static PlayerMovement;

public class HatItem : MonoBehaviour
{

    public HatColor hatColor = HatColor.Purple;

    [SerializeField] protected Sprite purpleHatSprite;
    [SerializeField] protected Sprite redHatSprite;
    [SerializeField] protected Sprite blueHatSprite;

    [SerializeField] protected SpriteRenderer hatSprite;

    void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

        if (player == null)
            return;

        player.hatController.SetHat(hatColor);
        AudioManager.Instance.PlaySFX("CollectHat");

        // TODO Animation?
        Destroy(gameObject);
    }
    void Start()
    {
        SetColor(hatColor);
    }

    protected void SetColor(HatColor color)
    {
        switch(color)
        {
            case HatColor.Purple:
                hatSprite.sprite = purpleHatSprite;
                break;
            case HatColor.Red:
                hatSprite.sprite = redHatSprite;
                break;
            case HatColor.Blue:
                hatSprite.sprite = blueHatSprite;
                break;
        }
        hatColor = color;
    }
}
