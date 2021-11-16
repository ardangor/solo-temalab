using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimator : MonoBehaviour
{
    const float locomotionAnimationSmoothTime = 0.1f;

    NavMeshAgent agent;
    public Animator animator;
    EnemyAI combatSystem;
    EnemyTarget selfTarget;

    float hitDelay = 0f;

    public AudioSource idle;
    public AudioSource attack;
    public AudioSource die;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();
        combatSystem = GetComponent<EnemyAI>();
        selfTarget = GetComponent<EnemyTarget>();
        combatSystem.OnAttack += OnAttack;
        selfTarget.OnGetHit += OnGetHit;
        selfTarget.OnDie += Die;
        idle.Play();
    }

    void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("speedPercent", speedPercent, locomotionAnimationSmoothTime, Time.deltaTime);

        animator.SetBool("inCombat", combatSystem.inCombat);
    }

    protected virtual void OnAttack()
    {
        if(!selfTarget.isDead)
        {
            attack.Play();
            animator.SetTrigger("attack");  
        }
            
    }

    protected virtual void OnGetHit()
    {
        if (!selfTarget.isDead && Time.time >= hitDelay)
        {         
            animator.SetTrigger("hit");
            hitDelay = Time.time + 0.4f;
        }         
    }

    protected virtual void Die()
    {
        die.Play();
        animator.SetTrigger("die");
    }
}
