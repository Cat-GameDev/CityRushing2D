using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : GameUnit
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        IHit hit = Cache.GetIHit(other);
        if (hit != null)
        {
            hit.Coin(); 
            AudioManager.Instance.PlaySFX("coin");
        }
        OnDespawn();
        CoinSpawner.Instance.RemoveToList(this);
    }

}
