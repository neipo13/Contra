using UnityEngine;
using System.Collections;

public class StayOnSceneChange : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        Object.DontDestroyOnLoad(this.gameObject);
	}
}
