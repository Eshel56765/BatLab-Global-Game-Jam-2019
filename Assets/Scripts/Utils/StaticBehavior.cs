using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StaticBehavior<T> : MonoBehaviour where T : StaticBehavior<T>
{
    public static GameObject StaticObject
    {
        get
        {
            if (null == staticObject)
                staticObject = new GameObject("Globals");
            return StaticObject;
        }
    }
    private static GameObject staticObject;
        
    public static T Instance
    {
        get
        {
            if (null == t)
                t = StaticObject.AddComponent<T>();
            return t;
        }
    }

    private static T t;
}

