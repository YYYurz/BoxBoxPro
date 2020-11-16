using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityEngine;

namespace BB
{
    public class ProcedureLaunch : ProcedureBase
    {
        protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);
            Debug.Log("InitHAHA");
        }

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            GameEntry.Event.Subscribe(LoadLuaFilesConfigSuccessEventArgs.EventId, OnLoadLuaFilesConfgSuccess);
            
            GameEntry.Lua.LoadLuaFilesConfig();
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            
            GameEntry.Event.Unsubscribe(LoadLuaFilesConfigSuccessEventArgs.EventId, OnLoadLuaFilesConfgSuccess);
        }

        private void OnLoadLuaFilesConfgSuccess(object sender, GameEventArgs e)
        {
            
        }
    }
}