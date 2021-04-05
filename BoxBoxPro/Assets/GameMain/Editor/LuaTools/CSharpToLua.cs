#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace BB.Editor
{
    public class CSharpToLua
    {
        private readonly List<Type> listTypes = new List<Type>() {
            // typeof(Constant.Game),
            // typeof(Constant.SpecialGameData),
            // typeof(Constant.Item),
            typeof(Constant.UIFormID),
            // typeof(Net.NETWORK_MSG),
            // typeof(Constant.DTGameConfig),
            // typeof(Constant.CSharpLuaDataBattleMoneyForm),
            // typeof(Constant.ExchangeClass),
            // typeof(Constant.TextDescription),
            // typeof(Constant.SoundID),
            // typeof(Constant.GameSettingData),
        };

        private List<FieldInfo> constants = new List<FieldInfo>();

        private readonly string rootPath;

        private FieldInfo[] currentFieldInfos;
        private string currentClassName;
        private string currentOutputFileFullPath;

        private string buffer;

        public CSharpToLua()
        {
            rootPath = Path.Combine(Application.dataPath, "GameAssets", "LuaScripts", "Data");
            var dirs = new DirectoryInfo(rootPath);
            if (!dirs.Exists)
            {
                dirs.Create();
            }
        }

        public void GenerateLuaConstantFiles()
        {
            foreach (var type in listTypes)
            {
                if (type.FullName != null)
                    currentClassName = type.FullName.Replace("+", "").Replace(".", "").Replace("`", "").Replace("&", "")
                        .Replace("[", "").Replace("]", "").Replace(",", "");
                currentOutputFileFullPath = Path.Combine(rootPath, currentClassName + ".lua.txt");
                GenLuaScriptByCSharpType(type);
            }
        }

        private void GenLuaScriptByCSharpType(Type type)
        {
            GetConstantFields(type);
            GenBuffer();
            GenFile();
        }

        private void GetConstantFields(Type type)
        {
            constants.Clear();

            //获取公有的，静态
            currentFieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (var info in currentFieldInfos)
            {
                if (info.IsLiteral && !info.IsInitOnly)
                {
                    constants.Add(info);
                }
            }
        }

        private void GenBuffer()
        {
            buffer = "local " + currentClassName + " = {}\n";
            foreach (var info in constants)
            {
                var outputValue = GetFiledOutputValue(info);
                buffer += $"{currentClassName}.{info.Name} = {outputValue}\n";
            }

            buffer += $"return {currentClassName}";
        }

        private void GenFile()
        {
            using (FileStream stream = new FileStream(currentOutputFileFullPath, FileMode.Create))
            {
                var sw = new StreamWriter(stream);
                sw.Write(buffer);
                sw.Flush();
                sw.Close();
            }

            AssetDatabase.Refresh();
        }

        private string GetFiledOutputValue(FieldInfo fieldInfo)
        {
            if (fieldInfo.FieldType.FullName == "System.String")
            {
                return "\"" + fieldInfo.GetRawConstantValue() + "\"";
            }
            else
            {
                return fieldInfo.GetRawConstantValue().ToString();
            }
        }
    }
}

#endif