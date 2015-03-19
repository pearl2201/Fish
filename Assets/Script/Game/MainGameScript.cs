using UnityEngine;
using System.Collections;

public class MainGameScript : MonoBehaviour
{

    public Cannon cannon;
    public tk2dCamera mCamera;
    public GameObject bulletContainer;
    public GameObject bulletModel;
    public GameObject fishContainer;
    public static bool lockBullet;
    public EffectManager effectManager;
    public float health;
    public tk2dClippedSprite healthDisplay;
    private Rect healthRect = new Rect(0, 0, 1, 1);
    public GameObject endGamePopup;
    public bool isHourGlassRunning;
    public float hourGlassCountDown;
    public GameObject hourGlassObject;
    public float timeInsHealthCountDown;
    public float stepDecHealth;
    public tk2dTextMesh timeText;
    public Clock clock;
    public enum GameState
    {
        SET, BANHMY, GUN, BOM, PAUSE, ENDGAME
    }
    public GameState state;
    private static MainGameScript _instance;

    public static MainGameScript Instance()
    {
        if (_instance == null)
        {
            Debug.Log("loi roi em oi");
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
        lockBullet = false;
        state = GameState.GUN;
        health = 100;
        Time.timeScale = 1f;
        stepDecHealth = 0.025f;
        clock = new Clock();
    }

    // Update is called once per frame
    void Update()
    {
        if (state != GameState.ENDGAME)
        {
            clock.update(Time.deltaTime);
            timeText.text = clock.getText();
            if (!isHourGlassRunning)
            {
                timeInsHealthCountDown += Time.deltaTime;
                if (timeInsHealthCountDown >10){
                    stepDecHealth = stepDecHealth + 0.005f;
                    timeInsHealthCountDown = 0;
                }
                health -= stepDecHealth;
            }
            else
            {
                hourGlassCountDown -= Time.deltaTime;
                if (hourGlassCountDown <=0)
                {
                    hourGlassCountDown = 0;
                    isHourGlassRunning = false;
                    hourGlassObject.gameObject.SetActive(false);
                }
            }

            if (health < 0)
            {
                health = 0;
                Debug.Log("Game Over");
                endGamePopup.gameObject.SetActive(true);
                Time.timeScale = 0f;
                state = GameState.ENDGAME;
                //SoundManager.Instance().playEndGame();
            }
            else if (health > 100)
            {
                health = 100;
            }
            fixDisplayHealth();
        }
    }

    public void fixDisplayHealth()
    {
        healthRect.x = -(1 - health / 100);
        healthDisplay.ClipRect = healthRect;
    }

    public void addHealth(float healthAdd)
    {
        health += healthAdd;
    }

    public void fireBullet()
    {

        if (state == GameState.GUN)
        {
            if (!lockBullet)
            {
                // get position of mouse;
                Vector3 aPoint = cannon.transform.position;
                Vector3 bPoint = new Vector3();
                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    bPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
                else
                {
                    if (Input.touchCount > 0)
                        bPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                }
                bPoint.z = aPoint.z;
                Vector3 targetDir = bPoint - aPoint;
                Vector3 forward = new Vector3(0, 1, 0);

                float goc = Vector3.Angle(targetDir, forward);
                if (targetDir.x > 0)
                {
                    goc *= (-1);
                }

                cannon.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, goc));

                // khoi tao bullet
                GameObject go = Instantiate(bulletModel) as GameObject;
                go.transform.parent = bulletContainer.transform;
                Bullet bulletScript = go.GetComponent<Bullet>();
                bulletScript.setup(cannon.transform, bulletContainer.transform.position.z, goc, cannon.indexCannon);
                lockBullet = true;
                cannon.fire();
                CoinManager.Instance().subGold(cannon.indexCannon);
                SoundManager.Instance().playShot();
            }
        }
        else if (state == GameState.BANHMY)
        {
            state = GameState.GUN;
            effectManager.setLuoiActive(false, state);
            Bounds b = effectManager.getLuoiCatcherBuonds();
            Fish[] fishes = fishContainer.GetComponentsInChildren<Fish>();
            b.center = new Vector3(b.center.x, b.center.y, fishContainer.transform.position.z);
            Debug.Log("Bounds :" + b.ToString());
            for (int i = 0; i < fishes.GetLength(0); i++)
            {
                if (fishes[i].getBound().Intersects(b))
                {
                    fishes[i].gapBanhMy(effectManager.point);
                }
            }
            effectManager.runBanhMy();

        }
        else if (state == GameState.SET)
        {
            state = GameState.GUN;
            effectManager.setLuoiActive(false, state);
            Bounds b = effectManager.getLuoiCatcherBuonds();
            Fish[] fishes = fishContainer.GetComponentsInChildren<Fish>();
            b.center = new Vector3(b.center.x, b.center.y, fishContainer.transform.position.z);
            Debug.Log("Bounds :" + b.ToString());
            for (int i = 0; i < fishes.GetLength(0); i++)
            {
                if (fishes[i].getBound().Intersects(b))
                {
                    fishes[i].setDanh(effectManager.setEffect);
                }
            }
            effectManager.runSet();
            SoundManager.Instance().playEventSet();

        }
        else if (state == GameState.BOM)
        {
            state = GameState.GUN;
            effectManager.setLuoiActive(false, state);
            Bounds b = effectManager.getLuoiCatcherBuonds();
            Fish[] fishes = fishContainer.GetComponentsInChildren<Fish>();
            b.center = new Vector3(b.center.x, b.center.y, fishContainer.transform.position.z);
            Debug.Log("Bounds :" + b.ToString());
            for (int i = 0; i < fishes.GetLength(0); i++)
            {
                if (fishes[i].getBound().Intersects(b))
                {
                    fishes[i].gapBomb();
                }
            }
            effectManager.runBomb();
            SoundManager.Instance().playEventBomb();
        }
    }

    public void ReturnMenuScreen()
    {
        Time.timeScale = 1f;
        Application.LoadLevel("StartScene");
        SoundManager.Instance().playBtnClick();
    }

    public void OnSetTouchDown()
    {
        if (Prefs.Instance().getNumbSet() > 0)
        {
            state = GameState.SET;
            effectManager.setLuoiActive(true, state);
            Prefs.Instance().setNumbSet(Prefs.Instance().getNumbSet() - 1);
            effectManager.updateNumSet();
        }
    }

    public void OnBanhMyTouchDown()
    {
        state = GameState.BANHMY;
        effectManager.setLuoiActive(true, state);
    }

    public void OnHourGlassTouchDown()
    {
        if (Prefs.Instance().getNumbGlass() > 0)
        {
            hourGlassObject.gameObject.SetActive(true);
            isHourGlassRunning = true;
            hourGlassCountDown += Constants.TIME_EVENT;
            SoundManager.Instance().playEventHourGlass();
            Prefs.Instance().setNumbHourGlass(Prefs.Instance().getNumbGlass() - 1);
            effectManager.updateNumHourGlass();
        }
    }

    public void OnBomTouchDown()
    {
        if (Prefs.Instance().getNumbBomb() > 0)
        {
            state = GameState.BOM;
            effectManager.setLuoiActive(true, state);
            Prefs.Instance().setNumbBomb(Prefs.Instance().getNumbBomb() - 1);
            effectManager.updateNumBomb();
        }
    }

    public void resetGame()
    {
        Time.timeScale = 1f;
        Application.LoadLevel(Application.loadedLevel);
        SoundManager.Instance().playBtnClick();
    }
}
