using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sptBg : MonoBehaviour {

    private SpriteRenderer m_SpriteRenderer; 
    private AssetManager m_AssetManager;            //资源管理器

    public void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_AssetManager =AssetManager.GetAssetManager();
        //设置游戏背景
        SetBackground();
    }

    /// <summary>
    /// 设置游戏背景
    /// </summary>
    private void SetBackground()
    {
        //获取张随机游戏背景
        int index = Random.Range(0, m_AssetManager.m_Background.Count);
        m_SpriteRenderer.sprite = m_AssetManager.m_Background[index];
    }
}
