using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_GamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI distanceText;
    [SerializeField] TextMeshProUGUI coinsText;

    [SerializeField] Image heartEmpty;
    [SerializeField] Image heartFull;
    PlayerController player;
    float distance;
    int coins;

    void Start()
    {
        //player = GameManager.instance.player;
        InvokeRepeating("UpdateInfo", 0, .2f);
    }

    private void UpdateInfo()
    {
        player = LevelManager.Instance.GetPlayer();
        distance = LevelManager.Instance.GetDistance();
        coins = LevelManager.Instance.GetCoin();

        if(distance > 0)
            distanceText.SetText(distance.ToString("F2") + "  m");
        else
            distanceText.SetText("0 m");

        if(coins > 0)
            coinsText.SetText(coins.ToString("#,#"));
        else
            coinsText.SetText("0");
        
        heartEmpty.enabled = !player.GetExtraLife();
        heartFull.enabled = player.GetExtraLife();
    }

    public void MuteButton()
    {

    }

    public void PauseButton()
    {
        GameManager.Instance.ChangeState(GameState.Setting);
        UIManager.Instance.OpenUI<UI_Pause>();
        Close(0f);
    }

    public void JumpButton()
    {
        player.Jump();
    }

    public void SildeButton()
    {
        player.SildeButton();
    }
}
