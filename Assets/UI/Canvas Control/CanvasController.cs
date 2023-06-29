using UnityEngine;

public abstract class CanvasController : MonoBehaviour
{
    [SerializeField] private CanvasWidgetAnimator _animator;
    public CanvasType type => _type;
    [SerializeField] protected CanvasType _type;

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

    public void CanvasDisable()
    {
        if (_animator)
        {
            _animator.PlayHideAnimation();
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


    protected virtual void OnHideAnimationOverEvent()
    {
        gameObject.SetActive(false);
    }
    protected virtual void OnAppearAnimationOverEvent() { }
}
