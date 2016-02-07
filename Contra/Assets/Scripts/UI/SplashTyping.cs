using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashTyping : MonoBehaviour 
{
    public string text = "/games";
    public float waitToBegin = 1f;
    public float timeBetweenLetters = 0.06f;
    public Text uiText;

	// Use this for initialization
	void Start () {
	   uiText.text = "";
       char[] txt = text.ToCharArray();
        float nextLetterTime= waitToBegin;
        foreach(char letter in txt)
        {
            StartCoroutine(AddLetter(letter, nextLetterTime));
            nextLetterTime += timeBetweenLetters;
        }
	}
    
    IEnumerator AddLetter(char letter, float delay){
        yield return new WaitForSeconds(delay);
        uiText.text += letter;
    }
}
