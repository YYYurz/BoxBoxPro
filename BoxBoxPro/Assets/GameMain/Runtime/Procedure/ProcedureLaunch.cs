using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityEngine;
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
            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            GameEntry.Event.Subscribe(OpenUIFormFailureEventArgs.EventId, OnOpenUIFormFailure);
            _allAssetLoadedComplete = false;
            
            
            GameEntry.Lua.LoadLuaFilesConfig();
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            a += elapseSeconds;
            if (_allAssetLoadedComplete)
            {
                if (a > 5)
                {
                    GameEntry.Lua.InitLuaEnvExternalInterface();
                    GameEntry.Lua.InitLuaCommonScript();
                    GameEntry.Lua.StartRunLuaLogic();
                    _allAssetLoadedComplete = false;
                    GameEntry.UI.OpenUIFormm();
                    Debug.Log("打开StartWindow成功！！！");
                }
            }
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            
            GameEntry.Event.Unsubscribe(LoadLuaFilesConfigSuccessEventArgs.EventId, OnLoadLuaFilesConfigSuccess);
            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            GameEntry.Event.Unsubscribe(OpenUIFormFailureEventArgs.EventId, OnOpenUIFormFailure);
        }

        private void OnLoadLuaFilesConfigSuccess(object sender, GameEventArgs e)
        {
            Debug.Log("OnLoadLuaFilesConfigSuccess");
            _allAssetLoadedComplete = true;
            PreloadLuaFileList assetLuaFileInfo = new PreloadLuaFileList();
            assetLuaFileInfo.SetLuaFileInfo(GameEntry.Lua.LuaFileInfos);
            GameEntry.AssetPreload.AddAssetPreloadList(assetLuaFileInfo);
            
            GameEntry.AssetPreload.StartPreloadAsset();
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