using UnityEngine;
using System.Collections;


/// <summary>
/// Should adjust drop rates to balance powerup spawn rates, powerups should never spawn while another powerup is active and spawn 
/// rates should increase slightly as time without a powerup increase.
/// </summary>
public class DropManager : MonoBehaviour 
{
    float timeWithoutAPowerup;                              //time after powerup expires that player has not gotten a powerup pickup
    public float noPowerupSpawnTime = 10.0f;                //time after a powerup pickup that another powerup should NOT drop in
    float powerupSpawnTimer;                                //local timer to measure against above
    bool canSpawnPowerup;

    CoinManager coins;                                      //local reference to coin manager for drops
    PowerupManager powerups;                                //local reference to powerup manager for drops

    void Start()
    {
        GameObject master = GameObject.Find("GameMaster");
        coins = master.GetComponent<CoinManager>();
        powerups = master.GetComponent<PowerupManager>();

        timeWithoutAPowerup = 0;
        powerupSpawnTimer = 0;
        canSpawnPowerup = true;
    }


    void Update()
    {
        if(!canSpawnPowerup)
        {
            powerupSpawnTimer += Time.deltaTime;
            if(powerupSpawnTimer >= noPowerupSpawnTime)
            {
                canSpawnPowerup = true;
            }
        }
        else
        {
            timeWithoutAPowerup += Time.deltaTime;
        }
    }

    public void SpawnDrop(Vector3 position)
    {
        //get a number 1-100
        int drop = Random.Range(1, 101);
        if (drop >= 90 && canSpawnPowerup)
        {
            //powerupmanager spawn
            powerups.SpawnPowerup(position);
            canSpawnPowerup = false;
            powerupSpawnTimer = 0;
        }
        else if (drop >= 80)
        {
            coins.SpawnCoins(3, position);
        }
        else if (drop >= 60)
        {
            coins.SpawnCoins(2, position);
        }
        else if (drop >= 30)
        {
            coins.SpawnCoins(1, position);
        }
    }

}
