using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateCards : MonoBehaviour
{
    [SerializeField] GameObject foxPrefab;
    [SerializeField] GameObject catPrefab;
    [SerializeField] GameObject dragonPrefab;
    [SerializeField] GameObject falconPrefab;
    GameObject prefab;
    public WaypointsSO waypointsSO;
    
    List<GameObject> deck = new List<GameObject>();
    List<GameObject> playerHand = new List<GameObject>();
    List<GameObject> compyHand = new List<GameObject>();
    List<string> houses = new List<string>();
    Transform startingPosition;
    
    private void Awake() 
    {
        startingPosition = waypointsSO.GetDeckWayPoint();
        houses.Add("Fox");
        houses.Add("Cat");
        houses.Add("Dragon");
        houses.Add("Falcon");
    }
    void Start()
    {
        
        // SpawnCards();
        // Shuffle(deck);
        // ShowTop();
        // Shift();
        // SetPlayerHand();
        // SortPlayerHand();
    }

    public List<GameObject> GetNewDeck()
    {
        ClearDeck();
        SpawnCards();
        Shuffle(deck);
        return deck;
    }

    void SpawnCards()
    {
        Vector2 waypointStart = startingPosition.position;
        foreach (string house in houses){
            for (int i = 2; i < 15; i++)
            {
                if (house == "Fox") prefab = foxPrefab;
                if (house == "Cat") prefab = catPrefab;
                if (house == "Dragon") prefab = dragonPrefab;
                if (house == "Falcon") prefab = falconPrefab;
                GameObject card = Instantiate(prefab, waypointStart, Quaternion.Euler(0, 180f, 0));
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
                    case 14:
                        cardDetails.cardValueText.text = "A";
                        break;
                    default:
                        cardDetails.cardValueText.text = i.ToString();
                        break;
                }                
                // card.SetActive(false);
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

    void ClearDeck()
    {
        if (deck != null)
        {
            foreach (GameObject card in deck)
            {
                Destroy(card);                
            }
            deck.Clear();
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

    // void SetCompyHand()
    // {
    //     for (int i = 0; i < 5; i++)
    //     {
    //         compyHand.Add(deck[0]);
    //         deck.RemoveAt(0);
    //     }
    // }

    // void SortPlayerHand()
    // {

    //     playerHand.Sort((card1, card2) =>
    //     {
    //         int cardValue1 = card1.GetComponent<ObjectDetails>().CardValue;
    //         int cardValue2 = card2.GetComponent<ObjectDetails>().CardValue;
    //         return cardValue1.CompareTo(cardValue2);
    //     });
    //     foreach (GameObject card in playerHand)
    //     {
    //         ObjectDetails cardDetails = card.GetComponent<ObjectDetails>();
    //         Debug.Log(cardDetails.CardValue);
    //     }
    // }

    // private int CompareCardValues(GameObject card1, GameObject card2)
    // {
    //     ObjectDetails details1 = card1.GetComponent<ObjectDetails>();
    //     ObjectDetails details2 = card2.GetComponent<ObjectDetails>();

    //     if (details1.CardValue < details2.CardValue)
    //     {
    //         return -1;
    //     }
    //     else if (details1.CardValue > details2.CardValue)
    //     {
    //         return 1;
    //     }
    //     else
    //     {
    //         return 0;
    //     }
    // }

    public void ShowTop()
    {
        deck[0].SetActive(true);
    }

    // public void Shift()
    // {
    //     moveCards.DealCardOne(deck[0]);
    // }
}
