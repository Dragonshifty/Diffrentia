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
                    // All tied and > 0
                }
            } else
            {
                leaderCase = 8;
                // All different scores
            }
        } else
        {
            leaderCase = 0;
            // no score
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
                    messageStream += $"Three clamber for the lead and all share {pointsValues[0]} points! You're amongst them - {clan} - so elbows out. Only {clanNames[3]} is on their own in last.";
                }
                break;
            case 3:
                if (place.Equals("First") || place.Equals("Second"))
                {
                    string tempTwoTiedClan = place.Equals("First") ? clanNames[1] : clanNames[0];
                    messageStream += $"Epically close-fought battle at the front as {tempTwoTiedClan} and you, Clan {clan}, share the lead with {pointsValues[0]} points.";
                } else
                {
                    messageStream += $"{clanNames[0]} and {clanNames[1]} are both tied and leading by {secondToThird} points. I think they're working together.";
                }
                break;
            case 4:
                if (place.Equals("First"))
                { 
                    messageStream += $"You're in the lead with {pointsValues[0]} points! Am I supposed to be unbiased? Go, Clan {clan}!";
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
                        messageStream += $"Your clan, {clan}, is yet to score. Still, early days.";
                    } else
                    {
                        messageStream += $"Your clan, {clan}, is valiantly bringing up the rear with {pointsValues[3]}.";
                    }
                }
                break;
            case 8:
                if (place.Equals("First"))
                {
                    messageStream += $"{GetLeadComments()} your own Clan {clan} leads by {leaderDifference} and with a total of {pointsValues[0]} points. {clanNames[1]} is second ({pointsValues[1]}), {clanNames[2]} third ({pointsValues[2]}), with {clanNames[3]} coming in last ({pointsValues[3]}).";
                }
                if (place.Equals("Second"))
                {
                    messageStream += $"You're chasing {clanNames[0]} for first by {leaderDifference} for Clan {clan} with {pointsValues[0]} and {pointsValues[1]} points, respectively. {clanNames[2]} is in third for {pointsValues[2]} points, with {clanNames[3]} last with {pointsValues[3]} points.";
                }
                if (place.Equals("Third"))
                {
                    messageStream += $"We have {clanNames[0]} in first with {pointsValues[0]} points and {clanNames[1]} with {pointsValues[1]}. You're representing Clan {clan} in third with {pointsValues[2]} points, and leading {clanNames[3]} by {loserDifference}.";
                }

                string losing = pointsValues[3] == 0 ? $"Unfortunately you're yet to score with Clan {clan}. Early days!" : $" Your clan, {clan} is trailing by {loserDifference} points. Let's push.";

                if (place.Equals("Fourth"))
                {
                    messageStream += $"{leader} is leading by {leaderDifference} points. {clanNames[1]} has {pointsValues[1]} points and {clanNames[2]} is coming in third with {pointsValues[2]} points." + losing;
                } 
                break;
            default:
                messageStream += "I obviously missed something if you're reading this.";
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


    string GetLeadComments()
    {
        int choice = Random.Range(0, 4);
        if (leaderDifference < 8)
        {
            switch (choice)
            {
                case 0:
                    return "By a whisker,";
                case 1:
                    return "By the narrowest of margains,";
                case 2:
                    return "Don't blink but";
                case 3:
                    return "I mean, only just, but";
            }
        }

        if (leaderDifference > 7 && leaderDifference < 20)
        {
            switch (choice)
            {
                case 0:
                    return "Edging into the lead,";
                case 1:
                    return "Starting to get comfortable,";
                case 2:
                    return "Not to get complacent but";
                case 3:
                    return "Looking hopeful,";
            }
        }

        if (leaderDifference > 19 && leaderDifference < 40)
        {
            switch (choice)
            {
                case 0:
                    return "Starting to get into their stride,";
                case 1:
                    return "A slightly more convinving lead,";
                case 2:
                    return "No resting on your laurels but,";
                case 3:
                    return "A confident lead,";
            }
        }

        if (leaderDifference > 39 && leaderDifference < 80)
        {
            switch (choice)
            {
                case 0:
                    return "A commanding lead,";
                case 1:
                    return "Is there time to relax as";
                case 2:
                    return "Pulling away from the pack as";
                case 3:
                    return "Leaping ahead,";
            }
        }

        if (leaderDifference > 79 && leaderDifference < 120)
        {
            switch (choice)
            {
                case 0:
                    return "Way out in the lead,";
                case 1:
                    return "Can anyone catch them,";
                case 2:
                    return "Off in the distance,";
                case 3:
                    return "Sit back and relax as";
            }
        }
        if (leaderDifference > 119)
        {
            switch (choice)
            {
                case 0:
                    return "Way, way out in the lead,";
                case 1:
                    return "Is this on easy mode? As";
                case 2:
                    return "Hoarding all the points,";
                case 3:
                    return "With a huge lead,";
            }
        }
        return "";
    }
}
