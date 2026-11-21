using UnityEngine;
using static PlayerMovement;

public class PlayerHatController : MonoBehaviour
{
   public SpriteRenderer hatSpriteRenderer;

    [SerializeField] protected Sprite purpleHatSprite;
    [SerializeField] protected Sprite redHatSprite;
    [SerializeField] protected Sprite blueHatSprite;

    public HatColor hatColor = HatColor.None;

    [SerializeField] protected Animator animator;

    [SerializeField] protected AnimationClip noneIdle;
    [SerializeField] protected AnimationClip purpleIdle;
    [SerializeField] protected AnimationClip redIdle;
    [SerializeField] protected AnimationClip blueIdle;

    [SerializeField] protected AnimationClip purpleToNoneAnim;
    [SerializeField] protected AnimationClip redToPurpleAnim;
    [SerializeField] protected AnimationClip blueToRedAnim;

    private void Start()
    {
        SetHat(hatColor);
    }

    private void Update()
    {
        UpdateAnimation();
    }

    protected void UpdateAnimation()
    {
        switch (hatColor)
        {
            case HatColor.Purple:
                animator.Play(purpleIdle.name);
                break;
            case HatColor.Red:
                animator.Play(redIdle.name);
                break;
            case HatColor.Blue:
                animator.Play(blueIdle.name);
                break;
            case HatColor.None:
                animator.Play(noneIdle.name);
                break;
        }
    }

    public void SetHat(HatColor color)
    {
        // TODO
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
            case HatColor.None:
                hatSpriteRenderer.sprite = null;
                break;
        }

        hatColor = color;
    }

    public void OnDissolveAnimFinished()
    {
        SetHat(hatColor);
    }
}
