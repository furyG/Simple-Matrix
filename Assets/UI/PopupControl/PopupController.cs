using UnityEngine;

public class PopupController : MonoBehaviour
{
    public PopupType type => _type;
    [SerializeField] private PopupType _type;

    [SerializeField] private PopupWidgetAnimator _popupAnimator;

    protected virtual void OnEnable()
    {
        if (_popupAnimator)
        {
            _popupAnimator.OnHideAnimationOver += OnHideAnimationOverEvent;
            _popupAnimator.OnAppearAnimationOver += OnAppearAnimationOver;
        }
    }

    protected virtual void OnDisable()
    {
        if (_popupAnimator)
        {
            _popupAnimator.OnHideAnimationOver -= OnHideAnimationOverEvent;
            _popupAnimator.OnAppearAnimationOver -= OnAppearAnimationOver;
        }
    }

    public void DisablePopup()
    {
        if(_popupAnimator)
        {
            _popupAnimator.PlayHideAnimation();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    protected virtual void OnHideAnimationOverEvent()
    {
        gameObject.SetActive(false);
    }
    protected virtual void OnAppearAnimationOver()
    {

    }
}
