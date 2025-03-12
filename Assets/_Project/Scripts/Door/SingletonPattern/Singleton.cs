using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Platformer397
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        public bool AutoUnparentOnAwake = true;

        protected static T instance;

        public static bool HasInstance => instance != null;
        public static T TryGetInstance () => HasInstance ? instance : null;

        public static T Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = FindAnyObjectByType<T>();
                    if (instance != null)
                    {
                        var go =  new GameObject(typeof(T).Name+" Generated");
                        instance = go.AddComponent<T>();
                        Debug.Log($"Create  {go.name}{typeof(T).Name}");
                    }
                }
                return instance;
            }
        }

        protected virtual void Awake ()
        {
            if (!AutoUnparentOnAwake)
            {
                transform.SetParent(null);
            }

            if (instance ==null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else 
            {
                Destroy(gameObject);
            }
        }
    }
}
