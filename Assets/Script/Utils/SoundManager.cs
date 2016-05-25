using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

    private static SoundManager _instance;
    public AudioClip coin;
    public AudioClip eventBomb;
    public AudioClip eventBanhMy;
    public AudioClip button;
    public AudioClip changeGun;
    public AudioClip shot;
    public AudioClip eventSet;
    public AudioClip endGame;

    

    public bool active;
    public static SoundManager Instance()
    {
        if (_instance == null)
        {
            _instance = GameObject.FindObjectOfType<SoundManager>();
            DontDestroyOnLoad(_instance);
        }
        return _instance;
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance.gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(this.gameObject);
            }
        }
    }
    // Use this for initialization
    void Start()
    {
        isSoundActive();
    }

    public bool isSoundActive()
    {
        active = Prefs.Instance().getSound();
        return active;
    }

    public void setSoundActive(bool _active)
    {
        active = _active;
        Prefs.Instance().setSound(_active);
        if (!active)
            stop();
    }

    public void toggle()
    {
        if (active)
            active = false;
        else
            active = true;
        setSoundActive(active);
    }


    ////////////////////

    public void playEndGame()
    {
        playSoundOnce(endGame);
    }
    public void playCoin()
    {
        playSoundOnce(coin);
    }

    public void playEventHourGlass()
    {
        playSoundOnce(eventBanhMy);
    }

    public void playEventSet()
    {
        playSoundOnce(eventSet);
    }


    public void playEventBomb()
    {
        playSoundOnce(eventBomb);
    }

    public void playBtnClick()
    {
        playSoundOnce(button);
    }

    public void playChangeGun()
    {
        playSoundOnce(changeGun);
    }

    public void playShot()
    {
        playSoundOnce(shot);
    }
    /// ////////////////////////////////////////////////////////////////////////

    public void playSound(AudioClip clip, float time)
    {
        if (active)
        {
            if (GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = clip;
            GetComponent<AudioSource>().loop = false;
            GetComponent<AudioSource>().Play();
            StartCoroutine(stopSoundAfterTime(time, clip));
        }

    }

    IEnumerator stopSoundAfterTime(float time, AudioClip clip)
    {
        if (active)
        {
            yield return new WaitForSeconds(time);
            if (GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Stop();

        }
    }

    public void playSoundOnce(AudioClip clip)
    {

        if (active)
        {
            Debug.Log("Play sound once: " + clip.name);
            GetComponent<AudioSource>().clip = clip;
            GetComponent<AudioSource>().loop = false;
            GetComponent<AudioSource>().Play();
            //AudioSource.PlayClipAtPoint (clip, Vector3.zero);
        }
    }

    public void stop()
    {
        if (active)
        {
            if (GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Stop();
        }
    }

    public void stop(AudioClip clip)
    {
        if (active)
        {
            if (GetComponent<AudioSource>().clip == clip && GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Stop();
        }
    }

    public void playLoop(AudioClip clip)
    {
        if (active)
        {

            if (GetComponent<AudioSource>().isPlaying)
                GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().clip = clip;
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().Play();
        }

    }
}
