using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCards : MonoBehaviour
{
    // [SerializeField] float rotateSpeed = 100f;
    [SerializeField] float rotationAngle = 180f;
    Quaternion initialRotation;
    Quaternion desiredRotation;
    public WaypointsSO waypointsSO;
    List<Transform> playerWaypoints;
    List<Transform> compyWaypoints;
    List<Transform> deckAndPileWaypoints;
    SoundHandler soundHandler;
    public int newGameIndicator = 0;
    
    [SerializeField] float moveSpeed = 20f;

    private void Awake() {
        playerWaypoints = waypointsSO.GetPlayerWaypoints();
        compyWaypoints = waypointsSO.GetCompyWaypoints();
        deckAndPileWaypoints = waypointsSO.GetDeckAndPileWaypoints();

        soundHandler = FindObjectOfType<SoundHandler>();

        // initialRotation = transform.rotation;
        // desiredRotation = initialRotation * Quaternion.AngleAxis(rotationAngle, Vector3.up);
    }

    public void DistributeCards(List<GameObject> cards, bool isPlayer)
    {
        StartCoroutine(DealCards(cards, isPlayer));
    }

    IEnumerator DealCards(List<GameObject> cards, bool isPlayer) {
        
        if (!isPlayer) 
        {
            yield return new WaitForSeconds(2);
        } 
        // else if (!isPlayer && newGameIndicator == 2)
        // {
        //     yield return new WaitForSeconds(.8f);
        // }
        
        
        int index = 0;
        foreach (GameObject card in cards)
        {
            card.SetActive(true);

            
            if (isPlayer)
            {
                initialRotation = Quaternion.Euler(0f, 180f, 0f);
                desiredRotation = initialRotation * Quaternion.AngleAxis(rotationAngle, Vector3.up);
            }

            // Set waypoints to player/compy
            Vector3 targetPosition = isPlayer ? 
                playerWaypoints[index].position : compyWaypoints[index].position;

            float distance = Vector2.Distance(card.transform.position, targetPosition);
            float rotationIncrement = rotationAngle / distance;

            

            while (Vector2.Distance(card.transform.position, targetPosition) > 0.01f)
            {
                // Move to position
                float delta = moveSpeed * Time.deltaTime;
                card.transform.position = Vector2.MoveTowards(card.transform.position, targetPosition, delta);

                // Rotate
                if (isPlayer && card.GetComponent<ObjectDetails>().showFront != true)
                {
                    float currentDistance = Vector2.Distance(card.transform.position, targetPosition);
                    float currentRotation = Mathf.Lerp(0f, rotationAngle, 1f - currentDistance / distance);
                    card.transform.rotation = initialRotation * Quaternion.AngleAxis(currentRotation, Vector3.up);

                    float dotProduct = Vector3.Dot(Camera.main.transform.forward, card.transform.forward);
                    bool isBackFaceVisible = (dotProduct < 0);

                    card.GetComponent<ObjectDetails>().cardFront.gameObject.SetActive(!isBackFaceVisible);
                    card.GetComponent<ObjectDetails>().cardBack.gameObject.SetActive(isBackFaceVisible);

                }

                yield return null;
            }

            card.transform.position = targetPosition;
            // yield return PlaySingleCard();
            
            card.GetComponent<ObjectDetails>().SetFrontBool();
            yield return null;
            index++;
            
        }
        index = 0;
        newGameIndicator++;
        if (isPlayer)
        {
            yield return MainGame.Instance.EnablePlayerHand(false);
        } else if (!isPlayer && newGameIndicator > 2) {
            yield return MainGame.Instance.EnablePlayerHand(true);
        }
        
        if (newGameIndicator == 2) yield return MainGame.Instance.CoinToss();
    }

    public void MoveCardToPile(GameObject card, bool isPlayer)
    {
        StartCoroutine(MoveToPileAnim(card, isPlayer));
    }

    IEnumerator MoveToPileAnim(GameObject card, bool isPlayer)
    {
        if (!isPlayer) 
        {
            // if (newGameIndicator == 2)
            // {
            //     yield return new WaitForSeconds(.4f);
            // } else 
            // {
                yield return new WaitForSeconds(1.6f);
            // }         
            initialRotation = Quaternion.Euler(0f, 180f, 0f);
            desiredRotation = initialRotation * Quaternion.AngleAxis(rotationAngle, Vector3.up);
        }
        
        Vector3 targetPosition = deckAndPileWaypoints[1].position;

        float distance = Vector2.Distance(card.transform.position, targetPosition);
        float rotationIncrement = rotationAngle / distance;

        while (Vector2.Distance(card.transform.position, targetPosition) > 0.01f)
        {
            float delta = moveSpeed * Time.deltaTime;
            card.transform.position = Vector2.MoveTowards(card.transform.position, targetPosition, delta);

            if (!isPlayer)
            {
                float currentDistance = Vector2.Distance(card.transform.position, targetPosition);
                float currentRotation = Mathf.Lerp(0f, rotationAngle, 1f - currentDistance / distance);
                card.transform.rotation = initialRotation * Quaternion.AngleAxis(currentRotation, Vector3.up);

                float dotProduct = Vector3.Dot(Camera.main.transform.forward, card.transform.forward);
                bool isBackFaceVisible = (dotProduct < 0);

                card.GetComponent<ObjectDetails>().cardFront.gameObject.SetActive(!isBackFaceVisible);
                card.GetComponent<ObjectDetails>().cardBack.gameObject.SetActive(isBackFaceVisible);
            }
            yield return null;
        }
        card.transform.position = targetPosition;
    }

    
    IEnumerator PlaySingleCard()
    {
        soundHandler.PlaySingleCardA();
        yield return null;
    }


    IEnumerator ShowTop(GameObject topCard)
    {
        topCard.SetActive(true);
        yield return null;
    }
}

