using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    private static MusicManager _instance;
    public AudioClip musicBg;
    public bool active;

    public static MusicManager Instance()
    {
        if (_instance == null)
        {
            _instance = GameObject.FindObjectOfType<MusicManager>();
            DontDestroyOnLoad(_instance);
        }
        return _instance;
    }
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }
    // Use this for initialization
    void Start()
    {
        setMusicActive(isMusicActive());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void stop()
    {
        GetComponent<AudioSource>().Stop();
    }

    public bool isMusicActive()
    {
        active = Prefs.Instance().getMusic();
        return active;
    }

    public void setMusicActive(bool _active)
    {
        active = _active;
        Prefs.Instance().setMusic(_active);
        if (active)
        {
            GetComponent<AudioSource>().mute = false;
            GetComponent<AudioSource>().clip = musicBg;
            GetComponent<AudioSource>().Play();
        }
        else
        {
            GetComponent<AudioSource>().mute = true;
            GetComponent<AudioSource>().Stop();
        }
    }

    public void toggle()
    {
        if (active)
            active = false;
        else
            active = true;
        setMusicActive(active);
    }

}
