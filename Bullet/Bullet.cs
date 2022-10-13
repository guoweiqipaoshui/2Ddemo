using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSpeed(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "monster")
            Destroy(gameObject);
    }
}
