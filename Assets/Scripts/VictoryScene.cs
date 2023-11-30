using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScene : MonoBehaviour
{
    public float delayAfterVictoryScene = 1f;
    void Start()
    {
        StartCoroutine(LoadSceneWithDelay());
    }

    IEnumerator LoadSceneWithDelay()
    {
        yield return new WaitForSeconds(delayAfterVictoryScene);

        SceneManager.LoadScene("Start Menu");
    }
}
