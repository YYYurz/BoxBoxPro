# automatically generated by the FlatBuffers compiler, do not modify

# namespace: GameConfig

import flatbuffers

class DTEntity(object):
    __slots__ = ['_tab']

    @classmethod
    def GetRootAsDTEntity(cls, buf, offset):
        n = flatbuffers.encode.Get(flatbuffers.packer.uoffset, buf, offset)
        x = DTEntity()
        x.Init(buf, n + offset)
        return x

    # DTEntity
    def Init(self, buf, pos):
        self._tab = flatbuffers.table.Table(buf, pos)

    # DTEntity
    def Id(self):
        o = flatbuffers.number_types.UOffsetTFlags.py_type(self._tab.Offset(4))
        if o != 0:
            return self._tab.Get(flatbuffers.number_types.Uint32Flags, o + self._tab.Pos)
        return 0

    # DTEntity
    def AssetName(self):
        o = flatbuffers.number_types.UOffsetTFlags.py_type(self._tab.Offset(6))
        if o != 0:
            return self._tab.String(o + self._tab.Pos)
        return None

def DTEntityStart(builder): builder.StartObject(2)
def DTEntityAddId(builder, Id): builder.PrependUint32Slot(0, Id, 0)
def DTEntityAddAssetName(builder, AssetName): builder.PrependUOffsetTRelativeSlot(1, flatbuffers.number_types.UOffsetTFlags.py_type(AssetName), 0)
def DTEntityEnd(builder): return builder.EndObject()
