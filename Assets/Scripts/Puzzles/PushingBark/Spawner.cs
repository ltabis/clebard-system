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
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        if (spawnTime == 0)
            spawnTime = 4;
        stoneText.text = spawnNbr + " stone on the planet.";
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedNbr >= spawnMax)
        {
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
        stoneText.text = spawnNbr + " stone on the planet.";

    }

    public void spawn()
    {
        float randX = Random.Range(minVec.x, maxVec.x);
        float randY = Random.Range(minVec.y, maxVec.y);
        float randZ = Random.Range(minVec.z, maxVec.z);

        Instantiate(spawnObject, new Vector3(randX, randY, randZ), transform.rotation);
        spawnNbr++;
        spawnedNbr++;
    }
    public void endGame()
    {
        print("GG WP C FINI");
    }
}
