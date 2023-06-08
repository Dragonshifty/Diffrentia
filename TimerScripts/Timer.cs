using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using System;

public class Timer : MonoBehaviour
{
    // public TextMeshProUGUI timer;
    public TextMeshProUGUI daysNum;
    public TextMeshProUGUI daysText;
    public TextMeshProUGUI hoursNum;
    public TextMeshProUGUI hoursText;
    public TextMeshProUGUI minutesNum;
    public TextMeshProUGUI minutesText;
    public TextMeshProUGUI secondsNum;
    // int countdownSeconds = 60;

    private void Awake() 
    {
            
    }
    private async void Start() 
    {
        DateTime currentDateTime = DateTime.Now;

        DateTime nextSunday = GetNextSunday(currentDateTime);
        DateTime targetDateTime = new DateTime(nextSunday.Year, nextSunday.Month, nextSunday.Day, 18, 0, 0);

        TimeSpan timeDifference = targetDateTime - currentDateTime;

        await StartCountDown(timeDifference);
    }

    async Task StartCountDown(TimeSpan timeDifference)
    {
        while (timeDifference.TotalSeconds > 0)
        {
            await Task.Delay(1000);

            timeDifference = timeDifference.Subtract(TimeSpan.FromSeconds(1));

            int days = (int)timeDifference.TotalDays;
            
            int totalHours = (int) timeDifference.TotalHours;

            int totalMinutes = (int)timeDifference.TotalMinutes;

            int totalSeconds = (int)timeDifference.TotalSeconds; 

            int hours = totalHours > 23 ? (totalHours % 24) : totalHours;

            int minutes = totalMinutes > 60 ? (totalMinutes % 60) : totalMinutes;

            int seconds = totalSeconds > 60 ? (totalSeconds % 60) : totalSeconds;

            // timer.text = $"Days: {days} \n Hours: {hours} \n Minutes: {minutes} \n Seconds {seconds}";
            daysNum.text = days.ToString();
            hoursNum.text = hours.ToString();
            minutesNum.text = minutes.ToString();
            secondsNum.text = seconds.ToString();
        }
    }

    // void UpdateTimer(int seconds)
    // {
    //     int minutes = seconds / 60;
    //     int remainingSeconds = seconds % 60;

    //     timer.text = $"{minutes}:{seconds}";
    // }

    DateTime GetNextSunday(DateTime currentDateTime)
    {
        int daysUntilSunday = ((int)DayOfWeek.Sunday - (int)currentDateTime.DayOfWeek + 7) % 7;
        DateTime nextSunday = currentDateTime.AddDays(daysUntilSunday);

        return nextSunday;
    }
}