using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCards : MonoBehaviour
{
    public WaypointsSO waypointsSO;
    List<Transform> playerWaypoints;
    List<Transform> compyWaypoints;
    List<Transform> deckAndPileWaypoints;
    
    [SerializeField] float moveSpeed = 20f;

    private void Awake() {
        playerWaypoints = waypointsSO.GetPlayerWaypoints();
        compyWaypoints = waypointsSO.GetCompyWaypoints();
        deckAndPileWaypoints = waypointsSO.GetDeckAndPileWaypoints();
    }

    public void DistributeCards(List<GameObject> cards, bool isPlayer, bool isSorted)
    {
        StartCoroutine(DealCards(cards, isPlayer, isSorted));
    }

    IEnumerator DealCards(List<GameObject> cards, bool isPlayer, bool isSorted) {
        int index = 0;
        foreach (GameObject card in cards)
        {
            card.SetActive(true);
            // Vector3 startingPosition = card.transform.position;
            Vector3 targetPosition = isPlayer ? 
                playerWaypoints[index].position : compyWaypoints[index].position;

            while (Vector2.Distance(card.transform.position, targetPosition) > 0.01f)
            {
                float delta = moveSpeed * Time.deltaTime;
                card.transform.position = Vector2.MoveTowards(card.transform.position, targetPosition, delta);
                yield return null;
            }

            card.transform.position = targetPosition;
            yield return null;
            index++;
        }
        index = 0;
        // yield return StartCoroutine(ShowTop(topCard));
        if (!isSorted) yield return StartCoroutine(SortHand(cards, isPlayer, isSorted));
    }

    
    IEnumerator SortHand(List<GameObject> hand, bool isPlayer, bool isSorted)
    {
        hand.Sort((card1, card2) =>
        {
            int cardValue1 = card1.GetComponent<ObjectDetails>().CardValue;
            int cardValue2 = card2.GetComponent<ObjectDetails>().CardValue;
            return cardValue1.CompareTo(cardValue2);
        });
        isSorted = true;
        yield return StartCoroutine(DealCards(hand, isPlayer, isSorted));
    }

    // IEnumerator DealSortedCards(List<GameObject> cards, bool isPlayer) {
    //     int index = 0;
    //     foreach (GameObject card in cards)
    //     {
    //         card.SetActive(true);
    //         Vector3 startingPosition = card.transform.position;
    //         Vector3 targetPosition = isPlayer ? 
    //             playerWaypoints[index].position : compyWaypoints[index].position;

            

    //         while (Vector2.Distance(card.transform.position, targetPosition) > 0.01f)
    //         {
    //             float delta = moveSpeed * Time.deltaTime;
    //             card.transform.position = Vector2.MoveTowards(card.transform.position, targetPosition, delta);
    //             yield return null;
    //         }

    //         card.transform.position = targetPosition;
    //         yield return null;
    //         index++;
    //     }
    //     index = 0;
    // }

    IEnumerator ShowTop(GameObject topCard)
    {
        topCard.SetActive(true);
        yield return null;
    }
}

