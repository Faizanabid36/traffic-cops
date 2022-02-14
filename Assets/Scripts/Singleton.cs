using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T __instance;

    public static T Instance
    {
        get
        {
            if (__instance == null)
            {
                __instance = FindObjectOfType<T>();
                if (__instance == null)
                {
                    GameObject newGO = new GameObject();
                    __instance = newGO.AddComponent<T>();
                }
            }
            return __instance;
        }
    }

    protected virtual void Awake()
    {
        __instance = this as T;
    }
}
