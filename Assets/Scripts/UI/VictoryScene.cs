using System.Collections;
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
        currentCreditText = GameObject.FindGameObjectWithTag("Display").GetComponent<TextMeshProUGUI>();

        credits.Push("For IADE Games Development Course");
        credits.Push("Code: Alfredo Burnay & Afonso Martinho");
        credits.Push("Special thanks: Célio Nódices");
        credits.Push("Music: Opengameart.org");
        credits.Push("Enemy and Hazard art: Pixel Adventure");
        credits.Push("Background, Player, Tileset Art: Jesse Munguia");
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
