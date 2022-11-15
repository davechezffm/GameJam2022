using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDialogueManager : MonoBehaviour
{
    public DialogueManager dialogueManager;
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.ExitDialogueMode();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
