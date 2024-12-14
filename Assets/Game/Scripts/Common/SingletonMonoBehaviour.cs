using System;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    protected abstract bool _dontDestroyOnLoad { get; }

    private static T _instance;
    public static T Instance
    {
        get
        {
            if (!_instance)
            {
                Type t = typeof(T);
                _instance = (T)FindObjectOfType(t);
                if (!_instance)
                {
                    Debug.LogError(t + "‚ª‚ ‚è‚Ü‚¹‚ñ");
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }
        if (_dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
