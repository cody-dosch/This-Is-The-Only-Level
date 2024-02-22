using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public TextMeshProUGUI deathCountText;
    public RectTransform levelCompleteBanner;

    private PlayerScript characterScript;
    private ExitPipeScript exitPipeScript;

    //private 
    [SerializeField] int deathCount = 0;
    [SerializeField] int currentLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
        characterScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        characterScript.OnDeath += IncrementDeathCount;

        exitPipeScript = GameObject.FindGameObjectWithTag("ExitPipe").GetComponent<ExitPipeScript>();
        exitPipeScript.OnLevelComplete += CompleteLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void IncrementDeathCount()
    {
        deathCount++;
        deathCountText.text = deathCount.ToString();
    }

    private void CompleteLevel()
    {
        levelCompleteBanner.gameObject.SetActive(true);
    }
}
