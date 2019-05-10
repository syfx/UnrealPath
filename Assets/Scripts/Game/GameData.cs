using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// -----------------------------------------------------------
// 用来保存游戏内数据
// -----------------------------------------------------------

[SerializeField]
public class GameData {
    private BgSprite nowBgSpriteType;
    /// <summary>
    /// 当前使用的游戏中背景
    /// </summary>
    public BgSprite NowBgSpriteType
    {
        get
        {
            return nowBgSpriteType;
        }
        set
        {
            nowBgSpriteType = value;
        }
    }

    private PlatformSprite nowPlatformSpriteType;
    /// <summary>
    /// 当前使用的台阶的类型
    /// </summary>
    public PlatformSprite NowPlatformSpriteType
    {
        get
        {
            return nowPlatformSpriteType;
        }
        set
        {
            nowPlatformSpriteType = value;
        }
    }
}
