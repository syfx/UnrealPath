using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// -----------------------------------------------------------
// 用来保存游戏内数据
// -----------------------------------------------------------

[SerializeField]
public class GameData {

    /// <summary>
    /// 当前使用的台阶的皮肤类型
    /// </summary>
    public PlatformSprite NowPlatformSpriteType { get; set; }
    /// <summary>
    /// 当前使用的主角的皮肤类型
    /// </summary>
    public PlayerSprite NowPlayerSpriteType { get; set; }
}
