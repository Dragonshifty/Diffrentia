using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectDetails : MonoBehaviour
{
    private MainGame mainGame;
    int cardValue;
    string house;

    public TextMeshProUGUI cardValueText;
    public TextMeshProUGUI houseText;
    Collider2D cardCollider;

    // public void Initialize(MainGame game)
    // {
    //     mainGame = game;
    // }
    void Awake() {
        cardCollider = GetComponent<Collider2D>();  
        cardCollider.enabled = false;  
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
        // SendToLog();
    }

    void SendToLog()
    {
        Debug.Log(cardValue);
    }

    public void SetCollider(bool isEnabled)
    {
        cardCollider.enabled = isEnabled;
    }
    
}
