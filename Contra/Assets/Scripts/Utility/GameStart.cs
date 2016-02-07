using UnityEngine;
using System.Collections;
using Soomla.Store;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour 
{

	// Use this for initialization
	void Start ()
    {
        Invoke("StartGame", 2.5f);
        Prime31.ZestKit.ZestKit.removeAllTweensOnLevelLoad = true;
        Soomla.Store.SoomlaStore.Initialize(new ContraForeverAssets());
	}

    void StartGame()
    {
        SceneManager.LoadScene("Waitforinput");
    }
}
