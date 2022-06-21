using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue", order = 1)]
public class DialogueSO : ScriptableObject
{
    [TextArea(12, 20)] [SerializeField] private string _dialogueText;
    [HideInInspector] [SerializeField] private bool _hasChoices;
    [SerializeField] private Sprite _background;
    [SerializeField] private Sprite _dialogueSprite;
    [SerializeField] private Sprite _secondDialogueSprite;
    public enum Parte
    {
        parte1,
        parte2
    }
    public Parte parteEnLaHistoria;
    [HideInInspector] [SerializeField] private List<string> _choicesText = new List<string>();
    [HideInInspector] [SerializeField] private List<DialogueSO> _choicesNextDialogue = new List<DialogueSO>(); // Si el dialogo tiene elecciones, los posibles siguientes dialogos se guardan aqui.
    [HideInInspector] [SerializeField] private DialogueSO _nextDialogue; //Si el dialogo no tiene elecciones, el siguiente dialogo se guarda aqui.
    [SerializeField] private AudioClip _voiceOver;
    [HideInInspector] [SerializeField] private int _amountOfChoices;
    [HideInInspector] [SerializeField] private bool _hasFadeIn;
    [HideInInspector] [SerializeField] private float _fadeInTime = 1f;
    [HideInInspector] [SerializeField] private bool _hasFadeOut;
    [HideInInspector] [SerializeField] private float _fadeOutTime = 1f;

    public string dialogueText
    {
        get { return _dialogueText; }
        set { _dialogueText = value; }

    }
    public bool hasChoices
    {
        get { return _hasChoices; }
        set { _hasChoices = value; }
    }
    public Sprite dialogueSprite
    {
        get { return _dialogueSprite; }
        set { _dialogueSprite = value; }
    }
    public Sprite secondDialogueSprite
    {
        get { return _secondDialogueSprite; }
        set { _secondDialogueSprite = value; }
    }
    public List<string> choicesText
    {
        get { return _choicesText; }
        set { _choicesText = value; }
    }
    public List<DialogueSO> choicesNextDialogue
    {
        get { return _choicesNextDialogue; }
        set { _choicesNextDialogue = value; }
    }
    public DialogueSO nextDialogue
    {
        get { return _nextDialogue; }
        set { _nextDialogue = value; }
    }
    public AudioClip voiceOver()
    {
        return _voiceOver;
    }
    public bool hasFadeIn
    {
        get { return _hasFadeIn; }
        set { _hasFadeIn = value; }
    }
    public float fadeInTime
    {
        get { return _fadeInTime; }
        set { _fadeInTime = value; }
    }
    public bool hasFadeOut
    {
        get { return _hasFadeOut; }
        set { _hasFadeOut = value; }
    }
    public float fadeOutTime
    {
        get { return _fadeOutTime; }
        set { _fadeOutTime = value; }
    }
    public int amountOfChoices
    {
        get { return _amountOfChoices; }
        set { _amountOfChoices = value; }
    }
    public Sprite background 
    {
        get { return _background; }
        set { _background = value; }
    }
}
