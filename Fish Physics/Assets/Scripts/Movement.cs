using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb2D;

    ParticleSystem gun;
    Animator anim;
    private float moveSpeed;
    private float jumpForce;
    private bool isJumping;
    private float x;
    private float y;



    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        gun = GetComponentInChildren<ParticleSystem>();
        moveSpeed = 3f;
        jumpForce = 10f;
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
 
         x = Input.GetAxisRaw("Horizontal");
         y = Input.GetAxisRaw("Vertical");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gun.Play();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            gun.Stop();
        }
    }


    void FixedUpdate()
    {
        if (x > 0.1f || x < -0.1f)
        {
            rb2D.AddForce(new Vector2(x * moveSpeed, 0f), ForceMode2D.Impulse);
        }

        if (!isJumping && y > 0.1f)
        {
            rb2D.AddForce(new Vector2(0f, y * jumpForce), ForceMode2D.Impulse);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
        }
    }

}
