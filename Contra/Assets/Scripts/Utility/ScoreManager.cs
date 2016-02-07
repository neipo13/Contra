using UnityEngine;
using UnityEngine.UI;
using Prime31.ZestKit;
using System.Collections;

public class ScoreManager : MonoBehaviour 
{
    public float score;
    [HideInInspector]
    public int highscore;
    public Text scoreText;
    public Text highscoreText;
    protected float scoreMultiplier = 10f;
    public static string highscoreKey = "SoHighSoHighSoHigh";

    //scoring flags
    protected float halfHigh;
    protected float quarterHigh;
    protected bool halfHit;
    protected bool threeQuarterHit;
    protected bool newHighHit;

	void Start () 
    {
        score = 0;
        highscore = PlayerPrefs.GetInt(highscoreKey, 0);
        highscoreText.text = "";
        highscoreText.rectTransform.localScale = new Vector3(1f, 0f, 1f);
        halfHit = threeQuarterHit = newHighHit = false;
        halfHigh = highscore / 2;
        quarterHigh = highscore / 4;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (!PausableMonoBehavior.paused)
        {
            score += StaticGameVariables.gameSpeed * Time.deltaTime * scoreMultiplier;
            if(!halfHit && ScoreToInt() >= halfHigh)
            {
                halfHit = true;
                //emit some halfway hit event/message
            }
            else if(!threeQuarterHit && ScoreToInt() >= (halfHigh + quarterHigh))
            {
                threeQuarterHit = true;
                //emit a three quarter hit event/message
            }
            else if(!newHighHit && ScoreToInt() >= highscore)
            {
                newHighHit = true;
                //emit some event/message about hitting a new high
            }
            scoreText.text = ScoreToInt() + "m";
        }
	}

    public int ScoreToInt()
    {
        return (int)Mathf.Floor(score);
    }

    public int HighScore()
    {
        return highscore;
    }

    public void SetHighScore(int newHigh)
    {
        if (newHigh > highscore)
        {
            highscore = newHigh;
            PlayerPrefs.SetInt(highscoreKey, newHigh);
            highscoreText.text += "NEW ";
        }
        highscoreText.text += "TOP " + highscore + "m";
        highscoreText.rectTransform.ZKlocalScaleTo(Vector3.one).setEaseType(EaseType.BounceOut).start();
    }

    public void ExpandScore()
    {
        scoreText.rectTransform.localScale = new Vector3(1f, 0.5f, 1f);
        scoreText.rectTransform.anchorMin = new Vector2(0.02f, 0.85f);
        scoreText.rectTransform.ZKlocalScaleTo(Vector3.one).setEaseType(EaseType.BounceOut).start();
    }
}
