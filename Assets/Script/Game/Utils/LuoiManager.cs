using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LuoiManager : MonoBehaviour
{
    public GameObject luoiModel;
    public Vector3 sizeLuoiNho = new Vector3(0.8f, 0.8f, 1f);
    public Vector3 sizeLuoiChuan = new Vector3(1, 1, 1);
    public int coinAdd;
    public GameObject fishContainer;
    private static LuoiManager _instance;

    public static LuoiManager Instance()
    {
        return _instance;
    }

    void Awake()
    {
        _instance = this;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setupLuoi(Bullet bullet)
    {
        GameObject go = Instantiate(luoiModel) as GameObject;

        go.transform.position = bullet.transform.position;
        tk2dSprite luoiSprite = go.GetComponent<tk2dSprite>();
        luoiSprite.SetSprite("vo" + bullet.indexCannon);
        Destroy(bullet.gameObject);
        coinAdd = 0;
        //check fish manager, kiem tra xem co con ca nao ao luoi hok
        Fish[] fishes = fishContainer.GetComponentsInChildren<Fish>();
        Bounds luoiBound = new Bounds(go.transform.position,new Vector3(go.GetComponent<BoxCollider2D>().size.x,go.GetComponent<BoxCollider2D>().size.y,1));
        for(int i = 0; i < fishes.GetLength(0); i++)
        {
            if(luoiBound.Intersects(fishes[i].getBound()))
            {
                if(fishes[i].canbeCapture(bullet.indexCannon))
                {
                    fishes[i].beCapture();
                    Debug.Log(luoiSprite.GetBounds().ToString());
                    Debug.Log(fishes[i].getBound().ToString());
                }
            }
        }
        // neu co ca vao luoi thi cho ca ve trang thai capture
        
        StartCoroutine(runLuoi(go));

    }

    IEnumerator runLuoi(GameObject luoi)
    {
        // scale luoi trong 0.1 s;
        float t = 0;
        luoi.transform.localScale = sizeLuoiNho;
        while(t <= 1f)
        {
            t += Time.deltaTime;
            luoi.transform.localScale = Vector3.Lerp(sizeLuoiNho, sizeLuoiChuan, t / 0.1f);
            yield return null;
        }
        MainGameScript.lockBullet = false;

        Destroy(luoi.gameObject);

        // display tien
       

    }
}

