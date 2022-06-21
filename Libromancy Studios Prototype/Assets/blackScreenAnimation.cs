using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackScreenAnimation : MonoBehaviour
{
    public sceneManager sceneM;
    void Start()
    {
        sceneM = FindObjectOfType<sceneManager>();
        this.GetComponent<Animator>().SetTrigger("FadeIn");
        Invoke("deactivateBlackScreen", 1f);
    }
    public void fadeOut() 
    {
        activateBlackScreen();
        this.GetComponent<Animator>().SetTrigger("FadeOut");
        Invoke("goToGameScene", 1.5f);        
    }
    private void activateBlackScreen() 
    {
        this.gameObject.SetActive(true);
    }
    private void deactivateBlackScreen()
    {        
        this.gameObject.SetActive(false);       
    }
    private void goToGameScene() 
    {
        sceneM.playButton();
    }
}
