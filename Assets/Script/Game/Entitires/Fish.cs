using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour
{
		public enum TYPEFISH : int
		{
				FISH1,
				FISH2,
				FISH3,
				FISH4,
				FISH5,
				FISH6,
				FISH72
		}
		public tk2dSpriteAnimator ani;
		public TYPEFISH typeFish;
		public Vector3 velocity;
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
