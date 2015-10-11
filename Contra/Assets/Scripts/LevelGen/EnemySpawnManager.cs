using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//various enemy types, will determine spawn location
public enum EnemyTypes { walking, flying }

//pairs enemies with types
    [System.Serializable]
public class EnemyTypeTuple
{
    public GameObject enemy;
    public EnemyTypes type;
}

public class EnemySpawnManager : MonoBehaviour 
{
    //a list of types of enemies that can be spawned
    public List<EnemyTypeTuple> enemyTypes;
    public List<EnemySpawnLocation> walkingLocations;
    public List<EnemySpawnLocation> flyingLocations;
    public float spawnRate = 1.5f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnRate, spawnRate);
    }


    void SpawnEnemy()
    {
        //pull a random enemy from the list
        int enemySelector = Random.Range(0, enemyTypes.Count); //random.range(inclusive min, exclusive max)
        GameObject enemy = new GameObject();

        if(enemyTypes[enemySelector].type == EnemyTypes.walking)
        {            
            //pull a random spawn location from that enemy type's location list
            int locationSelector = Random.Range(0, walkingLocations.Count);
            //spawn the enemy
            enemy = walkingLocations[locationSelector].SpawnEnemy(enemyTypes[enemySelector].enemy);
        }
        else if (enemyTypes[enemySelector].type == EnemyTypes.flying)
        {
            //pull a random spawn location from that enemy type's location list
            int locationSelector = Random.Range(0, flyingLocations.Count);
            //spawn the enemy
            enemy = flyingLocations[locationSelector].SpawnEnemy(enemyTypes[enemySelector].enemy);
        }

        enemy.SendMessage("Init");
    }

}
