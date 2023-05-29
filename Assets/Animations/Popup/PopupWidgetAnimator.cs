using System;
using UnityEngine;

public class PopupWidgetAnimator : MonoBehaviour
{
    public event Action OnHideAnimationOver;
    public event Action OnAppearAnimationOver;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayHide()
    {
        this.animator.SetTrigger("hide");
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
