using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public GameManager gameManager;
    public void Setup(string text)
    {
        finalScoreText.text = text;
    }

    public void ResetButtonCallback(int input)
    {
        gameManager.gameRestart.Invoke();
    }
}
