using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// -----------------------------------------------------------
// 用来保存游戏内数据
// -----------------------------------------------------------

[SerializeField]
[System.Serializable]
public class GameData {

    /*public PlatformSprite NowPlatformSpriteType;
    public PlayerSprite NowPlayerSpriteType;*/
    public bool IsMusicOn;
    public int BestScore;
    public int GemCount;
    public int[] Scores;
    public bool[] UnLockPlatformSkin;
    public bool[] UnLockPlayerSkin;


    /* /// <summary>
     /// 当前使用的台阶的皮肤类型
     /// </summary>
     public PlatformSprite NowPlatformSpriteType { get; set; }
     /// <summary>
     /// 当前使用的主角的皮肤类型
     /// </summary>
     public PlayerSprite NowPlayerSpriteType { get; set; }
     /// <summary>
     /// 有无声音
     /// </summary>
     public bool IsMusicOn{ set; get; } 
     /// <summary>
     /// 最高分
     /// </summary>
     public int BestScore { get; set; }
     /// <summary>
     /// 钻石数量
     /// </summary>
     public int GemCount { get; set; }
     /// <summary>
     /// 前三名
     /// </summary>
     public int[] Scores { get; set; }
     /// <summary>
     /// 标记平台皮肤是否解锁
     /// </summary>
     public bool[] UnLockPlatformSkin { get; set; }
     /// <summary>
     /// 标记主角皮肤是否解锁
     /// </summary>
     public bool[] UnLockPlayerSkin { get; set; }*/
}
