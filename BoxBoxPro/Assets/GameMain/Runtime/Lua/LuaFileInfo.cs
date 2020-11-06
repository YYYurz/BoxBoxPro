//------------------------------------------------------------
// Copyright © 2017-2020 Chen Hua. All rights reserved.
// Author: 一条猪儿虫
// Email: 1184923569@qq.com
//------------------------------------------------------------

using BB;
using System;
using UnityEngine;

[Serializable]
public class LuaFileInfo
{
    public LuaFileInfo(string luaName)
    {
        LuaName = luaName;
        AssetName = AssetUtility.GetLuaAsset(luaName);
    }

    [SerializeField]
    private string luaName;
    public string LuaName
    {
        private set
        {
            luaName = value;
        }
        get
        {
            return luaName;
        }
    }

    [SerializeField]
    private string assetName;
    public string AssetName
    {
        private set
        {
            assetName = value;
        }
        get
        {
            return assetName;
        }
    }
}