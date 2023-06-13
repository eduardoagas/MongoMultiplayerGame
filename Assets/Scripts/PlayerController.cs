using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.5f;
    private Rigidbody2D rigidBody2D;
    public Animator animator;
    void Start()
    {
         rigidBody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
    }
    void FixedUpdate() {

        //force applied over the rigidbody - sliding on ice effect
        /*float h = 0.0f;
        float v = 0.0f;
        if (Input.GetKey("w")) { v = 1.0f; }
        if (Input.GetKey("s")) { v = -1.0f; }
        if (Input.GetKey("a")) { h = -1.0f; }
        if (Input.GetKey("d")) { h = 1.0f; }
        rigidBody2D.AddForce(new Vector2(h, v) * speed);*/

        /*
        float h = 0.0f;
        float v = 0.0f;
        if (Input.GetKey("w")) { v = 1.0f; }
        if (Input.GetKey("s")) { v = -1.0f; }
        if (Input.GetKey("a")) { h = -1.0f; }
        if (Input.GetKey("d")) { h = 1.0f; }
        rigidBody2D.MovePosition(rigidBody2D.position + (new Vector2(h, v) * speed * Time.fixedDeltaTime));
        */
        //alternate movement programming, equals above but also concede arrow keys usage
    
        
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        animator.SetFloat("h", h);
        animator.SetFloat("v", v);
        rigidBody2D.MovePosition(rigidBody2D.position + (new Vector2(h, v) * speed * Time.fixedDeltaTime));

    }
}
