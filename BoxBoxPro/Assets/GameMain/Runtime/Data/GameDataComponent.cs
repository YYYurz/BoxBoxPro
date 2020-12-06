//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2019 Jiang Yin. All rights reserved.
// Homepage: http://gameframework.cn/
// Feedback: mailto:jiangyin@gameframework.cn
//------------------------------------------------------------

using GameFramework.Resource;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace BB
{
    [DisallowMultipleComponent]
    public class GameDataComponent : GameFrameworkComponent
    {
        /// <summary>
        /// 数据表
        /// </summary>
        public DataTableAssets DataTableInfo { get; private set; } = new DataTableAssets();

        private LoadAssetCallbacks LoadAssetCallbacks;

        private void Start()
        {
            LoadAssetCallbacks = new LoadAssetCallbacks(OnLoadDataFileSuccess, OnLoadDataFileFailure);
        }
        
        public void LoadCustomData(string strAssetPath, object userData)
        {
            Log.Debug("LoadCustomData " + strAssetPath);
            GameEntry.Resource.LoadAsset(strAssetPath, LoadAssetCallbacks, userData);
        }
        
        private void OnLoadDataFileFailure(string assetName, LoadResourceStatus status, string errorMessage, object userData)
        {
            Log.Error("GameDataComponent Load data file failed! name:{0} status:{1}", assetName, status);
        }

        private void OnLoadDataFileSuccess(string assetName, object asset, float duration, object userData)
        {
            Log.Debug("GameDataComponent Load data file success! name:{0} duration:{1}", assetName, duration);
            ParseConfigDataInfo parseConfigInfo = userData as ParseConfigDataInfo;
            if (parseConfigInfo == null)
            {
                Log.Error("GameDataComponent Load data file failed! name:{0} userData is invalid", assetName);
                return;
            }
            switch (parseConfigInfo.DataType)
            {
                case GameEnumType.GAMEASSET_TYPE.PAT_DATATABLE:
                    DataTableInfo.ParseDataTable(asset, parseConfigInfo);
                    break;
                default:
                    Log.Error("GameData unknow datatype!");
                    break;
            }

            GameEntry.Event.Fire(this, LoadCustomDataSuccessEventArgs.Create(assetName, duration, userData));

            ///卸载
            GameEntry.Resource.UnloadAsset(asset);
        }
    }
}