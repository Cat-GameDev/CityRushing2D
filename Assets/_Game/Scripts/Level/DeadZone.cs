using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        IHit hit = Cache.GetIHit(other);
        if(hit != null)
        {
            Debug.Log(2);
            hit.OnHit();
        }
    }
}
