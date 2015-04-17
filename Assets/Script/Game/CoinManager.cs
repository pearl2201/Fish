using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CoinManager : MonoBehaviour
{

    public GameObject coinModel;
    public Transform endOfCoin;
    public tk2dTextMesh valueText;
    public tk2dTextMesh[] valueTextList;
    public int valueG;
    public string tmp;
    private static CoinManager _instance;
    public static CoinManager Instance()
    {
        return _instance;
    }

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {

        valueG = Prefs.Instance().getCoin(); ;
        fixText();
    }
    public void createCoin(int value, Vector3 pos)
    {
        valueG += value;
        Prefs.Instance().setCoin(valueG);
      
        
        GameObject go = Instantiate(coinModel, pos, Quaternion.identity) as GameObject;
        CoinModel coinScript = go.GetComponent<CoinModel>();
        if(coinScript != null)
        {
            coinScript.runAnim(value, endOfCoin,this);
        }
        SoundManager.Instance().playCoin();
    }
    public int tmpLeng;
    public void fixText()
    {
        tmp = valueG + "";
        tmpLeng = tmp.Length;
        for(int i = 0; i < (6 - tmpLeng); i++  )
        {
            tmp = "0" + tmp;
        }
        for (int i =0; i<valueTextList.Length;i++)
        {
            valueTextList[i].text = tmp.Substring(i, 1);
        }
    }

    public void subGold(int value)
    {
        valueG -= value;
        if(valueG <= 0)
        {
            valueG = 0;
            MainGameScript.Instance().hetCoin();
        }
        Prefs.Instance().setCoin(valueG);
        fixText();
        
    }
}

