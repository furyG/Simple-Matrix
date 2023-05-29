using UnityEngine;
using UnityEngine.UI;

public class HeartPresenter : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private Image _coverImage;

    private void Awake()
    {
        _coverImage.enabled = false;
        _fillImage.enabled = true;
    }
    public void ResetFillAmount()
    {
        _fillImage.fillAmount = 0;
    }

    public void MakeHeartActive(bool isFull)
    {
        _fillImage.enabled = !isFull;
        _coverImage.enabled = isFull;
    }
    public float GetFillAmount()
    {
        return _fillImage.fillAmount;
    }

    public void ChangeFillAmount(float amount)
    {
        _fillImage.fillAmount = amount;
        MakeHeartActive(_fillImage.fillAmount >= 1);
    }
}
