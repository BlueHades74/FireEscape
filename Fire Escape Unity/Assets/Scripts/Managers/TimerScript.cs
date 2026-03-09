using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

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
    public string mainMenuScene = "MainMenu";

    public GameObject playerOne, playerTwo;

    private Dictionary<TimerFormat, string> timeFormats = new Dictionary<TimerFormat, string>();

    public GameObject gameOverUI;
    private bool isFailed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // find player objects
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (p.name == "Player 1") playerOne = p;
            else if (p.name == "Player 2") playerTwo = p;
        }

        // add timerformats
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

        if (countDown && currentTime <= 0 && !isFailed)
        {
            isFailed = true;
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
        playerOne.SetActive(false);
        playerTwo.SetActive(false);
        GameOver();
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

    /// <summary>
    /// Game Over Screen Functionality
    /// </summary>

    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToFirehouse()
    {
        SceneManager.LoadScene(levelSelectScene);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}

public enum TimerFormat
{
    Whole,
    TenthDecimal,
    HundrethsDecimal,
    Minutes,
}