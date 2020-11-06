# automatically generated by the FlatBuffers compiler, do not modify

# namespace: GameConfig

import flatbuffers

class DTSoundDataList(object):
    __slots__ = ['_tab']

    @classmethod
    def GetRootAsDTSoundDataList(cls, buf, offset):
        n = flatbuffers.encode.Get(flatbuffers.packer.uoffset, buf, offset)
        x = DTSoundDataList()
        x.Init(buf, n + offset)
        return x

    # DTSoundDataList
    def Init(self, buf, pos):
        self._tab = flatbuffers.table.Table(buf, pos)

    # DTSoundDataList
    def Data(self, j):
        o = flatbuffers.number_types.UOffsetTFlags.py_type(self._tab.Offset(4))
        if o != 0:
            x = self._tab.Vector(o)
            x += flatbuffers.number_types.UOffsetTFlags.py_type(j) * 4
            x = self._tab.Indirect(x)
            from .DTSoundData import DTSoundData
            obj = DTSoundData()
            obj.Init(self._tab.Bytes, x)
            return obj
        return None

    # DTSoundDataList
    def DataLength(self):
        o = flatbuffers.number_types.UOffsetTFlags.py_type(self._tab.Offset(4))
        if o != 0:
            return self._tab.VectorLen(o)
        return 0

def DTSoundDataListStart(builder): builder.StartObject(1)
def DTSoundDataListAddData(builder, data): builder.PrependUOffsetTRelativeSlot(0, flatbuffers.number_types.UOffsetTFlags.py_type(data), 0)
def DTSoundDataListStartDataVector(builder, numElems): return builder.StartVector(4, numElems, 4)
def DTSoundDataListEnd(builder): return builder.EndObject()