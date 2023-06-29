using System;
using UnityEngine;

public class PopupWidgetAnimator : MonoBehaviour, IAnimatableUI
{
    public event Action OnHideAnimationOver;
    public event Action OnAppearAnimationOver;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayHideAnimation()
    {
        this.animator.SetTrigger("hide");
    }

    public void AppearAnimationOver()
    {
        OnAppearAnimationOver?.Invoke();
    }

    public void HideAnimationOver()
    {
        OnHideAnimationOver?.Invoke();
    }

    public void IdleAnimationOver() { }

}
