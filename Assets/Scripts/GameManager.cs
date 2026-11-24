using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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

    private void Start()
    {
        Time.timeScale = 1f;

        gameOverScreen.SetActive(false);

        topPlayer.SetActive(playerOnTop);
        bottomPlayer.SetActive(!playerOnTop);

        InvokeRepeating(nameof(SpawnSomething), spawnRate, spawnRate);
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

    public void TogglePlayer()
    {
        playerOnTop = !playerOnTop;

        topPlayer.SetActive(playerOnTop);
        bottomPlayer.SetActive(!playerOnTop);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("MainScene");
    }
}
