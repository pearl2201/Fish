using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FishManager : MonoBehaviour
{

    // Use this for initialization
    public List<Fish> fish1;
    public List<Fish> fish2;
    public List<Fish> fish3;
    public List<Fish> fish4;
    public List<Fish> fish5;
    public List<Fish> fish6;
    public List<Fish> fish7;
    public List<Fish> fish8;
    public List<Fish> fish9;
    public List<Fish> fish10;
    public List<Fish> shark1;
    public List<Fish> shark2;
    public GameObject fishModel;
    public float tm;
    public float duration = 1f / 30f;
    public GameObject[] fishModelCenter;

    void Start()
    {
        float limit = Random.Range(2, 4);
        for(int i = 0; i < limit; i++)
        {
            spawnFishByChance(fish1, TYPEFISH.FISH1, 0);
        }
        limit = Random.Range(0, 4);
        for(int i = 0; i < limit; i++)
        {
            spawnFishByChance(fish1, TYPEFISH.FISH2, 1);
        }
        limit = Random.Range(0, 4);
        for(int i = 0; i < limit; i++)
        {
            spawnFishByChance(fish1, TYPEFISH.FISH3, 2);
        }
        limit = Random.Range(0, 4);
        for(int i = 0; i < limit; i++)
        {
            spawnFishByChance(fish1, TYPEFISH.FISH4, 3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        tm += Time.deltaTime;
        if(tm >= 0.5f)
        {
            tm = 0;
            if(transform.childCount < Constants.LIMIT_FISH_IN_SCREEN)
            {
                int t = Random.Range(0, 81);
                if(t <= 12)
                    spawnFishByChance(fish1, TYPEFISH.FISH1,0);
                else if(t <= 23)
                    spawnFishByChance(fish2, TYPEFISH.FISH2,1);
                else if(t <= 33)
                    spawnFishByChance(fish3, TYPEFISH.FISH3,2);
                else if(t <= 42)
                    spawnFishByChance(fish4, TYPEFISH.FISH4,3);
                else if(t <= 50)
                    spawnFishByChance(fish5, TYPEFISH.FISH5,4);
                else if(t <= 57)
                    spawnFishByChance(fish6, TYPEFISH.FISH6,5);
                else if(t <= 63)
                    spawnFishByChance(fish7, TYPEFISH.FISH7,6);
                else if(t <= 69)
                    spawnFishByChance(fish8, TYPEFISH.FISH8,7);
                else if(t <= 74)
                    spawnFishByChance(fish9, TYPEFISH.FISH9,8);
                else if(t <= 77)
                    spawnFishByChance(fish10, TYPEFISH.FISH10,9);
                else if(t == 79)
                    spawnFishByChance(shark1, TYPEFISH.SHARK1,10);
                else if(t == 80)
                    spawnFishByChance(shark2, TYPEFISH.SHARK2,11);
            }
        }
    }

    void spawnFishByChance2(List<Fish> fishList, TYPEFISH type)
    {
        if(fishList.Count < DataFishContainer.GetCoreFish(type).maxNumGroup)
        {
            //if(Random.Range(0, 1f) > 0.9)
            //{
            //    spawnFish(fishList, type);

            //}
            int t = Random.Range(0, DataFishContainer.GetCoreFish(type).maxNumGroup - fishList.Count);
            for(int i = 0; i < t; i++)
            {
                spawnFish(fishList, type);
            }
        }
    }

    void spawnFishByChance(List<Fish> fishList, TYPEFISH type, int kind)
    {
        if(fishList.Count < DataFishContainer.GetCoreFish(type).maxNumGroup)
        {
            //if(Random.Range(0, 1f) > 0.9)
            //{
            //    spawnFish(fishList, type);

            //}
            int t = Random.Range(0, DataFishContainer.GetCoreFish(type).maxNumGroup );
            int localScale = MyExtensions.RandomOneOrMinus();
            float posX = -(Constants.HALF_SCREEN_UNIT_WIDTH ) * localScale;
            for(int i = 0; i < t; i++)
            {
                GameObject go = Instantiate(fishModelCenter[kind], transform.position, Quaternion.identity) as GameObject;
                go.transform.parent = transform;
                Fish fishScript = go.GetComponent<Fish>();
                fishList.Add(fishScript);

                fishScript.setup(type, fishList,localScale,posX + -localScale*(i*0.1f));
            }
        }
    }

    void spawnFish(List<Fish> fishList, TYPEFISH type)
    {
        GameObject go = Instantiate(fishModel, transform.position, Quaternion.identity) as GameObject;
        go.transform.parent = transform;
        Fish fishScript = go.GetComponent<Fish>();
        fishList.Add(fishScript);

        fishScript.setup(type, fishList);


    }
}
