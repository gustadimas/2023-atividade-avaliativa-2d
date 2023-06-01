using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D coll;
    Animator anim;

    float dirX;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;

    [SerializeField] LayerMask jumpableGround;

    Vector3 respawnPoint;

    [SerializeField] GameObject fallDetector;

    enum MovementState { idle, run, jump};

    bool die = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();

        respawnPoint = transform.position;
    }

    private void Update()
    {
        Animations();

        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            AudioManager.instance.Play("Jump");
        }

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("falldetector"))
        transform.position = respawnPoint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("NextScene"))
        SceneManager.LoadScene("ShootingRange");

    }

    private void Die()
    {
        die = true;
        rb.bodyType = RigidbodyType2D.Static;
        SceneManager.LoadScene("Traps");
    }

    void Animations()
    {
        MovementState state;

        if (dirX > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            state = MovementState.run;
        }

        else if (dirX < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            state = MovementState.run;
        }

        else
        state= MovementState.idle;


        if (rb.velocity.y > 0.1f)
        state= MovementState.jump;

        else if (rb.velocity.y < -0.1f)
        state= MovementState.jump;


        anim.SetInteger("state", (int)state);

        anim.SetBool("die", die);

        //animator.SetBool("fogo", Mathf.Abs(rb.velocity.x) > 0.1f);
    }


}
