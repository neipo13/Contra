using UnityEngine;
using UnityEngine.UI;
using Prime31.ZestKit;
using System.Collections;

public class ExpandIn : MonoBehaviour 
{
    RectTransform trans;
    Vector3 originalScale;
    public float animTime = 3.0f;

	// Use this for initialization
	void Start () 
    {
        trans = GetComponent<RectTransform>();
        originalScale = trans.localScale;
        trans.localScale = new Vector3(originalScale.x, 0, originalScale.z);
        BeginExpand();
	}

    void BeginExpand()
    {
        trans.ZKlocalScaleTo(originalScale, animTime).setEaseType(EaseType.ElasticOut).start();
    }
}
