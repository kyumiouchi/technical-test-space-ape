using UnityEngine;

namespace DodgyBoxes
{
    /// <summary>
    ///     This is a generic Singleton implementation for Monobehaviours.
    ///     Create a derived class where the type T is the script you want to "Singletonize"
    /// </summary>
    public class MonoBehaviourSingleton<T> : MonoBehaviour
        where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var objs = FindObjectsOfType(typeof(T)) as T[];
                    if (objs.Length > 0)
                        _instance = objs[0];
                    if (objs.Length > 1)
                    {
                        Debug.LogError("There is more than one " + typeof(T).Name + " in the scene.");
                    }

                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.hideFlags = HideFlags.HideAndDontSave;
                        _instance = obj.AddComponent<T>();
                    }
                }

                return _instance;
            }
        }
    }
}