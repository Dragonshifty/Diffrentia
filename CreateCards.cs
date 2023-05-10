using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateCards : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform startingPosition;
    
    List<GameObject> deck = new List<GameObject>();
    List<GameObject> playerHand = new List<GameObject>();
    List<GameObject> compyHand = new List<GameObject>();
    List<string> houses = new List<string>();
    MoveCards moveCards;
    private void Awake() 
    {
        moveCards = FindObjectOfType<MoveCards>();
        houses.Add("Fox");
        houses.Add("Cat");
        houses.Add("Dragon");
        houses.Add("Owl");
    }
    void Start()
    {
        
        SpawnCards();
        Shuffle(deck);
        // ShowTop();
        // Shift();
        SetPlayerHand();
        SortPlayerHand();
    }

    void SpawnCards()
    {
        Vector2 waypointStart = startingPosition.position;
        foreach (string house in houses){
            for (int i = 1; i < 14; i++)
            {
                GameObject card = Instantiate(prefab, waypointStart, Quaternion.identity);
                ObjectDetails cardDetails = card.GetComponent<ObjectDetails>();
                cardDetails.CardValue = i;
                cardDetails.House = house;
                cardDetails.houseText.text = house;
                
                switch (i)
                {
                    case 11:
                        cardDetails.cardValueText.text = "J";
                        break;
                    case 12:
                        cardDetails.cardValueText.text = "Q";
                        break;
                    case 13:
                        cardDetails.cardValueText.text = "K";
                        break;
                    default:
                        cardDetails.cardValueText.text = i.ToString();
                        break;
                }                
                card.SetActive(false);
                deck.Add(card);
            }
        }
    }

    public void Shuffle<T>(IList<T> values)
    {
        System.Random rand = new System.Random();
        for (int i = values.Count - 1; i > 0; i--)
        {
            int k = rand.Next(i + 1);
            T value = values[k];
            values[k] = values[i];
            values[i] = value;
        }
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

    void SortPlayerHand()
    {
        // playerHand.Sort(CompareCardValues);

        playerHand.Sort((card1, card2) =>
        {
            int cardValue1 = card1.GetComponent<ObjectDetails>().CardValue;
            int cardValue2 = card2.GetComponent<ObjectDetails>().CardValue;
            return cardValue1.CompareTo(cardValue2);
        });
        foreach (GameObject card in playerHand)
        {
            ObjectDetails cardDetails = card.GetComponent<ObjectDetails>();
            Debug.Log(cardDetails.CardValue);
        }
    }

    private int CompareCardValues(GameObject card1, GameObject card2)
    {
        ObjectDetails details1 = card1.GetComponent<ObjectDetails>();
        ObjectDetails details2 = card2.GetComponent<ObjectDetails>();

        if (details1.CardValue < details2.CardValue)
        {
            return -1;
        }
        else if (details1.CardValue > details2.CardValue)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public void ShowTop()
    {
        deck[0].SetActive(true);
    }

    public void Shift()
    {
        moveCards.DealCardOne(deck[0]);
    }
}
