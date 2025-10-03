using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Playercontrol : MonoBehaviour
{
    Rigidbody2D rb;

    //public AudioSource audioJump;
    //public AudioSource audioDie;

    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    [SerializeField] bool grounded;
    [SerializeField] float castDist;
    [SerializeField] Vector2 boxcastSize;

    bool idling;
    bool jumping;
    bool running;

    //[SerializeField] bool isEnemyCollided;

    Animator animator;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();

        Move();

        JumpInput();

        ApplyAnimations();



        //Respawn();



    }


    /*public void toggleIsCollided()
    {
        isEnemyCollided = !isEnemyCollided;
    }*/

    private void CheckGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxcastSize, 0f, Vector2.down, castDist, ~3))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), rb.velocity.y);
    }

    private void JumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump(jumpForce);
            //audioJump.Play();
            //hasJumped = true;
        }
    }

    /*private void Respawn()
    {
        if ((transform.position.y < -7f) || isEnemyCollided)
        {
            Vector3 newPos = transform.position;
            newPos.y = 2f;
            newPos.x = -10.03f;
            transform.position = newPos;
            rb.velocity = rb.velocity * 0;


            isEnemyCollided = false;
            audioDie.Play();
        }

    }*/

    private void Jump(float force)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);

        rb.AddForce(new Vector2(0, jumpForce));


    }

    private void ApplyAnimations()
    {
        idling = false;
        jumping = false;
        running = false;

        if (!grounded)
        {
            jumping = true;
        }
        else if (rb.velocity.x != 0)
        {
            running = true;
            if (rb.velocity.x > 0)
            {
                sr.flipX = false;
            }
            else
            {
                sr.flipX = true;
            }
        }
        else
        {
            idling = true;
        }

        animator.SetBool("Jumping", jumping);
        animator.SetBool("Idling", idling);
        animator.SetBool("Running", running);
    }

    public void RespawnAuto()
    {
        Vector3 newPos = transform.position;
        newPos.y = 2f;
        newPos.x = -10.03f;
        transform.position = newPos;
        //audioDie.Play();
        rb.velocity = rb.velocity * 0;
    }


}