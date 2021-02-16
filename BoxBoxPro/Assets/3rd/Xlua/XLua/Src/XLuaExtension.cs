#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;

#endif


namespace XLua
{
    public partial class ObjectTranslator
    {
        internal T popValue<T>(RealStatePtr L, int oldTop)
        {
            var newTop = LuaAPI.lua_gettop(L);
            if (oldTop == newTop)
            {
                return default;
            }
            
            object ret = null;
            for (var i = oldTop + 1; i <= newTop; i++)
            {
                var obj = GetObject(L, i, typeof(T));
                if (obj != null)
                {
                    ret = obj;
                }
                return (T) ret;
            }
            LuaAPI.lua_settop(L, oldTop);
            return default;
        }
    }

    public static class XLuaExtension
    {
        public static LuaTable DoStringCustom(this LuaEnv luaEnv, string chunk, string chunkName = "chunk",
            LuaTable env = null)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(chunk);
            return luaEnv.DoStringCustom<LuaTable>(bytes, chunkName, env);
        }

        public static T DoStringCustom<T>(this LuaEnv luaEnv, byte[] chunk, string chunkName = "chunk",
            LuaTable env = null)
        {
#if THREAD_SAFE || HOTFIX_ENABLE
            lock (luaEnvLock)
            {
#endif
            var _L = luaEnv.L;
            var oldTop = LuaAPI.lua_gettop(_L);
            var errFunc = LuaAPI.load_error_func(_L, luaEnv.errorFuncRef);
            if (LuaAPI.xluaL_loadbuffer(_L, chunk, chunk.Length, chunkName) == 0)
            {
                var ol = LuaAPI.lua_gettop(_L);
                if (env != null)
                {
                    env.push(_L);
                    LuaAPI.lua_setfenv(_L, -2);
                }
                if (LuaAPI.lua_pcall(_L, 0, -1, errFunc) == 0)
                {
                    LuaAPI.lua_remove(_L, errFunc);
                    return luaEnv.translator.popValue<T>(_L, oldTop);
                }
                luaEnv.ThrowExceptionFromError(oldTop);
            }
            else
                luaEnv.ThrowExceptionFromError(oldTop);
            return default;
#if THREAD_SAFE || HOTFIX_ENABLE
            }
#endif
        }
    }
}