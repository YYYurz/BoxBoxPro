# automatically generated by the FlatBuffers compiler, do not modify

# namespace: GameConfig

import flatbuffers

class DTUIFormData(object):
    __slots__ = ['_tab']

    @classmethod
    def GetRootAsDTUIFormData(cls, buf, offset):
        n = flatbuffers.encode.Get(flatbuffers.packer.uoffset, buf, offset)
        x = DTUIFormData()
        x.Init(buf, n + offset)
        return x

    # DTUIFormData
    def Init(self, buf, pos):
        self._tab = flatbuffers.table.Table(buf, pos)

    # DTUIFormData
    def Id(self):
        o = flatbuffers.number_types.UOffsetTFlags.py_type(self._tab.Offset(4))
        if o != 0:
            return self._tab.Get(flatbuffers.number_types.Uint32Flags, o + self._tab.Pos)
        return 0

    # DTUIFormData
    def UIName(self):
        o = flatbuffers.number_types.UOffsetTFlags.py_type(self._tab.Offset(6))
        if o != 0:
            return self._tab.String(o + self._tab.Pos)
        return None

    # DTUIFormData
    def UIGroupName(self):
        o = flatbuffers.number_types.UOffsetTFlags.py_type(self._tab.Offset(8))
        if o != 0:
            return self._tab.String(o + self._tab.Pos)
        return None

    # DTUIFormData
    def AllowMultiInstance(self):
        o = flatbuffers.number_types.UOffsetTFlags.py_type(self._tab.Offset(10))
        if o != 0:
            return self._tab.Get(flatbuffers.number_types.Uint32Flags, o + self._tab.Pos)
        return 0

    # DTUIFormData
    def PauseCoveredUIForm(self):
        o = flatbuffers.number_types.UOffsetTFlags.py_type(self._tab.Offset(12))
        if o != 0:
            return self._tab.Get(flatbuffers.number_types.Uint32Flags, o + self._tab.Pos)
        return 0

    # DTUIFormData
    def AssetPath(self):
        o = flatbuffers.number_types.UOffsetTFlags.py_type(self._tab.Offset(14))
        if o != 0:
            return self._tab.String(o + self._tab.Pos)
        return None

    # DTUIFormData
    def LuaFile(self):
        o = flatbuffers.number_types.UOffsetTFlags.py_type(self._tab.Offset(16))
        if o != 0:
            return self._tab.String(o + self._tab.Pos)
        return None

def DTUIFormDataStart(builder): builder.StartObject(7)
def DTUIFormDataAddId(builder, Id): builder.PrependUint32Slot(0, Id, 0)
def DTUIFormDataAddUIName(builder, UIName): builder.PrependUOffsetTRelativeSlot(1, flatbuffers.number_types.UOffsetTFlags.py_type(UIName), 0)
def DTUIFormDataAddUIGroupName(builder, UIGroupName): builder.PrependUOffsetTRelativeSlot(2, flatbuffers.number_types.UOffsetTFlags.py_type(UIGroupName), 0)
def DTUIFormDataAddAllowMultiInstance(builder, AllowMultiInstance): builder.PrependUint32Slot(3, AllowMultiInstance, 0)
def DTUIFormDataAddPauseCoveredUIForm(builder, PauseCoveredUIForm): builder.PrependUint32Slot(4, PauseCoveredUIForm, 0)
def DTUIFormDataAddAssetPath(builder, AssetPath): builder.PrependUOffsetTRelativeSlot(5, flatbuffers.number_types.UOffsetTFlags.py_type(AssetPath), 0)
def DTUIFormDataAddLuaFile(builder, LuaFile): builder.PrependUOffsetTRelativeSlot(6, flatbuffers.number_types.UOffsetTFlags.py_type(LuaFile), 0)
def DTUIFormDataEnd(builder): return builder.EndObject()
