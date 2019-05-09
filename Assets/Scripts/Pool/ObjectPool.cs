using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool {

    private List<GameObject> objectList = new List<GameObject>();

    private GameObject prefab = null;                                //预制体     
    private string prefabPath = string.Empty;                     //预制体路径   
    private int creatCountOnce = 1;                                    //当池中物体用完时， 一次实例化游戏物体的个数

    public int CreatCountOnce
    {
        get
        {
            return creatCountOnce;
        }
        set
        {
            if (value > 0 && value < 10)
                creatCountOnce = value;
        }
    }

    /// <summary>
    /// 传入预制体
    /// </summary>
    /// <param name="prefab"></param>
    public ObjectPool(GameObject prefab)
    {
        this.prefab = prefab;
    }
    /// <summary>
    /// 传入预制体路径，注意预制体要在Resources文件夹下
    /// </summary>
    /// <param name="prefabPath"></param>
    public ObjectPool(string prefabPath, GameObject prefab = null)
    {
        this.prefabPath = prefabPath;
        this.prefab = prefab;
    }


    /// <summary>
    /// 放入游戏物体
    /// </summary>
    /// <param name="m_object"></param>
    public void PutInObject(GameObject m_object)
    {
        m_object.SetActive(false);
        //放入游戏物体
        objectList.Add(m_object); 
    }

    /// <summary>
    /// 取出游戏物体
    /// </summary>
    /// <returns></returns>
    public GameObject TakeOutObject()
    {
        GameObject new_Object = null;
        if (objectList.Count <= 0)
            new_Object = InstantiateObj();
        else
        {
            new_Object = objectList[0];
            objectList.RemoveAt(0);
        }
        return new_Object;
    }

    /// <summary>
    /// 用来实例化游戏物体
    /// </summary>
    /// <returns></returns>
    private GameObject InstantiateObj()
    {
        if(prefab == null)
            prefab = Resources.Load<GameObject>(prefabPath);
        if (prefab == null)
            return null;
        return Object.Instantiate(prefab);
    }
}
