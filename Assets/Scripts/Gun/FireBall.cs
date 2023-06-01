using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] float velocity;

    private void Update()
    {
        transform.Translate(new Vector2(velocity * Time.deltaTime, 0));
        Destroy(gameObject, 4f);
    }
}
