using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBreathing : MonoBehaviour
{

    public AudioSource breathingSound;

    void Start()
    {
        if (breathingSound != null)
        {
            breathingSound.Play();
        }
    }

    
    public void StopBreathing()
    {
        if(breathingSound != null && breathingSound.isPlaying)
        {
            breathingSound.Stop();
        }
    }
    
}
