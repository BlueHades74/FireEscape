using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor.Rendering;

//author : Alex
public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Setting")]
    public float currentTime;
    public bool countDown = false;

    [Header("Timer Limiter")]
    public bool hasLimit = false;
    public float timerLimit = 0f;

    [Header("Timer Format")]
    public bool hasFormat = true;
    public TimerFormat format = TimerFormat.Minutes;
    

    [Header("Load Scene if fail")]
    public string levelSelectScene = "Firehouse";

    private Dictionary<TimerFormat, string> timeFormats = new Dictionary<TimerFormat, string>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeFormats.Add(TimerFormat.Whole, "0");
        timeFormats.Add(TimerFormat.TenthDecimal, "0.0");
        timeFormats.Add(TimerFormat.HundrethsDecimal, "0.00");
        timeFormats.Add(TimerFormat.Minutes, "{0:00}:{1:00}");
        
    }

    // Update is called once per frame
    void Update()
    {
        //updates time
        currentTime = countDown ? currentTime - Time.deltaTime : currentTime + Time.deltaTime;

        if (countDown && currentTime <= 0)
        {
            currentTime = 0;
            timerText.color = Color.red;
            TimerFailed();
        }
        // Stops at 0 and stops from going negative
        if (hasLimit && !countDown && currentTime >= timerLimit)
        {
            currentTime = timerLimit;
            timerText.color = Color.red;
            enabled = false;
        }

        SetTimerText();
    }

    private void TimerFailed()
    {
        Debug.Log("timer failed");
        enabled = false; //stops timer from updating
        SceneManager.LoadScene(levelSelectScene);
    }

    private void SetTimerText()
    {
        if (hasFormat && format == TimerFormat.Minutes)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = string.Format(timeFormats[format], minutes, seconds);
        }
        else if (hasFormat)
        {
            timerText.text = currentTime.ToString(timeFormats[format]);
        }
        else
        {
            timerText.text = currentTime.ToString();
        }
    }
}

public enum TimerFormat
{
    Whole,
    TenthDecimal,
    HundrethsDecimal,
    Minutes,
}