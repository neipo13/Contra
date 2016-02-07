using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateEoLMenuText : MonoBehaviour
{
    public Text scoreText;
    public Text coinText;
    public Text btnStoreText;

    void Start()
    {
        scoreText.text = StaticGameVariables.lastGameScore + "m";
        coinText.text = StaticGameVariables.lastGameCoins.ToString();
        btnStoreText.text = "Store\n" + Soomla.Store.StoreInventory.GetItemBalance(ContraForeverAssets.CoinCurrencyID);
    }
}
