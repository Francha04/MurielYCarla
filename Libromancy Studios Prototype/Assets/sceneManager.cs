using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    public AudioSource DJ;

    public void playButton() 
    {
        SceneManager.LoadScene(1);
    }
}
