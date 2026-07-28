using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueUIController : MonoBehaviour
{
    VisualElement container;

    Label npcDialogue;
    string dialogue = "i am the boglin";
    string currentText;
    List<char> dialogueArray;
    public bool shouldLoadDialogue = true; // this should be false but is true for testing
    float timeToNextCharacter = Constants.c_timeToNextCharacter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueArray = dialogue.ToList();

        container = GetComponent<UIDocument>().rootVisualElement;

        npcDialogue = container.Q<Label>("DialogueText");
        npcDialogue.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldLoadDialogue == true)
        {
            timeToNextCharacter -= Time.deltaTime;

            if (timeToNextCharacter <= 0.0f)
            {
                if (dialogueArray.Count == 0)
                {
                    shouldLoadDialogue = false;
                    Debug.Log("returning");
                    return;
                }

                char currentChar = dialogueArray[0];
                npcDialogue.text += currentChar;
                Debug.Log(npcDialogue.text);
                dialogueArray.RemoveAt(0);

                timeToNextCharacter = Constants.c_timeToNextCharacter;
            }
        }
    }
}
