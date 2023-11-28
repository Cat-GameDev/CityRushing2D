using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_MainMenu : UICanvas
{
    [SerializeField] TextMeshProUGUI distanceText;
    [SerializeField] TextMeshProUGUI coinsText;

    Vector3 position = new Vector3(-26.2299995f,-4.84000015f);

    public override void Open()
    {
        AudioManager.Instance.PlayRandomMusic();
        base.Open();
        SimplePool.Spawn<LevelPart>(PoolType.LevelPart6, position, Quaternion.identity);
        

        int coinValue = UserData.Ins.LoadCoins();
        float distantValue = UserData.Ins.LoadHightDistance();
        coinsText.SetText("Coin: " + coinValue.ToString("#,#"));
        distanceText.SetText("Best Distance: " + distantValue.ToString("#,#"));
    }

    public void StartButton()
    {
        GameManager.Instance.ChangeState(GameState.GamePlay);
        LevelManager.Instance.StartLevel();
        Close(0f);
    }

    public void SettingButton()
    {
        GameManager.Instance.ChangeState(GameState.Setting);
        UIManager.Instance.OpenUI<UI_Setting>();
    }

    public void MuteButton()
    {

    }

    public void ShopButton()
    {
        GameManager.Instance.ChangeState(GameState.Shop);
        UIManager.Instance.OpenUI<UI_Shop>();
        Close(0f);
    }


}
