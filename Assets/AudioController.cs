using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : SingletonPersistent<AudioController>
{
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _bgm;
    [SerializeField]
    private AudioClip _clickSFX;
    [SerializeField]
    private AudioClip _shootSFX;
    [SerializeField]
    private AudioClip _pickupSFX;
    [SerializeField]
    private AudioClip _hitSFX;

    void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.loop = true;
        _audioSource.clip = _bgm;
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
            _audioSource.PlayOneShot(_clickSFX);
        }
    }

    public void ShootSFX()
    {
        if (_audioSource != null && _shootSFX != null)
        {
            _audioSource.PlayOneShot(_shootSFX);
        }
    }

    public void PickUpSFX()
    {
        if (_audioSource != null && _pickupSFX != null)
        {
            _audioSource.PlayOneShot(_pickupSFX);
        }
    }

    public void HitSFX()
    {
        if (_audioSource != null && _hitSFX != null)
        {
            _audioSource.PlayOneShot(_hitSFX);
        }
    }
}
