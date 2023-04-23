using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    private float creationRate = 1.0f;
    private int score;
    void Start()
    {
        StartCoroutine(CreateTarget());
        UpdateScore(0);
    }
    public void UpdateScore(int scoreToAdd)
    {
	score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    IEnumerator CreateTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(creationRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
}
