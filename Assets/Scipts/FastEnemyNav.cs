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

    // Horizontal Movement
    public float oscillationAmplitude = 0.1f;   // how far left/right
    public float oscillationSpeed = 1.5f;     // how fast it wiggles
    private float randomOffset;               // per-enemy phase offset

    // enemy Health
    private int currentHealth = 15;

    private void Start()
    {
        randomOffset = Random.Range(0f, 100f);
    }
    public void TakeDamage()
    {
        currentHealth--;
    }

    public void Ocsillate()
    {
        float yOffset = Mathf.Sin(Time.time * speed) * amplitudeY;
        transform.position = new Vector3(
            startPos.x,
            startPos.y + yOffset,
            startPos.z
        );
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 targetPositon = Camera.main.transform.position - Vector3.forward*minEnemyDistance;

        Agent.SetDestination(targetPositon);
        Agent.speed =  speed;

        Debug.Log("Enemy health" + currentHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }

        Ocsillate();
    }
}
