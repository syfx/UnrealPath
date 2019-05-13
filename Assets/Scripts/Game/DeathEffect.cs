using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    private ParticleSystem.MainModule main;

    /// <summary>
    /// 播放特效
    /// </summary>
    /// <param name="color">设置粒子颜色</param>
    /// <param name="playTime">播放时间</param>
    public void PlayWithOneColor(Color color, Vector2 pos, float playTime)
    {
        //设置粒子颜色
        gameObject.SetActive(false);
        transform.position = pos;
        main = GetComponent<ParticleSystem>().main;
        main.startColor = color;
        gameObject.SetActive(true);
    }
}
