using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour 
{
    public int maxHp = 3;
    protected int hp = 0;
    DropManager dropManager;

    void Start()
    {
        dropManager = GameObject.Find("GameMaster").GetComponent<DropManager>();
    }

    void Init()
    {
        hp = maxHp;
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        if (hp <= 0)
        {
            hp = 0;
            Killed();
        }
    }

    public void Killed()
    {
        dropManager.SpawnDrop(transform.position);
        maxHp = hp;
        gameObject.Recycle();
    }
}
