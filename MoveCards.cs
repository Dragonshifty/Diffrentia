using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCards : MonoBehaviour
{
    [SerializeField] List<Transform> playerPositions;
    [SerializeField] float moveSpeed = 5f;


    public void DealCardOne(GameObject card)
    {
        StartCoroutine(MoveToPositionOne(card));
    }

    private void Update() {
        
    }

    IEnumerator MoveToPositionOne(GameObject card) {
        Vector3 startingPosition = card.transform.position;
        Vector3 targetPosition = playerPositions[0].transform.position;

        yield return new WaitForSeconds(2);

        while (Vector2.Distance(startingPosition, targetPosition) > 0.01f)
        {
            float delta = moveSpeed * Time.deltaTime;
            card.transform.position = Vector3.MoveTowards(card.transform.position, targetPosition, delta);
            yield return null;
        }
    }

    IEnumerator Wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
