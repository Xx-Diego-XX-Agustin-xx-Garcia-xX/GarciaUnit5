using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRB;
    private GameManager gameManager;
    private float a = 16;
    private float b = 12;
    private float c = 8;
    private float d = 4;
    public ParticleSystem explosionParticle;
    public int pointValue;
    void Start()
    {
        targetRB = GetComponent<Rigidbody>();
        targetRB.AddForce(RandomForce(), ForceMode.Impulse);
        targetRB.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
	gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        transform.position = RandomCreation();
    }
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(b, a);
    }
    float RandomTorque()
    {
        return Random.Range(-c, c);
    }
    Vector3 RandomCreation()
    {
        return new Vector3(Random.Range(-d, d), Random.Range(-c, c));
    }
    private void OnMouseDown()
    {
        Destroy(gameObject);
	Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
	gameManager.UpdateScore(pointValue);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
