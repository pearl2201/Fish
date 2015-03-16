using UnityEngine;
using System.Collections;

public class CoreFish
{
    public string nameFish;
    public int coin;
    public float captureRate;
    public int maxNumGroup;
    public float minSpeed;
    public float maxSpeed;
    public float indexFish;

    public CoreFish(string nameFish, int coin, float captureRate, int maxNumGroup, float minSpeed, float maxSpeed, int indexFish)
    {
        this.nameFish = nameFish;
        this.coin = coin;
        this.captureRate = captureRate;
        this.maxNumGroup = maxNumGroup;
        this.minSpeed = minSpeed;
        this.maxSpeed = maxSpeed;
        this.indexFish = indexFish;
    }


    public CoreFish()
    {

    }

}

