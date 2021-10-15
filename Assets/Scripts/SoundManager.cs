using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audio;

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audio.Play();
    }
}
