using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityEngine;

namespace BB
{
    // ReSharper disable once UnusedType.Global
    public class ProcedureMain : ProcedureBase
    {
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            Debug.Log("Enter Main Yeah");
            GameEntry.Entity.ShowPlayer();
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
        }
    }
}