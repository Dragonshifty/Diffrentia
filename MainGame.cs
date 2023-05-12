using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    List<GameObject> deck = new List<GameObject>();
    List<GameObject> playerHand = new List<GameObject>();
    List<GameObject> compyHand = new List<GameObject>();
    MoveCards moveCards;
    CreateCards createCards;
    GameObject topCard;
    
    void Awake() {
        createCards = FindObjectOfType<CreateCards>();
        moveCards = FindObjectOfType<MoveCards>();
    }
    
    void Start()
    {
        GetNewDeck();
        SetPlayerHand();
        moveCards.DistributeCards(playerHand, true);
        ShowTop();
        SetCompyHand();
        moveCards.DistributeCards(compyHand, false);
        ShowTop();
        // SortHand(playerHand);
        // moveCards.DistributeCards(playerHand);

    }

    void GetNewDeck()
    {
        deck = createCards.GetNewDeck();
    }

    void SetPlayerHand()
    {
        for (int i = 0; i < 5; i++)
        {
            playerHand.Add(deck[0]);
            deck.RemoveAt(0);
        }
    }

    void SetCompyHand()
    {
        for (int i = 0; i < 5; i++)
        {
            compyHand.Add(deck[0]);
            deck.RemoveAt(0);
        }
    }

    void SortHand(List<GameObject> hand)
    {
        hand.Sort((card1, card2) =>
        {
            int cardValue1 = card1.GetComponent<ObjectDetails>().CardValue;
            int cardValue2 = card2.GetComponent<ObjectDetails>().CardValue;
            return cardValue1.CompareTo(cardValue2);
        });
    }

    public void ShowTop()
    {
        deck[0].SetActive(true);
    }
}
