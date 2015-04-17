using UnityEngine;
using System.Collections;
using System;
public class Prefs
{
    private static Prefs _instance;

    private static string VERSION_CODE = "version_code";
    private static string UPGRADE_SPEED = "upgrade_speed";
    private static string UPGRADE_DAMAGE = "upgrade_damage";
    private static string COIN = "coin";
    private static string SOUND = "sound";
    private static string MUSIC = "music";
    private static string NUMB_SET = "numb_set";
    private static string NUMB_BOMB = "numn_bomb";
    private static string NUMB_HOURGLASS = "numb_hourglass";
    private static string LIVE = "Live";
    private static string LAST_TIME_ADD_LIVE = "last_time_add_live";
    public static bool isFirstplay;
    public static Prefs Instance()
    {
        if (_instance == null)
        {
            _instance = new Prefs();

        }
        return _instance;
    }

    public Prefs()
    {
        if (PlayerPrefs.HasKey(VERSION_CODE))
        {
            // getData()
           // initData();
            if (PlayerPrefs.GetInt(VERSION_CODE) != Constants.VERSION_CODE)
            {
                isFirstplay = true;
                initData();
            }
        }
        else
        {
            isFirstplay = false;
            Debug.Log("no key");
            initData();
        }
    }

    private void initData()
    {
        Debug.Log("init Data");

        PlayerPrefs.SetInt(VERSION_CODE, Constants.VERSION_CODE);
        setCoin(1000);
        for (int i = 1; i < 8; i++)
        {
            setDamage(i, 1);
            setSpeed(i, 1);
            setNumbBomb(5);
            setNumbHourGlass(5);
            setNumbSet(5);
            setSound(true);
            setMusic(true);
            setLive(50);
            SetLastTimeAddLive(DateTime.Now);
        }
        PlayerPrefs.Save();
    }
    public void SetLastTimeAddLive(DateTime time)
    {

        SaveString(LAST_TIME_ADD_LIVE, time.ToString());
    }

    public DateTime GetLastTimeAddLive()
    {
        return Convert.ToDateTime(GetString(LAST_TIME_ADD_LIVE));
    }
    public void setDamage(int idCannon, int value)
    {
        SetInt(UPGRADE_DAMAGE + idCannon, value);
    }

    public void setSpeed(int idCannon, int value)
    {
        SetInt(UPGRADE_SPEED + idCannon, value);
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

    public bool getSound()
    {
        return GetBool(SOUND);
    }

    public void setSound(bool active)
    {
        SaveBool(SOUND, active);
    }

    public bool getMusic()
    {
        return GetBool(MUSIC);
    }

    public void setMusic(bool active)
    {
        SaveBool(MUSIC, active);
    }

    public void setNumbSet(int numb)
    {
        SetInt(NUMB_SET, numb);
    }

    public int getNumbSet()
    {
        return GetInt(NUMB_SET);
    }
    public void setNumbBomb(int numb)
    {
        SetInt(NUMB_BOMB, numb);
    }

    public int getNumbBomb()
    {
        return GetInt(NUMB_BOMB);
    }
    public void setNumbHourGlass(int numb)
    {
        SetInt(NUMB_HOURGLASS, numb);
    }

    public int getNumbGlass()
    {
        return GetInt(NUMB_HOURGLASS);
    }


    public void setLive(int live)
    {
        SetInt(LIVE, live);
    }

    public int getLive()
    {
        return GetInt(LIVE);
    }
    ////////////////////////////////////////////

    public int GetInt(string name)
    {
        if (PlayerPrefs.HasKey(name))
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
        if (PlayerPrefs.HasKey(name))
        {
            if (PlayerPrefs.GetInt(name) == 1)
                return true;
            else
                return false;
        }
        else
            return false;
    }

    public void SaveBool(string name, bool value)
    {
        if (value)
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
