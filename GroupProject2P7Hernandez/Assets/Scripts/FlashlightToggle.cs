using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FlashlightToggle : MonoBehaviour
{
    public GameObject lightGO; //light gameObject to work with
    private bool isOn = false; //is flashlight on or off?

    public AudioClip switchSound;
    
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        //set default off
        lightGO.SetActive(isOn);

        audioSource = GetComponent<AudioSource>();

        if(audioSource == null )
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
    }
    void Update()
    {
        if(!PauseMenu.isPaused)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                
                isOn = !isOn;
                //turn light on
                if (isOn)
                {
                    lightGO.SetActive(true);
                }
                //turn light off
                else
                {
                    lightGO.SetActive(false);

                }
                PlaySwitchSound();

            }
        }

       
        
    }

    

    private void PlaySwitchSound()
    {
        // When flashlight light active play flash light sound //
        if (audioSource != null)
        {
            audioSource.PlayOneShot(switchSound);
        }

    }


}
