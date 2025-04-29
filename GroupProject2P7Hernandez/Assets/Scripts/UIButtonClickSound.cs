using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonClickSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;

    public void PlayClickSound()
    {
        // Play Click Sound Button Clicked //
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
