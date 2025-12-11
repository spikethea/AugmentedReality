using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;   // for runtime navmesh
using System.Collections;

public class FastEnemyNav : MonoBehaviour
{
    [Header("NavMesh")]
    public NavMeshAgent Agent;
    private NavMeshSurface surface;

    [Header("Movement")]
    public float speed = 0.5f;
    public float minEnemyDistance = 2f;

    [Header("Vertical Oscillation")]
    public GameObject mesh;
    public float amplitudeY = 0.1f;
    private Vector3 startMeshPos;

    [Header("Horizontal Sway")]
    public float sideMoveAmount = 3f;
    public float sideChangeInterval = 1.2f;
    private float sideTimer;
    private float targetSideOffset;
    private float currentSideOffset;

    [Header("Health & Drop")]
    private int currentHealth = 15;
    public GameObject ThrowingPrefab;

    private void Start()
    {
        startMeshPos = mesh.transform.localPosition;

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

    void Update()
    {
        if (!Agent.isOnNavMesh)
            return;

        Vector3 playerPos = Camera.main.transform.position;

        // Direction toward player
        Vector3 toPlayer = (playerPos - transform.position).normalized;
        Vector3 right = Vector3.Cross(Vector3.up, toPlayer);

        // Horizontal sway timing
        sideTimer += Time.deltaTime;
        if (sideTimer >= sideChangeInterval)
        {
            sideTimer = 0f;
            targetSideOffset = Random.Range(-sideMoveAmount, sideMoveAmount);
        }

        // Smooth interpolation
        currentSideOffset = Mathf.Lerp(
            currentSideOffset,
            targetSideOffset,
            Time.deltaTime * 2f
        );

        // Final movement target
        Vector3 targetPos =
            playerPos
            - toPlayer * minEnemyDistance
            + right * currentSideOffset;

        // Ensure target is *on navmesh*
        if (NavMesh.SamplePosition(targetPos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            Agent.SetDestination(hit.position);

        Agent.speed = speed;

        // Floating animation
        ApplyVerticalOscillation();
    }

    void ApplyVerticalOscillation()
    {
        float yOffset = Mathf.Sin(Time.time * speed) * amplitudeY;
        mesh.transform.localPosition = startMeshPos + new Vector3(0, yOffset, 0);
    }
}