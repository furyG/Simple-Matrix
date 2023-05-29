using UnityEngine;

public abstract class CanvasController : MonoBehaviour
{
    [SerializeField] private CanvasWidgetAnimator _animator;
    protected CanvasType _type;
    public CanvasType type => _type;
    public void CanvasDisable()
    {
        if (_animator)
        {
            _animator.PlayHide();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    public bool IsCanvasDeactivated()
    {
        return gameObject.activeInHierarchy == false;
    }

    protected virtual void OnEnable()
    {
        if (_animator)
        {
            _animator.OnHideAnimationOver += OnHideAnimationOverEvent;
            _animator.OnAppearAnimationOver += OnAppearAnimationOverEvent;
        }
    }

    protected virtual void OnDisable()
    {
        if (_animator)
        {
            _animator.OnHideAnimationOver -= OnHideAnimationOverEvent;
            _animator.OnAppearAnimationOver -= OnAppearAnimationOverEvent;
        }
    }

    protected virtual void OnHideAnimationOverEvent()
    {
        gameObject.SetActive(false);
    }
    protected virtual void OnAppearAnimationOverEvent() { }
}
