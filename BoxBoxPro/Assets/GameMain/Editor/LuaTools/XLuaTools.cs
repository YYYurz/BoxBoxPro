using GameFramework;
using Hr;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace BB.Editor
{
    /// <summary>
    /// Lua相关工具
    /// </summary>
    public class XLuaTools
    {
        private List<string> _fileList = new List<string>();
        private Dictionary<string, bool> _loadedFlag;

        public XLuaTools()
        {

        }

        /// <summary>
        /// 生成Lua文件配置
        /// </summary>
        public void GenerateLuaFileConfig()
        {
            var strScriptsFolder = RemoveStartWith(Constant.Lua.LuaScriptsFolder, "Assets/");
            var rootPath = Path.Combine(Application.dataPath, strScriptsFolder);
            var suffix = ".lua.txt";
            GameUtils.CollectFilesWithSuffix(rootPath, ".lua.txt", ref _fileList);

            var sb = new StringBuilder();

            foreach (var file in _fileList)
            {
                var path = Utility.Path.GetRegularPath(file);
                var tempName = path.Substring(path.IndexOf("LuaScripts") + 11);
                var luaName = tempName.Substring(0, tempName.Length - suffix.Length);
                var json = GameUtils.SerializeObject(new LuaFileInfo(luaName));
                sb.Append(json);

                sb.Append("\n");
            }

            var strFilesConfigFile = Path.Combine(Application.dataPath, RemoveStartWith(Constant.Lua.LuaScriptsListConfig, "Assets/"));
            using (var stream = new FileStream(strFilesConfigFile, FileMode.Create))
            {
                var sw = new StreamWriter(stream);
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
            }

            AssetDatabase.Refresh();
        }

        private string RemoveStartWith(string strContent, string strStartWith)
        {
            if (strContent.StartsWith(strStartWith))
            {
                return strContent.Substring(strStartWith.Length, strContent.Length - strStartWith.Length);
            }

            return strContent;
        }
    }
}
