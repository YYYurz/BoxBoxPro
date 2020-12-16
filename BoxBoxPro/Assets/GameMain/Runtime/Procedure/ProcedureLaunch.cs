using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace BB
{
    // ReSharper disable once UnusedType.Global
    public class ProcedureLaunch : ProcedureBase
    {
        private bool _allAssetLoadedComplete;
        private float a = 0;

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.Event.Subscribe(LoadLuaFilesConfigSuccessEventArgs.EventId, OnLoadLuaFilesConfigSuccess);
            GameEntry.Event.Subscribe(PreloadProgressCompleteEventArgs.EventId, OnAllAssetsLoadedComplete);
            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            GameEntry.Event.Subscribe(OpenUIFormFailureEventArgs.EventId, OnOpenUIFormFailure);
            _allAssetLoadedComplete = false;
            
            GameEntry.Lua.LoadLuaFilesConfig();


            var uiRoot = GameObject.Find("UI Form Instances");
            var scaler = uiRoot.GetComponent<CanvasScaler>();
            var factor = scaler.scaleFactor;
            Debug.Log("UIFactor = " + factor);
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            if (!_allAssetLoadedComplete)
            {
                return;
            }

            _allAssetLoadedComplete = false;
            GameEntry.UI.OpenUIForm(Constant.UIFormID.StartWindow);
            Debug.Log("打开StartWindow成功！！！");
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            
            GameEntry.Event.Unsubscribe(LoadLuaFilesConfigSuccessEventArgs.EventId, OnLoadLuaFilesConfigSuccess);
            GameEntry.Event.Unsubscribe(PreloadProgressCompleteEventArgs.EventId, OnAllAssetsLoadedComplete);
            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            GameEntry.Event.Unsubscribe(OpenUIFormFailureEventArgs.EventId, OnOpenUIFormFailure);
        }

        private void OnLoadLuaFilesConfigSuccess(object sender, GameEventArgs e)
        {
            Debug.Log("OnLoadLuaFilesConfigSuccess");
            PreloadLuaFileList assetLuaFileInfo = new PreloadLuaFileList();
            assetLuaFileInfo.SetLuaFileInfo(GameEntry.Lua.LuaFileInfos);
            GameEntry.AssetPreload.AddAssetPreloadList(assetLuaFileInfo);
            
            PreloadDataTableList assetDataTableInfo = new PreloadDataTableList();
            assetDataTableInfo.AddOneAssetInfo(typeof(DTGameConfigTableReader));
            assetDataTableInfo.AddOneAssetInfo(typeof(DTUIFormDataTableReader));
            assetDataTableInfo.AddOneAssetInfo(typeof(DTSoundDataTableReader)); //加载音乐配置
            GameEntry.AssetPreload.AddAssetPreloadList(assetDataTableInfo);

            GameEntry.AssetPreload.StartPreloadAsset();
        }
        
        private void OnAllAssetsLoadedComplete(object sender, GameEventArgs e)
        {
            Log.Debug("OnAllAssetsLoadedComplete");
            GameEntry.Lua.InitLuaEnvExternalInterface();
            GameEntry.Lua.InitLuaCommonScript();
            GameEntry.Lua.StartRunLuaLogic();
            _allAssetLoadedComplete = true;
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            Debug.Log("OpenUIFOrmSuccess");
        }

        private void OnOpenUIFormFailure(object sender, GameEventArgs e)
        {
            var args = (OpenUIFormFailureEventArgs) e;
            Debug.Log("Open Failed" + args.ErrorMessage);
        }
    }
}