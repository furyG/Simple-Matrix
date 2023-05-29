using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TapeClickHandler : Subject
{
    private TapeManager _manager;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        
        _manager = GetComponent<TapeManager>();
    }
    private void OnEnable()
    {
        if (_manager != null)
            Attach(_manager);
    }
    private void OnDisable()
    {
        if (_manager != null)
            Detach(_manager);
    }
    private void Start()
    {
        _button.onClick.AddListener(OnTapeClick);
    }
    private void OnTapeClick()
    {
        NotifyObeservers();
    }
}

