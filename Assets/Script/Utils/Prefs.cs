﻿using UnityEngine;
using System.Collections;

public class Prefs
{
    private static Prefs _instance;

    private static string VERSION_CODE = "version_code";
    private static string UPGRADE_SPEED = "upgrade_speed";
    private static string UPGRADE_DAMAGE = "upgrade_damage";
    private static string COIN = "coin";
    public static Prefs Instance()
    {
        if(_instance == null)
        {
            _instance = new Prefs();

        }
        return _instance;
    }

    public Prefs()
    {
        if(PlayerPrefs.HasKey(VERSION_CODE))
        {
            // getData()

            if(PlayerPrefs.GetInt(VERSION_CODE) != Constants.VERSION_CODE)
            {
                initData();
            }
        }
        else
        {
            Debug.Log("no key");
            initData();
        }
    }

    private void initData()
    {
        Debug.Log("init Data");

        PlayerPrefs.SetInt(VERSION_CODE, Constants.VERSION_CODE);
        setCoin(10000);
        for(int i = 1; i < 8; i++ )
        {
            setDamage(i, 1);
            setSpeed(i, 1);
        }
            PlayerPrefs.Save();
    }

    public void setDamage(int idCannon, int value)
    {
        SetInt(UPGRADE_DAMAGE + idCannon, value);
    }

    public void setSpeed(int idCannon, int value)
    {
        SetInt(UPGRADE_SPEED+idCannon, value);
    }

    public int getDamage(int idCannon)
    {
        return GetInt(UPGRADE_DAMAGE + idCannon);
    }

    public int getSpeed(int idCannon)
    {
        return GetInt(UPGRADE_SPEED + idCannon);
    }

    public int getCoin()
    {
        return GetInt(COIN);
    }

    public void setCoin(int coin)
    {
        SetInt(COIN, coin);
    }
    ////////////////////////////////////////////

    public int GetInt(string name)
    {
        if(PlayerPrefs.HasKey(name))
        {
            return PlayerPrefs.GetInt(name);
        }
        else
            return 0;
    }

    public void SetInt(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
        PlayerPrefs.Save();
    }

    public bool GetBool(string name)
    {
        if(PlayerPrefs.HasKey(name))
        {
            if(PlayerPrefs.GetInt(name) == 1)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    public void SaveBool(string name, bool value)
    {
        if(value)
            PlayerPrefs.SetInt(name, 1);
        else
            PlayerPrefs.SetInt(name, 0);
        PlayerPrefs.Save();
    }

    public void SaveString(string name, string value)
    {
        PlayerPrefs.SetString(name, value);
        PlayerPrefs.Save();
    }

    public string GetString(string name)
    {
        return PlayerPrefs.GetString(name);
    }
}