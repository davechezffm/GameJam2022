using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.UnityIntegration;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timer;
    public DialogueManager dm;
    public TextAsset inkTextSuccess;
    public TextAsset inkTextFail;
    public bool gameOver;
    public int jigsawPiecesInPlace;
    public bool success;
    public Text countdownTimer;
    public string characterName;
    public TextMeshProUGUI characterText;

    // Start is called before the first frame update
    void Start()
    {
        dm = FindObjectOfType<DialogueManager>();
       
    }

    // Update is called once per frame
    void Update()
    {
        countdownTimer.text = timer.ToString();
        if (!success)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
        }
        if (timer <= 0)
        {
            {
                timer = 0;
                gameOver = true;
                dm.EnterDialogueMode(inkTextFail);
                characterText.text = characterName;
            }
        }
    

        if (jigsawPiecesInPlace == 9)
        {
            success = true;
            dm.EnterDialogueMode(inkTextSuccess);
            characterText.text = characterName;
        }
    }
}
