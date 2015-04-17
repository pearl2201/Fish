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
    private static EffectManager _instance;

    public static EffectManager Instance()
    {
        if (_instance == null)
        {
            Debug.Log("EffectManager is null");
        }
        return _instance;
    }

    void Awake()
    {
        _instance = this;
    }
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
        if(isLuoiActive)
        {
            if(Application.platform == RuntimePlatform.WindowsEditor)
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
        if(state == MainGameScript.GameState.BANHMY)
        {
            iconEffect.SetSprite("banhmy");
        }
        else if(state == MainGameScript.GameState.SET)
        {

            iconEffect.SetSprite("set");
        }
        else if(state == MainGameScript.GameState.BOM)
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

        while(t <= Constants.TIME_EVENT)
        {
            t += Time.deltaTime;
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
        while(effect.IsPlaying("set2"))
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
        while(effect.IsPlaying("ex"))
        {
            yield return null;
        }
        Destroy(effect.gameObject);
    }
    public Bounds getLuoiCatcherBuonds()
    {
        Bounds b = new Bounds(luoiCatcher.transform.position, new Vector3(1.91f, 1.89f, 1f));
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

    public GameObject modelDongHo;
    public GameObject modelSet;
    public GameObject modelBomb;
    public GameObject desDongHo;
    public GameObject desSet;
    public GameObject desBomb;
    public void dropSet(Vector3 position)
    {
        GameObject t = Instantiate(modelSet, position, Quaternion.identity) as GameObject;
        StartCoroutine(itemFlyTo(t, desSet.transform.position, TYPEITEMEFFECT.SET));
    }

    public void dropBomb(Vector3 position)
    {
        GameObject t = Instantiate(modelBomb, position, Quaternion.identity) as GameObject;
        StartCoroutine(itemFlyTo(t, desBomb.transform.position, TYPEITEMEFFECT.BOMB));
    }

    public void dropDongHo(Vector3 position)
    {
        GameObject t = Instantiate(modelDongHo, position, Quaternion.identity) as GameObject;
        StartCoroutine(itemFlyTo(t, desDongHo.transform.position, TYPEITEMEFFECT.SET));
    }

    
    IEnumerator itemFlyTo(GameObject item, Vector3 posTo, TYPEITEMEFFECT type)
    {
        float duration = 1f;
        float t = 0;
        Vector3 posFrom = item.gameObject.transform.position;
        Vector3 scaleFrom = Vector3.one;
        Vector3 scaleTo = new Vector3(1.5f, 1.5f, 1.5f); 
        while(t <= duration)
        {
            t += Time.deltaTime;
            item.gameObject.transform.position = Vector3.Lerp(posFrom, posTo, t / duration);
            item.gameObject.transform.localScale = Vector3.Lerp(scaleTo, scaleFrom, t / duration);
            yield return null;
        }
        Destroy(item.gameObject);
        if(type == TYPEITEMEFFECT.BOMB)
        {
            Prefs.Instance().setNumbBomb(Prefs.Instance().getNumbBomb() + 1);
            updateNumBomb();
        }
        else if(type == TYPEITEMEFFECT.DONGHO)
        {
            Prefs.Instance().setNumbHourGlass(Prefs.Instance().getNumbGlass() + 1);
            updateNumHourGlass();
        }
        else if(type == TYPEITEMEFFECT.SET)
        {
            Prefs.Instance().setNumbSet(Prefs.Instance().getNumbSet() + 1);
            updateNumSet();
        }
    }

    public enum TYPEITEMEFFECT
    {
        BOMB, SET, DONGHO
    }

    public void dropItem(Vector3 pos)
    {
        float r = Random.Range(0, 1f);
        if(r <= 0.1f)
        {
            r = Random.Range(0, 1f);
            if(r <= 0.33f)
            {
                dropBomb(pos);
            }
            else if(r < 0.367f)
            {
                dropDongHo(pos);
            }
            else
            {
                dropSet(pos);
            }
        }
    }
}
