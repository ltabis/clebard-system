using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dig : MonoBehaviour
{
    public Animator anim;
    private GameObject[] holes;
    // Start is called before the first frame update
    void Start()
    {
        if (holes == null)
            holes = GameObject.FindGameObjectsWithTag("Hole");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            DogDig();
    }

    private void DogDig()
    {
        Vector3 dir;
        float angle;
        foreach (GameObject hole in holes)
        {
            dir = hole.transform.position - transform.position;
            angle = Vector3.Angle(transform.forward, dir);
            if (Vector3.Distance(transform.position, hole.transform.position) < 1 && Mathf.Abs(angle) < 90)
            {
                anim.Play("IdleDig");
                GetComponent<SC_TPSController>().Teleportation(hole.GetComponent<DigHole>().Teleportation());
            }
        }
    }
}
