using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance { get; private set; }

    private void Awake()
    {
        instance = this as T;
        OnAwake();
    }

    public virtual void OnAwake()
    {

    }
}
