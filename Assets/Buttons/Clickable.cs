using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class Clickable : MonoBehaviour 
{
    public event Action buttonClicked;

    protected Button buttonComponent;

    protected virtual void Awake()
    {
        buttonComponent = GetComponent<Button>();
        buttonComponent.onClick.AddListener(OnClick);
    }

    protected virtual void OnClick()
    {
        buttonClicked?.Invoke();
    }
}
