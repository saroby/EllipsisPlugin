namespace Ellipsis
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class EllipsisManager : MonoBehaviour
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
#else
            return null;
#endif
        }
    }

    public partial class EllipsisManager
    {
        private static EllipsisManager _instance;

        public static EllipsisManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = (EllipsisManager)FindObjectOfType(typeof(EllipsisManager));
                    if (_instance != null) return _instance;

                    var singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<EllipsisManager>();
                    singletonObject.name = typeof(EllipsisManager).ToString() + " (Singleton)";

                    DontDestroyOnLoad(singletonObject);
                }

                return _instance;
            }
        }
    }
}