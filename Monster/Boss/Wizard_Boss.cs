using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


   public enum StateTypeWiz
    {
        Idle, Attack1, Attack2, Move, Attack3, Attack4
    }

[Serializable]
public class WizParameter
{
    [Header("血量相关")]
    public int maxHealth;
    public int currentHealth;
    public GameObject UI;
    public bool hurtSet = false;
    [Header("攻击相关")]
    public Transform target;
    public Transform[] InsEyeTran;
    public bool switchAT = false;

    public GameObject Attack1;
    public GameObject Attack2;
    public GameObject Attack3;
    public GameObject Attack4;
    [Header("移动相关")]
    public int Speed;
    [Header("component属性")]
    public SpriteRenderer spriteRenderer;

    public Animator Anim;
}


public class Wizard_Boss : MonoBehaviour
{
    private IState currentState;

    private Dictionary<StateTypeWiz, IState> states = new Dictionary<StateTypeWiz, IState>();

    public WizParameter parameter = new WizParameter();

    WaitForSeconds waitForHurtTimer;

    void Awake()
    {
        waitForHurtTimer = new WaitForSeconds(0.4f);
    }

    private void Start()
    {
        states.Add(StateTypeWiz.Idle, new WIdleState(this));
        states.Add(StateTypeWiz.Attack1, new Attack1State(this));
        states.Add(StateTypeWiz.Attack2, new Attack2State(this));
        states.Add(StateTypeWiz.Attack3, new Attack3State(this));
        states.Add(StateTypeWiz.Attack4, new Attack4State(this));
        states.Add(StateTypeWiz.Move, new MoveState(this));

        TransitionState(StateTypeWiz.Idle);

        FlyEyeMove.Target = parameter.target;

        parameter.Anim = GetComponent<Animator>();
        parameter.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        WizHealth.currentHealth = parameter.currentHealth;
        WizHealth.maxHealth = parameter.maxHealth;
        currentState.OnUpdate();

        //if(parameter.hurtSet)
            //ColorChange();
    }

    private void OnTriggerEnter2D(Collider2D body)
    {
        if (body.CompareTag("PlayerAttack"))
        {
            parameter.currentHealth -= 5;
            parameter.hurtSet = true;
        }
    }

    public void LevelTwo()
    {
        parameter.spriteRenderer.color = new Color32(255, 255, 120, 255);
    }

    /*void ColorChange()
    {
        parameter.spriteRenderer.color = new Color32(255, 255, 100, 150);
        StartCoroutine("ColorBack");             
    }

    IEnumerable ColorBack()
    {
        yield return waitForHurtTimer;
        parameter.spriteRenderer.color = new Color32(255, 255, 255, 255);
    }*/

    public void TransitionState(StateTypeWiz type)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = states[type];
        currentState.OnEnter();
    }
}