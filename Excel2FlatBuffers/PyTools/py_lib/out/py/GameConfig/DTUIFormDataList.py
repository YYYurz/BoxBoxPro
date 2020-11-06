# automatically generated by the FlatBuffers compiler, do not modify

# namespace: GameConfig

import flatbuffers

class DTUIFormDataList(object):
    __slots__ = ['_tab']

    @classmethod
    def GetRootAsDTUIFormDataList(cls, buf, offset):
        n = flatbuffers.encode.Get(flatbuffers.packer.uoffset, buf, offset)
        x = DTUIFormDataList()
        x.Init(buf, n + offset)
        return x

    # DTUIFormDataList
    def Init(self, buf, pos):
        self._tab = flatbuffers.table.Table(buf, pos)

    # DTUIFormDataList
    def Data(self, j):
        o = flatbuffers.number_types.UOffsetTFlags.py_type(self._tab.Offset(4))
        if o != 0:
            x = self._tab.Vector(o)
            x += flatbuffers.number_types.UOffsetTFlags.py_type(j) * 4
            x = self._tab.Indirect(x)
            from .DTUIFormData import DTUIFormData
            obj = DTUIFormData()
            obj.Init(self._tab.Bytes, x)
            return obj
        return None

    # DTUIFormDataList
    def DataLength(self):
        o = flatbuffers.number_types.UOffsetTFlags.py_type(self._tab.Offset(4))
        if o != 0:
            return self._tab.VectorLen(o)
        return 0

def DTUIFormDataListStart(builder): builder.StartObject(1)
def DTUIFormDataListAddData(builder, data): builder.PrependUOffsetTRelativeSlot(0, flatbuffers.number_types.UOffsetTFlags.py_type(data), 0)
def DTUIFormDataListStartDataVector(builder, numElems): return builder.StartVector(4, numElems, 4)
def DTUIFormDataListEnd(builder): return builder.EndObject()