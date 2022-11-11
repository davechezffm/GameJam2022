using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemData : MonoBehaviour
{[Header("Setup")]
    public string objectName;
    public Transform goToPoint;
    public int itemID, requiredItemID;
    public Vector2 nameTagSize = new Vector2(3, 0.85f);
    public bool flipX;
    [Header("Item in Inventory")]
    public string itemName;
    public Vector2 itemNameTagSize = new Vector2(3, 0.85f);
    public string itemTextInInventory;
    public Sprite itemSlotSprite;
    public bool canBePickedUp;


    [Header("Success")]
    public GameObject[] objectsToRemove;
    public GameObject[] objectsToSetActive;
    public AnimationData successAnimation;
    public TextAsset inkJson; 


    [Header("Look at Object")]
    [TextArea(3, 3)]
    public string hintMessage;
    public Vector2 hintBoxSize = new Vector2(6, 0.85f);
  [Header("Wrong item used on object")]
    public string wrongItem;

    
}
