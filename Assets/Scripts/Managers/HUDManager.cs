using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public IntVariable gameScore;

    private Vector3[] scoreTextPosition = {
        new Vector3(-747, 473, 0),
        new Vector3(0, 0, 0)
        };
    private Vector3[] restartButtonPosition = {
        new Vector3(844, 455, 0),
        new Vector3(0, -150, 0)
    };

    public GameObject scoreText;
    public Transform restartButton;
    public GameObject gameOverPanel;

    void Awake()
    {
        GameManager.instance.gameStart.AddListener(GameStart);
        GameManager.instance.gameOver.AddListener(GameOver);
        GameManager.instance.gameRestart.AddListener(GameStart);
        GameManager.instance.scoreChange.AddListener(SetScore);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {
        // hide gameover panel
        gameOverPanel.SetActive(false);
        scoreText.transform.localPosition = scoreTextPosition[0];
        restartButton.localPosition = restartButtonPosition[0];
    }

    public void SetScore(int score)
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }

    public void GameOver()
    {
        gameOverPanel.GetComponent<GameOverScreen>().Setup(
            scoreText.GetComponent<TextMeshProUGUI>().text,
            "TOP- " + gameScore.previousHighestValue.ToString("D6")
            );

        // show
        gameOverPanel.SetActive(true);
    }
}
