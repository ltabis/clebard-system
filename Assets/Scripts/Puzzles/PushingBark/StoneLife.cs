using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneLife : MonoBehaviour
{
    public GameObject planet;
    private bool counter = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Vector3.Distance(planet.transform.position, transform.position) > 20 && counter == false)
        {
            counter = true;
            if (FindObjectOfType<Spawner>().spawnNbr > 0)
                FindObjectOfType<Spawner>().spawnNbr--;
        }
        if (Vector3.Distance(planet.transform.position, transform.position) > 200)
            Destroy(gameObject);
    }
}
