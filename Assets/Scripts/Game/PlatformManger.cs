using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManger : MonoBehaviour {

    /// <summary>
    /// 下一平台生成位置与当前平台位置的水平差值
    /// </summary>
    public const float nextPosX = 0.544f;
    /// <summary>
    /// 下一平台生成位置与当前平台位置的垂直差值
    /// </summary>
    public const float nextPosY = 0.642f;
    /// <summary>
    /// 突刺到平台中间位置的偏移
    /// </summary>
    public const float spikeOfferY = 0.31f;
   
    private Vector2 platformCreatPos;                        // 平台生成的位置
    private float startPosX;                                          // 两边同时生成平台时起点平台的x坐标
    private Vector2 obstacleCreatPos;                        // 障碍物生成的位置
    private int platformCount = 5;                              // 生成的平台数量 
    private bool isLeftCreat = true;                             // 是否朝左边生成
    private bool isMeanwhileCreat = false;                // 是否两边同时生成
    private  PlatformSprite platformSpriteType;         // 当前平台的皮肤类型
    private Sprite platformSprite;                                //当前平台皮肤
    private ObjectPool platformPool;                          // 当前平台的对象池
    private ObjectPool[] obstaclePool;                        // 当前平台障碍物的对象池
    private ObjectPool spikePool;                               // 地刺障碍物的对象池
    private int obstacleSortingOrder = 0;                   // 当前生成的障碍物的层级
    private AssetManager assetManager;                  //资源管理器

    /// <summary>
    /// 障碍物生成概率
    /// </summary>
    public int ObstacleProbability { get; set; }
    /// <summary>
    /// 突刺生成概率
    /// </summary>
    public int SpikeProbability { get; set; }
    /// <summary>
    /// 平台存活时间
    /// </summary>
    public float PlatformLife { get; set; }
    /// <summary>
    /// 一次平台生成时的最大随机数量
    /// </summary>
    public int MaxCountOnce
    {
        get
        {
            return maxCountOnce;
        }

        set
        {
            maxCountOnce = value;
        }
    }
    private int maxCountOnce = 5;

    private void Awake()
    {
        //添加生成平台事件监听者
        EventCenter.AddListener(EventDefine.CreatPlatform, EnsurePath);
        //拿到资源管理类
        assetManager = AssetManager.GetAssetManager();
    }

    /// <summary>
    /// 初始化平台
    /// </summary>
    public void Init()
    {
        //初始平台生成个数为5
        platformCount = 5;
        //初始化障碍物生成概率为(1 / 5)
        ObstacleProbability = 5;
        //初始化突刺生成概率为(1 / 4)
        SpikeProbability = 3;
        //初始化平台存活时间为3秒
        PlatformLife = 30;
        //是否朝左侧生成
        isLeftCreat = true;
        //是否两边同时生成
        isMeanwhileCreat = false;
        //设置当前的使用的平台的皮肤的类型
        platformSpriteType = GameManager.instance.GameData.NowPlatformSpriteType;
        platformSprite = assetManager.platformSpriteSet[platformSpriteType];
        platformPool = PoolManager.PlatformPool;
        spikePool = PoolManager.SpikePool;
        //选择相应的对象池
        switch (platformSpriteType)
        {
            case PlatformSprite.Fire:
                obstaclePool = PoolManager.FireObstaclePool;
                break;
            case PlatformSprite.Grass:
                obstaclePool = PoolManager.GrassObstaclePool;
                break;
            case PlatformSprite.Ice:
                obstaclePool = PoolManager.IceObstaclePool;
                break;
            case PlatformSprite.Normal:
                obstaclePool = PoolManager.GrassObstaclePool;
                break;
        }
        //初始化第一个平台生成的位置
        platformCreatPos = new Vector2(nextPosX, -2 - nextPosY);
        //障碍物的位置保持在当前平台的另一侧
        if (isLeftCreat)
        {
            obstacleCreatPos = platformCreatPos + new Vector2(nextPosX * 2, 0);
        }
        else
        {
            obstacleCreatPos = platformCreatPos + new Vector2(-nextPosX * 2, 0);
        }
        while (platformCount > 0)
        {
            EnsurePath();
        }
    }

    /// <summary>
    /// 确定生成的路径
    /// </summary>
	private void EnsurePath()
    {
        if(platformCount > 0)
        {
            --platformCount;
            CreatOnePlatform();
            //生成障碍物
            if (Random.Range(0, ObstacleProbability) == 0)
            {
                CreatObstract();
            }
            else
            {
                obstacleSortingOrder = 0;
            }
        }
        else
        {
            //生成完一组台阶之后，随机获取下一组台阶数量
            platformCount = Random.Range(1, maxCountOnce);
            //改变生成方向
            isLeftCreat = !isLeftCreat;
            //生成突刺
            if (Random.Range(0, SpikeProbability) == 0)
            {
                isMeanwhileCreat = true;
                startPosX = platformCreatPos.x;
                CreatOnePlatform();
                CreatSpiket();
            }
            else{
                isMeanwhileCreat = false;
                CreatOnePlatform();
            }
        }
    }

    /// <summary>
    /// 生成一个台阶
    /// </summary>
    private void CreatOnePlatform()
    {
        if (isLeftCreat)
        {
            //更新下一平台生成位置
            platformCreatPos += new Vector2(-nextPosX, nextPosY);
            //更新下一障碍物生成位置
            obstacleCreatPos = platformCreatPos + new Vector2(nextPosX * 2, 0);
        }
        else
        {
            //更新下一平台生成位置
            platformCreatPos += new Vector2(nextPosX, nextPosY);
            //更新下一障碍物生成位置
            obstacleCreatPos = platformCreatPos + new Vector2(-nextPosX * 2, 0);
        }

        GameObject obj = PoolManager.PlatformPool.TakeOutObject();
        obj.transform.parent = transform;
        obj.transform.position = platformCreatPos;
        //设置这个平台的皮肤和下落时间
        obj.GetComponent<Platform>().Init(platformSprite, PlatformLife);
        //两边同时生成
        if (isMeanwhileCreat)
        {
            GameObject otherObj = PoolManager.PlatformPool.TakeOutObject();
            otherObj.transform.parent = transform;
            otherObj.transform.position = new Vector2(2 * startPosX - platformCreatPos.x, platformCreatPos.y);
            //设置这个平台的皮肤和下落时间
            otherObj.GetComponent<Platform>().Init(platformSprite, PlatformLife);
        }
    }

    /// <summary>
    /// 生成障碍物
    /// </summary>
    private void CreatObstract()
    {
        //随机获取一个当前背景下的障碍物
        int ran = Random.Range(0, obstaclePool.Length);
        GameObject Obj = obstaclePool[ran].TakeOutObject();
        Obj.transform.parent = transform;
        Obj.transform.position = obstacleCreatPos;
        //保证新生成的障碍物图层在上面
        Obj.GetComponent<SpriteRenderer>().sortingOrder = (obstacleSortingOrder--);
        //设置这个障碍物的对象池和存在时间
        Obj.GetComponent<Obstacle>().Init(obstaclePool[ran], PlatformLife);
    }

    /// <summary>
    /// 生成突刺
    /// </summary>
    private void CreatSpiket()
    {
        GameObject Obj = spikePool.TakeOutObject();
        Obj.transform.parent = transform;
        Obj.transform.position = new Vector2(2 * startPosX - platformCreatPos.x, platformCreatPos.y + spikeOfferY);
        //设置这个突刺的对象池和存在时间
        Obj.GetComponent<Obstacle>().Init(spikePool, PlatformLife);
    }

   /// <summary>
   /// 销毁所用平台
   /// </summary>
   /// <param name="isTureDestroy">是否整整销毁，为false时只是禁用</param>
    public void DestroyAllPlatForm(bool isTureDestroy)
    {
        if (isTureDestroy)
        {
            //销毁所有平台
            for(int i = 0; i < transform.childCount; ++i)
            {
                Destroy(transform.GetChild(i));
            }
        }
        else
        {
            //将所平台放入对象池
            for (int i = 0; i < transform.childCount; ++i)
            {
                if (transform.GetChild(i).gameObject.activeSelf == false)
                    continue;
                if(transform.GetChild(i).tag == "Platform")
                {
                    transform.GetChild(i).GetComponent<Platform>().Destroy(0);
                }
                if (transform.GetChild(i).tag == "Obstacle")
                {
                    transform.GetChild(i).GetComponent<Obstacle>().Destroy(0);
                }
            }
        }
    }

    private void OnDisable()
    {
        EventCenter.RemoveListener(EventDefine.CreatPlatform, EnsurePath);
    }
}
