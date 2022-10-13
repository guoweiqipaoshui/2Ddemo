using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    Rigidbody2D rb;
    Collider2D coll;
    Animator anim;

    //public Transform[] attackPoint; //基于射线检测的攻击
    // public float attackArea;
    public int currentHealth = 40;
    public int maxHealth = 40;

    bool isOpenBag = false;

    public Collider2D[] attack1Coll;


    public bool hurtSet = false;
    private float hurtTimer = 0;
    public bool isGameOver;

    public bool attackSet;
    private int attackCombo = 0; 
    private float attackTimer = 0;

    public Transform firePoint;
    public GameObject bulletPrefab;
    float bulletSpeed = 15f;

    public LayerMask monster;

    public float SlideTime;
    public float SlideSpeed;
    bool isSlide;
    public float Speed;
    Vector2 Move;

    // Start is called before the first frame update
    void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<PolygonCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
     void Update()
     {
        PlayerUI.currentHealth = currentHealth ; 
        PlayerUI.maxHealth = maxHealth;
        PlayerUI.isOpenBag = isOpenBag;

        if (Input.GetButtonDown("Bag"))
            Bag();

        Slide();
        Death();
        Attack();   
        Hurt();          
        RangeAttack();
     }
    private void FixedUpdate()
    {
          Movement();    
    }

    void Movement()
    {

        Move.x = Input.GetAxis("Horizontal");
        Move.y = Input.GetAxis("Vertical");
        if (!attackSet)
        {
            rb.MovePosition(rb.position + Move * Speed * Time.deltaTime);
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                anim.SetBool("Walk", true);
                if (Move.x >= 0)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }
            else
            {
                anim.SetBool("Walk", false);
            }
        }
        else
            rb.MovePosition(rb.position + Move * 5f * Time.deltaTime);
    }

    void Attack()
    {
        if(Input.GetButtonDown("Attack1") && !attackSet)
        {
            anim.SetTrigger("Attack1");
            attackSet = true;
            attack1Coll[0].enabled = true;
            attackCombo++;
            attackTimer = 1f;
           if(attackCombo > 1)
            {
                attackCombo = 0;
            }
            anim.SetInteger("ComboStep", attackCombo);
        }

        if(attackTimer != 0)
        {
            attackTimer -= Time.deltaTime;
            if(attackTimer <= 0)
            {
                AttackOver();
                attackTimer = 0;
                attackCombo = 0;
            }
        }
        
    }
    void AttackOver()
    {
        attack1Coll[0].enabled = false;
        attackSet = false;
    }

    void RangeAttack()
    {
        if (Input.GetButtonDown("Fire"))
        {
            PoolManager.Release(bulletPrefab,firePoint.position,transform.localScale,bulletSpeed * transform.localScale.x);
        }
    }

    void Slide()
    {
        if(Input.GetButtonDown("Slide"))
        {
            isSlide = true;
        }
        if (isSlide)
        {
            anim.SetBool("Slide", true);
            this.transform.Translate(transform.right * Time.deltaTime * SlideSpeed * transform.localScale.x);
        }
    }
    void SlideOver()
    {
        anim.SetBool("Slide", false);
        isSlide = false;
    }

    void Hurt()
    {
        if(hurtSet && hurtTimer <= 0 && !isGameOver)
        {
            anim.SetBool("IsHurt", true);

            currentHealth -=5;
            hurtTimer = 0.3f;
            hurtSet = false;
        }

        if (hurtTimer > 0)
        {
            hurtTimer -= Time.deltaTime;
            if (hurtTimer <= 0)
            { 
                anim.SetBool("IsHurt", false);

            }
        }

    }

    void Death()
    {      
        if( currentHealth <= 0)
        {
            isGameOver = true;
            anim.SetBool("Death", true);
            Speed = 0;
        }
    }

    void GameOver()
    {
        GameInsideUI.isGameOver = isGameOver;
        Time.timeScale = 0;
    }

    public void Bag()
    {
        if (!isOpenBag)
        {
            isOpenBag = true;
            Time.timeScale = 0;
        }
        else
        {
            isOpenBag = false;
            Time.timeScale = 1;
        }
    }


    /*if(Physics2D.OverlapCircle(parameter.attackPoint.position, parameter.attackArea, parameter.targetLayer))
           {
               manager.TransitionState(StateType.Attack);
           }*/

    public void HealEff(int Heal)
    {
        currentHealth += Heal;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Monster"))
        {
            other.GetComponent<FSM>().parameter.isHurt = true;
        }
    }


    /*public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint[0].transform.position, attackArea);
    }*/
}
