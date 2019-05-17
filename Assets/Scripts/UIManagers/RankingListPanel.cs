using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingListPanel : MonoBehaviour {

    private Text txtFirst;
    private Text txtSecond;
    private Text txtThridly;

    void Awake () {
        txtFirst = transform.Find("show/First/score").GetComponent<Text>();
        txtSecond = transform.Find("show/Second/score").GetComponent<Text>();
        txtThridly = transform.Find("show/Thirdly/score").GetComponent<Text>();
        transform.gameObject.SetActive(false);
        EventCenter.AddListener(EventDefine.ShowRankingPanel, Show);
    }
    /// <summary>
    /// 显示排行旁
    /// </summary>
    public void Show()
    {
        txtFirst.text = GameManager.instance.Scores[0].ToString();
        txtSecond.text = GameManager.instance.Scores[1].ToString();
        txtThridly.text = GameManager.instance.Scores[2].ToString();
        transform.gameObject.SetActive(true);
    }

    private void OnMouseUp()
    {
        //关闭排行榜
        transform.gameObject.SetActive(false);
    }
}
