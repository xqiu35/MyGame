using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Logger{

    public static bool loggable = true;

    [MethodImpl(MethodImplOptions.Synchronized)]
    public static void log(string msg)
    {
        if(loggable)
        {
            Debug.Log(msg);
        }
    }
}
