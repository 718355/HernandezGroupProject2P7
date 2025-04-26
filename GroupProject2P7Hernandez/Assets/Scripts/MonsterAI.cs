using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public Transform[] waypoints;
    public float viewDistance = 10f;
    public float viewAngle = 60f;
    public Transform player;

    private int currentWaypointIndex = 0;
    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;


    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        GoToNextWaypoint();
    }



    void Update()
    {
        if(animator != null)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }

        if (CanSeePlayer())
        {
            agent.SetDestination(player.position);
        }
        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextWaypoint();
        }
    }

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (directionToPlayer.magnitude <= viewDistance && angle < viewAngle / 2f)
        {
            Ray ray = new Ray(transform.position + Vector3.up, directionToPlayer.normalized);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, viewDistance))
            {
                if(hit.transform == player)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }
    }

}


