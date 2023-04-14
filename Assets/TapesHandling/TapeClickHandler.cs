using System;
using UnityEngine;
using UnityEngine.UI;

public class TapeClickHandler : MonoBehaviour
{
    public event Action OnTapeClicked;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    private void Start()
    {
        _button.onClick.AddListener(OnTapeClick);
    }
    private void OnTapeClick()
    {
        OnTapeClicked?.Invoke();
    }
}

