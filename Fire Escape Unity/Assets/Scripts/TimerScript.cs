using UnityEngine;
using TMPro;
using System.Collections.Generic;
public class Timer : MonoBehaviour
{
    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Setting")]
    public float currentTime;
    public bool countDown;

    [Header("Timer Limiter")]
    public bool hasLimit;
    public float timerLimit;

    [Header("Timer Format")]
    public bool hasFormat;
    public TimerFormat format;
    private Dictionary<TimerFormat, string> timeFormats = new Dictionary<TimerFormat, string>();   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeFormats.Add(TimerFormat.Whole, "0");
        timeFormats.Add(TimerFormat.TenthDecimal, "0.0");
        timeFormats.Add(TimerFormat.HundrethsDecimal, "0.00");
        timeFormats.Add(TimerFormat.Minutes, "{0:00}:{1:00}", minutes, seconds);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime = countDown ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        if (hasLimit && ((countDown && currentTime <= timerLimit || (!countDown && currentTime >= timerLimit))))

        {
            currentTime = timerLimit;
            SetTimerText();
            timerText.color = Color.red;
            enabled = false;
        }

        SetTimerText();
    }

    private void SetTimerText()
    {
        timerText.text = hasFormat ? currentTime.ToString(timeFormats[format]) : currentTime.ToString();
    }
}

public enum TimerFormat
{
    Whole,
    TenthDecimal,
    HundrethsDecimal,
    Minutes,
}