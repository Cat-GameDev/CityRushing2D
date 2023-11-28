using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Setting : UICanvas
{
    public void ExitButton()
    {
        GameManager.Instance.ChangeState(GameState.MainMenu);
        UIManager.Instance.OpenUI<UI_MainMenu>();
        Close(0f);
    }

    public void DayButton()
    {
        
    }
    public void NightButton()
    {
        
    }
    public void RandomButton()
    {
        
    }
}
