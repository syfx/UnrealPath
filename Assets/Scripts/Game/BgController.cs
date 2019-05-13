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
        //设置背景渲染层级
        SetBgSortingLayer("GameBg");
        //设置游戏背景
        SetBackground();
    }

    /// <summary>
    /// 设置游戏背景
    /// </summary>
    private void SetBackground()
    {
        mySpriteRenderer.sprite = myAssetManager.bgSpriteSet[bgSpriteType];
    }
    /// <summary>
    /// 设置游戏背景显示层级
    /// </summary>
    /// <param name="layer">层级名称</param>
    public void SetBgSortingLayer(string layer)
    {
        mySpriteRenderer.sortingLayerName = layer;
    }
}
