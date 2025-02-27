using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;

    public void Setup(string finalScore, string highScore)
    {
        finalScoreText.text = finalScore;
        highScoreText.text = highScore;
    }

    public void ResetButtonCallback(int input)
    {
        GameManager.instance.gameRestart.Invoke();
    }
}
