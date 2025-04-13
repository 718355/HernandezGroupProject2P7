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

    private enum State { Wandering, Chasing }
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
                Wander();
                if (distanceToPlayer <= sightRange)
                {
                    currentState = State.Chasing;
                    agent.speed = chaseSpeed;
                    anim.SetTrigger("Run");
                }
                break;

            case State.Chasing:
                agent.SetDestination(player.position);
                if (distanceToPlayer > giveUpDistance)
                {
                    currentState = State.Wandering;
                    agent.speed = wanderSpeed;
                    anim.SetTrigger("Walk");
                    GoToNextPoint();
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


