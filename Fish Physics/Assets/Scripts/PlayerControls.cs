using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private ScoreManager scoreManager;

    //Fields
    
    private float moveSpeed;
    private float jumpForce;
    private bool isJumping;
    private float moveHorizontal;
    private float moveVertical;
    private bool facingRight = true;



    // Start is called before the first frame update
    void Start()
    {
        scoreManager = GameObject.Find("Canvas").GetComponent<ScoreManager>();
        //gameObject references the game object it's attached to. We then grab the component in this case it is the Rigidbody2D component.
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        moveSpeed = 0.3f;
        jumpForce = 0.5f;
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        //From inputs we get two differen't numbers -1 for left, 0 for no key press and 1 Right.
        
        // We say that moveHorizontal will grab input from A and D or arrow keys Left or Right.
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        // We say that moveVertical will grab input from W and S or arrow keys Up or Down.
        moveVertical = Input.GetAxisRaw("Vertical");
        
        if (moveHorizontal < 0 && !facingRight)
        {
            Flip();
        }
        else if (moveHorizontal > 0 && facingRight)
        {
            Flip();
        }
    }

    // Because we are using physics we will use the fixed update function.
    void FixedUpdate()
    {
      // If Move Horizontal is moving Right (0.1) Or Left (-0.1). 
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            // Grab our player (rb2D) and add a force to them. Add a new X Y axis with Vector2, next we take the direction of X and times it by the move speed. The Y axis should reamin at 0f.
            // With ForceMode2D.Impulse we use this to add an instant force impulse to Rigidbody2D, using its mass.
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }

        //If the player is not jumping and the player is pressing W or Up. 
        if (!isJumping && moveVertical > 0.1f)
        {
            //Grab our player(rb2D) and add a force to them. Add a new X Y axis with Vector2, The X axis should reamin at 0f. next we take the direction of Y and times it by the move speed. 
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector2 currentScale = transform.localScale;
        currentScale.x *= -1;

        transform.localScale = currentScale;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
            scoreManager.gameOver();
        }
    }

}