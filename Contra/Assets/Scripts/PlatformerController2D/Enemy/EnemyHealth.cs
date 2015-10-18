using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour 
{
    public int maxHp = 3;
    protected int hp = 0;
    CoinManager coinManager;

    void Start()
    {
        coinManager = GameObject.Find("GameMaster").GetComponent<CoinManager>();
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
        coinManager.SpawnCoins(3, transform.position);
        maxHp = hp;
        gameObject.Recycle();
    }
}
