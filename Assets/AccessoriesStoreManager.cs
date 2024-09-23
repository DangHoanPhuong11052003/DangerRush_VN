using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] private GameObject modelReview;
    [SerializeField] private GameObject ButtonBuy;
    [SerializeField] private GameObject ButtonSelect;
    [SerializeField] private Transform SelectedUI;

    private List<AccessoriesData> localDatas=new List<AccessoriesData>();
    private AccessoriesItem currentItemSelect=new AccessoriesItem();
    private Dictionary<int, GameObject> accessoriesItemDic=new Dictionary<int, GameObject>();

    private bool isFistEnable=false;
    private AccessoriesManager accessoriesManager;

    private void Start()
    {
        localDatas=LocalData.instance.GetAccessoriesData();
        accessoriesManager= modelReview.GetComponentInChildren<AccessoriesManager>();

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
        isFistEnable = true;
    }

    private void OnEnable()
    {
        if (isFistEnable)
        {
            UpdateDataStore();
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
        if(currentItemSelect.id== id)
        {
            accessoriesManager.ActiveAccessoriesById(id);
            return;
        }

        AccessoriesItem item = itemsList.Find(i => i.id == id);

        if(item!=null)
        {
            currentItemSelect=item;

            nameItemText.text = item.name;

            accessoriesManager.ActiveAccessoriesById(id);

            if (!currentItemSelect.isUnlocked)
            {
                ButtonSelect.SetActive(false);
                ButtonBuy.GetComponentInChildren<TextMeshProUGUI>().text = currentItemSelect.price.ToString();
                ButtonBuy.SetActive(true);
            }
            else
            {
                ButtonBuy.SetActive(false);
                if (currentItemSelect.id == LocalData.instance.GetCurrentIdAccessories())
                {
                    ButtonSelect.SetActive(false);
                }
                else
                {
                    ButtonSelect.SetActive(true);
                }
            }
        }

        SelectedUI.transform.parent = accessoriesItemDic[currentItemSelect.id].transform;
        SelectedUI.transform.position = accessoriesItemDic[currentItemSelect.id].transform.position;
    }

    public void BuyAccessories()
    {
        int coin = LocalData.instance.GetCoin();

        if (coin >= currentItemSelect.price)
        {
            currentItemSelect.isUnlocked= true;

            localDatas.Find(i => i.id == currentItemSelect.id).isUnlocked=true;

            LocalData.instance.SetAccessoriesData(localDatas);

            coin -= currentItemSelect.price;

            LocalData.instance.SetCoin(coin);

            ButtonBuy.SetActive(false);
            ButtonSelect.SetActive(true);
        }
    }

    public void EquipAccessories()
    {
        ButtonSelect.SetActive(false);

        LocalData.instance.SetCurrentIdAccessories(currentItemSelect.id);
    }
}
