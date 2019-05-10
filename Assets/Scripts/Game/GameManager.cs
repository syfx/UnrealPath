using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    [Tooltip("当前游戏的背景管理器")]
    public BgController bgController;
    [Tooltip("当前游戏的平台生成管理器")]
    public PlatformMnager platformMnager;

    private AssetManager assetManager;              //游戏资源管理器
    /// <summary>
    /// 游戏数据类
    /// </summary>
    public GameData GameData { get; set; }
    /// <summary>
    /// 是否游戏开始
    /// </summary>
    public bool IsStart { get; set; }
    /// <summary>
    /// 是否游戏结束
    /// </summary>
    public bool IsEnd { get; set; }
    /// <summary>
    /// 玩家控制的主角
    /// </summary>
    public GameObject Player { get; set; }

    private void Awake()
    {
        instance = GetComponent<GameManager>();
        InitGameData();
    }
    
    private void InitGameData()
    {
        if(GameData == null)
        {
            GameData = new GameData
            {
                NowBgSpriteType = BgSprite.Ice,
                NowPlatformSpriteType = PlatformSprite.Ice
            };
        }
        IsStart = false;
        IsEnd = true;
        assetManager = AssetManager.GetAssetManager();
    }

    /// <summary>
    /// 点击开始按钮时调用
    /// </summary>
    public void StartGame()
    {
        IsStart = true;
        //初始化背景
        if (bgController != null)
        {
            bgController.InitBg();
        }
        //初始化平台
        if (platformMnager != null)
        {
            platformMnager.InitPlatforms();
        }
        //生成玩家
        Player = Instantiate(assetManager.playerPrefab);
    }

}
