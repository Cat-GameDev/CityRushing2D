using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPart : GameUnit
{
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] float distanceToDelete;
    [SerializeField] PlayerController player;
    [SerializeField] CoinSpawner[] coinSpawner;
    [SerializeField] Platform[] platforms;
    
    private void Start() 
    {
        player = LevelManager.Instance.GetPlayer();
    }

    private void Update() 
    {
        if(player != null || GameManager.Instance.IsState(GameState.GamePlay))
        {
            Vector2 direction = endPoint.position - player.TF.position;

            if(Vector2.Dot(direction.normalized, player.TF.right) < 0)
            {
                if(Vector2.Distance(player.TF.position, endPoint.position) > distanceToDelete)
                {
                    OnDespawn();
                }
            }
            
        }

    }

    public void OnInit()
    {
        //coinSpawner.SpawnCoin(transform.position);
        if(coinSpawner != null)
        {
            for(int i =0; i< coinSpawner.Length; i++)
            {
                coinSpawner[i].SpawnCoin();
            }
            
        }
        
    }

    public Vector3 GetStartPoint()
    {
        return startPoint.position;
    }

    public Vector3 GetEndPoint()
    {
        return endPoint.position;
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        LevelManager.Instance.RemoveToList(this);
        for(int i =0; i< platforms.Length; i++)
        {
            platforms[i].OnInit();
        }

        for(int i =0; i< coinSpawner.Length; i++)
        {
            coinSpawner[i].DespawnCoin();
        }
    }



    

}
