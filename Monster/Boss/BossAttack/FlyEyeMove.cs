using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeMove : MonoBehaviour
{
    public static Transform Target;
    float Speed = 13f;
    float timer = 2.5f;
    float ChaseTimer = 3f;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("Fly");
    }

    // Update is called once per frame
    void Update()
    {      
        TrackTarget();
    }

    void TrackTarget()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ChaseTimer -= Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        }
        if (ChaseTimer <= 0)
            anim.Play("Hit");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.Play("Hit");
            Target.GetComponent<PlayerControl>().hurtSet = true;
        }
    }

    void Disable()
    {
        
        gameObject.SetActive(false);
        timer = 2.5f;
        ChaseTimer = 3f;
    }
}
