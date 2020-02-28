using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using Ellipsis.Permission.Android;
#endif

namespace Ellipsis.Permission
{
    public static partial class Permission
    {
        private static bool CachePermission(string permission, PermissionState value)
        {
            if (PlayerPrefs.GetInt("ARTP_" + permission, -1) != (int)value)
            {
                PlayerPrefs.SetInt("ARTP_" + permission, (int)value);
                return true;
            }

            return false;
        }

        public static Dictionary<AndroidPermiossionType, PermissionState> ProcessPermissionRequest(AndroidPermiossionType[] types, string resultRaw)
        {
            if (resultRaw.Length != types.Length)
            {
                Debug.LogError("RequestPermissions: something went wrong");
                return null;
            }

            var shouldUpdateCache = false;
            var result = new Dictionary<AndroidPermiossionType, PermissionState>();
            for (int i = 0; i < types.Length; i++)
            {
                var _permission = resultRaw[i].ToPermissionState();
                result[types[i]] = _permission;

                if (CachePermission(types[i].ToPermissionString(), _permission))
                    shouldUpdateCache = true;
            }

            if (shouldUpdateCache)
                PlayerPrefs.Save();

            return result;
        }

        private static PermissionState GetCachedPermissionState(string permission, PermissionState defaultValue)
        {
            return (PermissionState)PlayerPrefs.GetInt("ARTP_" + permission, (int)defaultValue);
        }

        private static string GetCachedPermissionStates(string[] permissions)
        {
            var cachedPermissions = new StringBuilder(permissions.Length);
            for (int i = 0; i < permissions.Length; i++)
                cachedPermissions.Append((int)GetCachedPermissionState(permissions[i], PermissionState.ShouldAsk));

            return cachedPermissions.ToString();
        }

        public static Dictionary<AndroidPermiossionType, PermissionState> CheckState(AndroidPermiossionType[] types)
        {
            var output = new Dictionary<AndroidPermiossionType, PermissionState>();

#if UNITY_ANDROID
            var permissions = types.Select(e => e.ToPermissionString()).ToArray();

            var resultRaw = ajc.CallStatic<string>("CheckPermission", permissions, context);
            if (resultRaw.Length != permissions.Length)
            {
                Debug.LogError("CheckPermissions: something went wrong");
                return null;
            }

            for (int i = 0; i < types.Length; i++)
            {
                var state = resultRaw[i].ToPermissionState();
                if (state == PermissionState.Denied && GetCachedPermissionState(permissions[i], PermissionState.ShouldAsk) != PermissionState.Denied)
                    state = PermissionState.ShouldAsk;

                output[types[i]] = state;
            }

            return output;
#else
            Debug.LogError("invalid method calling in this platform");
            return output;
#endif
        }

        public static Dictionary<IOSPermissionType, PermissionState> CheckState(IOSPermissionType[] types)
        {
            var output = new Dictionary<IOSPermissionType, PermissionState>();


            return output;
        }

        public static Dictionary<AndroidPermiossionType, PermissionState> Request(AndroidPermiossionType[] types)
        {
            var output = new Dictionary<AndroidPermiossionType, PermissionState>();

#if UNITY_ANDROID
            var permissions = types.Select(e => e.ToPermissionString()).ToArray();

            PermissionCallback nativeCallback;
            object threadLock = new object();
            lock (threadLock)
            {
                nativeCallback = new PermissionCallback(threadLock);
                ajc.CallStatic("RequestPermission", permissions, context, nativeCallback, GetCachedPermissionStates(permissions));

                if (nativeCallback.Result == null)
                    System.Threading.Monitor.Wait(threadLock);
            }

            output = ProcessPermissionRequest(types, nativeCallback.Result);
            return output;
#else
            Debug.LogError("invalid method calling in this platform");
            return output;
#endif
        }

        public static void RequestAsync(AndroidPermiossionType[] types, AndroidPermissionResultMultiple callback)
        {
            var output = new Dictionary<AndroidPermiossionType, PermissionState>();

#if UNITY_ANDROID
            var permissions = types.Select(e => e.ToPermissionString()).ToArray();

            PermissionCallbackAsync nativeCallback = new PermissionCallbackAsync(types, callback);
            ajc.CallStatic("RequestPermission", permissions, context, nativeCallback, GetCachedPermissionStates(permissions));
#else
            if (callback != null)
                callback(permissions, GetDummyResult(permissions));
#endif
        }

        public static Dictionary<IOSPermissionType, PermissionState> Request(IOSPermissionType[] types)
        {
            var output = new Dictionary<IOSPermissionType, PermissionState>();


            return output;
        }

    }

    public static partial class Permission
    {
#if UNITY_ANDROID
        private static AndroidJavaClass _ajc;
        private static AndroidJavaClass ajc
        {
            get
            {
                if (_ajc == null)
                    _ajc = new AndroidJavaClass("com.dotdotdot.ellipsis.RuntimePermissions");
                return _ajc;
            }
        }

        private static AndroidJavaObject _context;
        private static AndroidJavaObject context
        {
            get
            {
                if (_context == null)
                {
                    using (var unityAjb = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                    {
                        _context = unityAjb.GetStatic<AndroidJavaObject>("currentActivity");
                    }
                }
                return _context;
            }
        }
#elif UNITY_IOS
        //TODO
#endif
    }

    static class Extensions
    {
        internal static PermissionState ToPermissionState(this char c)
        {
            return (PermissionState)(c - '0');
        }

        internal static string ToPermissionString(this AndroidPermiossionType type)
        {
            return "android.permission." + type.ToString();
        }
    }
}


