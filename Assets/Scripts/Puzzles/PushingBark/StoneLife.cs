using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneLife : MonoBehaviour
{
    public GameObject planet;
    private bool counter = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(planet.transform.position, transform.position) > 20 && counter == false)
        {
            counter = true;
            FindObjectOfType<Spawner>().spawnNbr--;
        }
        if (Vector3.Distance(planet.transform.position, transform.position) > 200)
            Destroy(gameObject);
    }
}
