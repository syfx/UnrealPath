using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour {

    #region 需要保存的游戏数据
    private PlatformSprite plarformSkinType;                //当前游戏中平台皮肤类型
    private PlayerSprite playerSkinType;                        //当前玩家皮肤类型
    /// <summary>
    /// 有无声音
    /// </summary>
    public bool IsMusicOn { get; set; }
    /// <summary>
    /// 最高分
    /// </summary>
    public int BestScore { get; private set; }
    /// <summary>
    /// 钻石数量
    /// </summary>
    public int GemCount { get; private set; }
    /// <summary>
    /// 前三名
    /// </summary>
    public int[] Scores { get; private set; }
    /// <summary>
    /// 标记平台皮肤是否解锁
    /// </summary>
    public bool[] UnLockPlatformSkin { get; private set; }
    /// <summary>
    /// 标记主角皮肤是否解锁
    /// </summary>
    public bool[] UnLockPlayerSkin { get; private set; }
    #endregion

    private AssetManager assetManager;              //游戏资源管理器
    private GameData gameData;                           //游戏数据类
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
    /// <summary>
    /// 本剧获得的钻石数
    /// </summary>
    public int GainGemCount { get; private set; }


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
        //加载游戏数据
        Read();
        if (gameData == null)
        {
            plarformSkinType = PlatformSprite.Normal;
            playerSkinType = PlayerSprite.Girl;
            IsMusicOn = true;
            BestScore = 0;
            GemCount = 0;
            Scores = new int[3] { 0, 0, 0 };
            UnLockPlatformSkin = new bool[assetManager.platformSpriteSet.sprites.Count];
            UnLockPlayerSkin = new bool[assetManager.playerSpriteSet.sprites.Count];
            UnLockPlatformSkin[0] = true;
            UnLockPlayerSkin[0] = true;
            gameData = new GameData();
        }
        //获取资源
        //plarformSkinType = gameData.NowPlatformSpriteType;
        //playerSkinType = gameData.NowPlayerSpriteType;

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
        platformMnager.DestroyAllPlatform(false);
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
        //本剧获得钻石数设为0
        GainGemCount = 0;
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
        platformMnager.DestroyAllPlatform(false);
        //更新分数和最高分
        UpdateScores();
        if (Score > BestScore)
        {
            BestScore = Score;
        }
        //增加钻石数
        GemCount += GainGemCount;
        //广播游戏结束消息
        EventCenter.Broadcast(EventDefine.GameOver);
        //保存游戏
        Save();
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
        platformMnager.DestroyAllPlatform(false);
        //广播主动结束游戏消息
        EventCenter.Broadcast(EventDefine.OverGame);
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
    /// 改变得分
    /// </summary>
    /// <param name="alter ">变动大小</param>
    public void ChangeScore(int alter)
    {
        Score += alter;
        gamePanel.UpdateScore(Score);
    }
    /// <summary>
    /// 改变钻石
    /// </summary>
    /// <param name="alter ">变动大小</param>
    public void ChangeGemCount(int alter)
    {
        GainGemCount += alter;
        gamePanel.UpdateGemCount(GainGemCount);
    }
    /// <summary>
    /// 更新最高分数数组
    /// </summary>
    private void UpdateScores()
    {
        if(Score < Scores[2])
        {
            return;
        }
        Scores[2] = Score;
        for(int i = 2; i > 0; --i)
        {
            if (Scores[i] > Scores[i - 1])
            {
                int temp = Scores[i];
                Scores[i] = Scores[i - 1];
                Scores[i - 1] = temp;
            }
            else
            {
                return;
            }
        }
    }
    /// <summary>
    /// 设置游戏数据
    /// </summary>
    private void Save()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Create(Application.persistentDataPath + "/GameData.data"))
            {
                gameData.BestScore = BestScore;
                gameData.GemCount = GemCount;
                gameData.IsMusicOn = IsMusicOn;
                gameData.Scores = Scores;
                //gameData.NowPlatformSpriteType = plarformSkinType;
                //gameData.NowPlayerSpriteType = playerSkinType;
                gameData.UnLockPlatformSkin = UnLockPlatformSkin;
                gameData.UnLockPlayerSkin = UnLockPlayerSkin;
                bf.Serialize(fs, gameData);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    /// <summary>
    /// 读取游戏数据
    /// </summary>
    private void Read()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.OpenRead(Application.persistentDataPath + "/GameData.data"))
            {
                gameData = (GameData)bf.Deserialize(fs);
                BestScore = gameData.BestScore;
                GemCount = gameData.GemCount;
                IsMusicOn = gameData.IsMusicOn;
                Scores = gameData.Scores;
                //plarformSkinType = gameData.NowPlatformSpriteType;
                //playerSkinType = gameData.NowPlayerSpriteType;
                UnLockPlatformSkin = gameData.UnLockPlatformSkin;
                UnLockPlayerSkin = gameData.UnLockPlayerSkin;
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
