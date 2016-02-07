using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameMaster : MonoBehaviour
{
    void Start()
    {
        StaticGameVariables.gameSpeed = StaticGameVariables.pausedGameSpeed = 1.0f;
        StaticGameVariables.lastGameCoins = 0;
        StaticGameVariables.lastGameScore = 0;
        InvokeRepeating("IncreaseGameSpeed", 5.0f, 5.0f);
        PausableMonoBehavior.paused = false;
        //DontDestroyOnLoad(gameObject);
    }

    void IncreaseGameSpeed()
    {
        StaticGameVariables.gameSpeed = StaticGameVariables.gameSpeed * 1.15f;
    }

    public void PauseGameSpeed()
    {
        StaticGameVariables.pausedGameSpeed = StaticGameVariables.gameSpeed;
        StopGameSpeed();
    }

    public void StartGameSpeed()
    {
        StaticGameVariables.gameSpeed = StaticGameVariables.pausedGameSpeed;
    }

    void StopGameSpeed()
    {
        StaticGameVariables.gameSpeed = 0.0f;
    }

    void EndRun()
    {
        StopGameSpeed();
        //add to coin totals
        CoinManager coinManager = GetComponent<CoinManager>();
        Soomla.Store.StoreInventory.GiveItem(ContraForeverAssets.CoinCurrencyID, coinManager.coins);
        //save coin and score to static
        ScoreManager scoreManager = GetComponent<ScoreManager>();
        scoreManager.ExpandScore();
        scoreManager.SetHighScore(scoreManager.ScoreToInt());
        StaticGameVariables.lastGameCoins = coinManager.coins;
        StaticGameVariables.lastGameScore = scoreManager.ScoreToInt();
        //stop player shooting

        //load menu
        Invoke("LoadMenu", 1.5f);

    }

    public void LoadMenu()
    {
        //load store page
        SceneManager.LoadScene("CrossyEOL", LoadSceneMode.Additive);
    }
}