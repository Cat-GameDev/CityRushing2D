using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] ParallaxBackground[] parallaxBackgrounds;
    Vector3 backGroundPosition = new Vector3(16.3999996f,3.79999995f,0);
    public void OnInit()
    {
        transform.position = backGroundPosition;
        for(int i=0;i<parallaxBackgrounds.Length;i++)
        {
            parallaxBackgrounds[i].OnInit();
        }
    }
}
