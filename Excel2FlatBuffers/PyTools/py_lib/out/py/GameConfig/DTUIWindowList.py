# automatically generated by the FlatBuffers compiler, do not modify

# namespace: GameConfig

import flatbuffers

class DTUIWindowList(object):
    __slots__ = ['_tab']

    @classmethod
    def GetRootAsDTUIWindowList(cls, buf, offset):
        n = flatbuffers.encode.Get(flatbuffers.packer.uoffset, buf, offset)
        x = DTUIWindowList()
        x.Init(buf, n + offset)
        return x

    # DTUIWindowList
    def Init(self, buf, pos):
        self._tab = flatbuffers.table.Table(buf, pos)

    # DTUIWindowList
    def Data(self, j):
        o = flatbuffers.number_types.UOffsetTFlags.py_type(self._tab.Offset(4))
        if o != 0:
            x = self._tab.Vector(o)
            x += flatbuffers.number_types.UOffsetTFlags.py_type(j) * 4
            x = self._tab.Indirect(x)
            from .DTUIWindow import DTUIWindow
            obj = DTUIWindow()
            obj.Init(self._tab.Bytes, x)
            return obj
        return None

    # DTUIWindowList
    def DataLength(self):
        o = flatbuffers.number_types.UOffsetTFlags.py_type(self._tab.Offset(4))
        if o != 0:
            return self._tab.VectorLen(o)
        return 0

def DTUIWindowListStart(builder): builder.StartObject(1)
def DTUIWindowListAddData(builder, data): builder.PrependUOffsetTRelativeSlot(0, flatbuffers.number_types.UOffsetTFlags.py_type(data), 0)
def DTUIWindowListStartDataVector(builder, numElems): return builder.StartVector(4, numElems, 4)
def DTUIWindowListEnd(builder): return builder.EndObject()