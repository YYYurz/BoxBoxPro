// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace GameConfig
{

using global::System;
using global::FlatBuffers;

public struct DTEntity : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static DTEntity GetRootAsDTEntity(ByteBuffer _bb) { return GetRootAsDTEntity(_bb, new DTEntity()); }
  public static DTEntity GetRootAsDTEntity(ByteBuffer _bb, DTEntity obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public DTEntity __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public uint Id { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetUint(o + __p.bb_pos) : (uint)0; } }
  public string AssetName { get { int o = __p.__offset(6); return o != 0 ? __p.__string(o + __p.bb_pos) : null; } }
#if ENABLE_SPAN_T
  public Span<byte> GetAssetNameBytes() { return __p.__vector_as_span(6); }
#else
  public ArraySegment<byte>? GetAssetNameBytes() { return __p.__vector_as_arraysegment(6); }
#endif
  public byte[] GetAssetNameArray() { return __p.__vector_as_array<byte>(6); }

  public static Offset<DTEntity> CreateDTEntity(FlatBufferBuilder builder,
      uint Id = 0,
      StringOffset AssetNameOffset = default(StringOffset)) {
    builder.StartObject(2);
    DTEntity.AddAssetName(builder, AssetNameOffset);
    DTEntity.AddId(builder, Id);
    return DTEntity.EndDTEntity(builder);
  }

  public static void StartDTEntity(FlatBufferBuilder builder) { builder.StartObject(2); }
  public static void AddId(FlatBufferBuilder builder, uint Id) { builder.AddUint(0, Id, 0); }
  public static void AddAssetName(FlatBufferBuilder builder, StringOffset AssetNameOffset) { builder.AddOffset(1, AssetNameOffset.Value, 0); }
  public static Offset<DTEntity> EndDTEntity(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<DTEntity>(o);
  }
};

public struct DTEntityList : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static DTEntityList GetRootAsDTEntityList(ByteBuffer _bb) { return GetRootAsDTEntityList(_bb, new DTEntityList()); }
  public static DTEntityList GetRootAsDTEntityList(ByteBuffer _bb, DTEntityList obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p.bb_pos = _i; __p.bb = _bb; }
  public DTEntityList __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public DTEntity? Data(int j) { int o = __p.__offset(4); return o != 0 ? (DTEntity?)(new DTEntity()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int DataLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<DTEntityList> CreateDTEntityList(FlatBufferBuilder builder,
      VectorOffset dataOffset = default(VectorOffset)) {
    builder.StartObject(1);
    DTEntityList.AddData(builder, dataOffset);
    return DTEntityList.EndDTEntityList(builder);
  }

  public static void StartDTEntityList(FlatBufferBuilder builder) { builder.StartObject(1); }
  public static void AddData(FlatBufferBuilder builder, VectorOffset dataOffset) { builder.AddOffset(0, dataOffset.Value, 0); }
  public static VectorOffset CreateDataVector(FlatBufferBuilder builder, Offset<DTEntity>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateDataVectorBlock(FlatBufferBuilder builder, Offset<DTEntity>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartDataVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<DTEntityList> EndDTEntityList(FlatBufferBuilder builder) {
    int o = builder.EndObject();
    return new Offset<DTEntityList>(o);
  }
};


}