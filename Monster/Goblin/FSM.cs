using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
    Idle,Patrol,Chase,TakeDamage,Attack,Death
}

[Serializable]
public class Parameter
{
    [Header("怪物血条")]
    public int currentHealth;
    public int maxHealth;
    public Transform healthBarPoint;
    public bool isActive;
    [Header("状态常量")]
    public float moveSpeed;
    public float chaseSpeed;
    public float idleTime;
    public float hurtTime;
    public bool isHurt = false;

    
    public Collider2D body;
    public GameObject self;


    

    [Header("巡逻")]
    public GameObject patrolCenter;
    public Transform[] patrolPoint;
    public float partolArea;

    [Header("追击")]
    public Transform[] chasePoint;
    public GameObject chaseCenter;
    public float chaseArea;
    public LayerMask targetLayer;
    public Transform target;

    [Header("攻击")]
    public float attackArea;
    public Transform attackPoint;
    public bool beenAttack = true;


    //public Collider2D attackColl; //碰撞开关攻击

    public Animator anim;
}


public class FSM : MonoBehaviour
{

    private IState currentState;

    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();

    public Parameter parameter = new Parameter();

    void Start()
    {
        
        states.Add(StateType.Idle, new IdleState(this));
        states.Add(StateType.Patrol, new PatrolState(this));
        states.Add(StateType.Chase, new ChaseState(this));
        states.Add(StateType.Attack, new AttackState(this));
        states.Add(StateType.TakeDamage, new TakedamageState(this));
        states.Add(StateType.Death, new DeathState(this));
        TransitionState(StateType.Idle);
        parameter.anim = GetComponent<Animator>();

        parameter.patrolCenter.transform.DetachChildren();//解除巡逻范围中心点与moster的父子关系
        parameter.chaseCenter.transform.DetachChildren();//解除追击范围中心点与moster的父子关系
        Vector2 PCenter = parameter.patrolPoint[1].position;
        Vector2 CCenter = parameter.chasePoint[1].position;
    }


    void Update()
    {
        currentState.OnUpdate();
    }

    public void  TransitionState(StateType type)
    {
        if(currentState != null )
        {
            currentState.OnExit();
        }
        currentState = states[type];
        currentState.OnEnter();
    }

    public void FlipTo(Vector2 target)
    {
        if(target != null)
        {
            if(transform.position.x > target.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }else if(transform.position.x < target.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parameter.target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            parameter.target = null;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(parameter.attackPoint.position, parameter.attackArea);//攻击范围
        Gizmos.DrawWireSphere(parameter.patrolPoint[1].transform.position, parameter.partolArea);//巡逻范围
        Gizmos.DrawWireSphere(parameter.chasePoint[1].transform.position, parameter.chaseArea);//追击范围
    }
}
