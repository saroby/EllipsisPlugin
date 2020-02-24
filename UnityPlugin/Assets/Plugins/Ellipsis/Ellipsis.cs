using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Ellipsis : MonoBehaviour
{
    public string GetTest()
    {
#if UNITY_EDITOR
        return "unity editor";
#elif UNITY_ANDROID
        var jo = new AndroidJavaObject("com.dotdotdot.ellipsis.Constants");
        return jo.CallStatic<string>("GetTest");
#elif UNITY_IOS
        return "TODO"; //TODO
#endif
        return null;
    }
}

public partial class Ellipsis
{
    private static Ellipsis _instance;

    public static Ellipsis Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (Ellipsis) FindObjectOfType(typeof(Ellipsis));
                if (_instance != null) return _instance;

                var singletonObject = new GameObject();
                _instance = singletonObject.AddComponent<Ellipsis>();
                singletonObject.name = typeof(Ellipsis).ToString() + " (Singleton)";

                DontDestroyOnLoad(singletonObject);
            }

            return _instance;
        }
    }
}