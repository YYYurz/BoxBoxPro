// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfig
{

using global::System;
using global::FlatBuffers;

public struct DTScene : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static DTScene GetRootAsDTScene(ByteBuffer _bb) { return GetRootAsDTScene(_bb, new DTScene()); }
  public static DTScene GetRootAsDTScene(ByteBuffer _bb, DTScene obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public DTScene __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public uint Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetUint(o + __p.bb_pos) : (uint)0; } }
  public string AssetName { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetAssetNameBytes() { return __p.__vector_as_span(6); }
#else
  public ArraySegment<byte>? GetAssetNameBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetAssetNameArray() { return __p.__vector_as_array<byte>(6); }

  public static Offset<DTScene> CreateDTScene(FlatBufferBuilder builder,
      uint Id = 0,
      StringOffset AssetNameOffset = default(StringOffset)) {
    builder.StartObject(2);
    DTScene.AddAssetName(builder, AssetNameOffset);
    DTScene.AddId(builder, Id);
    return DTScene.EndDTScene(builder);
  }

  public static void StartDTScene(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddId(FlatBufferBuilder builder, uint Id) { builder.AddUint(0, Id, 0); }
  public static void AddAssetName(FlatBufferBuilder builder, StringOffset AssetNameOffset) { builder.AddOffset(1, AssetNameOffset.Value, 0); }
  public static Offset<DTScene> EndDTScene(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<DTScene>(o);
  }
};

public struct DTSceneList : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static DTSceneList GetRootAsDTSceneList(ByteBuffer _bb) { return GetRootAsDTSceneList(_bb, new DTSceneList()); }
  public static DTSceneList GetRootAsDTSceneList(ByteBuffer _bb, DTSceneList obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public DTSceneList __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public DTScene? Data(int j) { int o = __p.__offset(4); return o != 0 ? (DTScene?)(new DTScene()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DataLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<DTSceneList> CreateDTSceneList(FlatBufferBuilder builder,
      VectorOffset dataOffset = default(VectorOffset)) {
    builder.StartObject(1);
    DTSceneList.AddData(builder, dataOffset);
    return DTSceneList.EndDTSceneList(builder);
  }

  public static void StartDTSceneList(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddData(FlatBufferBuilder builder, VectorOffset dataOffset) { builder.AddOffset(0, dataOffset.Value, 0); }
  public static VectorOffset CreateDataVector(FlatBufferBuilder builder, Offset<DTScene>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDataVectorBlock(FlatBufferBuilder builder, Offset<DTScene>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDataVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<DTSceneList> EndDTSceneList(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<DTSceneList>(o);
  }
};


}