using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaypointSO", menuName = "New WaypointSO")]
public class WaypointsSO : ScriptableObject
{
    [SerializeField] Transform playerWaypointPrefab;
    [SerializeField] Transform compyWaypointPrefeb;
    [SerializeField] Transform deckAndPileWaypointPrefab;

    public List<Transform> GetPlayerWaypoints()
    {
        List<Transform> playerPositions = new List<Transform>();
        foreach (Transform waypoint in playerWaypointPrefab)
        {
            playerPositions.Add(waypoint);
        }
        return playerPositions;
    }

    public List<Transform> GetCompyWaypoints()
    {
        List<Transform> compyPositions = new List<Transform>();
        foreach (Transform waypoint in compyWaypointPrefeb)
        {
            compyPositions.Add(waypoint);
        }
        return compyPositions;
    }

    public List<Transform> GetDeckAndPileWaypoints()
    {
        List<Transform> deckAndPilePositions = new List<Transform>();
        foreach (Transform waypoint in deckAndPileWaypointPrefab)
        {
            deckAndPilePositions.Add(waypoint);
        }
        return deckAndPilePositions;
    }

    public Transform GetDeckWayPoint()
    {
        return deckAndPileWaypointPrefab.GetChild(0);
    }
}
