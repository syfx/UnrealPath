using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AssetManager")]
public class AssetManager : ScriptableObject {

    public static AssetManager GetAssetManager()
    {
        return Resources.Load<AssetManager>("MyAssetManager");
    }
  
    /// <summary>
    /// 平台背景皮肤集合
    /// </summary>
    [Serializable]
    public class PlatformSpriteSet
    {
        public List<PlatformSprite> spriteType = new List<PlatformSprite>();
        public List<Sprite> sprites = new List<Sprite>();
        public List<int> prices = new List<int>();

        public Sprite this[PlatformSprite type]
        {
            get
            {
                int index = spriteType.IndexOf(type);
                if (index >= 0 && index < sprites.Count)
                {
                    return sprites[index];
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    spriteType.Add(type);
                    sprites.Add(value);
                }
            }
        }
        public PlatformSprite this[Sprite sprite]
        {
            get
            {
                int index = sprites.IndexOf(sprite);
                if (index >= 0 && index < spriteType.Count)
                {
                    return spriteType[index];
                }
                return PlatformSprite.Normal;
            }
        }
    }
    /// <summary>
    /// 主角皮肤集合
    /// </summary>
    [Serializable]
    public class PlayerSpriteSet
    {
        public List<PlayerSprite> spriteType = new List<PlayerSprite>();
        public List<Sprite> sprites = new List<Sprite>();
        public List<Color> spriteColor = new List<Color>();
        /// <summary>
        /// 用来展示皮肤的图片
        /// </summary>
        public List<Sprite> showSprites = new List<Sprite>();
        public List<int> prices = new List<int>();

        /// <summary>
        /// 根据皮肤类型，返回皮肤
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Sprite this[PlayerSprite type]
        {
            get
            {
                int index = spriteType.IndexOf(type);
                if(index >= 0 && index < sprites.Count)
                {
                    return sprites[index];
                }
                return null;
            }
            set
            {
                if(value != null)
                {
                    spriteType.Add(type);
                    sprites.Add(value);
                }
            }
        }
        /// <summary>
        /// 返回皮肤类型
        /// </summary>
        /// <param name="sprite"></param>
        /// <returns></returns>
        public PlayerSprite this[Sprite sprite]
        {
            get
            {
                int index = showSprites.IndexOf(sprite);
                if (index <= 0 || index >= spriteType.Count)
                {
                    index = sprites.IndexOf(sprite);
                }
                if (index >= 0 && index < spriteType.Count)
                {
                    return spriteType[index];
                }
                return PlayerSprite.Girl;
            }
        }
        /// <summary>
        /// 获取当前皮肤的主色调
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Color GetSpriteColor(PlayerSprite type)
        {
            int index = spriteType.IndexOf(type);
            if (index >= 0 && index < sprites.Count)
            {
                return spriteColor[index];
            }
            //默认颜色
            return new Color(170, 51, 17);
        }
        /// <summary>
        /// 根据类型选择展示皮肤的图片
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Sprite GetShowSprite(PlayerSprite type)
        {
            int index = spriteType.IndexOf(type);
            if (index >= 0 && index < sprites.Count)
            {
                return showSprites[index];
            }
            return null;
        }
    }

    [Tooltip("背景皮肤列表")]
    public PlatformSpriteSet bgSpriteSet = new PlatformSpriteSet();
    [Tooltip("平台皮肤列表")]
    public PlatformSpriteSet platformSpriteSet = new PlatformSpriteSet();
    [Tooltip("玩家皮肤列表")]
    public PlayerSpriteSet playerSpriteSet = new PlayerSpriteSet();

    [Tooltip("玩家预制")]
    public GameObject playerPrefab;
    [Tooltip("玩家死亡粒子特效")]
    public GameObject deathEffectPrefab;
    [Tooltip("用来选择皮肤的格子的预制")]
    public GameObject itemPrefab;
    [Tooltip("游戏音乐")]
    public List<AudioClip> audioClips = new List<AudioClip>();
}
