using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelectPanel : MonoBehaviour {

    private  AssetManager assetManager;
    private SkinPanel figurePanel;                  //人物选择面板
    private SkinPanel platformPanel;             //平台选择面板
    private ScrollRect scrollRect;                    //滑动组件
    private Button btnSelectButton;              //选择按钮
    private Button btnReturn;                        //返回按钮
    private Text gemCount;                           //钻石数量
    private Text txtPrice;                                //皮肤价格
    private bool isUnlock;                             //皮肤是否是解锁的          

    private void Start()
    {
        //获取资源管理器
        assetManager = AssetManager.GetAssetManager();
        scrollRect = GetComponent<ScrollRect>();
        figurePanel = transform.Find("FigurePanel").GetComponent<SkinPanel>() ;
        platformPanel = transform.Find("PlatformPanel").GetComponent<SkinPanel>();
        figurePanel.Init(assetManager.playerSpriteSet.showSprites, GameManager.instance.UnLockPlayerSkin);
        platformPanel.Init(assetManager.platformSpriteSet.sprites, GameManager.instance.UnLockPlatformSkin);
        //初始化时为隐藏状态
        figurePanel.gameObject.SetActive(false);
        platformPanel.gameObject.SetActive(false);

        btnSelectButton = transform.Find("Select").GetComponent<Button>();
        btnSelectButton.onClick.AddListener(OnSelectButtonClick);
        btnReturn = transform.Find("btnReturn").GetComponent<Button>();
        btnReturn.onClick.AddListener(OnReturnButtonClick);
        gemCount = transform.Find("Gem/txtGemNum").GetComponent<Text>();
        txtPrice = transform.Find("Select/price/txtGemNum").GetComponent<Text>();

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
        AudioManager.instance.PlayButtonClickMusic();
        gameObject.SetActive(false);
        //显示开始面板
        EventCenter.Broadcast(EventDefine.OpenStartPanel);
        if (figurePanel.gameObject.activeSelf == true)
        {
            figurePanel.gameObject.SetActive(false);
        }
        if (platformPanel.gameObject.activeSelf == true)
        {
            platformPanel.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 点击选择按钮
    /// </summary>
    private void OnSelectButtonClick()
    {
        AudioManager.instance.PlayButtonClickMusic();
        /*if (!isUnlock)
        {
            int price = 0;
            Int32.TryParse(txtPrice.text, out price);
            if (GameManager.instance.GemCount < price)
            {
                return;
            }
            //购买商品
            else
            {
                //钻石数减少
                GameManager.instance.ChangeGemCount(-price);
                gemCount.text = GameManager.instance.GemCount.ToString();
            }
        }*/
        if (figurePanel.gameObject.activeSelf == true)
        {
            figurePanel.SelectSprite();
            GameManager.instance.PlayerSkin = assetManager.playerSpriteSet[assetManager.playerSpriteSet[figurePanel.SelectiveSprite]];
        }
        if (platformPanel.gameObject.activeSelf == true)
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
        //更新显示钻石数量
        UpdateGemCount(GameManager.instance.GemCount);
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
        //更新显示钻石数量
        UpdateGemCount(GameManager.instance.GemCount);
        gameObject.SetActive(true);
        //隐藏开始面板
        EventCenter.Broadcast(EventDefine.CloseStartPanel);
        scrollRect.content = platformPanel.GetComponent<RectTransform>();
        platformPanel.gameObject.SetActive(true);
    }
    /// <summary>
    /// 更新钻石数量
    /// </summary>
    private void UpdateGemCount(int count)
    {
        gemCount.text = count.ToString();
    }
    /// <summary>
    /// 更新选择按钮图标
    /// </summary>
    public void UpdateSelectButton(bool isUnLock, int index)
    {
        this.isUnlock = isUnLock;
        if (isUnLock)
        {
            btnSelectButton.transform.GetChild(0).gameObject.SetActive(false);
            btnSelectButton.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            if(platformPanel.gameObject.activeSelf == true)
            {
                txtPrice.text = assetManager.platformSpriteSet.prices[index].ToString();
            }
            if (figurePanel.gameObject.activeSelf == true)
            {
                txtPrice.text = assetManager.playerSpriteSet.prices[index].ToString();
            }
            btnSelectButton.transform.GetChild(0).gameObject.SetActive(true);
            btnSelectButton.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
