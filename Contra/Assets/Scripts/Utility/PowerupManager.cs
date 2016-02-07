using UnityEngine;
using System.Collections;

public class PowerupManager : MonoBehaviour 
{
    public GameObject HeavyMachineGun;
    static int HeavyTimesSpawned;
    public AudioClip HeavyClip;
    AudioSource audioSource;

    void Start()
    {
        HeavyMachineGun.CreatePool(2);
        audioSource = GetComponent<AudioSource>();
    }

    public void SpawnPowerup(Vector3 location)
    {
        //equal chance of spawning each at start
        //weighted chance based on number of times spawned after that
        HeavyMachineGun.Spawn(location);
    }

    public void PlayEffect(string powerup)
    {
        if(powerup == "heavyPowerup")
        {
            audioSource.PlayOneShot(HeavyClip);
        }
    }
}
