using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    /// <summary>
    /// 是否已经拥有
    /// </summary>
    public bool IsOwned { get; private set; }
    /// <summary>
    /// 是否是当前使用的
    /// </summary>
    public bool isSelective { get; private set; }
    /// <summary>
    /// 价格
    /// </summary>
    public int Price { get; private set; }
 
    /// <summary>
    /// 初始化格子
    /// </summary>
    public void Init()
    {

    }
}
