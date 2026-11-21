using System.Drawing;
using UnityEngine;
using static PlayerMovement;

public class PlayerHatController : MonoBehaviour
{
   public SpriteRenderer hatSpriteRenderer;

    [SerializeField] protected Sprite purpleHatSprite;
    [SerializeField] protected Sprite redHatSprite;
    [SerializeField] protected Sprite blueHatSprite;
    [SerializeField] protected Sprite noneSprite;

    public HatColor hatColor = HatColor.None;

    [SerializeField] protected Animator animator;

    [SerializeField] protected AnimationClip noneIdle;
    [SerializeField] protected AnimationClip purpleIdle;
    [SerializeField] protected AnimationClip redIdle;
    [SerializeField] protected AnimationClip blueIdle;

    [SerializeField] protected AnimationClip purpleToNoneAnim;
    [SerializeField] protected AnimationClip redToPurpleAnim;
    [SerializeField] protected AnimationClip blueToRedAnim;

    protected bool bIsDissolving = false;

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
        if (bIsDissolving)
            return;

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
                hatSpriteRenderer.sprite = noneSprite;
                break;
        }

        hatColor = color;
    }

    [ContextMenu("Test Dissolve")]
    public void PlayDissovleAnimation()
    {
        bIsDissolving = true;

        switch (hatColor)
        {
            case HatColor.Purple:
                animator.Play(purpleToNoneAnim.name);
                hatColor = HatColor.None;
                break;

            case HatColor.Red:
                animator.Play(redToPurpleAnim.name);
                hatColor = HatColor.Purple;
                break;

            case HatColor.Blue:
                animator.Play(blueToRedAnim.name);
                hatColor = HatColor.Red;
                break;

            case HatColor.None:
                
                break;
        }
    }

    public void OnDissolveAnimFinished()
    {
        bIsDissolving = false;
        SetHat(hatColor);
    }
}
