using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusicManager : MonoBehaviour
{

    public AudioSource musicSource;
    public float fadeDuration = 1f;

    private void Start()
    {
        if(musicSource != null)
        {
            musicSource.Play();
        }
    }

    public void PauseMusic()
    {
        if(musicSource != null && musicSource.isPlaying)
        {
            StartCoroutine(FadeOutAndPause());
        }
    }

    public void ResumeMusic()
    {
        if(musicSource != null && !musicSource.isPlaying)
        {
            musicSource.UnPause();
            StartCoroutine(FadeIn());
        }
    }
    private IEnumerator FadeOutAndPause()
    {
        float startVolume = musicSource.volume;

        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.unscaledDeltaTime / fadeDuration;
            yield return null;
        }

        musicSource.Pause();
        musicSource.volume = startVolume;
    }

    private IEnumerator FadeIn()
    {
        float targetVolume = 1f;
        musicSource.volume = 0f;

        while (musicSource.volume < targetVolume)
        {
            musicSource.volume += Time.unscaledDeltaTime / fadeDuration;
            yield return null;
        }

        musicSource.volume = targetVolume;
    }
}
