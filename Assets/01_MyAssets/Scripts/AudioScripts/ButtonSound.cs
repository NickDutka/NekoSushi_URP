using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource buttonAudioSource;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonSound()
    {
        if (buttonAudioSource != null)
        {
            buttonAudioSource.Play(); // Play the sound
        }
    }
}
