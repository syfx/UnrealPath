using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    private Text txtRemCount;             //获得的钻石数
    private Text txtScore;                     //得分
    private Button btnPause;               //暂停按钮
    private Button btnPlay;                  //开始按钮
    private Button btnReturn;             //重新开始按钮

    public void Awake()
    {
        Init();
        EventCenter.AddListener(EventDefine.ShowGamePanel, Show);
    }

    private void Init()
    {
        //初始时设为禁用状态
        gameObject.SetActive(false);
        txtRemCount = transform.Find("Gem/txtGemCount").GetComponent<Text>();
        txtScore = transform.Find("txtScore").GetComponent<Text>();
        btnPause = transform.Find("Buttons/btnPause").GetComponent<Button>();
        btnPause.onClick.AddListener(OnPauseButtonClick);
        btnPlay = transform.Find("Buttons/btnPlay").GetComponent<Button>();
        //初始时禁用开始按钮
        btnPlay.gameObject.SetActive(false);
        btnPlay.onClick.AddListener(OnPlayButtonClick);
        btnReturn = transform.Find("Buttons/btnReturn").GetComponent<Button>();
        btnReturn.onClick.AddListener(OnReStartButtonClick);
    }

    /// <summary>
    /// 显示游戏面板
    /// </summary>
    private void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 返回到主界面
    /// </summary>
    private void OnReStartButtonClick()
    {
        GameManager.instance.GameOver(0);
        gameObject.SetActive(false);
        EventCenter.Broadcast(EventDefine.OpenStartPanel);
    }

    /// <summary>
    /// 点击开始按钮时调用
    /// </summary>
    private void OnPlayButtonClick()
    {
        //暂停游戏
        Time.timeScale = 1;
        btnPlay.gameObject.SetActive(false);
        btnPause.gameObject.SetActive(true);
    }

    /// <summary>
    /// 点击暂停按钮时调用
    /// </summary>
    private void OnPauseButtonClick()
    {
        //暂停游戏
        Time.timeScale = 0;
        btnPause.gameObject.SetActive(false);
        btnPlay.gameObject.SetActive(true);
    }
}
