using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObject;
    public float spawnTime = 0;
    public Vector3 minVec;
    public Vector3 maxVec;
    public float spawnNbr = 0;
    public Text stoneText;
    public bool launch = false;
    public float spawnMax = 50;

    private float spawnedNbr = 0;
    private float timer;
    void Start()
    {
        timer = 0f;
        if (spawnTime == 0)
            spawnTime = 4;
        stoneText.text = spawnNbr + " stone on the planet.";
    }

    void Update()
    {
        stoneText.text = spawnNbr + " stone on the planet.";
        if (spawnedNbr >= spawnMax)
        {
            if (spawnedNbr <= 0)
                endGame();
            return;
        }
        if (!launch)
            return;
        timer += Time.deltaTime;
        if (timer >= spawnTime)
        {
            spawn();
            timer = 0;
        }

    }

    public void spawn()
    {
        float randX = Random.Range(minVec.x, maxVec.x);
        float randY = Random.Range(minVec.y, maxVec.y);
        float randZ = Random.Range(minVec.z, maxVec.z);

        Instantiate(spawnObject, new Vector3(randX, randY, randZ), transform.rotation);
        spawnNbr++;
        spawnedNbr++;
        if (spawnTime > 0.3f)
            spawnTime -= 0.03f;
    }
    public void endGame()
    {
        print("GG WP C FINI");
    }
}
