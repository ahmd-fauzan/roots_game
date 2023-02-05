using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : SingletonPersistent<AudioController>
{
    private AudioSource _audioSource;

    public AudioClip bgm;
    public AudioClip clickSFX;

    void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.clip = bgm;
        _audioSource.Play();
    }

    public void SetVolume(float vol)
    {
        if (_audioSource != null)
        {
            _audioSource.volume = vol;
        }
    }

    public void ClickSFX()
    {
        if (_audioSource != null)
        {
            _audioSource.PlayOneShot(clickSFX);
        }
    }
}
