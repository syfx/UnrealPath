using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager {
    
    private static ObjectPool platformPool;
    /// <summary>
    /// 获取平台对象池
    /// </summary>
    public static ObjectPool PlatformPool
    {
        get
        {
            if (platformPool == null)
                platformPool = new ObjectPool("Prefabs/platform");
            return platformPool;
        }
    }

    private static ObjectPool[] iceObstaclePool = new ObjectPool[3] {
        new ObjectPool("Prefabs/iceObstacle_0"),
        new ObjectPool("Prefabs/iceObstacle_1"),
        new ObjectPool("Prefabs/iceObstacle_2")
    };
    /// <summary>
    /// 雪地障碍物对象池数组
    /// </summary>
    public static ObjectPool[] IceObstaclePool
    {
        get
        {
            return iceObstaclePool;
        }
    }

    private static ObjectPool[] grassObstaclePool = new ObjectPool[2] {
        new ObjectPool("Prefabs/grassObstacle_0"), 
        new ObjectPool("Prefabs/grassObstacle_1")
    };
    /// <summary>
    /// 草地障碍物对象池数组
    /// </summary>
    public static ObjectPool[] GrassObstaclePool
    {
        get
        {
            return grassObstaclePool;
        }
    }

    private static ObjectPool[] fireObstaclePool = new ObjectPool[2] {
        new ObjectPool("Prefabs/fireObstacle_0"),
        new ObjectPool("Prefabs/fireObstacle_1")
    };
    /// <summary>
    /// 火山障碍物对象池数组
    /// </summary>
    public static ObjectPool[] FireObstaclePool
    {
        get
        {
            return fireObstaclePool;
        }
    }

    private static ObjectPool spikePool;
    /// <summary>
    /// 地刺障碍物对象池
    /// </summary>
    public static ObjectPool SpikePool
    {
        get
        {
            if (spikePool == null)
                spikePool = new ObjectPool("Prefabs/spikes");
            return spikePool;
        }
    }

    private static ObjectPool diamondPool;
    /// <summary>
    /// 地刺障碍物对象池
    /// </summary>
    public static ObjectPool DiamondPool
    {
        get
        {
            if (diamondPool == null)
                diamondPool = new ObjectPool("Prefabs/diamond");
            return diamondPool;
        }
    }

}
