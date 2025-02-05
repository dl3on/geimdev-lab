using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Reset reset;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        scoreText.text = "Score: " + score.ToString();
    }

    public void ResetButtonCallback(int input)
    {
        Debug.Log("to reset");
        reset.ResetButtonCallback(input);
        Debug.Log("reseted");
        gameObject.SetActive(false);
    }
}
