using System;

public interface IAnimatableUI
{
    event Action OnHideAnimationOver;
    event Action OnAppearAnimationOver;

    void PlayHideAnimation();

    void AppearAnimationOver();
    void HideAnimationOver();

    void IdleAnimationOver();

}
