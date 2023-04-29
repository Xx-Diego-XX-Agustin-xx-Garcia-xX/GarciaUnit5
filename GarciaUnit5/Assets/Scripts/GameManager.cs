using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameoverText;
    public TextMeshProUGUI livesText;
    public Button restartButton;
    public AudioSource gameoverSound;
    public AudioClip gameoverClip;
    public int score;
    public bool isGameActive;
    private bool paused;
    private int lives;
    private float creationRate = 1.0f;
    public void Start()
    {
	gameoverSound = GetComponent<AudioSource>();
    }
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        StartCoroutine(CreateTarget());
        score = 0;
	creationRate /= difficulty;
        UpdateScore(0);
        UpdateLives(5);
        titleScreen.gameObject.SetActive(false);
    }
    public void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangePaused();
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
	score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void UpdateLives(int livesChanged)
    {
        lives += livesChanged;
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        gameoverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
	gameoverSound.PlayOneShot(gameoverClip, 1.0f);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private IEnumerator CreateTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(creationRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
}
