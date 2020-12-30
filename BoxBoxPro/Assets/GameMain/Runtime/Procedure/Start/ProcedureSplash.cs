using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using UnityEngine;
using UnityEngine.UI;

namespace BB
{
    public class ProcedureSplash : ProcedureBase
    {
        private float m_SplashTime = 2f;

        private float m_SplashCurTime = 0f;

        private GameObject m_SplashImageUI;

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            m_SplashCurTime = 0f;
            m_SplashImageUI = GameObject.Find("imgSplash");
            if(m_SplashImageUI == null)
            {
                Log.Error("UI Splash Image is invalid!!");
            }
            m_SplashImageUI.SetActive(false);

            Debug.Log("Enter ProcedureSplash  -- yzr");
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            ChangeState(procedureOwner, GameEntry.Base.EditorResourceMode ? typeof(ProcedurePreload) : typeof(ProcedureCheckVersion));
        }

    }
}
