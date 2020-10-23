using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour {
    public enum StartCondition { InShip, OnBody }

    public AstronomicalObject startBody;

    void Start() {
        PlayerController player = FindObjectOfType<PlayerController>();
        player.SetVelocity(startBody.initialVelocity);
    }
}