using GameFramework.Event;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.Resource;

namespace BB
{
    [DisallowMultipleComponent]
    public class PreloadComponent : GameFrameworkComponent
    {

        /// <summary>
        /// 资源加载完成的标志位
        /// </summary>
        private readonly Dictionary<string, PreloadAssetInfo> mDicLoadingAssetInfo = new Dictionary<string, PreloadAssetInfo>();
        private readonly  Dictionary<int, PreloadAssetInfo> mDicUiAssetInfoSerialId = new Dictionary<int, PreloadAssetInfo>();

        private int mLoadedAssetCount = 0;
        
        /// <summary>
        /// 是否开启了释放资源
        /// </summary>
        private bool mStartUnloadResource = false;

        /// <summary>
        /// 释放资源进行到第几步了
        /// </summary>
        private int mUnloadResourceSteps = 0;

        /// <summary>
        /// 缓存AssetExpireTime
        /// </summary>
        private float mCacheAssetExpireTime = 0;

        /// <summary>
        /// 缓存ResourceExpireTime
        /// </summary>
        private float mCacheResourceExpireTime = 0;


        private const int CheckResTypeAssetPath = 1;
        private const int CheckResTypeSerialId = 2;

        private void Start()
        {
        }

        private void Update()
        {
            if (mStartUnloadResource)
            {
                if (mUnloadResourceSteps == 0)
                {
                    mCacheAssetExpireTime = GameEntry.Resource.AssetExpireTime;
                    GameEntry.Resource.AssetExpireTime = 0.0001f;
                    GameEntry.Resource.AssetExpireTime = mCacheAssetExpireTime;
                }
                else if (mUnloadResourceSteps == 1)
                {
                    mCacheResourceExpireTime = GameEntry.Resource.ResourceExpireTime;
                    GameEntry.Resource.ResourceExpireTime = 0.0001f;
                    GameEntry.Resource.ResourceExpireTime = mCacheResourceExpireTime;
                    mStartUnloadResource = false;

                    Log.Info("UnloadResource Finished! AssetCount:{0} ResourceCount:{1}", GameEntry.Resource.AssetCount, GameEntry.Resource.ResourceCount);
                    GameEntry.Event.Fire(this, UnloadResourceFinishEventArgs.Create());
                }
                ++mUnloadResourceSteps;
            }
        }

        public void AddAssetPreloadList(PreloadAssetList preloadList)
        {
            var lisAssetInfo = preloadList.GetAssetInfoList();
            foreach (var iteAssetInfo in lisAssetInfo)
            {
                mDicLoadingAssetInfo[iteAssetInfo.AssetPath] = iteAssetInfo;
            }
        }

        public int GetTotalPreloadAssetCount()
        {
            return mDicLoadingAssetInfo.Count;
        }

        public void ResetAssetPreloadInfo()
        {
            mDicLoadingAssetInfo.Clear();
            mDicUiAssetInfoSerialId.Clear();
            mLoadedAssetCount = 0;
        }

        public void StartPreloadAsset()
        {
            AddEvent();

            GameEntry.Event.Fire(this, PreloadProgressLoadingEventArgs.Create(0, mDicLoadingAssetInfo.Count));

            foreach (var iteAssetInfo in mDicLoadingAssetInfo)
            {
                switch (iteAssetInfo.Value.AssetPreloadType)
                {
                    case GameEnumType.GAMEASSET_TYPE.PAT_LUAFILE:
                        GameEntry.Lua.LoadLuaFile((string)iteAssetInfo.Value.UserData, iteAssetInfo.Value.AssetPath);
                        break;
                    // case GameEnumType.GAMEASSET_TYPE.PAT_SCRIPTABLE:
                    // case GameEnumType.GAMEASSET_TYPE.PAT_DATATABLE:
                    //     GameEntry.GameData.LoadCustomData(iteAssetInfo.Value.AssetPath, iteAssetInfo.Value.UserData);
                    //     break;
                    // case GameEnumType.GAMEASSET_TYPE.PAT_CONFIGTXT:
                    //     {
                    //         Tuple<string, LoadType> tupUserData = iteAssetInfo.Value.UserData as Tuple<string, LoadType>;
                    //         GameEntry.Config.LoadConfig(tupUserData.Item1, iteAssetInfo.Value.AssetPath, tupUserData.Item2);
                    //     }
                    //     break;
                    // case GameEnumType.GAMEASSET_TYPE.PAT_LOCALIZATION:
                    //     {
                    //         Tuple<string, LoadType> tupUserData = iteAssetInfo.Value.UserData as Tuple<string, LoadType>;
                    //         GameEntry.Localization.LoadDictionary(tupUserData.Item1, iteAssetInfo.Value.AssetPath, tupUserData.Item2);
                    //     }
                    //     break;
                    // case GameEnumType.GAMEASSET_TYPE.PAT_UIFORM:
                    //     {
                    //         int nSerialID = (int)GameEntry.UI.OpenUIForm(iteAssetInfo.Value.UIFormID, iteAssetInfo.Value.UserData);
                    //         mDicUIAssetInfoSerialID[nSerialID] = iteAssetInfo.Value;
                    //     }
                    //     break;
                    // case GameEnumType.GAMEASSET_TYPE.PAT_PREFAB:
                    //     {
                    //         LoadAssetCallbacks callbacks = new LoadAssetCallbacks(OnPreLoadPrefabSuccess, OnPreLoadPrefabFailed);
                    //         GameEntry.Resource.LoadAsset(iteAssetInfo.Value.AssetPath, callbacks);
                    //     }
                        // break;
                }
            }
        }

        private void AddEvent()
        {
            GameEntry.Event.Subscribe(LoadConfigSuccessEventArgs.EventId, OnLoadConfigSuccess);
            GameEntry.Event.Subscribe(LoadDictionarySuccessEventArgs.EventId, OnLoadDictionarySuccess);
            GameEntry.Event.Subscribe(LoadLuaSuccessEventArgs.EventId, OnLoadLuaSuccess);
            // GameEntry.Event.Subscribe(LoadCustomDataSuccessEventArgs.EventId, OnLoadCustomDataSuccess);
            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnLoadUIFormSuccess);

        }

        private void RemoveEvent()
        {
            GameEntry.Event.Unsubscribe(LoadConfigSuccessEventArgs.EventId, OnLoadConfigSuccess);
            GameEntry.Event.Unsubscribe(LoadDictionarySuccessEventArgs.EventId, OnLoadDictionarySuccess);
            GameEntry.Event.Unsubscribe(LoadLuaSuccessEventArgs.EventId, OnLoadLuaSuccess);
            // GameEntry.Event.Unsubscribe(LoadCustomDataSuccessEventArgs.EventId, OnLoadCustomDataSuccess);
            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnLoadUIFormSuccess);
        }

        private void OneAssetLoadSuccess(string strAssetName, int nSerialID = -1)
        {
            ++mLoadedAssetCount;

            OnLoadAssetProgress();

            if (CheckAllAssetsLoaded())
            {
                OnLoadAssetComplete();
            }
        }

        private bool CheckNormalAssetLoaded(string strAssetName)
        {
            PreloadAssetInfo preAssetInfo = null;
            if (mDicLoadingAssetInfo.TryGetValue(strAssetName, out preAssetInfo))
            {
                preAssetInfo.AssetPreloadStatus = GameEnumType.PRELOADASSET_STATUS.PS_LOADED;
            }
            else
            {
                Log.Error("CheckNormalAssetLoaded error! can't find the asset:{0}", strAssetName);
                return false;
            }

            return true;
        }

        private bool CheckUIFormAssetLoaded(int nSerial)
        {
            PreloadAssetInfo preAssetInfo;
            if (mDicUiAssetInfoSerialId.TryGetValue(nSerial, out preAssetInfo))
            {
                preAssetInfo.AssetPreloadStatus = GameEnumType.PRELOADASSET_STATUS.PS_LOADED;
            }
            else
            {
                return false;
            }

            return true;
        }

        private void OnLoadConfigSuccess(object sender, GameEventArgs e)
        {
            LoadConfigSuccessEventArgs args = (LoadConfigSuccessEventArgs)e;
            //Log.Debug("PreloadComponent Load config '{0}' OK.", args.ConfigName);
            if (CheckNormalAssetLoaded(args.ConfigAssetName))
            {
                OneAssetLoadSuccess(args.ConfigAssetName);
            }
        }

        private void OnLoadDictionarySuccess(object sender, GameEventArgs e)
        {
            LoadDictionarySuccessEventArgs args = (LoadDictionarySuccessEventArgs)e;
            //Log.Debug("PreloadComponent Load Dictionary '{0}' OK.", args.ConfigName);
            if (CheckNormalAssetLoaded(args.DictionaryAssetName))
            {
                OneAssetLoadSuccess(args.DictionaryAssetName);
            }
        }

        private void OnLoadLuaSuccess(object sender, GameEventArgs e)
        {
            LoadLuaSuccessEventArgs args = (LoadLuaSuccessEventArgs)e;
            //Log.Debug("PreloadComponent OnLoadLuaSuccess! luaFile:{0} duration:{1}", args.AssetName, args.Duration);
            if (CheckNormalAssetLoaded(args.AssetName))
            {
                OneAssetLoadSuccess(args.AssetName);
            }
        }

        // private void OnLoadCustomDataSuccess(object sender, GameEventArgs e)
        // {
        //     LoadCustomDataSuccessEventArgs args = e as LoadCustomDataSuccessEventArgs;
        //     //Log.Debug("PreloadComponent OnLoadConfigSuccess! DataTable:{0} duration:{1}", args.DataName, args.Duration);
        //     if (CheckNormalAssetLoaded(args.DataName))
        //     {
        //         OneAssetLoadSuccess(args.DataName);
        //     }
        // }

        private void OnLoadUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs args = e as OpenUIFormSuccessEventArgs;
            if (CheckUIFormAssetLoaded(args.UIForm.SerialId))
            {
                OneAssetLoadSuccess(null, args.UIForm.SerialId);
            }
        }

        private void OnPreLoadPrefabSuccess(string assetName, object asset, float duration, object userData)
        {
            if (CheckNormalAssetLoaded(assetName))
            {
                OneAssetLoadSuccess(assetName);
            }
        }

        private void OnPreLoadPrefabFailed(string assetName, LoadResourceStatus status, string errorMessage, object userData)
        {
            //todo 错误处理
            Log.Error($"preload pefab error {assetName}, {errorMessage}");
        }

        private void OnLoadAssetComplete()
        {
            Log.Debug("PreloadComponent LoadAssetProgress load asset complete!");
            GameEntry.Event.Fire(this, PreloadProgressCompleteEventArgs.Create());
            GameEntry.Event.Fire(this, PreloadProgressLoadingEventArgs.Create(mDicLoadingAssetInfo.Count, mDicLoadingAssetInfo.Count));
            RemoveEvent();
        }

        private void OnLoadAssetProgress()
        {
            GameEntry.Event.Fire(this, PreloadProgressLoadingEventArgs.Create(mLoadedAssetCount, mDicLoadingAssetInfo.Count));
        }



        private bool CheckAllAssetsLoaded()
        {
            IEnumerator<PreloadAssetInfo> iter = mDicLoadingAssetInfo.Values.GetEnumerator();
            while (iter.MoveNext())
            {
                if (iter.Current.AssetPreloadStatus != GameEnumType.PRELOADASSET_STATUS.PS_LOADED)
                {
                    return false;
                }
            }

            return true;
        }
    }
}