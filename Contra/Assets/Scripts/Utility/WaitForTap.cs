using UnityEngine;
using System.Collections;

public class WaitForTap : MonoBehaviour 
{
	void Update () {
	    if(Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Runner");
        }
	}
}
