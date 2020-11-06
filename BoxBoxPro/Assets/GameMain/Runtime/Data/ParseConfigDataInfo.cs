using System;

namespace Hr
{
    public class ParseConfigDataInfo
    {
        public string ConfigName { get; private set; }

        public Type ConfigClassType { get; }

        public object UserData { get; private set; }

        // public GameEnumType.GAMEASSET_TYPE DataType { get; private set; }

        public ParseConfigDataInfo(string configName, Type type, object userData/*, GameEnumType.GAMEASSET_TYPE dataType*/)
        {
            ConfigName = configName;
            ConfigClassType = type;
            UserData = userData;
            // DataType = dataType;
        }

        /// <summary>
        /// todo
        /// </summary>
        public void Clear()
        {
            ConfigName = null;
            UserData = null;
            // DataType = GameEnumType.GAMEASSET_TYPE.PAT_NORMAL;
        }
    }
}
