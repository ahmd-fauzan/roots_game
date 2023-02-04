using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] float speed = 20;

    Rigidbody2D rb;

    public void Shoot(Vector3 direction)
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 dir = direction - transform.position;
        Vector3 rotation = transform.position - direction;
        rb.velocity = new Vector2(dir.x, dir.y).normalized * speed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);

    }
}
