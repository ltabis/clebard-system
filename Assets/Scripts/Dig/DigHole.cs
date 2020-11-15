using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DigHole : MonoBehaviour
{
    public GameObject hole2;
    public bool sameScene;
    public string sceneName = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 Teleportation()
    {
        if (sameScene)
            return(TeleportationSameScene());
        else
            return(TeleportationOtherScene());
    }

    private Vector3 TeleportationSameScene()
    {
        return (hole2.transform.position);
    }

    private Vector3 TeleportationOtherScene()
    {
        SceneManager.LoadScene(sceneName);
        return (Vector3.zero);
;    }
}
