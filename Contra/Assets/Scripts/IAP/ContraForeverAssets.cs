using UnityEngine;
using System.Collections;
using Soomla.Store;
using System.Collections.Generic;

public class ContraForeverAssets : IStoreAssets 
{
    //Item IDs

    //Currencies
    public static string CoinCurrencyID = "COIN_CURRENCY";

    public static string CoinDoublerID = "COIN_DOUBLER_SUI";


    public int GetVersion()
    {
        return 0;
    }

    public VirtualCurrency[] GetCurrencies()
    {
        return new VirtualCurrency [] { new VirtualCurrency ("Coins", "Coins earned in game.", CoinCurrencyID) };
    }

    public VirtualGood[] GetGoods()
    {
        return new VirtualGood[] { CoinDoubler };
    }

    public VirtualCurrencyPack[] GetCurrencyPacks()
    {
        return new VirtualCurrencyPack[]{};
    }

    public VirtualCategory[] GetCategories()
    {
        return new VirtualCategory[]{ GeneralCategory };
    }


    public static VirtualGood CoinDoubler = new SingleUseVG
    (
        "Coin Doubler",
        "Doubles the coins earned on the next playsession.",
        "COIN_DOUBLER_SUI",
        new PurchaseWithVirtualItem(CoinCurrencyID, 50)
    );

    public static VirtualCategory GeneralCategory = new VirtualCategory
    (
        "General",
        new List<string> { CoinCurrencyID, CoinDoublerID }
    );
}
