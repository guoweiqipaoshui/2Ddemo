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
    [Header("����Ѫ��")]
    public int currentHealth;
    public int maxHealth;
    public Transform healthBarPoint;
    public bool isActive;
    [Header("״̬����")]
    public float moveSpeed;
    public float chaseSpeed;
    public float idleTime;
    public float hurtTime;
    public bool isHurt = false;

    
    public Collider2D body;
    public GameObject self;


    

    [Header("Ѳ��")]
    public GameObject patrolCenter;
    public Transform[] patrolPoint;
    public float partolArea;

    [Header("׷��")]
    public Transform[] chasePoint;
    public GameObject chaseCenter;
    public float chaseArea;
    public LayerMask targetLayer;
    public Transform target;

    [Header("����")]
    public float attackArea;
    public Transform attackPoint;
    public bool beenAttack = true;


    //public Collider2D attackColl; //��ײ���ع���

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

        parameter.patrolCenter.transform.DetachChildren();//���Ѳ�߷�Χ���ĵ���moster�ĸ��ӹ�ϵ
        parameter.chaseCenter.transform.DetachChildren();//���׷����Χ���ĵ���moster�ĸ��ӹ�ϵ
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
        Gizmos.DrawWireSphere(parameter.attackPoint.position, parameter.attackArea);//������Χ
        Gizmos.DrawWireSphere(parameter.patrolPoint[1].transform.position, parameter.partolArea);//Ѳ�߷�Χ
        Gizmos.DrawWireSphere(parameter.chasePoint[1].transform.position, parameter.chaseArea);//׷����Χ
    }
}
