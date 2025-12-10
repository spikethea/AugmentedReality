//This script is inspired by this tutorial: https://www.youtube.com/watch?v=uMPb_B1OyH0

using UnityEngine;
public class SlowEnemyNav : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent Agent;
    public float speed = 1f;
    public float minEnemyDistance = 2f;
    public GameObject ThrowingPrefab;

    public float amplitudeY = 0.1f;
    private Vector3 startPos;

    // enemy Health
    private int currentHealth = 50;

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
        //transform.rotation =  Camera.main.transform.rotation;


        Agent.SetDestination(targetPositon);
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
