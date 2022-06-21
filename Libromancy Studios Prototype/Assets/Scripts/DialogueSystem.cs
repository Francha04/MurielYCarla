using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogueSystem : MonoBehaviour
{    
    public Text dialogueText;
    public Image background;
    private Sprite lastBackground;
    public Image currentImage;
    public Image currentSecondImage;
    public musicManager musicM;
    [SerializeField] private DialogueSO currentDialogue;
    public float timePerCharacter;
    private bool loadingDialogue;  //Esta variable indica si un dialogo esta actualmente cargandose al texto
    public bool fading;
    public GameObject[] choiceButtons = new GameObject[3];
    float timePerFadeFrame = 0.05f;
    private bool loadingImage;
    private bool fadeOutActive;
    private string startingDialogueBeforeAdding;
    private DialogueSO.Parte lastPart;    
    private void Start()
    {
        fading = false;
        loadingDialogue = false;
        nextDialogue(null);
        lastBackground = background.sprite;
    }
    
    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightArrow)) && !currentDialogue.hasChoices && !((Input.mousePosition.y > 170 && Input.mousePosition.y < 619.5) && (Input.mousePosition.x > 922 && Input.mousePosition.x < 940)))
        {
            if (!loadingDialogue && !loadingImage && !fadeOutActive)
            {
                if (currentDialogue.hasFadeOut) 
                {
                    StartCoroutine(fadeOut(currentDialogue.fadeOutTime));
                    return;
                }
                nextDialogue(null);
            }
            else 
            {
                if (loadingDialogue)
                {
                    print("Dialogueloading");
                    instantSprite();
                    instantShowDialogue();
                }
                if (loadingImage)
                {
                    instantSprite();
                }
            }                     
        }        
    }

    private void nextDialogue(DialogueSO newDialogue)
    {
        StopAllCoroutines();
        if (newDialogue == null)
        {
            currentDialogue = currentDialogue.nextDialogue;
        }
        else {
            currentDialogue = newDialogue;
        }
        startingDialogueBeforeAdding = dialogueText.text + "\n\n" + currentDialogue.dialogueText;
        if (currentDialogue.hasChoices)
        {
            spawnButtons();
        }
        if (currentDialogue.dialogueSprite != null)
        {
            currentImage.sprite = currentDialogue.dialogueSprite;
        }
        if (currentDialogue.secondDialogueSprite != null)
        {
            currentSecondImage.sprite = currentDialogue.secondDialogueSprite;
        }
        if (currentDialogue.hasFadeIn)
        {
            currentImage.color = new Color(1, 1, 1, 0);
            currentSecondImage.color = new Color(1, 1, 1, 0);
            StartCoroutine(fadeInSprite(currentDialogue.fadeInTime));
        }
        background.sprite = currentDialogue.background;
        if (currentDialogue.parteEnLaHistoria == DialogueSO.Parte.parte1)
        {
            Vector2 newPos = new Vector2(-641.4f, -155f);
            currentImage.GetComponent<RectTransform>().localPosition = newPos;
        }
        else if (currentDialogue.parteEnLaHistoria == DialogueSO.Parte.parte2)
        {
            Vector2 newPos = new Vector2(-641.4f, -218f);
            currentImage.GetComponent<RectTransform>().localPosition = newPos;
        }
        if (currentDialogue.background != lastBackground)
        {
            background.GetComponent<Animator>().SetTrigger("FadeIn");
            lastBackground = currentDialogue.background;
        }
        if (currentDialogue.parteEnLaHistoria != lastPart) 
        {
            musicM.musicChangeToPart2();
        }
        lastPart = currentDialogue.parteEnLaHistoria;    
        StartCoroutine(slowlyShowDialogue(currentDialogue.dialogueText));
    }

    IEnumerator slowlyShowDialogue(string textToDisplay)
    {
        dialogueText.text = dialogueText.text + "\n\n";
        //dialogueText.text = "";
        loadingDialogue = true;
        if (currentDialogue.hasFadeIn) { yield return new WaitForSeconds(currentDialogue.fadeInTime); }               
        for (int i = 0; i < textToDisplay.Length; i++)
        {
            dialogueText.text = dialogueText.text + textToDisplay[i];
            yield return new WaitForSeconds(timePerCharacter);
        }
        loadingDialogue = false;
    }
    private void instantShowDialogue()
    {
        print("Instantly showing dialogue");
        StopAllCoroutines();
        dialogueText.text = startingDialogueBeforeAdding;
        loadingDialogue = false;
    }
    private void spawnButtons()
    {
        for (int i = 0; i < currentDialogue.choicesText.Count; i++)
        {
            activateButton(i, currentDialogue.choicesText[i]);
        }
    }
    private void activateButton(int x, string s)
    {
        choiceButtons[x].SetActive(true);
        choiceButtons[x].GetComponent<Button>().interactable = true;
        choiceButtons[x].GetComponentInChildren<Text>().text = s;
    }
    private void deactivateButtons() 
    {
        foreach (GameObject button in choiceButtons)
        {
            button.GetComponent<Button>().interactable = false;
            button.GetComponentInChildren<Text>().text = "";
            button.SetActive(false);
        }
    }
    public void firstButton() 
    {
        instantShowDialogue();
        deactivateButtons();
        nextDialogue(currentDialogue.choicesNextDialogue[0]);
    }
    public void secondButton()
    {
        instantShowDialogue();
        deactivateButtons();
        nextDialogue(currentDialogue.choicesNextDialogue[1]);        
    }
    public void thirdButton()
    {
        instantShowDialogue();
        deactivateButtons();
        nextDialogue(currentDialogue.choicesNextDialogue[2]);        
    }
    private IEnumerator fadeInSprite(float x)
    {        
        loadingImage = true;
        Color newColor = Color.white;
        float progressPerFrame = timePerFadeFrame / x;
        newColor.a = 0;
        currentImage.color = newColor;
        currentSecondImage.color = newColor;
        for (float i = 0; i < x; i += timePerFadeFrame)
        {
            yield return new WaitForSeconds(timePerFadeFrame);            
            newColor.a += progressPerFrame;
            currentSecondImage.color = newColor;
            currentImage.color = newColor;
        }
        loadingImage = false ;
    }
    private void instantSprite() 
    {
        StopAllCoroutines();
        loadingImage = false;
        Color newColor = Color.white;
        currentImage.color = newColor;
        currentSecondImage.color = newColor;
    }
    private IEnumerator fadeOut(float x)
    {
        fadeOutActive = true;
        background.GetComponent<Animator>().speed = 1 / x-0.1f;
        background.GetComponent<Animator>().SetTrigger("FadeOut");
        Color newColor = Color.white;
        float progressPerFrame = timePerFadeFrame / x;        
        print("progress per frame:" + progressPerFrame);
        newColor.a = 1;
        currentImage.color = newColor;
        currentSecondImage.color = newColor;
        //background.color = newColor;
        for (float i = 0; i < x; i += timePerFadeFrame)
        {
            yield return new WaitForSeconds(timePerFadeFrame);
            newColor.a -= progressPerFrame;
            print("Fading, new alpha: " + newColor.a);
            currentSecondImage.color = newColor;
            currentImage.color = newColor;
            //background.color = newColor;
        }        
        nextDialogue(null);
        fadeOutActive = false;
    }
}
