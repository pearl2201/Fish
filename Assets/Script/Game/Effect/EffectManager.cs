using UnityEngine;
using System.Collections;

public class EffectManager : MonoBehaviour
{

    public GameObject setEffect;
    public GameObject set2Effect;
    public bool isLuoiActive;
    public BoxCollider luoiCatcher;
    public Vector3 point;
    public tk2dSprite iconEffect;
    public GameObject bombEffect;
    public GameObject banhmyEffect;
    public tk2dTextMesh numbBomb;
    public tk2dTextMesh numbHourGlass;
    public tk2dTextMesh numbSet;
    // Use this for initialization
    void Start()
    {
        luoiCatcher.gameObject.SetActive(false);
        banhmyEffect.gameObject.SetActive(false);
        isLuoiActive = false;
        updateNumBomb();
        updateNumHourGlass();
        updateNumSet();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLuoiActive)
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                point = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            }
            point.z = transform.position.z;
            luoiCatcher.gameObject.transform.position = point;
        }
    }

    public void setLuoiActive(bool t, MainGameScript.GameState state)
    {
        isLuoiActive = t;
        luoiCatcher.gameObject.SetActive(t);
        if (state == MainGameScript.GameState.BANHMY)
        {
            iconEffect.SetSprite("banhmy");  
        }
        else if (state == MainGameScript.GameState.SET)
        {

            iconEffect.SetSprite("set");  
        }
        else if (state == MainGameScript.GameState.BOM)
        {
            iconEffect.SetSprite("bomb");   
        }
    }

    public void runBanhMy()
    {
        StartCoroutine(hideBanhMy());
    }

    IEnumerator hideBanhMy()
    {
        banhmyEffect.gameObject.SetActive(true);
        banhmyEffect.transform.position = point;
        float t = 0;

        while (t<= Constants.TIME_EVENT)
        {
            t+= Time.deltaTime;
            yield return null;
        }
        banhmyEffect.gameObject.SetActive(false);
    }
    public void runSet()
    {
        GameObject go = Instantiate(set2Effect, luoiCatcher.gameObject.transform.position, Quaternion.identity) as GameObject;
        StartCoroutine(destroySet(go.GetComponent<tk2dSpriteAnimator>()));
    }

    IEnumerator destroySet(tk2dSpriteAnimator effect)
    {
        effect.Play("set2");
        while (effect.IsPlaying("set2"))
        {
            yield return null;
        }
        Destroy(effect.gameObject);
    }
    public void runBomb()
    {
        GameObject go = Instantiate(bombEffect, luoiCatcher.gameObject.transform.position, Quaternion.identity) as GameObject;
        StartCoroutine(destroyBomb(go.GetComponent<tk2dSpriteAnimator>()));
    }

    IEnumerator destroyBomb(tk2dSpriteAnimator effect)
    {
        effect.Play("ex");  
        while (effect.IsPlaying("ex"))
        {
            yield return null;
        }
        Destroy(effect.gameObject);
    }
    public Bounds getLuoiCatcherBuonds()
    {
        Bounds b = new Bounds(luoiCatcher.transform.position,new Vector3(1.91f,1.89f,1f))    ;
        return b;
    }

    public void updateNumSet()
    {
        numbSet.text = Prefs.Instance().getNumbSet() + "";
    }

    public void updateNumBomb()
    {
        numbBomb.text = Prefs.Instance().getNumbBomb() + "";
    }

    public void updateNumHourGlass()
    {
        numbHourGlass.text = Prefs.Instance().getNumbGlass() + "";
    }
}
