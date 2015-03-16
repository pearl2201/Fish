using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

    class MyExtensions
    {

        public static bool RandomBoolean()
        {
            int t = UnityEngine.Random.Range(0, 2);
            if(t == 0)
                return true;
            else
                return false;
        }

        public static int RandomOneOrMinus()
        {
            int t = UnityEngine.Random.Range(0, 2);
            if(t == 0)
                return 1;
            else
                return -1;
        }

        public static void Log(string className, string message)
        {
            UnityEngine.Debug.Log(className + " : " + message);
        }
    }

