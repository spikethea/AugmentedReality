using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;   // for runtime navmesh
using System.Collections;
using UnityEngine;
public class SlowEnemyNav : MonoBehaviour
{
    [Header("NavMesh")]
    private NavMeshSurface surface;


    public UnityEngine.AI.NavMeshAgent Agent;
    public float speed = 1f;
    public float minEnemyDistance = 2f;
    public GameObject ThrowingPrefab;

    public float amplitudeY = 0.1f;
    private Vector3 startMeshPos;

    // enemy Health
    private int currentHealth = 50;

    private void Start()
    {
        startMeshPos = transform.localPosition;

        // Find runtime navmesh surface (RoomModel)
        surface = FindObjectOfType<NavMeshSurface>();

        // Ensure navmesh exists before movement
        StartCoroutine(NavMeshInit());
    }

    // Check if navmesh exists (RoomModel builds it at runtime)
    bool NavMeshExists()
    {
        return NavMesh.CalculateTriangulation().vertices.Length > 0;
    }

    IEnumerator NavMeshInit()
    {
        // Wait until RoomModel navmesh is fully built
        while (!NavMeshExists())
            yield return null;

        // Snap onto navmesh
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 5f, NavMesh.AllAreas))
            transform.position = hit.position;

        Debug.Log("Enemy placed on NavMesh and ready.");
    }

    public void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            Instantiate(ThrowingPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void Ocsillate()
    {
        float yOffset = Mathf.Sin(Time.time * speed) * amplitudeY;
        transform.position = new Vector3(
            startMeshPos.x,
            startMeshPos.y + yOffset,
            startMeshPos.z
        );
    }

    // Update is called once per frame
    void Update()
    {

        if (!Agent.isOnNavMesh)
            Destroy(gameObject);

        Vector3 targetPositon = Camera.main.transform.position - Vector3.forward*minEnemyDistance;
        //transform.rotation =  Camera.main.transform.rotation;


        // Ensure target is *on navmesh*
        if (NavMesh.SamplePosition(targetPositon, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            Agent.SetDestination(hit.position);

        //Ocsillate();
        Agent.speed =  speed;

        //Debug.Log("Enemy health" + currentHealth);
        if (currentHealth <= 0)
        {
            Instantiate(ThrowingPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.3f);
        }
    }
}
