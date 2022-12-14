using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public Transform player;
    GameManager gameManager;
    public bool playerWalking;
    DialogueManager dialogueManager;
    public bool itemSuccess;
    public GameObject itemPoint;
    



    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }


    public void GoToItem(ItemData item)
    {
        if (dialogueManager.dialogueIsPlaying == false&&playerWalking==false)
        {
            //Play Click Sound
            gameManager.PlaySound(GameManager.soundsNames.click);
            //Hide Hint Box
            gameManager.UpdateHintBox(null, false);
            //Set the bool to true
            playerWalking = true;
         
            //Start Moving the Player
            StartCoroutine(gameManager.MoveToPoint(player, item.goToPoint.position));
            //Select the animation to use
            //player.GetComponent<SpriteAnimator>().PlayAnimation(gameManager.playerAnimations[1]);
            itemPoint = item.goToPoint.gameObject;

            //Check if the item can be pickedup
            TryGettingItem(item);

        }
    }



    public void TryGettingItem(ItemData item)
    {//-1 allows an item to be instantly picked up.
        bool canGetItem = (item.requiredItemID == -1 && (gameManager.selectedItemID <= 0) || gameManager.selectedItemID == item.requiredItemID);
        if (canGetItem)
        {
            if (item.canBePickedUp)
            {
                GameManager.collectedItems.Add(item);

            }
        }

        if (gameManager.selectedItemID == item.requiredItemID&&item.requiredItemID>=1)
        {
            itemSuccess = true;
            GameManager.collectedItems.RemoveAt(gameManager.selectedCanvasSlotID);
            gameManager.equipmentImages[gameManager.selectedCanvasSlotID].sprite =gameManager.emptyItemSlotSprite;
            gameManager.SelectItem(10);


        }

        StartCoroutine(UpdateSceneAfterItem(item, canGetItem));
    }

    private IEnumerator UpdateSceneAfterItem(ItemData item, bool canGetItem)
    {
        while (playerWalking)
        {    //Wait for the player to reach the item.
            yield return new WaitForSeconds(0.1f);
        }
        //Play Base Animation
        player.GetComponent<SpriteAnimator>().PlayAnimation(gameManager.playerAnimations[0]);
        player.GetComponentInChildren<SpriteRenderer>().flipX = item.flipX;
        yield return new WaitForSeconds(0.1f);


        //Destroy the game object of the item you pick up.
        if (canGetItem)
        {//play Sound
            gameManager.PlaySound(GameManager.soundsNames.use);
            itemSuccess = false;

            //Play the use animation
            player.GetComponent<SpriteAnimator>().PlayAnimation(gameManager.playerAnimations[2]);

            //Stops name tag appearing after collecting the item.
            gameManager.UpdateNameTag(null);

            //Remove certain objects. The one the player has picked up, for example.

            //Show Objects on screen

            if (item.successAnimation)
            {
                item.GetComponent<SpriteAnimator>().PlayAnimation(item.successAnimation);
                foreach (GameObject g in item.objectsToSetActive)

                    g.SetActive(true);
                foreach (GameObject g in item.objectsToRemove)
                {
                    Destroy(g, 4f);
                }
            }
            if (item.successAnimation == null)
            {
                foreach (GameObject g in item.objectsToRemove)

                    Destroy(g);
                foreach (GameObject g in item.objectsToSetActive)

                    g.SetActive(true);
            }
            gameManager.UpdateEquipmentCanvas();
            gameManager.UpdateHintBox(item, player.GetComponentInChildren<SpriteRenderer>().flipX);
            
            //display dialogue if you give a character the correct tiem.
            if (item.inkJson != null)
            {
                dialogueManager.EnterDialogueMode(item.inkJson);
            }
            
        }


        else
        {
            gameManager.UpdateHintBox(item, player.GetComponentInChildren<SpriteRenderer>().flipX);

        }
            //s the scene, checks if the game is completed.
            gameManager.CheckSpecialConditions(item, canGetItem);
            yield return null;
    }
    public void AddItem(ItemData item)
    {
        GameManager.collectedItems.Add(item);
        gameManager.UpdateEquipmentCanvas();
    }
}


