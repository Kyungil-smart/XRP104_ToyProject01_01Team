using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [field: SerializeField] public List<StageInfo> StageInfoList { get; private set; }
    [field: SerializeField] public List<Itembase> ItembaseList { get; private set; }
    
    private void Awake()
    {
        SingletonInit();
    }

    public void LoadStageInfo(int stageIndex)
    {
        Instantiate(StageInfoList[stageIndex]);
    }

    public void DropRandomItem()
    {
        int index = Random.Range(0, ItembaseList.Count);
        Instantiate(ItembaseList[index]).Drop();
    }
}
