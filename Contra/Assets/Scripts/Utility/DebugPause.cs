using UnityEngine;
using System.Collections;

public class DebugPause : PausableMonoBehavior 
{
    PausableMonoBehavior[] pausableList;

	void Update () 
    {
	    if(Input.GetKeyDown(KeyCode.P))
        {
            if (PausableMonoBehavior.paused == false)
            {
                pausableList = GameObject.FindObjectsOfType<PausableMonoBehavior>();
                for(int i = 0; i < pausableList.Length; i++)
                {
                    pausableList[i].Pause();
                }
            }
            else
            {
                for (int i = 0; i < pausableList.Length; i++)
                {
                    pausableList[i].UnPause();
                }
            }
        }
	}
}
