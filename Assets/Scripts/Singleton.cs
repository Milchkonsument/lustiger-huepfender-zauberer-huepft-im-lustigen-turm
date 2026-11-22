using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public abstract bool DestroyOnLoad { get; }
    private static T _instance;
    public static T I
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>();
            }

            if (_instance == null)
            {
                _instance = new GameObject($"{typeof(T)}").AddComponent<T>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (DestroyOnLoad == false)
        {
            DontDestroyOnLoad(this);
        }
    }
}
