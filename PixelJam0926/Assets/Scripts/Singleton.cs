using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance { get { return instance; } }

    protected virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return; 
        }

        instance = (T)this;

        if (transform.parent == null)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Debug.LogWarning($"Singleton '{gameObject.name}' is a child object. It will only survive scene loads if its parent is also persistent!");
        }
    }
}