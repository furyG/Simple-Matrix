using System;
using UnityEngine;

public class CanvasWidgetAnimator : MonoBehaviour, IAnimatableUI
{
    public event Action OnHideAnimationOver;
    public event Action OnAppearAnimationOver;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayHideAnimation()
    {
        this._animator.SetTrigger("hide");
    }

    public void AppearAnimationOver()
    {
        OnAppearAnimationOver?.Invoke();
    }

    public void HideAnimationOver()
    {
        OnHideAnimationOver?.Invoke();
    }

    public void IdleAnimationOver()
    {
        
    }
}
