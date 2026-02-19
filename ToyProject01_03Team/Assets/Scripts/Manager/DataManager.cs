using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [field: SerializeField] public List<Itembase> ItembaseList { get; private set; }
    
    private void Awake()
    {
        SingletonInit();
    }

    public void DropRandomItem(Transform dropPoint)
    {
        int index = Random.Range(0, ItembaseList.Count);
        Itembase item = Instantiate(ItembaseList[index], dropPoint.position, Quaternion.identity);
        item.transform.SetParent(transform);
        item.Drop();
    }
}
