using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.UnityIntegration;
using Ink.Runtime;

public class full_inventory : MonoBehaviour
{
    public GameManager gm;
    public DialogueManager dm;
    public TextAsset inkDialogue;
    private bool nxScene;
    public GameObject nameTagCanvas;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        dm = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.viewList.Count >= 4&&nxScene==false)
        {
            nexScene();
            nxScene = true;
        }   
    }

    private void nexScene()
    {
        dm.EnterDialogueMode(inkDialogue);
        nameTagCanvas.SetActive(false);

    }
}
