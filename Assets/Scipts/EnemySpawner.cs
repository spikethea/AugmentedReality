//This script followed this tutorial: https://www.youtube.com/watch?v=rc_7iuuYSGA
using UnityEngine;
using Meta.XR.MRUtilityKit;

public class EnemySpawner : MonoBehaviour
{
    //TODO:
    //public Transfrom spawnPos
    //public float EnemySpeed
    
    
    //Slow enemy spawning
    public float SpawnTimer = 5f;
    public GameObject EnemyToSpawn;

    //spawn location settings
    //public float MinEdgeDistance = 0.3f;
    //public MRUKAncho r.SceneLabels SpawnLabel;
    //public float NormalOffset = 0f; //from the spawn point
    public int SpawnTry = 10; //to test
    private float timer;
    public bool IsSpawnerStarted = false;

    public void ToggleSpawner(bool value)
    {
        IsSpawnerStarted = value;
        Debug.Log("Toggle Spawner called");
        return;
    }



    // Update is called once per frame
    void Update()
    {
        if (!MRUK.Instance && !MRUK.Instance.IsInitialized)
        {
            Debug.Log("MRUK Not Initialized");
            return;
        }

        if (IsSpawnerStarted) {
            timer += Time.deltaTime;
            if (timer > SpawnTimer)
            {
                SpawnEnemy();
                timer -= SpawnTimer;
            }
        }
        
    }

    public void SpawnEnemy()
    {
        MRUKRoom room = MRUK.Instance.GetCurrentRoom();
        int currentTry = 0;
        while (currentTry < SpawnTry)
        {
            //bool hasFoundPosition = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.VERTICAL, MinEdgeDistance, LabelFilter.Included(SpawnLabel), out Vector3 pos, out Vector3 norm);

            //if (hasFoundPosition)
            //{
                //Vector3 randomPositionNormalOffset = pos + norm * NormalOffset;
                //randomPositionNormalOffset.y = 0;
                Instantiate(EnemyToSpawn, transform.position, Quaternion.identity);
                return;
            //}
            //else
            //{
            //    currentTry++;
            //}
        }
        
        
    }
    
}
