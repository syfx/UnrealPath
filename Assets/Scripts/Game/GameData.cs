using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// -----------------------------------------------------------
// 用来保存游戏内数据
// -----------------------------------------------------------

[SerializeField]
public class GameData {
    private BgSprite nowBgSprite;
    /// <summary>
    /// 当前使用的游戏中背景
    /// </summary>
    public BgSprite NowBgSprite
    {
        get
        {
            return nowBgSprite;
        }
        set
        {
            nowBgSprite = value;
        }
    }

    private PlatformSprite nowPlatformSprite;
    /// <summary>
    /// 当前使用的台阶的类型
    /// </summary>
    public PlatformSprite NowPlatformSprite
    {
        get
        {
            return nowPlatformSprite;
        }
        set
        {
            nowPlatformSprite = value;
        }
    }
}
