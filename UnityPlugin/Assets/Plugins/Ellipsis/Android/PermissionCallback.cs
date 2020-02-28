#if UNITY_ANDROID
using System.Threading;
using UnityEngine;

namespace Ellipsis.Permission.Android
{
    public class PermissionCallback : AndroidJavaProxy
    {
        private object threadLock;
        public string Result { get; private set; }

        public PermissionCallback(object threadLock) : base("com.dotdotdot.ellipsis.RuntimePermissionsReceiver")
        {
            Result = null;
            this.threadLock = threadLock;
        }

        public void OnPermissionResult(string result)
        {
            Result = result;

            lock (threadLock)
            {
                Monitor.Pulse(threadLock);
            }
        }
    }
}
#endif