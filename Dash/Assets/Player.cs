using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform cam;
    public LayerMask layer;

    public float speed;
    public float jumpForce;

    private float moveX;
    private bool facingRight;
    public bool isGround;
    public float dashSpeed;
    private int direction;
    public float startDash;
    private float timeDash;
    void Start()
    {
        
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(
            moveX * speed,
            rb.velocity.y
        );

        if(moveX < 0 && !facingRight) {
            flip();
        }
        else if(moveX > 0 && facingRight) {
            flip();
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, layer);
        if(hit.collider != null) {
            isGround = true;
        }
        else {
            isGround = false;
        }

        jump();
        death();
        dash();
    }

    private void jump() {
        if(Input.GetKeyDown(KeyCode.Space) && isGround == true) {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    private void death() {
        if(transform.position.y <= cam.position.y - 6f) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void flip() {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
    private void dash() {
        if(Input.GetKeyDown(KeyCode.LeftShift)) {
            if( moveX > 0) {
                direction = 1;
            }
            else if(moveX < 0 ) {
                direction = 2;
            }
        }
        else {
            if(timeDash <= 0) {
                direction = 0;
                timeDash = startDash;
            }
            else {
                timeDash -= Time.deltaTime;
                if(direction == 1) {
                    rb.velocity = Vector2.right * dashSpeed;
                }
                else if(direction == 2) {
                    rb.velocity = Vector2.left * dashSpeed;
                } 
            }
        }
    }
}
