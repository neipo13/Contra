using UnityEngine;
using System.Collections;

public class PickupCollision : MonoBehaviour 
{
    GameObject gameMaster;

    void Start()
    {
        gameMaster = GameObject.Find("GameMaster");
    }
	void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            if(gameObject.tag == "coin")
            {
                Debug.Log("Coin Message Sent");
                gameMaster.SendMessage("CoinCollected");
            }
            gameObject.Recycle();
        }
    }
}
