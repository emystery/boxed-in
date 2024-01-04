using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryScene : MonoBehaviour
{
    GameStack<string> credits = new GameStack<string>();
    public string currentCredit;
    private TextMeshProUGUI currentCreditText;

    public float delayAfterVictoryScene = 2f;
    void Start()
    {
        //StartCoroutine(LoadSceneWithDelay());

        currentCreditText = GameObject.FindGameObjectWithTag("Display").GetComponent<TextMeshProUGUI>();

        credits.Push("Credit 6");
        credits.Push("Special Thanks: Credit 5");
        credits.Push("Sound Effects: Credit 4");
        credits.Push("Music: Credit 3");
        credits.Push("Background Art: Credit 2");
        credits.Push("World and Player Art: Credit 1");
    }

    private void Update()
    {
        if (!credits.IsEmpty() && Input.GetKeyDown(KeyCode.Space))
        {
            currentCreditText.text = credits.Peek().Data;
            credits.Pop();
        }
        if (credits.IsEmpty())
        {
            StartCoroutine(LoadSceneWithDelay());
        }
    }

    IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(delayAfterVictoryScene);

        SceneManager.LoadScene("Start Menu");
    }
}
