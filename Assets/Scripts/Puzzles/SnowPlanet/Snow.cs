using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow : MonoBehaviour
{
    private bool activated = false;
    private bool found = false;
    [SerializeField]
    private AudioSource foundAudioClip;
    [SerializeField]
    private ParticleSystem snowPS;
    [SerializeField]
    private GameObject snowFlake;

    void Start()
    {
        snowFlake.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!activated)
            return;
    
        if (other.gameObject.tag == "Player") {
            found = true;
            foundAudioClip.Play();
            Activated = false;
        }
    }

    public bool HasBeenFound()
    {
        return found;
    }

    public bool Activated {
        get {
            return activated;
        }

        set {
            if (activated == value)
                return;
    
            activated = value;
            if (value) {
                snowPS.Play();
                snowFlake.SetActive(true);
            } else {
                snowPS.Stop();
                snowFlake.SetActive(false);
            }
        }
    }
}
