using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region 需要保存的游戏数据
    private PlatformSprite plarformSkinType;        //当亲游戏中平台皮肤类型
    private PlayerSprite playerSkinType;                //当前玩家皮肤类型
    /// <summary>
    /// 最高分
    /// </summary>
    public int BestScore { get; private set; }
    #endregion

    private AssetManager assetManager;              //游戏资源管理器
    public static GameManager instance;
    [Tooltip("当前游戏的背景管理器")]
    public BgController bgController;
    [Tooltip("当前游戏的平台生成管理器")]
    public PlatformManger platformMnager;
    [Tooltip("当前游戏的主角管理器")]
    public PlayerManager playerManager;
    [Tooltip("游戏中的UI面板")]
    public GamePanel gamePanel;
    [Tooltip("游戏结束时的UI面板")]
    public OverPanel overPanel;
    /// <summary>
    /// 游戏数据类
    /// </summary>
    public GameData gameData { get; set; }
    /// <summary>
    /// 是否游戏开始
    /// </summary>
    public bool IsStart { get; set; }
    /// <summary>
    /// 是否游戏结束
    /// </summary>
    public bool IsEnd { get; set; }
    /// <summary>
    /// 当前游戏背景皮肤
    /// </summary>
    public Sprite GameBgskin { get; set; }
    /// <summary>
    /// 当前主角皮肤
    /// </summary>
    public Sprite PlayerSkin { get; set; }
    /// <summary>
    /// 当前平台皮肤
    /// </summary>
    public Sprite PlatformSkin { get; set; }
    /// <summary>
    /// 游戏得分
    /// </summary>
    public int Score { get; private set; }


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
        if (gameData == null)
        {
            //初始化游戏数据类
            gameData = new GameData
            {
                NowPlatformSpriteType = PlatformSprite.Normal, 
                NowPlayerSpriteType = PlayerSprite.Girl
            };
        }
        else
        {
            //TODO:
        }
        //获取资源
        plarformSkinType = gameData.NowPlatformSpriteType;
        playerSkinType = gameData.NowPlayerSpriteType;

        //设置皮肤
        PlayerSkin = assetManager.playerSpriteSet[playerSkinType];
        PlatformSkin = assetManager.platformSpriteSet[plarformSkinType];
        GameBgskin = assetManager.bgSpriteSet[plarformSkinType];

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
        //取消暂停游戏
        PlayGame();
        //设置得分为0
        Score = 0;
        //广播游戏开始事件
        EventCenter.Broadcast(EventDefine.GameStart);
    }
    /// <summary>
    /// 重新开始游戏
    /// </summary>
    public void RestartGame()
    {
        //结束未结束的协程
        StopAllCoroutines();
        //结束游戏
        OverGame();
        //开始游戏
        StartGame();
    }
    /// <summary>
    /// 游戏结束
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
        //更新分数和最高分
        if (Score > BestScore)
        {
            BestScore = Score;
        }
        //广播游戏结束消息
        EventCenter.Broadcast(EventDefine.GameOver);
    }
    /// <summary>
    /// 主动结束游戏
    /// </summary>
    public void OverGame()
    {
        //结束未结束的协程
        StopAllCoroutines();
        //销毁玩家
        playerManager.DeadPlayer();
        //设置游戏背景层级
        bgController.SetBgSortingLayer("LeadGameBg");
        //销毁当前平台
        platformMnager.DestroyAllPlatForm(false);
    }
    /// <summary>
    /// 暂停游戏
    /// </summary>
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    /// <summary>
    /// 取消暂停游戏
    /// </summary>
    public void PlayGame()
    {
        Time.timeScale = 1;
    }
    /// <summary>
    /// 设置得分
    /// </summary>
    /// <param name="alter ">变动大小</param>
    public void ChangeScore(int alter)
    {
        Score += alter;
        gamePanel.UpdateScore(Score);
    }
}
