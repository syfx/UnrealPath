using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    /// <summary>
    /// 游戏数据类
    /// </summary>
    public GameData GameData { get; set; }

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
                NowBgSprite = BgSprite.Normal,
                NowPlatformSprite = PlatformSprite.Normal
            };
        }
    }

}
