using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

		private static SoundManager _instance;

		public static SoundManager Instance ()
		{
				if (_instance == null) {
						_instance = GameObject.FindObjectOfType<SoundManager> ();
						DontDestroyOnLoad (_instance);
				}
				return _instance;
		}

		void Awake ()
		{
				if (_instance == null) {
						_instance = this;
						DontDestroyOnLoad (_instance.gameObject);
				} else {
						if (_instance != this) {
								Destroy (this.gameObject);
						}
				}
		}
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
