using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D coll;
    public float speed;
    public Transform attackTarget;
    public LayerMask targetLayer;
    float Filp;
    float timer;

    private void Start()
    {

    }

    private void Update()
    {
        if (attackTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, attackTarget.position, speed * Time.deltaTime * 1.5f);
            Hit();
        }

        if (timer < 3f && gameObject.activeSelf)
            timer += Time.deltaTime;
        else
        {
            attackTarget = null;
            gameObject.SetActive(false);
            timer = 0;
        }

     
    }

    public void SetSpeed(float speed)
    {
        rb.velocity = new Vector2(speed, 0);
    }

    void Hit()
    {
        if (Physics2D.OverlapCircle(transform.position, 1f,targetLayer))
        {
           if(attackTarget)
                attackTarget.GetComponent<FSM>().parameter.isHurt = true;
            gameObject.SetActive(false);
            attackTarget = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Monster"))
        {
            attackTarget = collision.transform;
        }
    }
}
