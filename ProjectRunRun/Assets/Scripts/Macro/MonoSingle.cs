using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Smart Singleton
public class MonoSingle<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _inst;

    public static T Inst
    {
        get
        {
            if (_inst == null)
            {
                _inst = FindObjectOfType(typeof(T)) as T;

                // ���� ���ٸ� �ν��Ͻ� ����
                if (_inst == null)
                {
                    var singletonObject = new GameObject();
                    _inst = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString();

                    DontDestroyOnLoad(singletonObject);
                }
            }

            return _inst;
        }
    }
}
