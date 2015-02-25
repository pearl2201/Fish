using UnityEngine;
using System.Collections;

public class Constants
{
		public static int SCREEN_WIDTH = 800;
		public static int SCREEN_HEIGHT = 480;
		public static int RESOLUTION = 72 ;
		public static float SCREEN_UNIT_WIDTH = ((float)SCREEN_WIDTH) / ((float)RESOLUTION);
		public static float SCREEN_UNIT_HEIGHT = ((float)SCREEN_HEIGHT) / ((float)RESOLUTION);
		public static float HALF_SCREEN_UNIT_WIDTH = SCREEN_UNIT_WIDTH / 2;
		public static float HALF_SCREEN_UNIT_HEIGHT = SCREEN_UNIT_HEIGHT / 2;
}

