﻿using UnityEngine;
using System.Collections;

public class ItemPopup : MonoBehaviour
{

    public tk2dTextMesh speedValue;
    public tk2dSprite speedUpgradeBtnBg;
    public tk2dTextMesh speedUpgradeText;
    public tk2dTextMesh damageValue;
    public tk2dSprite damageUpgradeBtnBg;
    public tk2dTextMesh damageUprageText;
    public tk2dSprite infoImage;
    public int idCannon;
    public tk2dSprite iconImage;
    public tk2dSprite bg;
    public UpgradePopup upgradePopup;
    // Use this for initialization
    void Start()
    {
        
    }

    public void loadItem()
    {
        setDamage();
        setSpeed();
        bg.SetSprite("nc1");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void upgradeSpeedOnDown()
    {
        if(Prefs.Instance().getSpeed(idCannon) != 10)
            speedUpgradeBtnBg.SetSprite("nut-nc-2-cham");
    }

    public void upgradeSpeedOnUp()
    {
        if(Prefs.Instance().getSpeed(idCannon) != 10)
        {
            speedUpgradeBtnBg.SetSprite("nut-nc-1");
            //check money

            // neu hok du money thi thong bao popup la hok du tien

            if (Prefs.Instance().getCoin() >=300)
            {
                Prefs.Instance().setCoin(Prefs.Instance().getCoin() - 300);
                Prefs.Instance().setSpeed(idCannon,Prefs.Instance().getSpeed(idCannon) + 1);
                // thong bao thanh cong
                setSpeed();
                PopupText.Instance().Show("Nâng cấp thành công.", 1f);
            }
            else
            {
                // thong bao hok du tien
                PopupText.Instance().Show("Không đủ vàng để thực hiện.", 1f);
            }
        }
    }
    public void upgradeDamageOnDown()
    {
        if(Prefs.Instance().getSpeed(idCannon) != 10)
            damageUpgradeBtnBg.SetSprite("nut-nc-2-cham");
    }

    public void upgradeDamageOnUp()
    {
        if(Prefs.Instance().getSpeed(idCannon) != 10)
        {
            damageUpgradeBtnBg.SetSprite("nut-nc-1");
            //check money

            // neu du money, thi thong bao ra la nang cap thanh cong

            // neu hok du money thi thong bao popup la hok du tien

            if(Prefs.Instance().getCoin() >= 300)
            {
                Prefs.Instance().setCoin(Prefs.Instance().getCoin() - 300);
                Prefs.Instance().setDamage(idCannon, Prefs.Instance().getDamage(idCannon) + 1);
                setDamage();
                PopupText.Instance().Show("Nâng cấp thành công.");
            }
            else
            {
                // thong bao hok du tien
                PopupText.Instance().Show("Không đủ vàng để thực hiện.");
            }
        }
    }

    public void onItemClick()
    {
        upgradePopup.click(idCannon);
    }

    public void unLoadItem()
    {
        bg.SetSprite("nc-2");
    }

    void setSpeed()
    {
        speedValue.text = "Tốc độ: " + Prefs.Instance().getSpeed(idCannon) + "/10";
        if(Prefs.Instance().getSpeed(idCannon) == 10)
        {
            speedUpgradeBtnBg.color = new Color(0.2f, 0.2f, 0.2f, 1f);
            speedUpgradeText.text = "Full";
        }
        else
        {
            speedUpgradeBtnBg.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    void setDamage()
    {
        damageValue.text = "Sát thương: " + Prefs.Instance().getDamage(idCannon) + "/10";
        if(Prefs.Instance().getDamage(idCannon) == 10)
        {
            damageUpgradeBtnBg.color = new Color(0.2f, 0.2f, 0.2f, 1f);
            damageUprageText.text = "Full";
        }
        else
        {
            damageUpgradeBtnBg.color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
