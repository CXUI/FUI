using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 加载TexturePacker图集
/// </summary>
public class UDLoadTextureManage : MonoBehaviour {

    //private static GameObject m_pMainObject;
    private static UDLoadTextureManage m_pContainer = null;
    public static UDLoadTextureManage getInstance()
    {
        if (m_pContainer == null)
        {
            m_pContainer = new GameObject("LoadTexture").AddComponent<UDLoadTextureManage>();
        }
        return m_pContainer;
    }
    private Dictionary<string, Object[]> m_pAtlasDic;//图集的集合
    void Awake()
    {
        initData();
    }
    private void initData()
    {
        m_pContainer = this;
        DontDestroyOnLoad(this);
        //UDLoadTextureManage.m_pMainObject = gameObject;
        m_pAtlasDic = new Dictionary<string, Object[]>();
    }
    /// <summary>
    /// 加载图集上的一个精灵
    /// </summary>
    /// <param name="_spriteAtlasPath"></param>
    /// <param name="_spriteName"></param>
    /// <returns></returns>
    public Sprite LoadAtlasSprite(string _spriteAtlasPath, string _spriteName)
    {
        Sprite _sprite = FindSpriteFormBuffer(_spriteAtlasPath, _spriteName);
        if (_sprite == null)
        {
            Object[] _atlas = Resources.LoadAll(_spriteAtlasPath);
            m_pAtlasDic.Add(_spriteAtlasPath, _atlas);
            _sprite = SpriteFormAtlas(_atlas, _spriteName);
        }
        return _sprite;
    }
    //删除图集缓存
    public void DeleteAtlas(string _spriteAtlasPath)
    {
        if (m_pAtlasDic.ContainsKey(_spriteAtlasPath))
        {
            m_pAtlasDic.Remove(_spriteAtlasPath);
        }
    }
    //从缓存中查找图集，并找出sprite
    private Sprite FindSpriteFormBuffer(string _spriteAtlasPath, string _spriteName)
    {
        if (m_pAtlasDic.ContainsKey(_spriteAtlasPath))
        {
            Object[] _atlas = m_pAtlasDic[_spriteAtlasPath];
            Sprite _sprite = SpriteFormAtlas(_atlas, _spriteName);
            return _sprite;
        }
        return null;
    }
    //从图集中，并找出sprite
    private Sprite SpriteFormAtlas(Object[] _atlas, string _spriteName)
    {
        for (int i = 0; i < _atlas.Length; i++)
        {
            if (_atlas[i].GetType() == typeof(UnityEngine.Sprite))
            {
                if (_atlas[i].name == _spriteName)
                {
                    return (Sprite)_atlas[i];
                }
            }
        }
        Debug.LogWarning("图片名:" + _spriteName + ";在图集中找不到");
        return null;
    }

}



