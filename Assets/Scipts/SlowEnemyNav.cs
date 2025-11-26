//This script is inspired by this tutorial: https://www.youtube.com/watch?v=uMPb_B1OyH0

using UnityEngine;
public class SlowEnemyNav : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent Agent;
    public float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPositon = Camera.main.transform.position;
        Agent.SetDestination(targetPositon);
        Agent.speed =  speed;
    }
}
