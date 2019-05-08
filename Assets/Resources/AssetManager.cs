using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AssetManager")]
public class AssetManager : ScriptableObject {

    public static AssetManager GetAssetManager()
    {
        return Resources.Load<AssetManager>("MyAssetManager");
    }

    public List<Sprite> m_Background = new List<Sprite>();
}
