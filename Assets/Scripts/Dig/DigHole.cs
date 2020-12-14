using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DigHole : MonoBehaviour
{
    public GameObject hole2;
    public bool sameScene;
    public string sceneName = "";

    private bool hasBeenDug = false;
    private bool playerInArea = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            playerInArea = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            playerInArea = false;
    }

    public Vector3 Teleportation()
    {
        if (sameScene)
            return(TeleportationSameScene());
        else
            return(TeleportationOtherScene());
    }

    public Vector3 TeleportationSameScene()
    {
        hasBeenDug = true;
        return (hole2.transform.position);
    }

    private Vector3 TeleportationOtherScene()
    {
        SceneManager.LoadScene(sceneName);
        return (Vector3.zero);
    }

    public bool HasBeenDug {
        get {
            return hasBeenDug;
        }
    }
    
    public bool PlayerInArea {
        get {
            return playerInArea;
        }
    }
}
