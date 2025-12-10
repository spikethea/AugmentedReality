//This script is inspired by this tutorial: https://www.youtube.com/watch?v=uMPb_B1OyH0

using UnityEngine;

public class FastEnemyNav : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent Agent;
    public float speed = 0.5f;
    public float minEnemyDistance = 2f;

    // Child Mesh
    public GameObject mesh;
    public float amplitudeY = 0.1f;
    private Vector3 startPos;
    private float randomOffset;               // per-enemy phase offset

    [Header("Horizontal Random Movement")]
    public float sideMoveAmount = 3f;   // max left/right distance
    public float sideChangeInterval = 1.2f; // how often direction changes

    private float currentSideOffset;
    private float targetSideOffset;
    private float sideTimer;



    // enemy Health
    private int currentHealth = 15;

    // Coins
    public GameObject ThrowingPrefab;
    public Transform MeshTransform;
    public BoxCollider MeshCollider;

    private void Start()
    {
        randomOffset = Random.Range(0f, 100f);
    }
    public void TakeDamage()
    {
        currentHealth--;
        Debug.Log("Taking Damage Flying Object");
    }

    public void Ocsillate()
    {
        float yOffset = Mathf.Sin(Time.time * speed) * amplitudeY;
        mesh.transform.localPosition = new Vector3(
            startPos.x,
            startPos.y + yOffset,
            startPos.z
        );
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = Camera.main.transform.position;

        // Direction from enemy to player
        Vector3 toPlayer = (playerPos - transform.position).normalized;

        // Perpendicular (left/right)
        Vector3 right = Vector3.Cross(Vector3.up, toPlayer);

        // Change target offset occasionally
        sideTimer += Time.deltaTime;
        if (sideTimer >= sideChangeInterval)
        {
            sideTimer = 0f;
            targetSideOffset = Random.Range(-sideMoveAmount, sideMoveAmount);
        }

        // Smooth movement so it¡¯s not jittery
        currentSideOffset = Mathf.Lerp(
            currentSideOffset,
            targetSideOffset,
            Time.deltaTime * 2f
        );

        // Final target position
        Vector3 targetPosition =
            playerPos
            - toPlayer * minEnemyDistance
            + right * currentSideOffset;

        Agent.SetDestination(targetPosition);
        Agent.speed =  speed;

        //Debug.Log("Enemy health" + currentHealth);
        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
            Instantiate(ThrowingPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.3f);
        }

        Ocsillate();
    }
}
