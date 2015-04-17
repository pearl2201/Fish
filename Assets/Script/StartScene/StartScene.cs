using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class StartScene : MonoBehaviour,CloseUpgradePopupInterface 
{

    public GameObject uiStartScene;
    public GameObject upgradePopup;

    public static StartScene _instance;

    public static StartScene Instance()
    {
        return _instance;
    }
    void Awake()
    {
        _instance = this;
    }
    public void StartCLick()
    {
        SoundManager.Instance().playBtnClick();
        Application.LoadLevel("GameScene");
    }

    public void ExitClick()
    {
        SoundManager.Instance().playBtnClick();
        Application.Quit();
    }

    public void UpgradeClick()
    {
        SoundManager.Instance().playBtnClick();
        uiStartScene.SetActive(false);
        if (upgradePopup!=null)
        {
            upgradePopup.SetActive(true);
        }
    }

    public void HideUpgradePopup()
    {

        uiStartScene.SetActive(true);
        if(upgradePopup != null)
        {
            upgradePopup.SetActive(false);
        }
    }

    public void CloseUpgradePopup()
    {
        upgradePopup.gameObject.SetActive(false);
        uiStartScene.gameObject.SetActive(true);
    }

    public void hideAllPopupMain()
    {
        upgradePopup.gameObject.SetActive(false);
        uiStartScene.gameObject.SetActive(false);
    }

    public void showUpgradePopupOnly()
    {
        upgradePopup.gameObject.SetActive(true);
    }

    public void showUiStartSceneOnly()
    {
        uiStartScene.gameObject.SetActive(true);
    }

    
}

