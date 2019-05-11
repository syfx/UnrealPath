using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private AssetManager assetManager;              //游戏资源管理器
    public static GameManager instance;
    [Tooltip("当前游戏的背景管理器")]
    public BgController bgController;
    [Tooltip("当前游戏的平台生成管理器")]
    public PlatformManger platformMnager;
    [Tooltip("当前游戏的主角管理器")]
    public PlayerManager playerManager;
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


    private void Awake()
    {
        instance = GetComponent<GameManager>();
        InitGameData();
    }
    
    private void InitGameData()
    {
        assetManager = AssetManager.GetAssetManager();
        if (GameData == null)
        {
            //初始化游戏数据类
            GameData = new GameData
            {
                NowBgSpriteType = BgSprite.Normal,
                NowPlatformSpriteType = PlatformSprite.Normal, 
                NowPlayerSpriteType = PlayerSprite.Girl
            };
        }
        IsStart = false;
        IsEnd = true;
    }

    /// <summary>
    /// 点击开始按钮时调用
    /// </summary>
    public void StartGame()
    {
        IsStart = true;
        IsEnd = false;
        //初始化背景
        if (bgController != null)
        {
            bgController.Init();
        }
        //初始化平台
        if (platformMnager != null)
        {
            platformMnager.Init();
        }
        //生成玩家
        playerManager.Init();
    }
    /// <summary>
    /// 重新开始游戏
    /// </summary>
    public void ReStartGame()
    {
        //销毁当前平台
        platformMnager.DestroyAllPlatForm(false);
        IsStart = true;
        IsEnd = false;
        bgController.Init();
        platformMnager.Init();
        playerManager.Init();
        //相机跟随主角
        Camera.main.GetComponent<CameraFollow>().Reset();
    }
}
