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
    public AudioSource audioSource;
    public AudioClip decreaseClip;
    public AudioClip increaseClip;
    public AudioClip gameoverClip;
    public AudioClip createtargetClip;
    public Button restartButton;
    public int score;
    public bool isGameActive;
    private bool paused;
    private int lives;
    private float creationRate = 1.0f;
    public void Start()
    {
	audioSource = GetComponent<AudioSource>();
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
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
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
        if (scoreToAdd > 0)
        {
	    PlaySound(increaseClip);
	}
	else if (scoreToAdd < 0)
	{
	    PlaySound(decreaseClip);
	}
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
	PlaySound(gameoverClip);
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
	    PlaySound(createtargetClip);
        }
    }
}
