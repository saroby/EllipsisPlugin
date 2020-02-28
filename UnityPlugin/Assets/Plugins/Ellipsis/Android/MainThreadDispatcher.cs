#if UNITY_ANDROID
using System.Collections;
using UnityEngine;

namespace Ellipsis.Permission.Android
{
	public class MainThreadDispatcher : MonoBehaviour
	{
		private static MainThreadDispatcher _instance;
		private static MainThreadDispatcher instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new GameObject("MainThreadDispatcher").AddComponent<MainThreadDispatcher>();
					DontDestroyOnLoad(_instance.gameObject);
				}

				return _instance;
			}
		}

		public static void ExecuteOnMainThread(System.Action functionToExecute)
		{
			if (functionToExecute != null)
				instance.StartCoroutine(ExecuteOnMainThreadCoroutine(functionToExecute));
		}

		private static IEnumerator ExecuteOnMainThreadCoroutine(System.Action functionToExecute)
		{
			yield return null;
			functionToExecute();
		}
	}
}
#endif