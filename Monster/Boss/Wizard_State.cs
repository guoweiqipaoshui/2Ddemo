using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WIdleState : IState
{
    private Wizard_Boss manager;
    private WizParameter parameter;
    float timer = 5f;
    public WIdleState(Wizard_Boss manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
      
    }

    public void OnEnter()
    {
        parameter.Anim.Play("Idle");
        //Debug.LogError("isIn");
    }

    public void OnUpdate()
    {
        if (parameter.maxHealth - parameter.currentHealth >= parameter.currentHealth)
            manager.TransitionState(StateTypeWiz.Move);
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            if (!parameter.switchAT)
                manager.TransitionState(StateTypeWiz.Attack1);
            else
                manager.TransitionState(StateTypeWiz.Attack2);
            //Debug.LogError("isTrans");
        }
    }

    public void OnExit()
    {
        timer = 3f;
    }
}



public class Attack1State : IState
{
    private Wizard_Boss manager;
    private WizParameter parameter;
    float interval = 1.5f;
    int count = 3;

    public Attack1State(Wizard_Boss manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.Anim.Play("Attack1");
        parameter.switchAT = true;
    }

    public void OnUpdate()
    {
        if (parameter.maxHealth - parameter.currentHealth >= parameter.currentHealth)
            manager.TransitionState(StateTypeWiz.Move);
        interval -= Time.deltaTime;
       
        if (interval <= 0 && count >= 0)
        {            
            interval = 1.5f;
            InsNewAt();
        }   
        if(count < 0)
            manager.TransitionState(StateTypeWiz.Idle);
    }

    public void OnExit()
    {
        interval = 1.5f;
        count = 3;
    }

    void InsNewAt()
    {           
        PoolManager.Release(parameter.Attack1, parameter.InsEyeTran[count].position);
        count--;
    }
}

public class Attack2State : IState
{
    private Wizard_Boss manager;
    private WizParameter parameter;
    float interval = 1f;
    int count = 5;

    public Attack2State(Wizard_Boss manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.Anim.Play("Attack1");
        parameter.switchAT = false;
    }

    public void OnUpdate()
    {
        if (parameter.maxHealth - parameter.currentHealth >= parameter.currentHealth)
            manager.TransitionState(StateTypeWiz.Move);
        interval -= Time.deltaTime;

        if (interval <= 0 && count >= 0)
        {
            interval = 1f;
            InsNewAt();
        }
        if (count < 0)
            manager.TransitionState(StateTypeWiz.Idle);
    }

    public void OnExit()
    {
        interval = 1f;
        count = 5;
    }

    void InsNewAt()
    {
        PoolManager.Release(parameter.Attack2, parameter.target.transform.position);
        count--;
    }
}

    public class MoveState : IState
    {
        private Wizard_Boss manager;
        private WizParameter parameter;
        Vector3 curr;
        float timer = 0;

        public MoveState(Wizard_Boss manager)
        {
            this.manager = manager;
            this.parameter = manager.parameter;
        }

        public void OnEnter()
        {
            parameter.Anim.Play("MoveAT");
            manager.LevelTwo();
            curr = manager.transform.localPosition;
            manager.transform.position = parameter.target.transform.position;
        }

        public void OnUpdate()
        {
            timer += Time.deltaTime;
            if (timer >= 1.5f)
                manager.TransitionState(StateTypeWiz.Attack3);
        }

        public void OnExit()
        {
            timer = 0;
            manager.transform.position = curr;
        }
    }

public class Attack3State : IState
{
    private Wizard_Boss manager;
    private WizParameter parameter;

    public Attack3State(Wizard_Boss manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.Anim.Play("Attack1");
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}

public class Attack4State : IState
{
    private Wizard_Boss manager;
    private WizParameter parameter;

    public Attack4State(Wizard_Boss manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.Anim.Play("Attack1");
    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }
}
