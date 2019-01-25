using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLockManager : StaticBehavior<CursorLockManager>
{
    private List<object> Users = new List<object>();
    
    public static bool IsInUse
    {
        get
        {
            return Instance.Users.Count > 0;
        }
    }

    public static void UseMouse(object Caller)
    {
        if (!Instance.Users.Contains(Caller))
            Instance.Users.Add(Caller);
        Cursor.lockState = CursorLockMode.Confined;
    }
    public static void ReleaseMouse(object Caller)
    {
        Instance.Users.Remove(Caller);
        if (Instance.Users.Count == 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
