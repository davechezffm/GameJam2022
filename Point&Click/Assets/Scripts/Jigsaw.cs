using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jigsaw : MonoBehaviour
{
    private bool dragging = false;
    private GameObject answer;
    public bool correct = false;
    private Vector2 startPosition;
    public RectTransform rTransform;
    public string answerTag;
    private float y;
    private float x;
    private bool inPlace;
    private void Start()
    {
        startPosition = transform.position;
        y = rTransform.localPosition.y;
        x = rTransform.localPosition.x;

        
        
    }

    void Update()
    {
        if (transform.position.x==startPosition.x&&transform.position.y==startPosition.y)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        if (!inPlace)
        {
            if (dragging)
            {
                transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            }
        }
    }

    public void StartDrag()
    {
       
            startPosition = transform.position;
            dragging = true;
        
        
    }

    public void EndDrag()
    {
        dragging = false;
        if (correct==true)
        {
            gameObject.GetComponent<RectTransform>().localPosition = new Vector2(x,y);
            inPlace = true;
        }

        else { 
            transform.position = startPosition;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {  if(collider2D.CompareTag(answerTag))
        Debug.Log("Detection");
        correct = true;
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(answerTag))
        {
            correct = false;
            Debug.Log("No Detection");
        }
    }
}
