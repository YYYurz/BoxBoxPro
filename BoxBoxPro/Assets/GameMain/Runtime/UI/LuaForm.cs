using UnityGameFramework.Runtime;
using XLua;

/// <summary>
/// Lua界面
/// </summary>
namespace BB
{
    public class LuaForm : UGuiForm
    {
        private string uiName;
        private LuaTable luaScriptTable;
        private UIFormOpenDataInfo formDataInfo { get; set; }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            formDataInfo = userData as UIFormOpenDataInfo;
            if (formDataInfo == null)
            {
                Log.Error("LuaForm Open Error! invalid userData!");
                return;
            }
            
            Name = Name.Replace("(Clone)", "");

            luaScriptTable = GameEntry.Lua.DoStringCustom( formDataInfo.LuaFile);
            // ReSharper disable once InvertIf
            if (luaScriptTable != null)
            {
                luaScriptTable.Set("uiFormID", formDataInfo.FormID);
                luaScriptTable.Set("transform", transform);
                luaScriptTable.Set("gameObject", gameObject);
                GameEntry.Lua.CallLuaFunction(luaScriptTable, "OnCreate");
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
            if (formDataInfo.UserData as string == "Preload")
            {
                return;
            }

            // GameEntry.UI.OpenUIForm(Constant.UIFormID.UIFormHideMask);
            if (luaScriptTable != null)
            {
                GameEntry.Lua.CallLuaFunction(luaScriptTable, "OnOpen", formDataInfo.UserData);
                // GameEntry.Lua.CallLuaFunction(luaScriptTable, "Open", uiName, CachedTransform, UIForm.SerialId, formDataInfo.UserData);
            }
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            if (luaScriptTable != null)
            {
                GameEntry.Lua.CallLuaFunction(luaScriptTable, "OnClose");
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
            if (luaScriptTable == null) return;
            GameEntry.Lua.CallLuaFunction(luaScriptTable, "OnDestroy");
            luaScriptTable.Dispose();
            luaScriptTable = null;
        }


    }
}