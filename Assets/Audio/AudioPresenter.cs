using Architecture;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioPresenter : MonoBehaviour
{
    [SerializeField] private Button _muteButton;
    [SerializeField] private AudioSource _backgroundMusicSource;
    [SerializeField] private Sprite[] _soundStateSprites;

    private AudioInteractor _audioInteractor;
    private AudioSource _audioSource; 

    private bool _muted = false;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _audioInteractor = Game.GetInteractor<AudioInteractor>();
        if (_audioInteractor != null)
        {
            _audioInteractor.OnSoundPlaying += PlaySound;
        }

        _muteButton.AddListener(OnMutedButtonClicked);
    }
    private void OnDisable()
    {
        if (_audioInteractor != null)
        {
            _audioInteractor.OnSoundPlaying -= PlaySound;
        }
    }
    private void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
    private void OnMutedButtonClicked()
    {
        _muted = !_muted;

        _audioSource.mute = _muted;
        _backgroundMusicSource.mute = _muted;

        _muteButton.image.sprite = _soundStateSprites[Convert.ToInt32(_muted)];
    }
}
