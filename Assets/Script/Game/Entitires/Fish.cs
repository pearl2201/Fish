using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Fish : MonoBehaviour
{

    public tk2dSpriteAnimator ani;
    public TYPEFISH typeFish;
    public Vector3 velocity;
    public bool hadBeenCaptured;
    public Vector3 pos;
    public bool canMoving;
    public bool canRotate;
    public bool canChangeDirection;
    public CoreFish coreFish;
    public Vector2 size;
    public float defaultSpeed;
    public bool hadPassScreen;
    public Vector2 sizeBoxCollider;

    public float stepRotation;
    public float angle;
    public Vector3 angleVec;
    public float t;
    public bool isSetDanh;
    public GameObject set;


    public int localScale;
    public int isMoveDown;
    private List<Fish> listFish;
    public GameObject pivotPoint;
    public bool eatBanhMy;
    public Vector3 toadoBanhMy;

    public void setup(TYPEFISH type, List<Fish> fishLish)
    {
        this.typeFish = type;
        this.listFish = fishLish;
        canMoving = true;
        coreFish = DataFishContainer.GetCoreFish(type);
        ani.Stop();
        ani.Play(coreFish.nameFish + "Swim");
        // ani.Play("fish4Swim");
        Debug.Log("has changime fish to sw " + coreFish.nameFish + "Swim " + ani.CurrentClip.name);

        canRotate = MyExtensions.RandomBoolean();
        canChangeDirection = MyExtensions.RandomBoolean();
        isMoveDown = MyExtensions.RandomOneOrMinus();
        sizeBoxCollider = ani.GetComponent<BoxCollider2D>().size;

        size = ani.GetComponent<tk2dSprite>().GetBounds().size;


        localScale = MyExtensions.RandomOneOrMinus(); ;
        transform.localScale = new Vector3(localScale, 1, 1);
        defaultSpeed = Random.Range(coreFish.minSpeed, coreFish.maxSpeed) / 100 * localScale;
        if (MainGameScript.Instance().clock.time < 20)
        {

        }
        else if (MainGameScript.Instance().clock.time < 40)
        {
            defaultSpeed *= 1.5f;
        }
        else if (MainGameScript.Instance().clock.time < 60)
        {
            defaultSpeed *=2f;
        }
        else if (MainGameScript.Instance().clock.time < 60)
        {
            defaultSpeed *= 3f;
        }
        else
        {
            defaultSpeed *= 4f;
        }
        angle = Random.Range(0, 10) * isMoveDown;
        stepRotation = Random.Range(0, 0.1f) * isMoveDown;
        pos = transform.position;
        pos.y = -Random.Range(0, Constants.HALF_SCREEN_UNIT_HEIGHT - size.y - 0.5f) * isMoveDown;
        pos.x = -(Constants.HALF_SCREEN_UNIT_WIDTH + size.x) * localScale;

        if (!canRotate)
        {
            velocity.y = 0;
            velocity.x = defaultSpeed;
        }
        else
        {
            velocity.x = defaultSpeed * Mathf.Cos(angle * Mathf.Deg2Rad);
            velocity.y = defaultSpeed * Mathf.Sin(angle * Mathf.Deg2Rad);
        }

        transform.position = pos;
        angleVec.z = angle;
        transform.localRotation = Quaternion.Euler(angleVec);

    }

    public void setup(TYPEFISH type, List<Fish> fishLish, int localScale, float posX)
    {
        this.typeFish = type;
        this.listFish = fishLish;
        canMoving = true;
        coreFish = DataFishContainer.GetCoreFish(type);
        ani.Stop();
        ani.Play(coreFish.nameFish + "Swim");
        // ani.Play("fish4Swim");


        canRotate = MyExtensions.RandomBoolean();
        canChangeDirection = MyExtensions.RandomBoolean();
        this.isMoveDown = MyExtensions.RandomOneOrMinus();
        sizeBoxCollider = ani.GetComponent<BoxCollider2D>().size;

        size = ani.GetComponent<tk2dSprite>().GetBounds().size;


        this.localScale = localScale; ;
        transform.localScale = new Vector3(localScale, 1, 1);
        defaultSpeed = Random.Range(coreFish.minSpeed, coreFish.maxSpeed) / 100 * localScale;

        angle = Random.Range(0, 10) * isMoveDown;
        stepRotation = Random.Range(0, 0.5f) * isMoveDown;
        pos = transform.position;
        pos.y = -Random.Range(0, Constants.HALF_SCREEN_UNIT_HEIGHT - size.y - 0.5f) * isMoveDown;
        this.pos.x = posX - size.x * localScale;

        if (!canRotate)
        {
            velocity.y = 0;
            velocity.x = defaultSpeed;
        }
        else
        {
            velocity.x = defaultSpeed * Mathf.Cos(angle * Mathf.Deg2Rad);
            velocity.y = defaultSpeed * Mathf.Sin(angle * Mathf.Deg2Rad);
        }

        transform.position = pos;
        angleVec.z = angle;
        transform.localRotation = Quaternion.Euler(angleVec);

    }
    public void changeDir(float angle)
    {
        velocity.x = defaultSpeed * Mathf.Cos(angle * Mathf.Deg2Rad);
        velocity.y = defaultSpeed * Mathf.Sin(angle * Mathf.Deg2Rad);

        angleVec.z = angle;
        transform.localRotation = Quaternion.Euler(angleVec);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (MainGameScript.Instance().state != MainGameScript.GameState.ENDGAME)
        {
            if (!isSetDanh)
            {
                // random di thang, neu khong di than thi co 3 kha nang, re len phia tren va xoay
                if (!outOfScreen())
                {
                    if (canMoving)
                    {
                        if (!hadPassScreen)
                            if (isAppearScreen())
                                hadPassScreen = true;
                        if (!eatBanhMy)
                        {
                            if (canRotate)
                            {
                                angle += stepRotation;
                                if (angle > 360)
                                    angle -= 360;
                                if (isMoveDown > 0)
                                {
                                    if (localScale > 0)
                                        angle = Mathf.Clamp(angle, -15, 0);
                                    else
                                        angle = Mathf.Clamp(angle, 0, 15);
                                }
                                else
                                {
                                    if (localScale > 0)
                                        angle = Mathf.Clamp(angle, 0, 15);
                                    else
                                        angle = Mathf.Clamp(angle, -15, 0);
                                }
                                changeDir(angle);
                            }
                            pos = this.transform.position;
                            pos += velocity;
                            this.transform.position = pos;
                            if (canChangeDirection)
                            {
                                t += Time.deltaTime;
                                if (t >= 10f)
                                {
                                    if (Random.Range(0, 1f) >= 0.7f)
                                    {
                                        canRotate = !canRotate;

                                    }
                                    else
                                    {
                                        stepRotation = Random.Range(-0.5f, 0.5f);
                                    }
                                    canChangeDirection = false;
                                }
                            }
                        }
                        else if (eatBanhMy)
                        {

                            banhmyTimePercent += Time.deltaTime / Constants.TIME_EVENT;
                            currentGocBanhMy = Mathf.Lerp(beginAngle, targetAngle, banhmyTimePercent);
                            pos.x = bankinh * Mathf.Cos(currentGocBanhMy * Mathf.Deg2Rad) * (1 - banhmyTimePercent / 2) + toadoBanhMy.x;
                            pos.y = bankinh * Mathf.Sin(currentGocBanhMy * Mathf.Deg2Rad) * (1 - banhmyTimePercent / 2) + toadoBanhMy.y;
                            transform.position = pos;
                            angle = beginGocFish + currentGocBanhMy - beginAngle;
                            angleVec.z = angle;
                            transform.localRotation = Quaternion.Euler(angleVec);
                            if (banhmyTimePercent > 1)
                            {
                                eatBanhMy = false;
                            }
                        }
                    }
                }
                else
                {
                    MyExtensions.Log("fish", "call form update, destroy Fish");
                    // destroy fish
                    destroySelf();
                }
            }
        }
    }

    public bool canbeCapture(int power)
    {
        return Random.Range(0, 1f) <= coreFish.captureRate * (0.5 + (power - 1) * 0.125f) + 0.05f * (Prefs.Instance().getDamage(power));
    }

    public void beCapture()
    {
        //chuyen sang trang thai bi capture
        MyExtensions.Log("Fish", "da bi bat");
        // khi trang thai capture bi het, destroy seft + create coin
        canMoving = false;

        hadBeenCaptured = true;
        StartCoroutine(runAnimationCaptured());
    }

    IEnumerator runAnimationCaptured()
    {
        ani.Play(coreFish.nameFish + "Capture");
        while (ani.IsPlaying(coreFish.nameFish + "Capture"))
        {
            yield return null;
        }
        // create coin
        CoinManager.Instance().createCoin(coreFish.coin, transform.position);
        MainGameScript.Instance().addHealth(coreFish.indexFish * 1f);
        destroySelf();


    }
    public int getCoin()
    {
        return coreFish.coin;
    }

    public Bounds getBound()
    {
        return new Bounds(transform.position, new Vector3(sizeBoxCollider.x, sizeBoxCollider.y, 1));
    }

    public bool outOfScreen()
    {
        if (hadPassScreen)
        {
            float maxEdge = Mathf.Max(size.x, size.y);

            if (Mathf.Abs(transform.position.x) > Constants.HALF_SCREEN_UNIT_WIDTH + maxEdge)
                return true;
            if (Mathf.Abs(transform.position.y) > Constants.HALF_SCREEN_UNIT_HEIGHT + maxEdge)
                return true;
        }
        return false;
    }

    public bool isAppearScreen()
    {
        if (Mathf.Abs(transform.position.x) < Constants.HALF_SCREEN_UNIT_WIDTH)
            return true;
        return false;
    }

    public void destroySelf()
    {
        MyExtensions.Log("fish", "auto destroy self");
        listFish.Remove(this);
        Destroy(this.gameObject);
    }

    public void setDanh(GameObject modelSet)
    {
        set = Instantiate(modelSet, pivotPoint.transform.position, Quaternion.identity) as GameObject;
        set.gameObject.transform.parent = transform;
        isSetDanh = true;
        StartCoroutine(destroySet());
    }

    public void gapBomb()
    {
        beCapture();
    }


    public float targetAngle;
    public float beginAngle;
    public float banhmyTimePercent;
    public float bankinh;
    public float currentGocBanhMy;
    public float beginGocFish;
    public void gapBanhMy(Vector3 toadoBanhMy)
    {
        Debug.Log("setup gap banh my");
        eatBanhMy = true;
        this.toadoBanhMy = toadoBanhMy;

        banhmyTimePercent = 0;
        bankinh = Mathf.Sqrt((transform.position.x - toadoBanhMy.x) * (transform.position.x - toadoBanhMy.x) + (transform.position.y - toadoBanhMy.y) * (transform.position.y - toadoBanhMy.y));
        beginGocFish = angle;

        Vector3 aPoint = toadoBanhMy;
        Vector3 bPoint = transform.position;
        bPoint.z = aPoint.z;
        Vector3 targetDir = bPoint - aPoint;
        Vector3 forward = new Vector3(1, 0, 0);

        currentGocBanhMy = Vector3.Angle(targetDir, forward);
        if (targetDir.y < 0)
        {
            currentGocBanhMy *= (-1);
        }
        if (localScale * targetDir.y < 0)
        {
            beginAngle = currentGocBanhMy;
            targetAngle = currentGocBanhMy + 360;
        }
        else
        {
            beginAngle = currentGocBanhMy;
            targetAngle = currentGocBanhMy - 360;
        }
    }
    IEnumerator destroySet()
    {
        yield return new WaitForSeconds(3);
        DestroyImmediate(set.gameObject);
        isSetDanh = false;
    }
}
