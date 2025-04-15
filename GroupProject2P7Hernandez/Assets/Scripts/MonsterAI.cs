using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Transform[] wanderPoints;
    public float sightRange = 10f;
    public float chaseSpeed = 6f;
    public float wanderSpeed = 3.5f;
    public float giveUpDistance = 15f;
    public Transform player;

    private UnityEngine.AI.NavMeshAgent agent;
    private int currentPoint = 0;
    private Animator anim;

    public float attackRange = 2f;
    public float attackCoolDown = 1.5f;
    private float lastAttackTime = -Mathf.Infinity;

    private enum State { Wandering, Chasing, Attacking }
    private State currentState = State.Wandering;



    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.autoBraking = false;
        GoToNextPoint();
    }



    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Wandering:
                anim.SetFloat("Speed", 1f);
                Wander();
                if (distanceToPlayer <= sightRange)
                {
                    currentState = State.Chasing;
                    agent.speed = chaseSpeed;
                }
                break;

            case State.Chasing:
                anim.SetFloat("Speed", 2f);
                agent.SetDestination(player.position);

                if(distanceToPlayer <= attackRange)
                {
                    agent.ResetPath();
                    anim.SetTrigger("Attack");
                }
                else if (distanceToPlayer > giveUpDistance)
                {
                    currentState = State.Wandering;
                    agent.speed = wanderSpeed;
                }
                break;
            case State.Attacking:
                transform.LookAt(player);

                if(Time.time - lastAttackTime >= attackCoolDown)
                {
                    anim.SetTrigger("Attack");
                    lastAttackTime = Time.time;
                }

                if(distanceToPlayer > attackRange)
                {
                    currentState = State.Chasing;
                    agent.SetDestination(player.position);
                    anim.SetTrigger("Run");
                }
                break;
        }
    }

    void Wander()
    {
        if(!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
    }

    void GoToNextPoint()
    {
        if (wanderPoints.Length == 0)
            return;

        agent.destination = wanderPoints[currentPoint].position;
        currentPoint = (currentPoint + 1) % wanderPoints.Length;
        anim.SetTrigger("Walk");
    }

}


