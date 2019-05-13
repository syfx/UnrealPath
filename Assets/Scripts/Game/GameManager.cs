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
    /// <summary>
    /// 初始化游戏
    /// </summary>
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
    /// 开始游戏
    /// </summary>
    public void StartGame()
    {
        //停止所有协程
        StopAllCoroutines();
        //销毁当前平台
        platformMnager.DestroyAllPlatForm(false);
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
        //初始化主角
        if(playerManager != null)
        {
            playerManager.Init();
            //相机跟随主角
            Camera.main.GetComponent<CameraFollow>().Reset();
        }
    }
    /// <summary>
    /// 重新开始游戏
    /// </summary>
    public void RestartGame()
    {
        //销毁玩家
        playerManager.DeadPlayer();
        //结束未结束的协程
        StopAllCoroutines();
        //结束游戏
        GameOver(0);
        //开始游戏
        StartGame();
    }
    /// <summary>
    /// 结束游戏
    /// </summary>
    /// <param name="time">多少秒后执行</param>
    public void GameOver(float time)
    {
        StartCoroutine("DelayGameOver", time);
    }
    IEnumerator DelayGameOver(float time)
    {
        yield return new WaitForSeconds(time);
        //设置游戏背景层级
        bgController.SetBgSortingLayer("LeadGameBg");
        //销毁当前平台
        platformMnager.DestroyAllPlatForm(false);
    }
}
