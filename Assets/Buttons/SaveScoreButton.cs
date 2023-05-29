using TMPro;
using UnityEngine;

public class SaveScoreButton : Clickable
{
    [SerializeField] private TMP_InputField nameInputField;

    protected override ButtonType type => ButtonType.SaveScore;

    private PlayerManager _playerManager;

    private void Start()
    {
        _playerManager = GameModeManager.GetInstance().playerManager;
    }

    protected override void OnClick()
    {
        _playerManager.SetPlayerName(nameInputField.text);
        _playerManager.SubmitScore();
    }
}
