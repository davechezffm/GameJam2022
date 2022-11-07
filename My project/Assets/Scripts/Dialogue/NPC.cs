using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject requiredItem;
    public bool test;
    public DialogueManager dialogueManager;
    public string item;
    GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        test = ((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState(item)).value;
        if (requiredItem==null)
        {
           
             ((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState(item)).value = true;
        }
    }
}
