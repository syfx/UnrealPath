using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMnager : MonoBehaviour {

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
    /// <summary>
    /// 平台生成的位置
    /// </summary>
    private Vector2 platformCreatPos;
    /// <summary>
    /// 两边同时生成平台时起点平台的x坐标
    /// </summary>
    private float startPosX;
    /// <summary>
    /// 障碍物生成的位置
    /// </summary>
    private Vector2 obstacleCreatPos;
    /// <summary>
    /// 生成的平台数量
    /// </summary>
    private int platformCount = 5;
    /// <summary>
    /// 是否朝左边生成
    /// </summary>
    private bool isLeftCreat = true;
    /// <summary>
    /// 是否两边同时生成
    /// </summary>
    private bool isMeanwhileCreat = false;
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
    /// <summary>
    /// 当前平台的皮肤类型
    /// </summary>
    private  PlatformSprite platformRender;
    private Sprite platformSprite;          //当前平台皮肤
    /// <summary>
    /// 当前平台的对象池
    /// </summary>
    private ObjectPool platformPool;
    /// <summary>
    /// 当前平台障碍物的对象池
    /// </summary>
    private ObjectPool[] obstaclePool;
    /// <summary>
    /// 地刺障碍物的对象池
    /// </summary>
    private ObjectPool spikePool;
    /// <summary>
    /// 当前生成的障碍物的层级
    /// </summary>
    private int obstacleSortingOrder = 0;
    private AssetManager assetManager;

    /// <summary>
    /// 障碍物生成概率
    /// </summary>
    public int ObstacleProbability { get; set; }
    /// <summary>
    /// 突刺生成概率
    /// </summary>
    public int SpikeProbability { get; set; }

    public void Start()
    {
        InitPlatforms();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            EnsurePath();
        if (Input.GetKeyDown(KeyCode.B))
            CreatObstract();
    }

    /// <summary>
    /// 初始化平台
    /// </summary>
    public void InitPlatforms()
    {
        assetManager = AssetManager.GetAssetManager();
        //初始化障碍物生成概率为(1 / 5)
        ObstacleProbability = 5;
        //初始化突刺生成概率为(1 / 4)
        SpikeProbability = 3;
        //设置当前的使用的平台的皮肤
        platformRender = GameManager.instance.GameData.NowPlatformSprite;
        platformPool = PoolManager.PlatformPool;
        spikePool = PoolManager.SpikePool;
        //选择相应的对象池
        switch (platformRender)
        {
            case PlatformSprite.Fire:
                obstaclePool = PoolManager.FireObstaclePool;
                platformSprite = assetManager.platformRender[2];
                break;
            case PlatformSprite.Grass:
                obstaclePool = PoolManager.GrassObstaclePool;
                platformSprite = assetManager.platformRender[1];
                break;
            case PlatformSprite.Ice:
                obstaclePool = PoolManager.IceObstaclePool;
                platformSprite = assetManager.platformRender[0];
                break;
            case PlatformSprite.Normal:
                obstaclePool = PoolManager.GrassObstaclePool;
                platformSprite = assetManager.platformRender[3];
                break;
        }
        //初始化第一个平台生成的位置
        platformCreatPos = new Vector2(0, -2);
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
        obj.GetComponent<Platform>().Init(platformSprite, 3);
        //两边同时生成
        if (isMeanwhileCreat)
        {
            GameObject otherObj = PoolManager.PlatformPool.TakeOutObject();
            otherObj.transform.parent = transform;
            otherObj.transform.position = new Vector2(2 * startPosX - platformCreatPos.x, platformCreatPos.y);
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
        Obj.GetComponent<Obstacle>().Init(obstaclePool[ran], 3);
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
        Obj.GetComponent<Obstacle>().Init(spikePool, 3);
    }
}
