using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer))]
public class ScrollingTexture : MonoBehaviour 
{
    Material mat;
    Vector2 offset;
    public float scrollSpeedModifer = 0.5f;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        offset = mat.mainTextureOffset;
    }
	// Update is called once per frame
	void Update () 
    {
        offset.x += Time.deltaTime * scrollSpeedModifer * StaticGameVariables.gameSpeed;
        mat.mainTextureOffset = offset;
    }
}
