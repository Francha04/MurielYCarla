using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour
{
    public DialogueSystem dialogueSystem;
    public AudioClip musicPart1;
    public AudioClip musicPart2;    
    private void Start()
    {
        this.GetComponent<AudioSource>().clip = musicPart1;
    }
    public void musicChangeToPart2() 
    {
        this.GetComponent<AudioSource>().Stop();
        this.GetComponent<AudioSource>().clip = musicPart2;
        this.GetComponent<AudioSource>().Play();
    }
}
