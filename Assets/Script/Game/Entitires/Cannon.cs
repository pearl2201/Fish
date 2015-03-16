using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour
{
    public int indexCannon = 1;
    public tk2dSprite sprite;
    public int MAX_CANNON_INDEX = 7;
    public int MIN_CANNON_INDEX = 1;
    float timeTmp = 0;
    float timeFire = 0.1f;
    bool runAnim;
    // Use this for initialization
    void Start()
    {
        sprite.SetSprite("sung" + indexCannon + "-1");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void nextCannon()
    {
        if(!runAnim)
        {
            indexCannon++;
            if(indexCannon > MAX_CANNON_INDEX)
            {
                indexCannon = MIN_CANNON_INDEX;
            }
            sprite.SetSprite("sung" + indexCannon + "-1");
        }
    }

    public void oldCannon()
    {
        if(!runAnim)
        {
            indexCannon--;
            if(indexCannon < MIN_CANNON_INDEX)
            {
                indexCannon = MAX_CANNON_INDEX;
            }
            sprite.SetSprite("sung" + indexCannon + "-1");
        }
    }

    public void fire()
    {
        StartCoroutine(cannonFireAnimRun());
    }

    IEnumerator cannonFireAnimRun()
    {
        runAnim = true;
        bool runImage1 = true;
        bool runImage2 = false;
        timeTmp = 0;
        while(timeTmp < timeFire)
        {
            timeTmp += Time.deltaTime;
            if(runImage1 && timeTmp > timeFire * 1 / 3)
            {
                runImage2 = true;
                runImage1 = false;
                sprite.SetSprite("sung" + indexCannon + "-2");
            }
            else if(runImage2 && timeTmp > timeFire * 2 / 3)
            {
                runImage2 = false;
                sprite.SetSprite("sung" + indexCannon + "-3");
            }
            yield return null;
        }
        sprite.SetSprite("sung" + indexCannon + "-1");
        runAnim = false;
    }
}

