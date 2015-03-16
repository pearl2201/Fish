using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using System.Collections;
public class CoinModel : MonoBehaviour
{

    public tk2dTextMesh value;
    public tk2dSpriteAnimator coin;
    public Transform endPosOfValue;
    public Transform endOfCoin;
    public Vector3 beginPosOfValue;
    public Vector3 beginOfCoin;
    public void runAnim(int coinValue, Transform endOfCoin)
    {
        if(coinValue <= 10)
        {
            coin.Play("CoinBac");
        }
        else
        {
            coin.Play("CoinVip");
        }
        value.text = "x" +coinValue;
        beginOfCoin = coin.transform.position;
        beginPosOfValue = value.transform.position;
        this.endOfCoin = endOfCoin;
        StartCoroutine(run());
    }

    IEnumerator run()
    {
        float sumDeltaTime = 0;
        float duration = 0.6f;
        while (sumDeltaTime <=duration)
        {
            sumDeltaTime += Time.deltaTime;
            coin.transform.position = Vector3.Lerp(beginOfCoin, endOfCoin.position, sumDeltaTime);
            value.transform.position = Vector3.Lerp(beginPosOfValue, endPosOfValue.transform.position, sumDeltaTime);
            yield return null;
        }
        Destroy(this.gameObject);

        
    }
}

