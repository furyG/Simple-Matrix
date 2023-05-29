using System;
using UnityEngine;

public class TileAnimationHandler : MonoBehaviour
{
    public event Action OnTakeAnimationOver;

    [SerializeField] private Animator animator;
    public void PlayTaking()
    {
        animator.SetTrigger("take");
    }
    private void TakeAnimationOver()
    {
        OnTakeAnimationOver?.Invoke();
    }
}
