using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] LevelPart[] levelParts;
    [SerializeField] Vector3 nextPartPosition;
    [SerializeField] PlayerController player;
    [SerializeField] float distanceToSpawn;
    [SerializeField] Transform startPoint;
    [SerializeField] Background background;

    public Color plaformColor;
    public List<LevelPart> parts = new List<LevelPart>();
    int coin;
    float distance;

    
    void Update()
    {
        if(GameManager.Instance.IsState(GameState.GamePlay))
        {
            SpawnPlaform();
            coin = player.GetCoin();
            distance = player.GetDistance();
        }
        
    }

    public void OnInit()
    {
        background.OnInit();
        distance = 0;
        nextPartPosition = Vector2.zero;
        player.OnInit();
       
    }


    public Vector3 GetStartPoint() => startPoint.position;
    public PlayerController GetPlayer() => player;
    public int GetCoin() => coin;
    public float GetDistance() => distance;

    private void SpawnPlaform()
    {
        while(Vector2.Distance(player.TF.position, nextPartPosition) < distanceToSpawn)
        {
            LevelPart part = levelParts[UnityEngine.Random.Range(0, levelParts.Length)];

            Vector2 newPosition = new Vector2(nextPartPosition.x - part.GetStartPoint().x, 0);

            LevelPart newPart = SimplePool.Spawn<LevelPart>(part.poolType, newPosition, Quaternion.identity);
            newPart.OnInit();
            parts.Add(newPart);
            nextPartPosition = newPart.GetEndPoint();
        }
    }

    public void MainMenu()
    {
        UIManager.Instance.CloseAll();
        GameManager.Instance.ChangeState(GameState.MainMenu);
        UIManager.Instance.OpenUI<UI_MainMenu>();
        OnInit();
        OnDespawnObject();
    }

    private void OnDespawnObject()
    {
        for(int i =0; i< parts.Count; i++)
        {
            parts[i].OnDespawn();
        }
        parts.Clear();
    }

    public void RemoveToList(LevelPart levelPart)
    {
        parts.Remove(levelPart);
    }

    public void StartLevel()
    {
        UIManager.Instance.OpenUI<UI_GamePlay>();
        GameManager.Instance.ChangeState(GameState.GamePlay);
        OnInit();
    }

    public void FinishGame()
    {
        UIManager.Instance.CloseUI<UI_GamePlay>();
        GameManager.Instance.ChangeState(GameState.Finish);
        UIManager.Instance.OpenUI<UI_EndGame>().DisplayInfo(distance,coin);
    }

}
