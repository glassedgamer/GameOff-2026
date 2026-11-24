using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int gameScore = 0;
    public int highscore = 0;

    public float objSpeedMult = 1;

    [Header("Player Stuff")]
    [SerializeField] GameObject topPlayer;
    [SerializeField] GameObject bottomPlayer;
    public bool playerOnTop;

    [Header("Object Stuff")]
    [SerializeField] GameObject[] objArray;
    [SerializeField] Transform[] spawnArray;
    [SerializeField] float spawnRate = 1f;

    [Header("UI Stuff")]
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] Text highscoreText;
    [SerializeField] Text scoreText;

    private void Start()
    {
        Time.timeScale = 1f;

        highscore = PlayerPrefs.GetInt("Highscore", 0);

        gameOverScreen.SetActive(false);

        topPlayer.SetActive(playerOnTop);
        bottomPlayer.SetActive(!playerOnTop);

        scoreText.text = gameScore.ToString();

        InvokeRepeating(nameof(SpawnSomething), spawnRate, spawnRate);
        InvokeRepeating(nameof(AddPoints), spawnRate/(2*spawnRate), spawnRate / (2 * spawnRate));
        InvokeRepeating(nameof(IncreaseObjSpeed), 3*spawnRate, 3*spawnRate);
    }

    void Update()
    {
        
    }
    void SpawnSomething()
    {
        int pickSpawner = Random.Range(0, spawnArray.Length);
        int pickObj = Random.Range(0, objArray.Length);

        Instantiate(objArray[pickObj], spawnArray[pickSpawner].position, Quaternion.identity);
    }

    void AddPoints()
    {
        gameScore++;
        scoreText.text = gameScore.ToString();

        if (gameScore > highscore)
        {
            highscore = gameScore;
            PlayerPrefs.SetInt("HighScore", gameScore);
            PlayerPrefs.Save(); // Writes the score to disk immediately
        }
    }

    void IncreaseObjSpeed()
    {
        objSpeedMult += 0.1f;
    }

    public void TogglePlayer()
    {
        playerOnTop = !playerOnTop;

        topPlayer.SetActive(playerOnTop);
        bottomPlayer.SetActive(!playerOnTop);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        highscoreText.text = $"Highscore: {highscore}";
        Time.timeScale = 0f;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("MainScene");
    }
}
