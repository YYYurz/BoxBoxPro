// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfig
{

using global::System;
using global::FlatBuffers;

public struct DTUIWindow : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static DTUIWindow GetRootAsDTUIWindow(ByteBuffer _bb) { return GetRootAsDTUIWindow(_bb, new DTUIWindow()); }
  public static DTUIWindow GetRootAsDTUIWindow(ByteBuffer _bb, DTUIWindow obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public DTUIWindow __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public uint Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetUint(o + __p.bb_pos) : (uint)0; } }
  public string UIName { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetUINameBytes() { return __p.__vector_as_span(6); }
#else
  public ArraySegment<byte>? GetUINameBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetUINameArray() { return __p.__vector_as_array<byte>(6); }
  public string UIGroupName { get { int o = __p.__offset(8); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetUIGroupNameBytes() { return __p.__vector_as_span(8); }
#else
  public ArraySegment<byte>? GetUIGroupNameBytes() { return __p.__vector_as_arraysegment(8); }
#endif
  public byte[] GetUIGroupNameArray() { return __p.__vector_as_array<byte>(8); }
  public uint AllowMultiInstance { get { int o = __p.__offset(10); return o != 0 ? __p.bb.GetUint(o + __p.bb_pos) : (uint)0; } }
  public uint PauseCoveredUIForm { get { int o = __p.__offset(12); return o != 0 ? __p.bb.GetUint(o + __p.bb_pos) : (uint)0; } }
  public string AssetPath { get { int o = __p.__offset(14); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetAssetPathBytes() { return __p.__vector_as_span(14); }
#else
  public ArraySegment<byte>? GetAssetPathBytes() { return __p.__vector_as_arraysegment(14); }
#endif
  public byte[] GetAssetPathArray() { return __p.__vector_as_array<byte>(14); }
  public string LuaFile { get { int o = __p.__offset(16); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetLuaFileBytes() { return __p.__vector_as_span(16); }
#else
  public ArraySegment<byte>? GetLuaFileBytes() { return __p.__vector_as_arraysegment(16); }
#endif
  public byte[] GetLuaFileArray() { return __p.__vector_as_array<byte>(16); }

  public static Offset<DTUIWindow> CreateDTUIWindow(FlatBufferBuilder builder,
      uint Id = 0,
      StringOffset UINameOffset = default(StringOffset),
      StringOffset UIGroupNameOffset = default(StringOffset),
      uint AllowMultiInstance = 0,
      uint PauseCoveredUIForm = 0,
      StringOffset AssetPathOffset = default(StringOffset),
      StringOffset LuaFileOffset = default(StringOffset)) {
    builder.StartObject(7);
    DTUIWindow.AddLuaFile(builder, LuaFileOffset);
    DTUIWindow.AddAssetPath(builder, AssetPathOffset);
    DTUIWindow.AddPauseCoveredUIForm(builder, PauseCoveredUIForm);
    DTUIWindow.AddAllowMultiInstance(builder, AllowMultiInstance);
    DTUIWindow.AddUIGroupName(builder, UIGroupNameOffset);
    DTUIWindow.AddUIName(builder, UINameOffset);
    DTUIWindow.AddId(builder, Id);
    return DTUIWindow.EndDTUIWindow(builder);
  }

  public static void StartDTUIWindow(FlatBufferBuilder builder) { builder.StartObject(7); }
  public static void AddId(FlatBufferBuilder builder, uint Id) { builder.AddUint(0, Id, 0); }
  public static void AddUIName(FlatBufferBuilder builder, StringOffset UINameOffset) { builder.AddOffset(1, UINameOffset.Value, 0); }
  public static void AddUIGroupName(FlatBufferBuilder builder, StringOffset UIGroupNameOffset) { builder.AddOffset(2, UIGroupNameOffset.Value, 0); }
  public static void AddAllowMultiInstance(FlatBufferBuilder builder, uint AllowMultiInstance) { builder.AddUint(3, AllowMultiInstance, 0); }
  public static void AddPauseCoveredUIForm(FlatBufferBuilder builder, uint PauseCoveredUIForm) { builder.AddUint(4, PauseCoveredUIForm, 0); }
  public static void AddAssetPath(FlatBufferBuilder builder, StringOffset AssetPathOffset) { builder.AddOffset(5, AssetPathOffset.Value, 0); }
  public static void AddLuaFile(FlatBufferBuilder builder, StringOffset LuaFileOffset) { builder.AddOffset(6, LuaFileOffset.Value, 0); }
  public static Offset<DTUIWindow> EndDTUIWindow(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<DTUIWindow>(o);
  }
};

public struct DTUIWindowList : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static DTUIWindowList GetRootAsDTUIWindowList(ByteBuffer _bb) { return GetRootAsDTUIWindowList(_bb, new DTUIWindowList()); }
  public static DTUIWindowList GetRootAsDTUIWindowList(ByteBuffer _bb, DTUIWindowList obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public DTUIWindowList __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public DTUIWindow? Data(int j) { int o = __p.__offset(4); return o != 0 ? (DTUIWindow?)(new DTUIWindow()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DataLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<DTUIWindowList> CreateDTUIWindowList(FlatBufferBuilder builder,
      VectorOffset dataOffset = default(VectorOffset)) {
    builder.StartObject(1);
    DTUIWindowList.AddData(builder, dataOffset);
    return DTUIWindowList.EndDTUIWindowList(builder);
  }

  public static void StartDTUIWindowList(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddData(FlatBufferBuilder builder, VectorOffset dataOffset) { builder.AddOffset(0, dataOffset.Value, 0); }
  public static VectorOffset CreateDataVector(FlatBufferBuilder builder, Offset<DTUIWindow>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDataVectorBlock(FlatBufferBuilder builder, Offset<DTUIWindow>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDataVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<DTUIWindowList> EndDTUIWindowList(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<DTUIWindowList>(o);
  }
};


}
