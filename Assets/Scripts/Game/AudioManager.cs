using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;
    private AudioSource playerAudioSource;                  //主角身上的音频播放器
    private AudioSource gameAudioSource;                   //游戏主音频播放器
    private List<AudioClip> clips;                                    //游戏音效资源

    private void Awake()
    {
        instance = this;
        clips = AssetManager.GetAssetManager().audioClips;
        gameAudioSource = GetComponent<AudioSource>();
    }
    void Update () {
		if(playerAudioSource == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            playerAudioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        }
	}
    /// <summary>
    /// 播放点击按钮声音
    /// </summary>
    public void PlayButtonClickMusic()
    {
        gameAudioSource.PlayOneShot(clips[0]);
    }
    /// <summary>
    /// 播放吃到钻石的声音
    /// </summary>
    public void PlayGetGemMusic()
    {
        if (playerAudioSource != null)
        {
            playerAudioSource.PlayOneShot(clips[1]);
        }
    }
    /// <summary>
    /// 播放玩家坠落的声音
    /// </summary>
    public void PlayPlayerFallMusic()
    {
        //博凡皮卡丘特有的坠落声音
        if(AssetManager.GetAssetManager().playerSpriteSet[GameManager.instance.PlayerSkin] == PlayerSprite.Pkq)
        {
            gameAudioSource.clip = clips[5];
            gameAudioSource.Play();
        }
        else
        {
            gameAudioSource.PlayOneShot(clips[2]);
        }
    }
    /// <summary>
    /// 播放玩家撞击障碍物的声音
    /// </summary>
    public void PlayPlayerHitMusic()
    {
        gameAudioSource.PlayOneShot(clips[3]);
    }
    /// <summary>
    /// 播放玩家跳跃声音
    /// </summary>
    public void PlayPlayerJumpMusic()
    {
        if (playerAudioSource != null)
        {
            playerAudioSource.PlayOneShot(clips[4]);
        }
    }
    public void IsMusicOn(bool isOn)
    {
        if (isOn)
        {
            if (playerAudioSource != null)
            {
                playerAudioSource.volume = 1;
            }
            gameAudioSource.volume = 1;
        }
        else
        {
            if(playerAudioSource != null)
            {
                playerAudioSource.volume = 0;
            }
            gameAudioSource.volume = 0;
        }
    }
}
