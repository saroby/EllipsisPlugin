#if UNITY_ANDROID
using UnityEngine;

namespace Ellipsis.Permission.Android
{
    public class PermissionCallbackAsync : AndroidJavaProxy
    {
        private string result;
        private AndroidPermiossionType[] permissions;
        private AndroidPermissionResultMultiple callback;

        public PermissionCallbackAsync(AndroidPermiossionType[] permissions, AndroidPermissionResultMultiple callback) : base("com.dotdotdot.ellipsis.RuntimePermissionsReceiver")
        {
            result = null;

            this.permissions = permissions;
            this.callback = callback;
        }

        public void OnPermissionResult(string result)
        {
            this.result = result;
            MainThreadDispatcher.ExecuteOnMainThread(ExecuteCallback);
        }

        private void ExecuteCallback()
        {
            if (callback != null)
            {
                callback(Permission.ProcessPermissionRequest(permissions, result));
                callback = null;
            }
        }
    }
}
#endif