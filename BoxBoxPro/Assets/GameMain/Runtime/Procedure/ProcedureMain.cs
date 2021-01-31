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
            GameEntry.UI.OpenUI(Constant.UIFormID.LoginWindow);
            GameEntry.Entity.ShowSoldier();

//             var test = GameEntry.Lua.DoStringCustom(@"
//                 local test = {}
//                 test.a = 0
//                 function test:Fun()
//                     print(a)
//                 end
//
//                 return test
// ");
            Debug.Log("Enter Main Yeah");

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