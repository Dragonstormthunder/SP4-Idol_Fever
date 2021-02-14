using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlaySound : MonoBehaviour
{

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        source?.Play();
    }

}
