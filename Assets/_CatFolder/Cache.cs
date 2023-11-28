using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    private static Dictionary<Collider2D, IHit> ihits = new Dictionary<Collider2D, IHit>();

    public static IHit GetIHit(Collider2D collider2D)
    {
        if (!ihits.ContainsKey(collider2D))
        {
            ihits.Add(collider2D, collider2D.GetComponent<IHit>());
        }

        return ihits[collider2D];
    }

}
