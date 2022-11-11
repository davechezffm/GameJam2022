using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.UnityIntegration;
using Ink.Runtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CutsceneDialogue : MonoBehaviour
{
    public DialogueManager dm;
    public TextAsset dialogue;
    public string objectItem;
    public GameObject cutscene;
    public bool cutsceneFinished;
    public bool cutsceneMore;
    public string sceneName;
    public GameObject sceneClose;
    public GameObject sceneOpen;
    public bool endScene;
    public string characterName;
  
    public TextMeshProUGUI characterText;
    // Start is called before the first frame update

    private void Start()
    {
        dm = FindObjectOfType<DialogueManager>();
        
    }
    // Update is called once per frame
    void Update()
    {
       
        //If the cutscene continues after the dialogue.


        if (cutsceneMore == true)
            //The bool here is in the globals list. This needs to be made true at the end of the dialogue in INK
       cutsceneFinished = ((Ink.Runtime.BoolValue)DialogueManager.GetInstance().GetVariableState(objectItem)).value;
        if (cutsceneFinished == true)
        {//This simply will enable the next part of the cutscene.
            //SceneManager.LoadScene(sceneName);
            cutscene.SetActive(true);
        }
    }

    public void Dialogue( TextAsset dialogue)
    {
        characterText.text = characterName;
        dm.EnterDialogueMode(dialogue);
    }

    public void ChangeScene()
    {
       
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene()
    {
        sceneClose.SetActive(false);
        sceneOpen.SetActive(true);
    }

 




}
