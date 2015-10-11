using UnityEngine;
using System.Collections;
using Prime31.ZestKit;

public class FollowTouch : MonoBehaviour 
{
    Transform _transform;                            //local cached transform
    public float camOrthoSize = 5f;                 //camera's orthographic size
    protected Material  spriteRendererMat;

	// Use this for initialization
	void Start () 
    {
        _transform = GetComponent<Transform>();
        spriteRendererMat = GetComponent<SpriteRenderer>().material;
	}
	
	// Update is called once per frame
	void Update () 
    {
#if UNITY_EDITOR
        var v3 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        v3.z = 0;
        if (v3.x <= 0)
            v3.x = 0;
        _transform.position = v3;
        if (Input.GetMouseButton(0))
            spriteRendererMat.color = new Color(spriteRendererMat.color.r, spriteRendererMat.color.g, spriteRendererMat.color.b, 0);
        else
            spriteRendererMat.color = new Color(spriteRendererMat.color.r, spriteRendererMat.color.g, spriteRendererMat.color.b, 1);
#endif
#if UNITY_ANDROID
        if(Input.touchCount > 0)
        {
            spriteRendererMat.ZKalphaTo(1);
            foreach(Touch touch in Input.touches)
            {
                if(touch.phase != TouchPhase.Canceled && touch.phase != TouchPhase.Ended &&  touch.position.x > Screen.width/2)
                {
                    var vec3 = Camera.main.ScreenToWorldPoint(touch.position);
                    vec3.z = 0;
                    _transform.position = vec3;
                    return;
                }
            }
        }
        else
        {
            if(spriteRendererMat.color.a == 1)
            {
                spriteRendererMat.ZKalphaTo(0);
            }
        }
#endif

    }
}
