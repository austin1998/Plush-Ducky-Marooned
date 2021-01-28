using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for any singleton to inherit from
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T m_instance;
    public static T Instance
    {
        get
        {
            if(m_instance == null)
            {
                Debug.LogError(typeof(T).ToString() + " instance is null!");
            }
            return m_instance;
        }
    }

    protected virtual void Awake()
    {
        if(m_instance != null)
        {
            Debug.LogError("Already have a singleton of type " + typeof(T).ToString() + ", destroying this one");
            Destroy(gameObject);
        }

        m_instance = this as T;
        Init();
    }

    // Optional override
    public virtual void Init() { }
}
