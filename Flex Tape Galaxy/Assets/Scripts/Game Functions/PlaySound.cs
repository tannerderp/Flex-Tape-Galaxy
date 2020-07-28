using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ExecuteSoundPlay(AudioClip audioClip) //i cant call it play sound because thats the name of the script dangit. should've named the script soundplayer but oh well
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
