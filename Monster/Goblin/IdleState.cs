using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private FSM manager;
    private Parameter parameter;

    private float timer;

    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        if (manager.isActiveAndEnabled)
        {
            parameter.anim.Play("idle");
        }
    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;

        if(parameter.isHurt == true)
        {
            manager.TransitionState(StateType.TakeDamage);
        }

        if(parameter.target != null && parameter.target.position.x <= parameter.chaseArea + parameter.chasePoint[1].position.x &&
            parameter.target.position.x >= -parameter.chaseArea + parameter.chasePoint[1].position.x &&
            parameter.target.position.y <= parameter.chaseArea + parameter.chasePoint[1].position.y &&
            parameter.target.position.y >= -parameter.chaseArea + parameter.chasePoint[1].position.y)
        {
            manager.TransitionState(StateType.Chase);
        }

        if(timer >= parameter.idleTime)
        {
            manager.TransitionState(StateType.Patrol);
        }
    }

    public void OnExit()
    {
        timer = 0;
    }
}

public class PatrolState : IState
    {
        private FSM manager;
        private Parameter parameter;
        private float patrolPx, patrolPy;
        private Vector2 patrolTarget;
        //private int patrolPosition;

        public PatrolState(FSM manager)
        {
            this.manager = manager;
            this.parameter = manager.parameter;
        }

        public void OnEnter()
        {

            parameter.anim.Play("walk");
            RandomPatrolPoint();

        }

        public void OnUpdate()
        {
        if (parameter.isHurt == true)
        {
            manager.TransitionState(StateType.TakeDamage);
        }
        if (parameter.target != null && parameter.target.position.x <= parameter.chaseArea + parameter.chasePoint[1].position.x &&
            parameter.target.position.x >= -parameter.chaseArea + parameter.chasePoint[1].position.x &&
            parameter.target.position.y <= parameter.chaseArea + parameter.chasePoint[1].position.y &&
            parameter.target.position.y >= -parameter.chaseArea + parameter.chasePoint[1].position.y)
            {
                manager.TransitionState(StateType.Chase);
            }

            manager.FlipTo(patrolTarget);

            manager.transform.position = Vector2.MoveTowards(manager.transform.position,patrolTarget, parameter.moveSpeed * Time.deltaTime);

        //两点巡逻
        /*manager.transform.position = Vector2.MoveTowards(manager.transform.position,
        parameter.patrolPoint[patrolPosition].position, parameter.moveSpeed * Time.deltaTime);
        */

        if(Vector2.Distance(manager.transform.position,patrolTarget) < .1f)
        {
            manager.TransitionState(StateType.Idle);
            RandomPatrolPoint();
        }
    }

        public void OnExit()
        {
            /*patrolPosition++;
            if(patrolPosition >= parameter.patrolPoint.Length)两点巡逻
            {
                patrolPosition=0;
            }*/
        }

        public void RandomPatrolPoint() //重随巡逻目标点
        {
            patrolPx = UnityEngine.Random.Range(-parameter.partolArea, parameter.partolArea);
            patrolPy = UnityEngine.Random.Range(-parameter.partolArea, parameter.partolArea);
            patrolTarget = new Vector2(parameter.patrolPoint[1].position.x + patrolPx,parameter.patrolPoint[1].position.y + patrolPy);
        }
}


public class ChaseState : IState
    {
        private FSM manager;
        private Parameter parameter;

        public ChaseState(FSM manager)
        {
            this.manager = manager;
            this.parameter = manager.parameter;
        }

        public void OnEnter()
        {
            parameter.anim.Play("walk");
        }

        public void OnUpdate()
        {
            if (parameter.isHurt == true)
            {
                manager.TransitionState(StateType.TakeDamage);
            }

            if(parameter.target)
            {
                manager.FlipTo(new Vector2(parameter.target.position.x, parameter.target.position.y));
                manager.transform.position = Vector2.MoveTowards(manager.transform.position, parameter.target.position, parameter.chaseSpeed * Time.deltaTime);
        }
            if(parameter.target == null || parameter.target.position.x >= parameter.chaseArea + parameter.chasePoint[1].position.x ||
                parameter.target.position.x <= -parameter.chaseArea + parameter.chasePoint[1].position.x ||
                parameter.target.position.y >= parameter.chaseArea + parameter.chasePoint[1].position.y ||
                parameter.target.position.y <= -parameter.chaseArea + parameter.chasePoint[1].position.y)//ReferenceEquals(parameter.target, null)
       
            {
                manager.TransitionState(StateType.Idle);
            }
            if(Physics2D.OverlapCircle(parameter.attackPoint.position,parameter.attackArea,parameter.targetLayer))
            {
                manager.TransitionState(StateType.Attack);
            }
        }

        public void OnExit()
        {
        }
    }


public class AttackState : IState
    {
        private FSM manager;
        private Parameter parameter;
        private AnimatorStateInfo info;
        private float hitTimer;

        public AttackState(FSM manager)
        {
            this.manager = manager;
            this.parameter = manager.parameter;
        }

        public void OnEnter()
        {
            //parameter.attackColl.enabled = true;
            parameter.anim.Play("attack");
            hitTimer = 0.8f;
        }

        public void OnUpdate()
        {
             hitTimer -= Time.deltaTime;
            if (parameter.isHurt == true)
            {
                manager.TransitionState(StateType.TakeDamage);
            }

            info = parameter.anim.GetCurrentAnimatorStateInfo(0);
            
            if(info.normalizedTime >= 1.2f)
            {
                manager.TransitionState(StateType.Chase);
            }
            if (Physics2D.OverlapCircle(parameter.attackPoint.position, parameter.attackArea, parameter.targetLayer) && parameter.beenAttack && hitTimer <= 0) 
            {
                parameter.target.GetComponent<PlayerControl>().hurtSet = true;
                hitTimer = 0.8f;
                parameter.beenAttack = false;
            }
    }

        public void OnExit()
        {
            parameter.beenAttack = true;
        }
}


public class TakedamageState : IState
    {
        private FSM manager;
        private Parameter parameter;
        private AnimatorStateInfo info;
        private float timer;

    public TakedamageState(FSM manager)
        {
            this.manager = manager;
            this.parameter = manager.parameter;
        }

        public void OnEnter()
        {
            if (parameter.target)
            {
                manager.FlipTo(new Vector2(parameter.target.position.x, parameter.target.position.y));
            }
            parameter.currentHealth -= 5;
            parameter.isHurt = false;
            parameter.anim.Play("TakeDamage");
        //hitback();
        }

        public void OnUpdate()
        {
            if (parameter.isHurt == true)
            {
                manager.TransitionState(StateType.TakeDamage);
            }
            timer += Time.deltaTime;
            if(timer > parameter.hurtTime)
            {
                 manager.TransitionState(StateType.Chase);
            }
            if (parameter.currentHealth <= 0)
            {
                manager.TransitionState(StateType.Death);
            }
        }

        public void OnExit()
        {
            timer = 0;
            parameter.isHurt = false;
        }

        void Hitback() //击退效果，未使用
        {
            if(parameter.target != null && manager.transform.position.x < parameter.target.position.x)
            {
                manager.transform.position = Vector2.MoveTowards(manager.transform.position,new Vector2(manager.transform.position.x - 4, manager.transform.position.y),3f);
                manager.transform.position = Vector2.MoveTowards(manager.transform.position,new Vector2(manager.transform.position.x - 4, manager.transform.position.y),3f);
            }
            else if(parameter.target != null && manager.transform.position.x > parameter.target.position.x)

            {
                manager.transform.position = Vector2.MoveTowards(manager.transform.position, new Vector2(manager.transform.position.x + 4, manager.transform.position.y), 3f);
            }
        }
    }

public class DeathState : IState
{
    private FSM manager;
    private Parameter parameter;
    private AnimatorStateInfo info;
    private float timer = 0.4f;


    public DeathState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        parameter.anim.Play("Death");
        
    }

    public void OnUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            parameter.self.SetActive(false);
            parameter.self.transform.position = new Vector2(-80, -5);
            manager.TransitionState(StateType.Idle);
        }
    }
    public void OnExit()
    {
        parameter.currentHealth = parameter.maxHealth;
    }

}