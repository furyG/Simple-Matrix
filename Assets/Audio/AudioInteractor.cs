using Architecture;
using System;
using UnityEngine;

public class AudioInteractor : Interactor
{
    public event Action<AudioClip> OnSoundPlaying;

    private AudioRepository _audioRepository;

    public SoundsContainer soundsContainer => _audioRepository.soundsContainer;

    public override void OnCreate()
    {
        _audioRepository = Game.GetRepository<AudioRepository>();
    }
    public void PlayComboBuildedSound()
    {
        OnSoundPlaying?.Invoke(soundsContainer.comboBuilded);
    }
    public void PlayTimeEndedSound()
    {
        OnSoundPlaying?.Invoke(soundsContainer.timeEnded);
    }
    public void PlayButtonClickedSound()
    {
        OnSoundPlaying?.Invoke(soundsContainer.buttonClicked);
    }
}
