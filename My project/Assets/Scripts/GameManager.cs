using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Pathfinding;


public class GameManager : MonoBehaviour
{
    public static List<ItemData> collectedItems=new List<ItemData>();
    public List<ItemData> viewList = collectedItems;
    static float moveSpeed = 3.5f, moveAccuracy = 0.15f;
    [Header("Setup")]
    public AnimationData[] playerAnimations;
    public RectTransform nameTag, hintBox;
    public AudioSource soundTrackSource;
    public AudioClip[] soundEffects;
    public bool moving;
    AIPath path;
    public GameObject target;
    public GameObject player;
    //public GameObject inventory:
    public enum soundsNames
    { 
    none,
    click,
    introLand,
    step,
    use,
    plantGrow,
    crunch,
    explosion,
    santaAppears,
    playerInABag,
    win,
    credits    
    }

    [Header("Local Scenes")]
    public Image blockingImage;
    public GameObject[] localScenes;
    public int activeLocalScene=1;
    public Transform []  playerStartingPositions;
    public ClickManager clickManager;

    [Header("Equipment Setup")]
    public GameObject equipmentCanvas;
    public Image [] equipmentSlots, equipmentImages;
    public string[] itemDescription;
    public Sprite emptyItemSlotSprite;
    public Color selectedItemColor;
    public int selectedCanvasSlotID;
        public int selectedItemID=-1;
    public bool selectableItem;
    public AstarPath path2;
    private DialogueManager dialogueManager;


    public void Start()
    {

        dialogueManager = FindObjectOfType<DialogueManager>();
        path = GetComponent<AIPath>();
        clickManager = FindObjectOfType<ClickManager>();
    }
    public IEnumerator MoveToPoint(Transform myObject, Vector2 point)
    {
        if (moving == false)
        {
            //myObject.Translate(moveSpeed * myObject.transform.position*Time.deltaTime);
            if (clickManager.itemPoint == null)
            {
                yield return new WaitForSeconds(0.1f);
                
            }
            target = clickManager.itemPoint;
            player.GetComponent<SpriteAnimator>().PlayAnimation(playerAnimations[1]);


            player.GetComponent<Pathfinding.AIDestinationSetter>().target = target.transform;
            player.GetComponent<AIPath>().canMove = true;
               


            Vector2 positionDifference = point - (Vector2)myObject.position;
            //Change animation of the Player
            if (myObject.GetComponentInChildren<SpriteRenderer>() && positionDifference.x != 0)
            {
                //If the X difference is bigger than zero, X is flipped so the player animation goes to the right.
                myObject.GetComponentInChildren<SpriteRenderer>().flipX = positionDifference.x > 0;

            }
            while (positionDifference.magnitude > moveAccuracy)
            {
                moving = true;
               

                //myObject.Translate(moveSpeed * positionDifference.normalized * Time.deltaTime);
                positionDifference = point - (Vector2)myObject.position;
                yield return null;
            }
            myObject.position = point;
           
            if (myObject == FindObjectOfType<ClickManager>().player)
                FindObjectOfType<ClickManager>().playerWalking = false;
            player.GetComponent<AIPath>().canMove = false;
            moving = false;
            player.GetComponent<Pathfinding.AIDestinationSetter>().target = null;
            clickManager.itemPoint = null;
            yield return null;
        }
    }

    public void UpdateNameTag(ItemData item)
    {
        if (dialogueManager.dialogueIsPlaying == false)
        {
            if (item == null)
            {
                nameTag.parent.gameObject.SetActive(false);
                return;
            }

            string nameText = item.objectName;
            Vector2 size = item.nameTagSize;
            nameTag.parent.gameObject.SetActive(true);
            //If the item has been collected, use a different name and size
            if (collectedItems.Contains(item))
            {
                //Change the name
                nameText = item.itemName;
                //Change the size of the name tag box
                size = item.itemNameTagSize;
            }
            //Change the name
            nameTag.GetComponentInChildren<TextMeshProUGUI>().text = nameText;
            //Change the size of the name tag box
            nameTag.sizeDelta = size;
            //Move the name tag so it is on the screen
            nameTag.localPosition = new Vector2(0, 0.3f);
        }
    }

    public void UpdateHintBox(ItemData item, bool playerFlipped)
    {
        if (item == null)
        {//Hide Hint Box
            hintBox.gameObject.SetActive(false);
            return;
        }

        // Show Hint Box
        hintBox.gameObject.SetActive(true);
        //Chnage name
        if (selectedItemID > 0 && selectedItemID != item.requiredItemID)
        {
            hintBox.GetComponentInChildren<TextMeshProUGUI>().text = item.wrongItem;
        }
        else
        {hintBox.GetComponentInChildren<TextMeshProUGUI>().text = item.hintMessage;}
        //Change Size
        hintBox.sizeDelta = item.hintBoxSize;
        //Move Tag
        /*if (playerFlipped)
            hintBox.localPosition = new Vector2(0, 0);
        else hintBox.localPosition = Vector2.zero;*/
    }

    public void CheckSpecialConditions(ItemData item, bool cangetitem)
    {// Checks the value of the item
        switch (item.itemID)
        {
            case -11:
                //Go to Scene 1 from the kitchen
                StartCoroutine(ChangeScene(1, 0.1f));
                break;
                //path2.Scan();

            case -12:
                //Go to Scene 2
                StartCoroutine(ChangeScene(2, 0.1f));
                break;

            case -13:
                //Go to Scene 3
                StartCoroutine(ChangeScene(3, 0.1f));
                break;

            case -14:
                //Go to Comedy
                StartCoroutine(ChangeScene(5, 0.1f));
                break;

            case -15:
                //Go to Scene 1 from the Living Room
                StartCoroutine(ChangeScene(4, 0.1f));
                break;

            case -1:
                //Win the Game
                if (cangetitem)
                {
                    float delay =
                    item.successAnimation.sprites.Length * item.successAnimation.framesOfGap * AnimationData.targetFrameTime;
                    StartCoroutine(ChangeScene(3, delay));
                }
                break;
            default:
                break;

        }
    }
    public void SelectItem(int equipmentCanvasID)
    {   //Make a color transparent
        Color c = Color.white;
        c.a = 0;
        //Change the alpha of the previous slot to 0
        equipmentSlots[selectedCanvasSlotID].color = c;

        // save changes and stop if an empty slot is selected or the last item is removed
        if (equipmentCanvasID >= collectedItems.Count || equipmentCanvasID < 0)
        {//no item selected
            selectedItemID = -1;
            selectedCanvasSlotID = 0;
            return;
        }

        Debug.Log("Code");
        //change the alpha of the new slot to x
        equipmentSlots[equipmentCanvasID].color = selectedItemColor;
        //save changes
        selectedCanvasSlotID = equipmentCanvasID;
        selectedItemID = collectedItems[selectedCanvasSlotID].itemID;
        
        
    }

    public void ShowItemName(int equipmentCanvasID)
    {//IF an item is in this slot
        if (equipmentCanvasID < collectedItems.Count)
        {
            UpdateNameTag(collectedItems[equipmentCanvasID]);
        }
    }

    public void UpdateEquipmentCanvas()
    {
        //Find out how many items we have and when to stop
        int itemsAmount = collectedItems.Count, itemSlotsAmount=equipmentSlots.Length;
        //replace no item sprites and old item sprites with collected items[x] images.
        for(int i=0;i<itemsAmount+2; i++)
        {
            //choose between no item image and item sprite
            if (i < itemsAmount)
            {
                Debug.Log("Less");
                itemDescription[i] = collectedItems[i].itemTextInInventory;
                equipmentImages[i].gameObject.GetComponent<ItemInventoryDescription>().itemDescription = itemDescription[i];
                equipmentImages[i].sprite = collectedItems[i].itemSlotSprite;
                equipmentImages[i].gameObject.GetComponent<ItemInventoryDescription>().itemSelectable=true;


            }

            if(i>=itemsAmount)
            {
                Debug.Log(equipmentImages[i]);
                
                equipmentImages[i].sprite = emptyItemSlotSprite;
                
                equipmentImages[i].gameObject.GetComponent<ItemInventoryDescription>().itemSelectable = false;
            }


            //add spécial conditions for selecting items
                if (itemsAmount == 0)
            {
                SelectItem(-1);
            }
          
        }
    }
    public IEnumerator ChangeScene(int sceneNumber, float delay)
    {// scene goes black and no clicking allowed

        yield return new WaitForSeconds(delay);
        //If end Game, remove the player.

        if (sceneNumber == localScenes.Length - 1){
            PlaySound(soundsNames.win);
            FindObjectOfType<ClickManager>().player.gameObject.SetActive(false);
            yield return new WaitForSeconds (0.5f);
        }
        Color c = blockingImage.color;
        //Screen goes black in one second
        blockingImage.enabled = true;
        while (blockingImage.color.a < 1)
        {
            c.a += Time.deltaTime;
            blockingImage.color = c;
            

        }
       //Change the Scene
        //Hide the old Scene
        localScenes[activeLocalScene].SetActive(false);
        //Show the new Scene
        localScenes[sceneNumber].SetActive(true);
        if (activeLocalScene == 0)
        {
            soundTrackSource.Play();
        }
        //Save the current scene
        activeLocalScene = sceneNumber;
        //Teleport the player to the correct position
        FindObjectOfType<ClickManager>().player.position = playerStartingPositions[sceneNumber].position;
        //Hide the hintbox loading a new scene
        UpdateHintBox(null, false);
        //Hide Name Tag
        UpdateNameTag(null);
        //Reset the animations
        foreach(SpriteAnimator spriteAnimator in FindObjectsOfType<SpriteAnimator>())
        {
            spriteAnimator.PlayAnimation(null);
        }
        //Equipment Bar Hide/Show
        equipmentCanvas.gameObject.SetActive(sceneNumber>0&&sceneNumber<(localScenes.Length-1));
        while (blockingImage.color.a > 0)
        {
            c.a -= Time.deltaTime;
            blockingImage.color = c;
        }
        blockingImage.enabled = false;
        path2.Scan();

        yield return null;
    }
    //Reloads the current scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        StartCoroutine(ChangeScene(1, 0));
    }

    public void PlaySound(soundsNames name)
    {
        if (name != soundsNames.none)
        {//Plays a sound efffect in the list, The int name obviously is which one.
            AudioSource.PlayClipAtPoint(soundEffects[(int)name], transform.position);
        }
    }

    public void OpenOnventory()
    {
        equipmentCanvas.SetActive(true);
        Debug.Log("Clicked");
    }

    public void ItemInInventoryDescription(ItemInventoryDescription item)
    {
        if (item.itemSelectable)
        {
            hintBox.gameObject.SetActive(false);
            // Show Hint Box
            hintBox.gameObject.SetActive(true);
            //Change name
            hintBox.GetComponentInChildren<TextMeshProUGUI>().text = item.itemDescription;
            //Change Size
            hintBox.sizeDelta = item.hintBoxSize;
        }
        //Move Tag
    }


    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hintBox.gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Color c = Color.white;
            c.a = 0;
            equipmentSlots[selectedCanvasSlotID].color = c;
            selectedItemID = -1;
            selectedCanvasSlotID = 0;
            Debug.Log("Mouse Button 2");
        }
    }

    public void DisableObjects(GameObject objectToDisable)
    {
        objectToDisable.SetActive(false);
    }

}

