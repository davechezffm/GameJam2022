using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceHighlighted : MonoBehaviour
{
    public GameObject choice;
   public void HighlightChoice()
    {
        choice.GetComponent<Image>().color = Color.red; 
            }

    public void NormalColor()
    {
        choice.GetComponent<Image>().color =new Color (0, 0, 0);
    }

 
}
