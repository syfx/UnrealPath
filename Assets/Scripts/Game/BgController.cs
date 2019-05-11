using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgController : MonoBehaviour {

    private SpriteRenderer mySpriteRenderer; 
    private AssetManager myAssetManager;            //资源管理器
    private BgSprite bgSpriteType;                            //当前游戏背景图片

    public void Init()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAssetManager = AssetManager.GetAssetManager();
        bgSpriteType = GameManager.instance.GameData.NowBgSpriteType;
        //设置游戏背景
        SetBackground();
    }

    /// <summary>
    /// 设置游戏背景
    /// </summary>
    private void SetBackground()
    {

        mySpriteRenderer.sprite = myAssetManager.bgSpriteSet[bgSpriteType];
        //switch (NowBgSprite)
        //{
        //    case BgSprite.Fire:
        //        mySpriteRenderer.sprite = myAssetManager.gameBgSprite[1];
        //        break;
        //    case BgSprite.Grass:
        //        mySpriteRenderer.sprite = myAssetManager.gameBgSprite[2];
        //        break;
        //    case BgSprite.Ice:
        //        mySpriteRenderer.sprite = myAssetManager.gameBgSprite[0];
        //        break;
        //    case BgSprite.Normal:
        //        mySpriteRenderer.sprite = myAssetManager.gameBgSprite[3];
        //        break;
        //    default :
        //        mySpriteRenderer.sprite = myAssetManager.gameBgSprite[3];
        //        break;
        //}
    }
}
