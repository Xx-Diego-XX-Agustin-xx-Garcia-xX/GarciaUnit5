using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float creationRate = 1.0f;
    void Start()
    {
        StartCoroutine(CreateTarget());
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
