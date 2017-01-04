using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// 实现单例
///  1.需要继承QSingleton。
///  2.需要实现非public的构造方法。
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class QSingleton<T> : MonoBehaviour where T : QSingleton<T>
{

    protected QSingleton ()
    {
    }

    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T> ();

                if (FindObjectsOfType<T> ().Length > 1)
                {
                    Debug.Log ( "More than 1!" );
                    return instance;
                }

                if (instance == null)
                {
                    string instanceName = typeof ( T ).Name;
                    Debug.Log ( "Instance Name: " + instanceName );
                    GameObject instanceGO = GameObject.Find ( instanceName );

                    if (instanceGO == null)
                        instanceGO = new GameObject ( instanceName );
                    instance = instanceGO.AddComponent<T> ();
                    DontDestroyOnLoad ( instanceGO );  //保证实例不会被释放
                    Debug.Log ( "Add New Singleton " + instance.name + " in Game!" );
                }
                else
                {
                    Debug.Log ( "Already exist: " + instance.name );
                }
            }
            return instance;
        }

    }

    protected virtual void OnDestroy ()
    {
        instance = null;
    }
}
// 1.需要继承QSingleton。
// 2.需要实现非public的构造方法。
public class PoolManager : QSingleton<PoolManager>
{
    private PoolManager ()
    {
    }

    /// <summary>
    /// 存储动可服用的GameObject
    /// </summary>
    private List<GameObject> dormantObjects = new List<GameObject> ();


    /// <summary>
    /// 在pool中获取与go类型相同的GameObject，若没有则new个。
    /// </summary>
    /// <param name="go"></param>
    /// <returns></returns>
    public GameObject Spawng ( GameObject go )
    {
        GameObject temp = null;
        if (dormantObjects.Count > 0)
        {
            foreach (GameObject item in dormantObjects)
            {
                if (item.name == go.name)
                {
                    temp = item;
                    dormantObjects.Remove ( item );
                    return temp;
                }
            }
        }
        temp = GameObject.Instantiate ( go ) as GameObject;
        temp.name = go.name;
        temp.transform.SetParent ( go.transform, false );

        return temp;
    }

    /// <summary>
    /// 将用完的gameobject放入pool中。
    /// </summary>
    /// <param name="go"></param>
    public void Despawn ( GameObject go )
    {

        //go.transform.SetParent ( transform );
        go.SetActive ( false );
        dormantObjects.Add ( go );
        Trim ();
    }

    /// <summary>
    /// 如果dormantObjects大于最大个数则将之前的GameObject都推出来。
    /// </summary>
    public void Trim ()
    {
        while (dormantObjects.Count > 50)
        {
            GameObject dob = dormantObjects[0];
            dormantObjects.RemoveAt ( 0 );
            UnityEngine.Object.Destroy ( dob );
        }
    }
}
