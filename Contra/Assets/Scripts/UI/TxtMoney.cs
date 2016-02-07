using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TxtMoney : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Text txt = GetComponent<Text>();
        txt.text = Soomla.Store.StoreInventory.GetItemBalance(ContraForeverAssets.CoinCurrencyID) + " c";
    }
}
