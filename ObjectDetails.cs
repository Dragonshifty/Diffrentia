using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectDetails : MonoBehaviour
{
    private MainGame mainGame;
    int cardValue;
    string house;
    public RectTransform cardFront;
    public RectTransform cardBack;

    public TextMeshProUGUI cardValueText;
    public TextMeshProUGUI houseText;
    Collider2D cardCollider;
    Canvas canvas;
    public bool showFront;

    void Awake() {
        cardCollider = GetComponent<Collider2D>();  
        cardCollider.enabled = false;  
        canvas = GetComponentInChildren<Canvas>();
        if (canvas != null)
        {
            canvas.sortingOrder = 0;
        }
        canvas.overrideSorting = true;
        cardBack.gameObject.SetActive(true);
        cardFront.gameObject.SetActive(false);
        showFront = false;
    }

    void Start() {
        
    }
    public int CardValue
    { 
        get { return cardValue;} 
        set { cardValue = value; } 
    }

    public string House
    { 
        get { return house;} 
        set { house = value; }  
    }

    void OnMouseDown() 
    {
        MainGame.Instance.GetCardToPlay(gameObject);
    }


    public void SetCollider(bool isEnabled)
    {
        cardCollider.enabled = isEnabled;
    }

    public void RaiseSortingOrder(int sortOrder)
    {
        canvas.sortingOrder = sortOrder;
    }

    public void SetFrontBool()
    {
        showFront = true;
    }
    
}
