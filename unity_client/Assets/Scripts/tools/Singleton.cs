using UnityEngine;
using System.Collections;
using System;

public abstract class Singteion<T> where T : class, new()
{
    private static T _instance;

    public static T Instance()
    {
        if(Singteion<T>._instance == null)
        {
            Singteion<T>._instance = Activator.CreateInstance<T>();
            if(Singteion<T>._instance == null)
            {
                Debug.Log("instance error");
            }
        }
        return Singteion<T>._instance;
    }
}
