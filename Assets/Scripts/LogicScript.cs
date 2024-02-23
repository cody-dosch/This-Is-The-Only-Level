using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public TextMeshProUGUI deathCountText;
    public TextMeshProUGUI timerText;
    public RectTransform levelCompleteBanner;

    public Action PauseGame;
    public Action UnpauseGame;
    public bool isGamePaused;

    private PlayerScript characterScript;
    private ExitPipeScript exitPipeScript;

    //private 
    [SerializeField] int deathCount = 0;
    [SerializeField] int currentLevel = 1;
    [SerializeField] float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        characterScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        characterScript.OnDeath += IncrementDeathCount;

        exitPipeScript = GameObject.FindGameObjectWithTag("ExitPipe").GetComponent<ExitPipeScript>();
        exitPipeScript.OnLevelComplete += CompleteLevel;

        currentTime = PlayerPrefs.GetFloat(PlayerPrefsKeys.Keys.TimerValue);
        deathCount = PlayerPrefs.GetInt(PlayerPrefsKeys.Keys.DeathCount);
        deathCountText.text = deathCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGamePaused)
        {
            currentTime += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            timerText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds.ToString();
        }
    }

    private void IncrementDeathCount()
    {
        deathCount++;
        deathCountText.text = deathCount.ToString();
    }

    private void CompleteLevel()
    {
        levelCompleteBanner.gameObject.SetActive(true);

        PlayerPrefs.SetFloat(PlayerPrefsKeys.Keys.TimerValue, currentTime);
        PlayerPrefs.SetInt(PlayerPrefsKeys.Keys.DeathCount, deathCount);

        currentLevel++;
        PlayerPrefs.SetInt(PlayerPrefsKeys.Keys.CurrentHighestLevel, currentLevel);
    }

    public void PauseUnpauseGame()
    {
        if (isGamePaused)
        {
            isGamePaused = false;
            UnpauseGame?.Invoke();
        }
        else
        {
            isGamePaused = true;
            PauseGame?.Invoke();
        }
    }
}
