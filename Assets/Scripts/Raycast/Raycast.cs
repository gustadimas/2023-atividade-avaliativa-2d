using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    [SerializeField] Transform point;
    LayerMask layer;
    RaycastHit2D hit;
    BoxCollider2D coll;
    [SerializeField] bool dirLeft;

    private void Start()
    {
        layer = LayerMask.GetMask("Player");
        coll = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (dirLeft)
            hit = Physics2D.Raycast(point.position, point.TransformDirection(Vector2.left), 3f, layer);
        else
            hit = Physics2D.Raycast(point.position, point.TransformDirection(Vector2.right), 3f, layer);

        if (hit)
        {
            //float distance = Vector2.Distance(point.position, hit.point);
            GetComponent<SpriteRenderer>().enabled = true;

            //Debug.Log(hit.transform.name);

            if (dirLeft)
                Debug.DrawRay(point.position, point.TransformDirection(Vector2.left) * 3f, Color.green);
            else
                Debug.DrawRay(point.position, point.TransformDirection(Vector2.right) * 3f, Color.green);
            if (layer == LayerMask.GetMask("Player"))
                coll.enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
            coll.enabled = false;
        }
    }
}
