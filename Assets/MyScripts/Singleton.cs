using UnityEngine;

public abstract class Singleton<T> : RPGMonoBehaviour where T : RPGMonoBehaviour
{
    private static T instance;
    private static object lockObj = new object();

    public static T Instance
    {
        get
        {
            lock (lockObj)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        Debug.LogWarning(typeof(T).Name + " instance is not found.");
                    }
                }
                return instance;
            }
        }
    }

    protected override void Awake()
    {
        lock (lockObj)
        {
            if (instance == null)
            {
                instance = this as T;
                Debug.Log(typeof(T).Name + " instance is set.");
            }
            else if (instance != this)
            {
                Debug.LogWarning("Replacing existing instance of " + typeof(T).Name + ".");
                instance = this as T;
            }
        }
    }
}
