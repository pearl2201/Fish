using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
		private static MusicManager _instance;
	
		public static MusicManager GetInstance ()
		{
				if (_instance == null) {
						_instance = GameObject.FindObjectOfType<MusicManager> ();
						DontDestroyOnLoad (_instance);
				}
				return _instance;
		}
		void Awake ()
		{
				if (_instance == null) {
						_instance = this;
						DontDestroyOnLoad (_instance);
				} else {
						if (this != _instance)
								Destroy (this.gameObject);
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
