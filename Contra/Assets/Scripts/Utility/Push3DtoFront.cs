using UnityEngine;
using System.Collections;

public class Push3DtoFront : MonoBehaviour 
{
    public string layerToPushTo;

	// Use this for initialization
	void Awake () 
    {
        GetComponent<Renderer>().sortingLayerID = LayerMask.NameToLayer(layerToPushTo);
	}
}
