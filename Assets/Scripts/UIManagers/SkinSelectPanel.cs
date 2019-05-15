using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelectPanel : MonoBehaviour {

    private  AssetManager assetManager;
    private SkinPanel figurePanel;                  //人物选择面板
    private SkinPanel platformPanel;              //平台选择面板
    private ScrollRect scrollRect;                                 //滑动组件
    private Button btnSelectButton;                           //选择按钮
    private Button btnReturn;                                     //返回按钮
    private Text GemNum;                                          //钻石数量

    private void Awake()
    {
        //获取资源管理器
        assetManager = AssetManager.GetAssetManager();
        scrollRect = GetComponent<ScrollRect>();
        figurePanel = transform.Find("FigurePanel").GetComponent<SkinPanel>() ;
        platformPanel = transform.Find("PlatformPanel").GetComponent<SkinPanel>();
        figurePanel.Init(assetManager.playerSpriteSet.showSprites);
        platformPanel.Init(assetManager.platformSpriteSet.sprites);
        //初始化时为隐藏状态
        figurePanel.gameObject.SetActive(false);
        platformPanel.gameObject.SetActive(false);

        btnSelectButton = transform.Find("Select").GetComponent<Button>();
        btnSelectButton.onClick.AddListener(OnSelectButtonClick);
        btnReturn = transform.Find("btnReturn").GetComponent<Button>();
        btnReturn.onClick.AddListener(OnReturnButtonClick);
        GemNum = transform.Find("Gem/txtGemNum").GetComponent<Text>();
        //GemNum.text = ;

        EventCenter.AddListener(EventDefine.SelectFigure, OnFigureSelectButtonClick);
        EventCenter.AddListener(EventDefine.SelectPlatform, OnPlatformSelectButtonClick);
        //初始化时隐藏
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 点击返回按钮
    /// </summary>
    private void OnReturnButtonClick()
    {
        gameObject.SetActive(false);
        //显示开始面板
        EventCenter.Broadcast(EventDefine.OpenStartPanel);
        if (figurePanel == true)
        {
            figurePanel.gameObject.SetActive(false);
        }
        if (platformPanel == true)
        {
            platformPanel.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 点击选择按钮
    /// </summary>
    private void OnSelectButtonClick()
    {
        if(figurePanel == true)
        {
            figurePanel.SelectSprite();
            GameManager.instance.PlayerSkin = assetManager.playerSpriteSet[assetManager.playerSpriteSet[figurePanel.SelectiveSprite]];
        }
        if(platformPanel == true)
        {
            platformPanel.SelectSprite();
            //设置平台皮肤
            GameManager.instance.PlatformSkin = platformPanel.SelectiveSprite;
            //同步游戏背景与平台皮肤相同
            GameManager.instance.GameBgskin = assetManager.bgSpriteSet[assetManager.platformSpriteSet[platformPanel.SelectiveSprite]];
        }
    }

    /// <summary>
    /// 打开人物皮肤选择面板
    /// </summary>
    public void OnFigureSelectButtonClick()
    {
        gameObject.SetActive(true);
        //隐藏开始面板
        EventCenter.Broadcast(EventDefine.CloseStartPanel);
        scrollRect.content = figurePanel.GetComponent<RectTransform>();
        figurePanel.gameObject.SetActive(true);
    }

    /// <summary>
    /// 打开平台皮肤选择面板
    /// </summary>
    public void OnPlatformSelectButtonClick()
    {
        gameObject.SetActive(true);
        //隐藏开始面板
        EventCenter.Broadcast(EventDefine.CloseStartPanel);
        scrollRect.content = platformPanel.GetComponent<RectTransform>();
        platformPanel.gameObject.SetActive(true);
    }
}
