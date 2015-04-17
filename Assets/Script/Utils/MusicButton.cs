using UnityEngine;
using System.Collections;

public class MusicButton : MonoBehaviour
{

		public tk2dSprite display;
		// Use this for initialization
		void Start ()
		{
				checkDisplaySprite ();
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void toggle ()
		{
				MusicManager.Instance ().toggle ();
				checkDisplaySprite ();
		}

		public void checkDisplaySprite ()
		{
				if (MusicManager.Instance ().isMusicActive ()) {
						display.SetSprite ("btnMusic_1");
				} else {
						display.SetSprite ("btnMusic_2");
				}
		}
}
