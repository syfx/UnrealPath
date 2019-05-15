using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverPanel : MonoBehaviour {
    private Text txtFinalScore;                //显示最终得分
    private Text txtBestScore;                 //显示最高分
    private Text txtGemCount;               //获得钻石数量
    private Button btnHome;                 //主菜单按钮
    private Button btnReStart;               //重新开始按钮

    private void Awake()
    {
        gameObject.SetActive(false);
        txtFinalScore = transform.Find("FinalScore").GetComponent<Text>();
        txtBestScore = transform.Find("BestScore").GetComponent<Text>();
        txtGemCount = transform.Find("Gem/txtGemCount").GetComponent<Text>();
        btnHome = transform.Find("btnHome").GetComponent<Button>();
        btnHome.onClick.AddListener(OnHomeButtonClick);
        btnReStart = transform.Find("btnReStart").GetComponent<Button>();
        btnReStart.onClick.AddListener(OnReStartButtonClick);
        Init(0, 0, 0);
        EventCenter.AddListener(EventDefine.GameOver, Show);
    }

    /// <summary>
    /// 设置显示的数据
    /// </summary>
    /// <param name="finalScore">最终得分</param>
    /// <param name="bestScore">最高分</param>
    /// <param name="gemCount">获得的钻石数</param>
    public void Init(int finalScore, int bestScore, int gemCount)
    {
        txtFinalScore.text = finalScore.ToString();
        txtBestScore.text = "最高分数：" + bestScore.ToString();
        txtGemCount.text = "+" + gemCount.ToString();
    }

    /// <summary>
    /// 点击重新开始按钮
    /// </summary>
    private void OnReStartButtonClick()
    {
        //重新开始按钮
        gameObject.SetActive(false);
        GameManager.instance.RestartGame();
    }
    /// <summary>
    /// 点击返回主界面按钮
    /// </summary>
    private void OnHomeButtonClick()
    {
        gameObject.SetActive(false);
        EventCenter.Broadcast(EventDefine.OpenStartPanel);
    }
    /// <summary>
    /// 显示结束面板
    /// </summary>
    private void Show()
    {
        Init(GameManager.instance.Score, GameManager.instance.BestScore, 0);
        gameObject.SetActive(true);
    }
    /// <summary>
    /// 设置最终得分
    /// </summary>
    /// <param name="score">分数</param>
    public void SetFinalScore(int score)
    {
        txtFinalScore.text = score.ToString();
    }
    /// <summary>
    /// 设置最高得分
    /// </summary>
    /// <param name="score">分数</param>
    public void SetBestScore(int bestScore)
    {
        txtFinalScore.text = "最高分数：" + bestScore.ToString();
    }
}
