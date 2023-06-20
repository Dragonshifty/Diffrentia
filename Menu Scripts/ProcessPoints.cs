using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProcessPoints  
{
    private BattleInfo battleInfo;
    private string clan;
    private int leaderCase = 0;
    private int trailCase = 0;
    private string messageStream = "";

    private bool noScores = false;
    private bool allTied = false;
    private bool threeLead = false;

    private Dictionary<string, int> clanPoints;
    private int[] pointsValues;
    private string[] clanNames;

    string leader;
    int leaderDifference;
    int secondToThird;
    int thirdToFourth;
    int loserDifference;
    string place = "";

    public string Process(BattleInfo battleInfo)
    {
        this.battleInfo = battleInfo;

        clan = battleInfo.PlayerClan;
        
        clanPoints = new Dictionary<string, int>
                {
                    {"Fox", battleInfo.FoxPoints},
                    {"Cat", battleInfo.CatPoints},
                    {"Dragon", battleInfo.DragonPoints},
                    {"Falcon", battleInfo.FalconPoints}
                };

        List<KeyValuePair<string, int>> sortedPointsList = clanPoints.OrderByDescending(pair => pair.Value).ToList();

        pointsValues = new int[]
        {
            sortedPointsList[0].Value,
            sortedPointsList[1].Value,
            sortedPointsList[2].Value,
            sortedPointsList[3].Value
        };

        clanNames = new string[]
        {
            sortedPointsList[0].Key,
            sortedPointsList[1].Key,
            sortedPointsList[2].Key,
            sortedPointsList[3].Key
        };

        leader = clanNames[0].Equals(clan) ? $"Your clan, {clan}," : clanNames[0];
        leaderDifference = pointsValues[0] - pointsValues[1];
        secondToThird = pointsValues[1] - pointsValues[2];
        thirdToFourth = pointsValues[2] - pointsValues[3];
        loserDifference = pointsValues[2] - pointsValues[3];

        if (clanNames[0].Equals(clan)) place = "First";
        if (clanNames[1].Equals(clan)) place = "Second";
        if (clanNames[2].Equals(clan)) place = "Third";
        if (clanNames[3].Equals(clan)) place = "Fourth";


        AssignCases();
        AssignMessageLeader();
        AssignMessageTrail();

        battleInfo = null;
        
        return messageStream;
    }

    private void AssignCases()
    {
        noScores = pointsValues.All(x => x == 0);

        if (!noScores)
        {
            bool allDifferent = pointsValues.Distinct().Count() == pointsValues.Length;

            if (!allDifferent)
            {
                // All tied
                allTied = pointsValues.All(x => x == pointsValues[0]);

                if (!allTied)
                {
                    // Tied for lead or clear leader
                    if (pointsValues[0] == pointsValues[1])
                    {
                        if (pointsValues[1] == pointsValues[2])
                        {
                            threeLead = true;
                            leaderCase = 2;
                        } else
                        {
                            // twoLead = true;
                            leaderCase = 3;
                        }
                    } else if (pointsValues[0] > pointsValues[1])
                    {
                        // oneLead = true;
                        leaderCase = 4;
                    }

                    // Runners up
                    if (!threeLead && pointsValues[1] == pointsValues[2])
                    {
                        if (pointsValues[2] == pointsValues[3])
                        {
                            // threeLose = true;
                            trailCase = 5;
                        } else
                        {
                            // middleTwo = true;
                            trailCase = 6;
                        }
                    } else if (pointsValues[2] == pointsValues[3])
                    {
                        // lastTwo = true;
                        trailCase = 7;
                    } 
                } else
                {
                    leaderCase = 1;
                }
            } else
            {
                leaderCase = 8;
            }
        } else
        {
            leaderCase = 0;
        }
    }

    private void AssignMessageLeader()
    {
        switch (leaderCase)
        {
            case 0:
                messageStream += "No-one has scored yet. What are you guys up to?!";
                break;
            case 1:
                messageStream += $"All clans are tied with {pointsValues[0]} points. The tension is palpable.";
                break;
            case 2:
                if (place.Equals("Fourth"))
                {
                    messageStream += $"Three clans share the lead with {pointsValues[0]}. Your clan, {clan}, are trailing a little bit with {pointsValues[3]} points.";
                } else
                {
                    messageStream += $"Your clan, {clan}, shares the lead with {pointsValues[0]} points, with only {clanNames[3]} on their own in last.";
                }
                
                break;
            case 3:
                if (place.Equals("First") || place.Equals("Second"))
                {
                    messageStream += $"Your clan, {clan}, is tied for the lead with {pointsValues[0]} points.";
                } else
                {
                    messageStream += $"{clanNames[0]} and {clanNames[1]} are both tied and leading by {secondToThird} points.";
                }
                break;
            case 4:
                if (place.Equals("First"))
                { 
                    messageStream += $"{leader} is in the lead with {pointsValues[0]} points.";
                } else if (place.Equals("Second"))
                {
                    messageStream += $"Your clan, {clan}, are currently runners-up and trailing {clanNames[0]} by {leaderDifference} points.";
                } else if (place.Equals("Third"))
                {
                    messageStream += $"Your clan, {clan}, are in third with {pointsValues[2]} points - {secondToThird} points behind clan {clanNames[1]}, who are behind {clanNames[0]}.";
                } else
                {
                    if (pointsValues[3] == 0)
                    {
                        messageStream += $"Your clan, {clan}, is yet to score. Still, early days (probably).";
                    } else
                    {
                        messageStream += $"Your clan, {clan}, is valiantly bringing up the rear with {pointsValues[3]}.";
                    }
                }
                break;
            case 8:
                string losing = pointsValues[3] == 0 ? $" {clanNames[3]} is yet to score." : $" {clanNames[3]} is trailing by {loserDifference} points.";
                
                messageStream += $"{leader} is leading by {leaderDifference} points. {clanNames[1]} has {pointsValues[1]} points and {clanNames[2]} is coming in third with {pointsValues[2]} points." + losing;
                break;
            default:
                messageStream += "I obviously missed something if you're reading this";
                break;
        }
    }

    void AssignMessageTrail()
    {
        switch (trailCase)
        {
            case 5:
                string threeTrailing = pointsValues[1] == 0 ? " All other clans are yet to score." 
                    : $" All other clans are tied and trailing with {pointsValues[1]} points. Are they ganging up? It seems like they're ganging up.";
                messageStream += threeTrailing;
                break;
            case 6:
                if (place.Equals("Second") || place.Equals("Third"))
                {
                    messageStream += $" Your clan, {clan}, is tied for second place with {pointsValues[1]} points. {clanNames[3]} are currently fourth.";
                } else
                {
                    messageStream += $" {clanNames[1]} and {clanNames[2]} both have {pointsValues[1]} points for second place. {clanNames[3]} are currently last.";
                }
                break;
            case 7:
                if (place.Equals("Third") || place.Equals("Fourth"))
                {
                    messageStream += $" {clanNames[1]} have {pointsValues[1]} points for second, with your clan, {clan}, sharing last and trailing by {secondToThird} points.";
                }
                break;
        }
    }
}
