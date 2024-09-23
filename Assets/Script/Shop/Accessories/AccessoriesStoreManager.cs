using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Animations;
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
    [SerializeField] private Transform tranformModelReview;
    [SerializeField] private GameObject ButtonBuy;
    [SerializeField] private GameObject ButtonSelect;
    [SerializeField] private Transform SelectedUI;
    [SerializeField] private AnimatorController animatorController;

    private List<AccessoriesData> localDatas=new List<AccessoriesData>();
    private AccessoriesItem currentItemSelect=new AccessoriesItem();
    private Dictionary<int, GameObject> accessoriesItemDic=new Dictionary<int, GameObject>();
    private int currentIdModel = 0;

    private bool isFistEnable=false;
    private GameObject modelReview;

    private void Start()
    {
        localDatas=LocalData.instance.GetAccessoriesData();

        UpdateDataStore();
        UpdateUIStore();
        CreateModelReview();
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

            CreateModelReview();

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
        AccessoriesManager accessoriesManager = modelReview.GetComponentInChildren<AccessoriesManager>();

        if (currentItemSelect.id== id)
        {
            accessoriesManager.ActiveAccessoriesById(id,false);
            return;
        }

        AccessoriesItem item = itemsList.Find(i => i.id == id);

        if(item!=null)
        {
            currentItemSelect=item;

            nameItemText.text = item.name;

            accessoriesManager.ActiveAccessoriesById(id,false);

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


    private void CreateModelReview()
    {
        int currentIdData = LocalData.instance.GetCurrentChar();

        if (currentIdModel != currentIdData)
        {
            if (modelReview != null)
            {
                Destroy(modelReview);
            }
            modelReview = Instantiate(CharacterManager.instance.GetPrefabCharacterById(currentIdData), tranformModelReview);
            currentIdModel = currentIdData;
            modelReview.GetComponent<Animator>().runtimeAnimatorController = animatorController;

            Animator animator = modelReview.GetComponent<Animator>();
            animator.SetInteger("RandomIdle", UnityEngine.Random.Range(0, 5));
        }
        else
        {
            if (modelReview == null)
            {
                modelReview = Instantiate(CharacterManager.instance.GetPrefabCharacterById(currentIdData), tranformModelReview);
                modelReview.GetComponent<Animator>().runtimeAnimatorController = animatorController;
            }

            Animator animator = modelReview.GetComponent<Animator>();
            animator.SetInteger("RandomIdle", UnityEngine.Random.Range(0, 5));
        }
    }
}
