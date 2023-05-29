using System;
using UnityEngine;

public class CanvasWidgetAnimator : MonoBehaviour
{
    public event Action OnHideAnimationOver;
    public event Action OnAppearAnimationOver;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayHide()
    {
        this._animator.SetTrigger("hide");
    }

    private void AppearAnimationOver()
    {
        OnAppearAnimationOver?.Invoke();
    }

    private void HideAnimationOver()
    {
        OnHideAnimationOver?.Invoke();
    }
}
