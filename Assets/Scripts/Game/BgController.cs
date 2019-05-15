using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgController : MonoBehaviour {

    private SpriteRenderer mySpriteRenderer; 

    public void Init()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        //设置背景渲染层级
        SetBgSortingLayer("GameBg");
        //设置游戏背景
        SetBackground(GameManager.instance.GameBgskin);
    }

    /// <summary>
    /// 设置游戏背景
    /// </summary>
    private void SetBackground(Sprite sprite)
    {
        mySpriteRenderer.sprite = sprite;
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
