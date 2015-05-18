using UnityEngine;
using System.Collections;

public class RecycleOnAnimationEnd : MonoBehaviour 
{
    public void Recycle()
    {
        //Destroy(gameObject);
        gameObject.Recycle();
    }

    public  void Destroy()
    {
        Destroy(gameObject);
    }
}
