using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        // IEnumerator MoveCard(Transform cardTransform, Vector2 targetPosition, float speed, Transform starting)
    // {
    //     while (Vector2.Distance(cardTransform.position, targetPosition) > 0.01f)
    //     {
    //         cardTransform.position = Vector2.MoveTowards(cardTransform.position, targetPosition, speed * Time.deltaTime);
    //         yield return null;
    //     }
    // }

    // IEnumerator MoveCardsy()
    // {
    //     Vector2 targetPosition = startingPosition.position;
    //     for (int i = 0; i < deck.Count; i++)
    //     {
    //         GameObject card = deck[i];
    //         card.SetActive(true);

    //         ObjectDetails cardDetails = card.GetComponent<ObjectDetails>();
    //         cardDetails.textMesh.text = cardDetails.CardValue.ToString();

    //         StartCoroutine(MoveCard(card.transform, targetPosition, moveSpeed));
    //         yield return new WaitForSeconds(0.1f); // Delay between moving cards

    //         targetPosition += Vector2.right * 2f; // Move target position to the right
    //     }
    // }

    // Update is called once per frame

    //     GameObject card = Instantiate(prefab, waypointStart, Quaternion.identity);
//                 ObjectDetails cardDetails = card.GetComponent<ObjectDetails>();
//                 cardDetails.CardValue = i;
//                 cardDetails.House = house;
//                 // cardDetails.cardValueText = card.GetComponentInChildren<TextMeshProUGUI>();
//                 // cardDetails.houseText = card.GetComponentInChildren<TextMeshProUGUI>();
//                 // cardDetails.cardValueText = card.transform.Find("Card Value").GetComponent<TextMeshProUGUI>();
//                 // cardDetails.houseText = card.transform.Find("House").GetComponent<TextMeshProUGUI>();
//                 cardDetails.houseText.text = house;

// //                 Transform cardValueTransform = card.transform.Find("Card Value");
// // Transform houseTransform = card.transform.Find("House");
// // Debug.Log("Card Value: " + cardValueTransform);
// // Debug.Log("House: " + houseTransform);
}
