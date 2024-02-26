using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLogicScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.Keys.CurrentHighestLevel, 1);
        PlayerPrefs.SetInt(PlayerPrefsKeys.Keys.DeathCount, 0);
        PlayerPrefs.SetFloat(PlayerPrefsKeys.Keys.TimerValue, 0f);
        GameState.State.CurrentLevel = 1;
    }

    public void PlayAgain()
    {       
        SceneManager.LoadScene($"Level_1");
    }
}
