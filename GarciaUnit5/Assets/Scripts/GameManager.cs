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
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameoverText;
    public Button restartButton;
    public bool isGameActive;
    public int score;
    private float creationRate = 1.0f;
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        StartCoroutine(CreateTarget());
        score = 0;
	creationRate /= difficulty;
        UpdateScore(0);
	titleScreen.gameObject.SetActive(false);
    }
    public void UpdateScore(int scoreToAdd)
    {
	score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void GameOver()
    {
        gameoverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
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
