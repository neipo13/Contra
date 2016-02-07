using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TxtHighscore : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
    {
        Text txt = GetComponent<Text>();
        txt.text = "TOP " + PlayerPrefs.GetInt(ScoreManager.highscoreKey, 0);
	}
}
