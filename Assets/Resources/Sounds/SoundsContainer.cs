using UnityEngine;

[CreateAssetMenu(fileName = "Sound Object", menuName = "Audio/Sound object")]
public class SoundsContainer : ScriptableObject
{
    [SerializeField] private AudioClip _comboBuilded;
    [SerializeField] private AudioClip _timeEnded;
    [SerializeField] private AudioClip _buttonClicked;

    public AudioClip comboBuilded  => _comboBuilded;
    public AudioClip timeEnded => _timeEnded;
    public AudioClip buttonClicked => _buttonClicked;
}

