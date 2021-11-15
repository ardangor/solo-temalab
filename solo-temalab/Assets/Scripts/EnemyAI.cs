using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    NavMeshAgent selfAgent;
    Transform target;
    EnemyTarget selfTarget;

    public float damage = 10;
    public float attackDelay = 0.6f;
    float nextTimeToAttack = 0;
    public float attackSpeed = 1f;
    public bool isFlying = false;

    public bool inCombat { get; private set; }
    public event System.Action OnAttack;
    public LayerMask hitMask;
    void Start()
    {
        selfAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag("Player").transform;
        selfTarget = GetComponent<EnemyTarget>();
    }

    void Update()
    {
        if (!selfTarget.isDead)
        {
            float distance = Vector3.Distance(target.position, transform.position);

            RaycastHit hit;
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), transform.forward, out hit, 10f, hitMask) || isFlying)
            {
                if (distance <= selfAgent.stoppingDistance)
                {
                    Target targetToAttack = target.GetComponent<Target>();
                    if (targetToAttack != null && Time.time >= nextTimeToAttack)
                    {
                        StartCoroutine(Attack(targetToAttack, attackDelay));

                        if (OnAttack != null)
                            OnAttack();

                        nextTimeToAttack = Time.time + 1f / attackSpeed;
                    }

                    inCombat = true;                
                } else
                {
                    inCombat = false;
                }
            }

            FaceTarget();      
            selfAgent.SetDestination(target.position);

        } else
        {
            selfAgent.isStopped = true;
        }
    }

    IEnumerator Attack(Target target, float delay)
    {
        yield return new WaitForSeconds(delay);
        target.TakeDamage(damage);
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRoration = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRoration, Time.deltaTime * 5f);
    }
}
