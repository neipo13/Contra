using UnityEngine;
using System.Collections;

public class EnemySpawnLocation : MonoBehaviour 
{
    Transform _transform;                   //cached spawn location transform

    void Start()
    {
        _transform = GetComponent<Transform>();
    }


    public GameObject SpawnEnemy(GameObject enemy)
    {
        return enemy.Spawn(_transform.position, Quaternion.identity);
    }
}
