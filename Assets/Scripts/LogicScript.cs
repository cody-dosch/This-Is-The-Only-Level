using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField] int deathCount = 0;
    [SerializeField] float currentTime;
    [SerializeField] float nextLevelWait = 2f;
    [SerializeField] bool isLevelComplete = false;

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

    private void IncrementDeathCount()
    {
        deathCount++;
        deathCountText.text = deathCount.ToString();
    }

    private void CompleteLevel()
    {
        // Only run the level complete code once
        if (!isLevelComplete)
        {
            StartCoroutine(NextLevelRoutine());
        }       
    }

    IEnumerator NextLevelRoutine()
    {
        // When level is complete, show the banner and pause the game for set amount of seconds before loading the next level
        isLevelComplete = true;
        //isGamePaused = true;

        levelCompleteBanner.gameObject.SetActive(true);
        yield return new WaitForSeconds(nextLevelWait);
        levelCompleteBanner.gameObject.SetActive(false);

        PlayerPrefs.SetFloat(PlayerPrefsKeys.Keys.TimerValue, currentTime);
        PlayerPrefs.SetInt(PlayerPrefsKeys.Keys.DeathCount, deathCount);

        GameState.State.CurrentLevel++;
        PlayerPrefs.SetInt(PlayerPrefsKeys.Keys.CurrentHighestLevel, GameState.State.CurrentLevel);

        var nextLevelIndex = SceneUtility.GetBuildIndexByScenePath($"Level_{GameState.State.CurrentLevel}");

        if (nextLevelIndex > 0)
        {
            Debug.Log($"Loading Level {GameState.State.CurrentLevel}");
            SceneManager.LoadScene($"Level_{GameState.State.CurrentLevel}");
        }
        else
        {
            SceneManager.LoadScene($"End_Scene");
        }
    }
}
