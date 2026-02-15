using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [field: SerializeField] public List<StageInfo> StageInfoList { get; private set; }

    private void Awake()
    {
        SingletonInit();
    }

    public void LoadStageInfo(int stageIndex)
    {
        Instantiate(StageInfoList[stageIndex]);
    }
}
