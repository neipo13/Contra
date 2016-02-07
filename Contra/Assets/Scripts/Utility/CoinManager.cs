using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinManager : MonoBehaviour 
{
    public GameObject CoinObject;
    public int coins;
    public int totalCoins;
    public Text coinText;

	// Use this for initialization
	void Start () 
    {
        CoinObject.CreatePool(30);
        coins = 0;
        totalCoins = Soomla.Store.StoreInventory.GetItemBalance(ContraForeverAssets.CoinCurrencyID);
        coinText.text = (totalCoins + coins).ToString() + " c";
	}


    public void SpawnCoins(int numCoinsToSpawn, Vector3 position)
    {
        GameObject coinTemp;
        for (int i = 0; i < numCoinsToSpawn; i++ )
        {
            coinTemp = CoinObject.Spawn(position);
            coinTemp.GetComponent<CharacterController2D>().velocity = new Vector3(Random.Range(-5f, 5f), Random.Range(3f, 10f), 0);
        }
    }

    public void CoinCollected()
    {
        //Debug.Log("Coin COllected");
        coins += 1;
        coinText.text = (totalCoins + coins).ToString();
    }
}
