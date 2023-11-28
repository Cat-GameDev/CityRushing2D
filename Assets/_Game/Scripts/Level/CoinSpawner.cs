using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : Singleton<CoinSpawner>
{
    int coinAmount;
    [SerializeField] int coinMin;
    [SerializeField] int coinMax;
    List<Coin> coins = new List<Coin>();
    public void SpawnCoin()
    {
        coinAmount = Random.Range(coinMin, coinMax);
        int additionalOffset = coinAmount/2;

        for (int i = 0; i < coinAmount; i++)
        {
            Vector2 newSpawnPosition = new Vector2(i - additionalOffset, 0);
            Coin coin = SimplePool.Spawn<Coin>(PoolType.Coin, new Vector2(transform.position.x + newSpawnPosition.x, transform.position.y + newSpawnPosition.y), Quaternion.identity);
            coins.Add(coin);
        }
    }

    public void DespawnCoin()
    {
        for(int i=0; i< coins.Count; i++)
        {
            coins[i].OnDespawn();
        }
    }

    public void RemoveToList(Coin coin)
    {
        coins.Remove(coin);
    }
}
