using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager {
    //获取平台
    private static ObjectPool platformPool;
    public static ObjectPool PlatformPool
    {
        get
        {
            if (platformPool == null)
                platformPool = new ObjectPool("Prefabs/fire");
            return platformPool;
        }
    }
}
