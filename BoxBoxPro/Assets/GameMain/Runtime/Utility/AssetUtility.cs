﻿using GameFramework;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BB
{
    public static class AssetUtility
    {
        private static string sAppPath;

        public static string AppPath
        {
            get
            {
                if (sAppPath == null)
                {
                    if (Application.platform == RuntimePlatform.WindowsEditor
                        || Application.platform == RuntimePlatform.OSXEditor)
                    {
                        sAppPath = Application.dataPath + "/..";
                    }
                    else
                    {
                        if (Application.platform == RuntimePlatform.OSXPlayer
                          || Application.platform == RuntimePlatform.IPhonePlayer)
                        {
                            sAppPath = Application.persistentDataPath;
                        }
                        else if (Application.platform == RuntimePlatform.Android)
                        {
                            sAppPath = Application.persistentDataPath;
                        }
                        else
                        {
                            sAppPath = Application.dataPath + "/..";
                        }
                    }
                }

                return sAppPath;
            }
        }

        // public static string GetScenePath(GameEnum.SCENE_TYPE sceneType)
        // {
        //     switch (sceneType)
        //     {
        //         case GameEnum.SCENE_TYPE.ST_SURVIVEBATTLEFIELD_SCENE:
        //             return Constant.AssetPath.BattleFieldScene;
        //         case GameEnum.SCENE_TYPE.ST_LOBBY_MAIN:
        //             return Constant.AssetPath.LobbyMainScene;
        //         case GameEnum.SCENE_TYPE.ST_LOGIN_SCENE:
        //             return Constant.AssetPath.LoginScene;
        //         case GameEnum.SCENE_TYPE.ST_MONEYBATTLEFIELD_SCENE:
        //             return Constant.AssetPath.MoneyBattleFieldScene;
        //     }
        //     return "";
        // }


        // /// <summary>
        // /// Lua列表文件
        // /// </summary>
        // /// <returns></returns>
        public static string GetLuaFileConfig()
        {
            return Constant.Lua.LuaScriptsListConfig;
        }

        /// <summary>
        /// Lua资源
        /// </summary>
        /// <param name="assetName">资源名</param>
        /// <returns></returns>
        public static string GetLuaAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameAssets/LuaScripts/{0}.lua.txt", assetName);
        }

        /// <summary>
        /// 获取指定目录下所有指定后缀资源名的文件路径
        /// </summary>
        /// <param name="rootPath">根路径</param>
        /// <param name="suffix">后缀名</param>
        /// <param name="fileList">得到的文件列表</param>
        public static void GetSuffixAssetPaths(string rootPath, string suffix, ref List<string> fileList)
        {
            var dirs = Directory.GetDirectories(rootPath);
            foreach (var path in dirs)
            {
                GetSuffixAssetPaths(path, suffix, ref fileList);
            }

            var files = Directory.GetFiles(rootPath);
            foreach (var filePath in files)
            {
                if (filePath.Substring(filePath.IndexOf(".")) == suffix)
                {
                    fileList.Add(filePath);
                }
            }
        }

        public static string GetUIFormAsset(string assetName)
        {
            return Utility.Text.Format(Constant.AssetPath.UIFormFolder, assetName);
        }

        // public static string GetConfigAsset(string assetName, LoadType loadType)
        // {
        //     return Utility.Text.Format("Assets/GameMain/Configs/{0}.{1}", assetName, loadType == LoadType.Text ? "txt" : "bytes");
        // }
        //
        // public static string GetDataTableAsset(string assetName, LoadType loadType)
        // {
        //     return Utility.Text.Format("Assets/GameMain/DataTables/{0}.{1}", assetName, loadType == LoadType.Text ? "txt" : "bytes");
        // }
        //
        // public static string GetDictionaryAsset(string assetName, LoadType loadType)
        // {
        //     return Utility.Text.Format("Assets/GameMain/Localization/{0}/Dictionaries/{1}.{2}", GameEntry.Localization.Language.ToString(), assetName, loadType == LoadType.Text ? "xml" : "bytes");
        // }
        //
        // public static string GetFontAsset(string assetName)
        // {
        //     return Utility.Text.Format("Assets/GameMain/Fonts/{0}.ttf", assetName);
        // }

        public static string GetSceneAsset(string assetName)
        {
            return Utility.Text.Format("Assets/Art/Art3D/Scenes/{0}.unity", assetName);
        }

        public static string GetMusicAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Music/{0}.mp3", assetName);
        }

        public static string GetSoundAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Sounds/{0}.wav", assetName);
        }

        public static string GetEntityAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameAssets/Entities/{0}.prefab", assetName);
        }

        public static string GetEffectAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Entities/Effects/{0}.prefab", assetName);
        }

        public static string GetUISoundAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/UI/UISounds/{0}.wav", assetName);
        }
    }
}
