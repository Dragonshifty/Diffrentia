using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInfo 
{
    private int foxPoints;
    private int catPoints;
    private int dragonPoints;
    private int falconPoints;
    private int playerPoints;
    private int leaderPoints;
    private string playerClan;
    private string currentLeader;

    public BattleInfo (int foxPoints, int catPoints, int dragonPoints, int falconPoints, int playerPoints,
                    int leaderPoints, string playerClan, string currentLeader)
    {
        this.foxPoints = foxPoints;
        this.catPoints = catPoints;
        this.dragonPoints = dragonPoints;
        this.falconPoints = falconPoints;
        this.playerPoints = playerPoints;
        this.leaderPoints = leaderPoints;
        this.playerClan = playerClan;
        this.currentLeader= currentLeader;
    }

    public int FoxPoints { get { return foxPoints; }}
    public int CatPoints { get { return catPoints; }}
    public int DragonPoints { get { return dragonPoints; }}
    public int FalconPoints { get { return falconPoints; }}
    public int PlayerPoints { get { return playerPoints; }}
    public int LeaderPoints { get { return leaderPoints; }}
    public string PlayerClan { get { return playerClan; }}
    public string CurrentLeader { get { return currentLeader; }}
}
