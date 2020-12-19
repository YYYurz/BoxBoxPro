using UnityGameFramework.Runtime;
using XLua;

/// <summary>
/// Lua界面
/// </summary>
namespace BB
{
    public class LuaForm : UGuiForm
    {
        private string mFormManagerName = "LuaFormManager";
        private string mFormName = "";
        private LuaTable mFormManagerLuaTable;

        public UIFormOpenDataInfo formDataInfo { get; private set; } = null;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            mFormName = Name;
            int nCloneIndex = mFormName.IndexOf("(Clone)");
            if (nCloneIndex >= 0)
            {
                mFormName = mFormName.Substring(0, nCloneIndex);
            }
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            formDataInfo = userData as UIFormOpenDataInfo;
            if (formDataInfo == null)
            {
                Log.Error("LuaForm Open Error! invalid userData!");
                return;
            }

            GameEntry.Lua.RequireLuaFile(formDataInfo.LuaFile);

            //再调用LuaFormManager的方法
            mFormManagerLuaTable = GameEntry.Lua.GetClassLuaTable(mFormManagerName);

            if (formDataInfo.UserData as string == "Preload")
            {
                return;
            }

            // GameEntry.UI.OpenUIForm(Constant.UIFormID.UIFormHideMask);
            if (mFormManagerLuaTable != null)
            {
                GameEntry.Lua.CallLuaFunction(mFormManagerLuaTable, "Open", mFormName, CachedTransform, UIForm.SerialId, formDataInfo.UserData);
            }


        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            if (mFormManagerLuaTable != null)
            {
                GameEntry.Lua.CallLuaFunction(mFormManagerLuaTable, "OnClose", mFormName);
            }

            base.OnClose(isShutdown, userData);
        }

        //protected override void OnOpenComplete()
        //{
        //    base.OnOpenComplete();

        //    if (m_FormManagerLuaTable != null)
        //    {
        //        GameManager.Lua.CallLuaFunction(m_FormManagerLuaTable, "OnOpenComplete", m_FormName);
        //    }
        //}

        public void OnDestroy()
        {
            if (mFormManagerLuaTable != null)
            {
                GameEntry.Lua.CallLuaFunction(mFormManagerLuaTable, "OnDestroy", mFormName);
                mFormManagerLuaTable.Dispose();
                mFormManagerLuaTable = null;
            }
        }


    }
}