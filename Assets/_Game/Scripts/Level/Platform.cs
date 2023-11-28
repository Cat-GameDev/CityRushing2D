using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
    [SerializeField] SpriteRenderer headerSr;

    private void Start()
    {
        if(headerSr != null)
        {
            headerSr.transform.parent = transform.parent;
            headerSr.transform.localScale = new Vector2(sr.bounds.size.x, .2f);
            headerSr.transform.position = new Vector2(transform.position.x, sr.bounds.max.y - .1f);
        }
        
    }

    public void OnInit()
    {
        if(headerSr != null)
        {
            headerSr.color = sr.color;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(headerSr != null)
        {
            headerSr.color = LevelManager.Instance.plaformColor;
        }
        
    }
}
