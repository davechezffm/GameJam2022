using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using Ink.UnityIntegration;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{[Header("Dailogue UI")]
    private static DialogueManager instance;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] TextMeshProUGUI dialogueText;
    private Story currentStory;
    
    public bool dialogueIsPlaying { get; private set; }
    [Header("ChoicesUI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    private GameManager gameManager;
    private DialogueVariables dialogueVariables;
    public bool test;
    

    [Header("Global Ink Files")]
    [SerializeField] private InkFile globalsInkFile;
   
    


    private void Awake()
    {
        dialogueVariables = new DialogueVariables(globalsInkFile.filePath);
        

        if (instance != null)
        {
            Debug.LogWarning("There is more than one instance of dialogue manager");
            
            
        }
        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
                

    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        gameManager.equipmentCanvas.SetActive(false);
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        dialogueVariables.StartListening(currentStory);
        test = true;
        

        ContinueStory();  
       
    }
    private void ExitDialogueMode()
    {
        dialogueVariables.StopListening(currentStory);
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        gameManager.equipmentCanvas.SetActive(true);
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            
            if (currentStory.currentChoices.Count<1)
                
            {
                ContinueStory();
            }
        }
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {//Set text for the current dialogue line
            dialogueText.text = currentStory.Continue();
            //Display choices, if any for this dialogue.
            DisplayChoices();
            string savedJson = currentStory.state.ToJson();
        }

        else
        {
            ExitDialogueMode();
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        int index = 0;
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            StartCoroutine(choicesFixed(choices[index].gameObject));

            index++;
        }
        //Go through the remaining choices and make sure they are hidden.
        for(int i=index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        //StartCoroutine(SelectFirstChoice());
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
        
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);

        if (variableValue == null){

        }

        return variableValue;
    }

    public void SetVariableState(string variableName, Ink.Runtime.Object variableValue)
    {
        if (dialogueVariables.variables.ContainsKey(variableName))
        {
            dialogueVariables.variables.Remove(variableName);
            dialogueVariables.variables.Add(variableName, variableValue);
        }
        else
        {
            Debug.LogWarning("Tried to update variable that wasn't initialized by globals.ink: " + variableName);
        }
    }


    public IEnumerator choicesFixed(GameObject item)
    {
        Canvas.ForceUpdateCanvases();
        item.GetComponent<HorizontalLayoutGroup>().enabled = false;
        item.GetComponent<HorizontalLayoutGroup>().enabled = true;
       

        yield return null;

    }
}
