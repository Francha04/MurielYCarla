using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DialogueSO))]
public class DialogueInspector : Editor
{
    public Sprite emptyBackground;
    public Sprite emptyImage;    
    public override void OnInspectorGUI()
    {        
        base.OnInspectorGUI();
        EditorUtility.SetDirty(target);
        DialogueSO dialogue = (DialogueSO)target;
        dialogue.hasChoices = EditorGUILayout.ToggleLeft("Has Choices? ", dialogue.hasChoices);
        
        if (!dialogue.hasChoices) 
        {            
            dialogue.nextDialogue = (DialogueSO)EditorGUILayout.ObjectField("Next Dialogue", dialogue.nextDialogue, typeof(DialogueSO), false);            
        }

        if (dialogue.hasChoices) 
        {
            dialogue.amountOfChoices = EditorGUILayout.IntField("How many choices?", dialogue.amountOfChoices);
            if (dialogue.choicesText.Count != dialogue.amountOfChoices)
            {   
                dialogue.choicesText.Clear();
                for (int i = 0; i < dialogue.amountOfChoices; i++)
                {
                    dialogue.choicesText.Add("");
                }
            }
            if (dialogue.choicesNextDialogue.Count != dialogue.amountOfChoices)
            {
                dialogue.choicesNextDialogue.Clear();
                for (int i = 0; i < dialogue.amountOfChoices; i++)
                {
                    dialogue.choicesNextDialogue.Add(null);
                }
            }
            switch (dialogue.amountOfChoices)
            {
                case 0:
                    EditorGUILayout.LabelField("CUIDADO. Marcaste el dialogo como que tiene elecciones (Has Choices), \n pero no colocaste ninguna opcion (How Many Choices = 0) .",GUILayout.Width(500f) , GUILayout.Height(40f));
                    break;
                case 1:
                    dialogue.choicesText[0] = EditorGUILayout.TextField("First Choice", dialogue.choicesText[0], GUILayout.Width(500f), GUILayout.Height(50f), GUILayout.ExpandHeight(true));
                    dialogue.choicesNextDialogue[0] = (DialogueSO)EditorGUILayout.ObjectField("First Dialogue", dialogue.choicesNextDialogue[0], typeof(DialogueSO), false);
                    break;
                case 2:
                    dialogue.choicesText[0] =  EditorGUILayout.TextField("First Choice", dialogue.choicesText[0], GUILayout.Width(500f), GUILayout.Height(50f),     GUILayout.ExpandHeight(true));
                    dialogue.choicesText[1] = EditorGUILayout.TextField("Second Choice", dialogue.choicesText[1], GUILayout.Width(500f), GUILayout.Height(50f), GUILayout.ExpandHeight(true));
                    dialogue.choicesNextDialogue[0] = (DialogueSO)EditorGUILayout.ObjectField("First Dialogue", dialogue.choicesNextDialogue[0], typeof(DialogueSO), false);
                    dialogue.choicesNextDialogue[1] = (DialogueSO)EditorGUILayout.ObjectField("Second Dialogue", dialogue.choicesNextDialogue[1], typeof(DialogueSO), false);
                    break;
                case 3:
                    dialogue.choicesText[0] = EditorGUILayout.TextField("First Choice", dialogue.choicesText[0], GUILayout.Width(500f), GUILayout.Height(50f), GUILayout.ExpandHeight(true));
                    dialogue.choicesText[1] = EditorGUILayout.TextField("Second Choice", dialogue.choicesText[1], GUILayout.Width(500f), GUILayout.Height(50f), GUILayout.ExpandHeight(true));
                    dialogue.choicesText[2] = EditorGUILayout.TextField("Third Choice", dialogue.choicesText[2], GUILayout.Width(500f), GUILayout.Height(50f), GUILayout.ExpandHeight(true));
                    dialogue.choicesNextDialogue[0] = (DialogueSO)EditorGUILayout.ObjectField("First Dialogue", dialogue.choicesNextDialogue[0], typeof(DialogueSO), false);
                    dialogue.choicesNextDialogue[1] = (DialogueSO)EditorGUILayout.ObjectField("Second Dialogue", dialogue.choicesNextDialogue[1], typeof(DialogueSO), false);
                    dialogue.choicesNextDialogue[2] = (DialogueSO)EditorGUILayout.ObjectField("Third Dialogue", dialogue.choicesNextDialogue[2], typeof(DialogueSO), false);                    
                    break;
                case 4:
                    dialogue.choicesText[0] = EditorGUILayout.TextField("First Choice", dialogue.choicesText[0], GUILayout.Width(500f), GUILayout.Height(50f), GUILayout.ExpandHeight(true));
                    dialogue.choicesText[1] = EditorGUILayout.TextField("Second Choice", dialogue.choicesText[1], GUILayout.Width(500f), GUILayout.Height(50f), GUILayout.ExpandHeight(true));
                    dialogue.choicesText[2] = EditorGUILayout.TextField("Third Choice", dialogue.choicesText[2], GUILayout.Width(500f), GUILayout.Height(50f), GUILayout.ExpandHeight(true));
                    dialogue.choicesText[3] = EditorGUILayout.TextField("Fourth Choice", dialogue.choicesText[3], GUILayout.Width(500f), GUILayout.Height(50f), GUILayout.ExpandHeight(true));
                    dialogue.choicesNextDialogue[0] = (DialogueSO)EditorGUILayout.ObjectField("First Dialogue", dialogue.choicesNextDialogue[0], typeof(DialogueSO), false);
                    dialogue.choicesNextDialogue[1] = (DialogueSO)EditorGUILayout.ObjectField("Second Dialogue", dialogue.choicesNextDialogue[1], typeof(DialogueSO), false);
                    dialogue.choicesNextDialogue[2] = (DialogueSO)EditorGUILayout.ObjectField("Third Dialogue", dialogue.choicesNextDialogue[2], typeof(DialogueSO), false);
                    dialogue.choicesNextDialogue[3] = (DialogueSO)EditorGUILayout.ObjectField("Fourth Dialogue", dialogue.choicesNextDialogue[3], typeof(DialogueSO), false);
                    break;
                default:
                    EditorGUILayout.LabelField("CUIDADO. Marcaste un numero invalido para la cantidad de elecciones. \n Tiene que ser un numero entre 1 y 4", GUILayout.Width(500f), GUILayout.Height(40f));
                    break;                    
            }
        }
        
        dialogue.hasFadeIn = EditorGUILayout.ToggleLeft("Has Fade In?",dialogue.hasFadeIn);
        if (dialogue.hasFadeIn)
        {   
            dialogue.fadeInTime = EditorGUILayout.Slider(dialogue.fadeInTime, 0f, 3f);      
            //GUILayout.Label("");
            if (dialogue.fadeInTime == 0) { EditorGUILayout.LabelField("CUIDADO. Tenes un valor de 0 de fadein, para eso \n deberías desactivar 'Has Fade In?'", GUILayout.Width(500f), GUILayout.Height(40f)); }
        }


        dialogue.hasFadeOut = EditorGUILayout.ToggleLeft("Has Fade Out?", dialogue.hasFadeOut);
        if (dialogue.hasFadeOut)
        {
            dialogue.fadeOutTime = EditorGUILayout.Slider(dialogue.fadeOutTime, 0f, 5f);
            //GUILayout.Label("");
            if (dialogue.fadeOutTime == 0) { EditorGUILayout.LabelField("CUIDADO. Tenes un valor de 0 de fadeout, para eso \n deberías desactivar 'Has Fade Out?'", GUILayout.Width(500f), GUILayout.Height(40f)); }
        }
        EditorGUILayout.Space();
        if (GUILayout.Button("Check all Dialogues")) 
        {
            checkAllDialogues(); 
        }
        if (GUILayout.Button("No Images")) 
        {
            dialogue.background = emptyBackground;
            dialogue.dialogueSprite = emptyImage;
            dialogue.secondDialogueSprite = emptyImage;
        }        
    }
    private void checkAllDialogues() 
    {
        DialogueSO[] dialoguesToCheck = Resources.LoadAll<DialogueSO>("Dialogues");
        foreach (DialogueSO dialogue in dialoguesToCheck)
        {
            string log = "El dialogo " + dialogue.name + ": ";
            if (dialogue.hasFadeIn && dialogue.fadeInTime == 0) 
            {
                log += "\n - Tiene marcado hasFadeIn como verdadero, pero tiene el tiempo en 0. Esto se arregla desactivando hasFadeIn o cambiando el tiempo fadeInTime a un valor distinto de cero";
            }
            if (dialogue.hasFadeOut && dialogue.fadeOutTime == 0)
            {
                log += "\n - Tiene marcado hasFadeOut como verdadero, pero tiene el tiempo en 0. Esto se arregla desactivando hasFadeOut o cambiando el tiempo fadeOutTime a un valor distinto de cero";
            }
            if (dialogue.hasChoices && (dialogue.choicesText.Count < 1 || dialogue.choicesText.Count > 4)) 
            {
                log += "\n - Tiene marcado hasChoices como verdadero, pero no tiene una cantidad de elecciones validad. Recuerda que debe ser un numero entre 1 y 4";
            }
            if (dialogue.dialogueSprite == null)
            {
                log += "\n - No tiene sprite marcado.";
            }
            if (dialogue.secondDialogueSprite == null)
            {
                log += "\n - No tiene segundo sprite marcado.";
            }
            if (dialogue.nextDialogue == null && !dialogue.hasChoices) 
            {
                log += "\n - No tiene un nextDialogue.";
            }
            /*if (dialogue.voiceOver() == null) 
            {
                log += "\n - No tiene voiceover";
            }*/
            if (dialogue.dialogueText == "") 
            {
                log += "\n - No tiene texto de dialogo.";
            }
            if (dialogue.background == null)
            {
                log += "\n - No tiene fondo.";
            }
            if (log != "El dialogo " + dialogue.name + ": ") { Debug.Log(log); }
        }
    }
}
