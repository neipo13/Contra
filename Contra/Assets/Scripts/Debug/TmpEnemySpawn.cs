using UnityEngine;
using System.Collections;

public class TmpEnemySpawn : PausableMonoBehavior 
{
    public GameObject enemyType;


	// Use this for initialization
	void Start () 
    {
        enemyType.CreatePool();
        InvokeRepeating("SpawnEnemy", 1.0f, 1.0f);
	}

    void SpawnEnemy()
    {
        if(!paused)
        {
            enemyType.Spawn(_transform, _transform.position);
        }
    }


}
