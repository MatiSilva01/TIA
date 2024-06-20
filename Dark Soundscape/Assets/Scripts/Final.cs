using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Final : MonoBehaviour
{   
    public AudioSource audioSource;

    
    private void OnTriggerEnter(Collider other)
    {
        
        if (audioSource != null && !audioSource.isPlaying)
        {
           
            audioSource.Play();
        }
    }
   
}