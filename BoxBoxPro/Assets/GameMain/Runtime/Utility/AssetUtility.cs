//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2019 Jiang Yin. All rights reserved.
// Homepage: http://gameframework.cn/
// Feedback: mailto:jiangyin@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BB
{
    public static class AssetUtility
    {
        private static string sAppPath = null;

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

        // public static string GetScenePath(GameEnumType.SCENE_TYPE sceneType)
        // {
        //     switch (sceneType)
        //     {
        //         case GameEnumType.SCENE_TYPE.ST_SURVIVEBATTLEFIELD_SCENE:
        //             return Constant.AssetPath.BattleFieldScene;
        //         case GameEnumType.SCENE_TYPE.ST_LOBBY_MAIN:
        //             return Constant.AssetPath.LobbyMainScene;
        //         case GameEnumType.SCENE_TYPE.ST_LOGIN_SCENE:
        //             return Constant.AssetPath.LoginScene;
        //         case GameEnumType.SCENE_TYPE.ST_MONEYBATTLEFIELD_SCENE:
        //             return Constant.AssetPath.MoneyBattleFieldScene;
        //     }
        //     return "";
        // }

        // public static string GetLaunchAssetABPath()
        // {
        //     return Path.Combine(Application.streamingAssetsPath, Constant.AssetPath.LaunchAsset);
        // }
        //
        // public static string GetSettingConfigDir()
        // {
        //     return Path.Combine(AppPath, "config_setting");
        // }
        //
        // public static string GetDictionaryAsset(string assetName, LoadType loadType)
        // {
        //     return Utility.Text.Format("Assets/GameAssets/Localization/{0}/Dictionaries/{1}.{2}", GameEntry.Localization.Language.ToString(), assetName, loadType == LoadType.Text ? "xml" : "bytes");
        // }
        //
        // public static string GetConfigAsset(string assetName, LoadType loadType)
        // {
        //     return Utility.Text.Format("Assets/GameAssets/Configs/AB/{0}.{1}", assetName, loadType == LoadType.Text ? "txt" : "bytes");
        // }
        //
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
            return string.Format("Assets/GameAssets/LuaScripts/{0}.lua.txt", assetName);
        }

        /// <summary>
        /// 获取指定目录下所有指定后缀资源名的文件路径
        /// </summary>
        /// <param name="rootPath">根路径</param>
        /// <param name="suffix">后缀名</param>
        /// <param name="fileList">得到的文件列表</param>
        public static void GetSuffixAssetPaths(string rootPath, string suffix, ref List<string> fileList)
        {
            string[] dirs = Directory.GetDirectories(rootPath);
            foreach (string path in dirs)
            {
                GetSuffixAssetPaths(path, suffix, ref fileList);
            }

            string[] files = Directory.GetFiles(rootPath);
            foreach (string filePath in files)
            {
                if (filePath.Substring(filePath.IndexOf(".")) == suffix)
                {
                    fileList.Add(filePath);
                }
            }
        }

        // /// <summary>
        // /// UI
        // /// </summary>
        // /// <param name="assetName"></param>
        // /// <returns></returns>
        public static string GetUIFormAsset(string assetName)
        {
            return Utility.Text.Format(Constant.AssetPath.UIFormFolder, assetName);
        }
        
        // /// <summary>
        // /// 音乐
        // /// </summary>
        // /// <param name="assetName"></param>
        // /// <returns></returns>
        // public static string GetSoundAsset(string assetName)
        // {
        //     return Utility.Text.Format(Constant.AssetPath.SoundFolder, assetName);
        // }

        #region ENTITY
        //
        // /// <summary>
        // /// 获取敌机的资源路径
        // /// </summary>
        // /// <param name="strAssetname"></param>
        // /// <returns></returns>
        // public static string GetEnemyAircraftAsset(string strPrefabName)
        // {
        //     return Utility.Text.Format(Constant.AssetPath.EnemyAircraftFolder, strPrefabName);
        // }
        //
        // /// <summary>
        // /// 背景资源路径
        // /// </summary>
        // /// <param name="strPrefabName"></param>
        // /// <returns></returns>
        // public static string GetBackgroundAsset(string strPrefabName)
        // {
        //     return Utility.Text.Format(Constant.AssetPath.Background, strPrefabName);
        // }

        #endregion
    }
}
