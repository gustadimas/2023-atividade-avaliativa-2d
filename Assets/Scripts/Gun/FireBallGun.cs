using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallGun : MonoBehaviour
{
    [SerializeField] GameObject fireBall, gun;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(fireBall, new Vector2(gun.transform.position.x, gun.transform.position.y), gun.transform.rotation);
    }
}
