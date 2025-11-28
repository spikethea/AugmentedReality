//This script is inspired by this tutorial: https://www.youtube.com/watch?v=uMPb_B1OyH0

using UnityEngine;
public class SlowEnemyNav : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent Agent;
    public float speed = 1f;
    public float minEnemyDistance = 2f;

    // enemy Health
    private int currentHealth = 50;

    public void TakeDamage()
    {
        currentHealth--;
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
    }
}
