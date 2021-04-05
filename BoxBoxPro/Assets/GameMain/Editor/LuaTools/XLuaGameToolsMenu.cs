#if UNITY_EDITOR
using UnityEditor;

namespace BB.Editor
{
    /// <summary>
    /// 自定义编辑器工具
    /// </summary>
    public static class XLuaGameToolsMenu
    {
        private static XLuaTools s_luaTool = new XLuaTools();

        [MenuItem("BoxBox/Create LuaFilesConfig.json", false, 100)]
        public static void GenerateLuaFilesConfig()
        {
            s_luaTool.GenerateLuaFileConfig();
        }

        private static CSharpToLua s_csharpToLua = new CSharpToLua();
        [MenuItem("BoxBox/Create LuaConstantFiles", false, 110)]
        public static void GenerateLuaConstantFiles()
        {
            s_csharpToLua.GenerateLuaConstantFiles();
        }
    }
}

#endif