using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartLogicScript : MonoBehaviour
{
    int currentHighestLevel = 1;

    public Button StartNewButton;
    public Button ContinueButton;

    // Start is called before the first frame update
    void Start()
    {
        currentHighestLevel = PlayerPrefs.GetInt(PlayerPrefsKeys.Keys.CurrentHighestLevel);

        // If the player hasn't beaten level 1, only show the Start New button
        if (currentHighestLevel <= 1)
        {
            ContinueButton.gameObject.SetActive(false);
            StartNewButton.transform.position = new Vector3(StartNewButton.transform.position.x + 150, StartNewButton.transform.position.y, StartNewButton.transform.position.z);
        }
    }

    public void Continue()
    {
        var nextLevelIndex = SceneUtility.GetBuildIndexByScenePath($"Level_{currentHighestLevel}");

        if (nextLevelIndex > 0)
        {
            GameState.State.CurrentLevel = currentHighestLevel;
            SceneManager.LoadScene($"Level_{currentHighestLevel}");
        }
        else
        {
            SceneManager.LoadScene($"End_Scene");
        }
    }

    public void StartNew()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.Keys.CurrentHighestLevel, 1);
        PlayerPrefs.SetInt(PlayerPrefsKeys.Keys.DeathCount, 0);
        PlayerPrefs.SetFloat(PlayerPrefsKeys.Keys.TimerValue, 0f);
        GameState.State.CurrentLevel = 1;
        SceneManager.LoadScene($"Level_1");
    }
}
