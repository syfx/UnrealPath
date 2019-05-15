using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    private Text txtGemCount;             //获得的钻石数
    private Text txtScore;                     //得分
    private Button btnPause;               //暂停按钮
    private Button btnPlay;                  //开始按钮
    private Button btnReturn;             //重新开始按钮

    public void Awake()
    {
        Init();
        EventCenter.AddListener(EventDefine.GameStart, Show);
        EventCenter.AddListener(EventDefine.GameOver, GameOver);
    }

    private void Init()
    {
        //初始时设为禁用状态
        gameObject.SetActive(false);
        txtGemCount = transform.Find("Gem/txtGemCount").GetComponent<Text>();
        txtScore = transform.Find("txtScore").GetComponent<Text>();
        btnPause = transform.Find("Buttons/btnPause").GetComponent<Button>();
        btnPause.onClick.AddListener(OnPauseButtonClick);
        btnPlay = transform.Find("Buttons/btnPlay").GetComponent<Button>();
        //初始时禁用开始按钮
        btnPlay.gameObject.SetActive(false);
        btnPlay.onClick.AddListener(OnPlayButtonClick);
        btnReturn = transform.Find("Buttons/btnReturn").GetComponent<Button>();
        btnReturn.onClick.AddListener(OnReTurnButtonClick);
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
    private void OnReTurnButtonClick()
    {
        GameManager.instance.OverGame();
        gameObject.SetActive(false);
        EventCenter.Broadcast(EventDefine.OpenStartPanel);
    }

    /// <summary>
    /// 点击开始按钮时调用
    /// </summary>
    private void OnPlayButtonClick()
    {
        //开始游戏
        GameManager.instance.PlayGame();
        btnPlay.gameObject.SetActive(false);
        btnPause.gameObject.SetActive(true);
    }

    /// <summary>
    /// 点击暂停按钮时调用
    /// </summary>
    private void OnPauseButtonClick()
    {
        //暂停游戏
        GameManager.instance.PauseGame();
        btnPause.gameObject.SetActive(false);
        btnPlay.gameObject.SetActive(true);
    }
    /// <summary>
    /// 游戏结束时调用
    /// </summary>
    private void GameOver()
    {
        gameObject.SetActive(false);
    }
    /// <summary>
    /// 更新得分
    /// </summary>
    /// <param name="score">分数</param>
    public void UpdateScore(int score)
    {
        txtScore.text = score.ToString();
    }
    /// <summary>
    /// 更新钻石数
    /// </summary>
    /// <param name="gemCount">钻石数量</param>
    public void UpdateGemCount(int gemCount)
    {
        txtGemCount.text = gemCount.ToString();
    }
}
