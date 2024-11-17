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
                if (agent != null)
                {
                    Vector3 targetPosition = lastWaypoint.ContainsKey(npc) && lastWaypoint[npc] != null 
                    ? lastWaypoint[npc].position : transform.position;
                    agent.SetDestination(GetRandomPointOnNavMesh(targetPosition));
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
        if (agent != null)
        {
            npcMovingToWaypoint[npc] = true;
            Transform waypoint;
            do
            {
                waypoint = waypoints[Random.Range(0, waypoints.Count)];
            } while (waypoint == lastWaypoint[npc]);
            lastWaypoint[npc] = waypoint;
            agent.SetDestination(waypoint.position);
            StartCoroutine(ResetWaypointFlagAfterArrival(npc, agent));
        }
    }

    IEnumerator ResetWaypointFlagAfterArrival(GameObject npc, NavMeshAgent agent)
    {
        while (!agent.pathPending && agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }
        npcMovingToWaypoint[npc] = false;
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