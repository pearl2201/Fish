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


    public int localScale;
    public int isMoveDown;
    private List<Fish> listFish;

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

        angle = Random.Range(0, 10) * isMoveDown;
        stepRotation = Random.Range(0, 0.1f) * isMoveDown;
        pos = transform.position;
        pos.y = -Random.Range(0, Constants.HALF_SCREEN_UNIT_HEIGHT - size.y  - 0.5f) * isMoveDown;
        pos.x = -(Constants.HALF_SCREEN_UNIT_WIDTH + size.x ) * localScale;

        if(!canRotate)
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

    public void setup(TYPEFISH type, List<Fish> fishLish,  int localScale, float posX)
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
        this.pos.x = posX - size.x*localScale;

        if(!canRotate)
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
        // random di thang, neu khong di than thi co 3 kha nang, re len phia tren va xoay
        if(!outOfScreen() )
        {
            if(canMoving)
            {
                if(!hadPassScreen)
                    if(isAppearScreen())
                        hadPassScreen = true;
                if(canRotate)
                {
                    angle += stepRotation;
                    if(isMoveDown > 0)
                    {
                        if(localScale > 0)
                            angle = Mathf.Clamp(angle, -15, 0);
                        else
                            angle = Mathf.Clamp(angle, 0, 15);
                    }
                    else
                    {
                        if(localScale > 0)
                            angle = Mathf.Clamp(angle, 0, 15);
                        else
                            angle = Mathf.Clamp(angle, -15, 0);
                    }
                    changeDir(angle);
                }
                pos = this.transform.position;
                pos += velocity;
                this.transform.position = pos;
                if(canChangeDirection)
                {
                    t += Time.deltaTime;
                    if(t >= 10f)
                    {
                        if(Random.Range(0, 1f) >= 0.7f)
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
        }
        else 
        {
            MyExtensions.Log("fish", "call form update, destroy Fish");
            // destroy fish
            destroySelf();
        }
    }

    public bool canbeCapture(int power)
    {
        return Random.Range(0,1f) <= coreFish.captureRate*(0.5+(power-1)*0.125f) + 0.05f*(Prefs.Instance().getDamage(power));
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
        while(ani.IsPlaying(coreFish.nameFish + "Capture"))
        {
            yield return null;
        }
        // create coin
        CoinManager.Instance().createCoin(coreFish.coin, transform.position);
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
        if(hadPassScreen)
        {
            float maxEdge = Mathf.Max(size.x, size.y);

            if(Mathf.Abs(transform.position.x) > Constants.HALF_SCREEN_UNIT_WIDTH + maxEdge)
                return true;
            if(Mathf.Abs(transform.position.y) > Constants.HALF_SCREEN_UNIT_HEIGHT + maxEdge)
                return true;
        }
        return false;
    }

    public bool isAppearScreen()
    {
        if(Mathf.Abs(transform.position.x) < Constants.HALF_SCREEN_UNIT_WIDTH)
            return true;
        return false;
    }

    public void destroySelf()
    {
        MyExtensions.Log("fish", "auto destroy self");
        listFish.Remove(this);
        Destroy(this.gameObject);
    }
}
