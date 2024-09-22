using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class AccessoriesItem
{
    public int id;
    public string name;
    public string description;
    public bool isUnlocked;
    public int price;
    public Sprite icon;

    public AccessoriesItem()
    {
        id = -1;
    }
}

public class AccessoriesStoreManager : MonoBehaviour
{
    [SerializeField] private List<AccessoriesItem> itemsList=new List<AccessoriesItem>();
    [SerializeField] private Transform parentItem;
    [SerializeField] private GameObject UiItem;
    [SerializeField] private TextMeshProUGUI nameItemText;
    [SerializeField] private Transform parentModelReview;
    [SerializeField] private GameObject ButtonBuy;
    [SerializeField] private GameObject ButtonSelect;

    private List<AccessoriesData> localDatas=new List<AccessoriesData>();
    private AccessoriesItem currentItemSelect=new AccessoriesItem();
    private Dictionary<int, GameObject> accessoriesItemDic=new Dictionary<int, GameObject>();


    private void Start()
    {
        localDatas=LocalData.instance.GetAccessoriesData();

        UpdateDataStore();
        UpdateUIStore();

        int selectAccessId = LocalData.instance.GetCurrentIdAccessories();  

        if (selectAccessId != -1)
        {
            SelectItem(selectAccessId);
        }
        else
        {
            SelectItem(0);
        }
    }


    private void UpdateDataStore()
    {
        if(localDatas.Count == 0)
        {
            foreach (var item in itemsList)
            {
                localDatas.Add(new AccessoriesData(item.id, item.isUnlocked));
            }
            LocalData.instance.SetAccessoriesData(localDatas);
        }
        else
        {
            foreach (var item in itemsList)
            {
                AccessoriesData accessoriesData = localDatas.Find(i => i.id == item.id);
                if (accessoriesData == null)
                {
                    localDatas.Add(new AccessoriesData(item.id,item.isUnlocked));
                }
                else
                {
                    item.isUnlocked = accessoriesData.isUnlocked;
                }
            }
        }
    }

    private void UpdateUIStore()
    {
        foreach (var item in itemsList)
        {
            GameObject ui = Instantiate(UiItem, parentItem);
            ui.GetComponent<AccessoriesItemUI>().SetData(item, this);
            accessoriesItemDic.Add(item.id, ui);
        }
    }

    public void SelectItem(int id)
    {
        if(currentItemSelect.id== id) return;

        AccessoriesItem item = itemsList.Find(i => i.id == id);

        if(item!=null)
        {
            currentItemSelect=item;

            nameItemText.text = item.name;

            if (!currentItemSelect.isUnlocked)
            {
                ButtonBuy.GetComponentInChildren<TextMeshProUGUI>().text = currentItemSelect.price.ToString();
                ButtonBuy.SetActive(true);
            }
            else
            {
                ButtonBuy.SetActive(false);
                if (currentItemSelect.id == LocalData.instance.GetCurrentChar())
                {
                    ButtonSelect.SetActive(false);
                }
                else
                {
                    ButtonSelect.SetActive(true);
                }
            }
        }
    }
}
