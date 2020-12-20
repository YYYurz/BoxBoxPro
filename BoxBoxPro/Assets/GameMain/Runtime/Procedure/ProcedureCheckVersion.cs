using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace BB
{
    public class ProcedureCheckVersion : ProcedureBase
    {
        private bool m_InitResourcesComplete;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            Debug.Log("Enter ProcedureCheckVersion  -- yzr");

            m_InitResourcesComplete = false;
            GameEntry.Resource.InitResources(OnInitResourcesComplete);
        }
        
        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (!m_InitResourcesComplete)
            {
                return;
            }

            // 检查版本流程由于未使用GameFrameWork框架的NetWork组件，所以此流程作为GameFrameWork的单机模式初始化资源  Yzr
            ChangeState<ProcedurePreload>(procedureOwner);
        }
        
        private void OnInitResourcesComplete()
        {
            m_InitResourcesComplete = true;

            Log.Info("Init resources complete.");
        }
    }
}
