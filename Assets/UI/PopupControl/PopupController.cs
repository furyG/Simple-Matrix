using UnityEngine;

public class PopupController : MonoBehaviour
{
    public PopupType type => _type;
    [SerializeField] private PopupType _type;

    [SerializeField] private PopupWidgetAnimator _animator;

    protected virtual void OnEnable()
    {
        if (_animator)
        {
            _animator.OnHideAnimationOver += OnHideAnimationOverEvent;
            _animator.OnAppearAnimationOver += OnAppearAnimationOver;
        }
    }

    protected virtual void OnDisable()
    {
        if (_animator)
        {
            _animator.OnHideAnimationOver -= OnHideAnimationOverEvent;
            _animator.OnAppearAnimationOver -= OnAppearAnimationOver;
        }
    }

    public void DisablePopup()
    {
        if(_animator)
        {
            _animator.PlayHide();
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
