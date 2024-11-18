using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCManager : MonoBehaviour
{
    public GameObject npcPrefab;
    public int npcCount = 10;
    public float minMoveInterval = 2f;
    public float maxMoveInterval = 5f;
    public List<Transform> spawnPoints;
    public List<Transform> waypoints;

    private List<GameObject> npcs = new List<GameObject>();
    private Dictionary<GameObject, bool> npcMovingToWaypoint = new Dictionary<GameObject, bool>();
    private Dictionary<GameObject, Transform> lastWaypoint = new Dictionary<GameObject, Transform>();

    void Start()
    {
        for (int i = 0; i < npcCount; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            GameObject npc = Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity);
            npcs.Add(npc);
            npcMovingToWaypoint[npc] = false;
            lastWaypoint[npc] = null;

            Animator animator = npc.GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator component missing on NPC prefab.");
            }
            else
            {
                animator.SetBool("IsIdle", true);
                animator.SetBool("IsWalking", false);
            }

            StartCoroutine(MoveNPC(npc));
            StartCoroutine(MoveToWaypoints(npc, true));
        }
    }

    IEnumerator MoveNPC(GameObject npc)
    {
        while (true)
        {
            float waitTime = Random.Range(minMoveInterval, maxMoveInterval);
            yield return new WaitForSeconds(waitTime);

            if (!npcMovingToWaypoint[npc])
            {
                NavMeshAgent agent = npc.GetComponent<NavMeshAgent>();
                Animator animator = npc.GetComponent<Animator>();
                if (agent != null && animator != null)
                {
                    Vector3 targetPosition = lastWaypoint.ContainsKey(npc) && lastWaypoint[npc] != null ? lastWaypoint[npc].position : transform.position;
                    agent.SetDestination(GetRandomPointOnNavMesh(targetPosition));
                    animator.SetBool("IsWalking", true);
                    animator.SetBool("IsIdle", false);
                    StartCoroutine(UpdateNPCRotation(npc, agent));
                }
            }
        }
    }

    IEnumerator MoveToWaypoints(GameObject npc, bool immediate = false)
    {
        if (immediate)
        {
            MoveToRandomWaypoint(npc);
        }

        while (true)
        {
            yield return new WaitForSeconds(60f);
            MoveToRandomWaypoint(npc);
        }
    }

    void MoveToRandomWaypoint(GameObject npc)
    {
        NavMeshAgent agent = npc.GetComponent<NavMeshAgent>();
        Animator animator = npc.GetComponent<Animator>();
        if (agent != null && animator != null)
        {
            npcMovingToWaypoint[npc] = true;
            Transform waypoint;
            do
            {
                waypoint = waypoints[Random.Range(0, waypoints.Count)];
            } while (waypoint == lastWaypoint[npc]);
            lastWaypoint[npc] = waypoint;
            agent.SetDestination(waypoint.position);
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsIdle", false);
            StartCoroutine(UpdateNPCRotation(npc, agent));
            StartCoroutine(ResetWaypointFlagAfterArrival(npc, agent, animator));
        }
    }

    IEnumerator ResetWaypointFlagAfterArrival(GameObject npc, NavMeshAgent agent, Animator animator)
    {
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }
        npcMovingToWaypoint[npc] = false;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsIdle", true);
    }

    IEnumerator UpdateNPCRotation(GameObject npc, NavMeshAgent agent)
    {
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            if (agent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                Vector3 direction = agent.velocity.normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                npc.transform.rotation = Quaternion.LookRotation(direction);
            }
            yield return null;
        }
    }

    Vector3 GetRandomPointOnNavMesh(Vector3 targetPosition)
    {
        Vector3 randomPoint = Random.insideUnitSphere * 10f;
        randomPoint += targetPosition;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas);
        return hit.position;
    }
}