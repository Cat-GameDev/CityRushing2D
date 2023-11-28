using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : GameUnit
{

    void OnTriggerEnter2D(Collider2D other)
    {
        IHit hit = Cache.GetIHit(other);
        if(hit != null)
        {
            hit.OnHit();
        }
    }


}
