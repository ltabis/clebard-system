using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchScene : MonoBehaviour
{
    [SerializeField]
    private string sceneToLaunch;

    public void OnLaunchScene()
    {
        SceneManager.LoadScene(sceneToLaunch);
    }
}
