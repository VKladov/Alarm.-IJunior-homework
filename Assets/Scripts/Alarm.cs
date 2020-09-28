using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private Color _alarmColor;
    [SerializeField] float _blinkDuration = 0.2f;
    [SerializeField] float _soundFadeDuration = 2f;
    [SerializeField] float _maxSoundVolume = 0.5f;

    private SpriteRenderer _renderer;
    private Color _defaultColor;
    private AudioSource _audio;
    private float _blinkingTime = 0;
    private bool isPlaying = false;
    private Coroutine colorJob;
    private Coroutine soundJob;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _defaultColor = _renderer.color;
        _audio = GetComponent<AudioSource>();
        _audio.volume = 0;
    }

    public void Play()
    {
        isPlaying = true;
        StartColorAnimation();
        PlaySound();
    }

    public void Stop()
    {
        isPlaying = false;
        StopColorAnimation();
        StopSound();
    }

    private void PlaySound()
    {
        if (soundJob != null)
            StopCoroutine(soundJob);

        soundJob = StartCoroutine(FadeInSound());
    }

    private void StopSound()
    {
        if (soundJob != null)
            StopCoroutine(soundJob);

        soundJob = StartCoroutine(FadeOutSound());
    }

    private void StartColorAnimation()
    {
        if (colorJob != null)
            StopCoroutine(colorJob);

        colorJob = StartCoroutine(UpdateColor());
    }

    private void StopColorAnimation()
    {
        StopCoroutine(colorJob);
        _renderer.color = _defaultColor;
    }

    private IEnumerator UpdateColor()
    {
        while (isPlaying)
        {
            if (_blinkingTime >= _blinkDuration)
                _blinkingTime = 0;


            if (_blinkingTime < _blinkDuration / 2f)
            {
                float percent = _blinkingTime * 2f / _blinkDuration;
                _renderer.color = Color.Lerp(_defaultColor, _alarmColor, percent);
            }
            else
            {
                float percent = (_blinkingTime - _blinkDuration / 2f) * 2 / _blinkDuration;
                _renderer.color = Color.Lerp(_alarmColor, _defaultColor, percent);
            }
                

            _blinkingTime += Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator FadeInSound()
    {
        _audio.Play();
        while (_audio.volume < _maxSoundVolume)
        {
            float delta = _maxSoundVolume * Time.deltaTime / _soundFadeDuration;
            _audio.volume += delta;
            yield return null;
        }
    }

    private IEnumerator FadeOutSound()
    {
        while (_audio.volume > 0)
        {
            _audio.volume -= _maxSoundVolume * Time.deltaTime / _soundFadeDuration;
            yield return null;
        }

        _audio.Stop();
    }
}