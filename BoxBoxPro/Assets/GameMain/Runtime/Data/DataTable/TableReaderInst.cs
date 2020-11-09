using FlatBuffers;
using GameConfig;


public class DTGameConfigTableReader : TableReader<DTGameConfig, DTGameConfigList, DTGameConfigTableReader>
{
    public override string TablePath => "Assets/GameAssets/DataTables/bytes/DTGameConfig.bytes";   
    protected override DTGameConfig? GetData(DTGameConfigList dataList, int i)
    {
        return dataList.Data(i);
    }
    protected override int GetDataLength(DTGameConfigList dataList)
    {
        return dataList.DataLength;
    }
    protected override uint GetKey(DTGameConfig data)
    {
        return data.Id;
    }
    protected override DTGameConfigList GetTableDataList(ByteBuffer byteBuffer)
    {
        return DTGameConfigList.GetRootAsDTGameConfigList(byteBuffer);
    }
}

public class DTSoundDataTableReader : TableReader<DTSoundData, DTSoundDataList, DTSoundDataTableReader>
{
    public override string TablePath => "Assets/GameAssets/DataTables/bytes/DTSoundData.bytes";   
    protected override DTSoundData? GetData(DTSoundDataList dataList, int i)
    {
        return dataList.Data(i);
    }
    protected override int GetDataLength(DTSoundDataList dataList)
    {
        return dataList.DataLength;
    }
    protected override uint GetKey(DTSoundData data)
    {
        return data.Id;
    }
    protected override DTSoundDataList GetTableDataList(ByteBuffer byteBuffer)
    {
        return DTSoundDataList.GetRootAsDTSoundDataList(byteBuffer);
    }
}

public class DTUIFormDataTableReader : TableReader<DTUIFormData, DTUIFormDataList, DTUIFormDataTableReader>
{
    public override string TablePath => "Assets/GameAssets/DataTables/bytes/DTUIFormData.bytes";   
    protected override DTUIFormData? GetData(DTUIFormDataList dataList, int i)
    {
        return dataList.Data(i);
    }
    protected override int GetDataLength(DTUIFormDataList dataList)
    {
        return dataList.DataLength;
    }
    protected override uint GetKey(DTUIFormData data)
    {
        return data.Id;
    }
    protected override DTUIFormDataList GetTableDataList(ByteBuffer byteBuffer)
    {
        return DTUIFormDataList.GetRootAsDTUIFormDataList(byteBuffer);
    }
}
