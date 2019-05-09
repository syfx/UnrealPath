using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sptBg : MonoBehaviour {

    private SpriteRenderer mySpriteRenderer; 
    private AssetManager myAssetManager;            //资源管理器
    private BgSprite NowBgSprite;                             //当前游戏背景图片

    public void Awake()
    {
        InitBg();
    }

    public void InitBg()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myAssetManager = AssetManager.GetAssetManager();
        NowBgSprite = GameManager.instance.GameData.NowBgSprite;
        //设置游戏背景
        SetBackground();
    }

    /// <summary>
    /// 设置游戏背景
    /// </summary>
    private void SetBackground()
    {
        switch (NowBgSprite)
        {
            case BgSprite.Fire:
                mySpriteRenderer.sprite = myAssetManager.bgRender[1];
                break;
            case BgSprite.Grass:
                mySpriteRenderer.sprite = myAssetManager.bgRender[2];
                break;
            case BgSprite.Ice:
                mySpriteRenderer.sprite = myAssetManager.bgRender[0];
                break;
            case BgSprite.Normal:
                mySpriteRenderer.sprite = myAssetManager.bgRender[3];
                break;
            default :
                mySpriteRenderer.sprite = myAssetManager.bgRender[3];
                break;
        }
    }
}
