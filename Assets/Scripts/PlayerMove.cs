using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    private Rigidbody rb;
    public float speed = 7;
    public float jumpHeight = 1.2f;
    private Vector3 moveDirection;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        float xMove = Input.GetAxis("Horizontal") * speed *
            Time.deltaTime;
        float zMove = Input.GetAxis("Vertical") * speed *
            Time.deltaTime;
        moveDirection = new Vector3(xMove, 0, zMove);
        if (Input.GetKey(KeyCode.Space)) 
        {
            rb.AddForce(transform.up * 40000 * jumpHeight * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveDirection * speed * Time.deltaTime));
        
    }
}