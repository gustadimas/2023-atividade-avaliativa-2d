using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Rigidbody2D rb;

    Animator anim;

    [SerializeField] Transform point;

    LayerMask layer;

    float force = 1.2f;

    bool explode = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        layer = LayerMask.GetMask("Player");
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(point.position, point.TransformDirection(Vector2.down), 10f, layer);

        if (hit)
        {
            float distance = Vector2.Distance(point.position, hit.point);
            Debug.DrawRay(point.position, point.TransformDirection(Vector2.down) * distance, UnityEngine.Color.green);
            Debug.Log(hit.transform.name);

            if (layer == LayerMask.GetMask("Player"))
            FallBombs();
        }

        BombAnim();
    }

    void FallBombs()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(new Vector2(0, -force), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            AudioManager.instance.Play("Explode");
            explode = true;
            Destroy(gameObject, 1f);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }

    void BombAnim()
    {
        anim.SetBool("explode", explode);
    }
}
