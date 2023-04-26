using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRB;
    private GameManager gameManager;
    private float a = 16;
    private float b = 8;
    private float c = 4;
    public ParticleSystem explosionParticle;
    public int pointValue;
    private void Start()
    {
        targetRB = GetComponent<Rigidbody>();
        targetRB.AddForce(RandomForce(), ForceMode.Impulse);
        targetRB.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
	    gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        transform.position = RandomCreation();
    }
    private void Update()
    {
	if (gameManager.score < 0)
	{
            gameManager.GameOver();
        }
    }
    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("BadTarget"))
        {
            gameManager.GameOver();
        }
    }
    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(b, a);
    }
    private Vector3 RandomCreation()
    {
        return new Vector3(Random.Range(-c, c), Random.Range(-c, c));
    }
    private float RandomTorque()
    {
        return Random.Range(-b, b);
    }
}
