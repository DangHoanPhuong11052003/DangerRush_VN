using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;



public class AccessoriesStoreManager : MonoBehaviour
{
    [SerializeField] private AccessoriesData accessoriesAssetData;
    [SerializeField] private Transform parentItem;
    [SerializeField] private GameObject UiItem;
    [SerializeField] private TextMeshProUGUI nameItemText;
    [SerializeField] private Transform tranformModelReview;
    [SerializeField] private GameObject ButtonBuy;
    [SerializeField] private GameObject ButtonSelect;
    [SerializeField] private Transform SelectedUI;

    private List<int> lst_idAccessoriesOwnedData=new List<int>();
    private List<Accessory> lst_accessoriesData = new List<Accessory>();
    private int idCurrentItemSelect;
    private Dictionary<int, GameObject> accessoriesItemDic=new Dictionary<int, GameObject>();
    private int currentIdModel = 0;

    private bool isFistEnable=false;
    private GameObject modelReview;

    private void Start()
    {

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
        lst_accessoriesData.Clear();
        lst_idAccessoriesOwnedData = LocalData.instance.GetIdAccessoriesOwnedData();
        foreach (var item in accessoriesAssetData.Accessories)
        {
            if (lst_idAccessoriesOwnedData.Contains(item.id))
            {
                Accessory accessory = new Accessory(item);
                accessory.isUnlocked = true;

                lst_accessoriesData.Add(accessory);
            }
            else
            {
                lst_accessoriesData.Add(new Accessory(item));
            }
        }
    }

    private void UpdateUIStore()
    {
        foreach (var item in lst_accessoriesData)
        {
            GameObject ui = Instantiate(UiItem, parentItem);
            ui.GetComponent<AccessoriesItemUI>().SetData(item, this);
            accessoriesItemDic.Add(item.id, ui);
        }
    }

    public void SelectItem(int id)
    {
        AccessoriesManager accessoriesManager = modelReview.GetComponentInChildren<AccessoriesManager>();

        if (idCurrentItemSelect == id)
        {
            accessoriesManager.ActiveAccessoriesById(id, false);
            return;
        }

        Accessory accessory = lst_accessoriesData.Find(i => i.id == id);

        if (accessory != null)
        {
            idCurrentItemSelect = accessory.id;

            nameItemText.text = accessory.name;

            accessoriesManager.ActiveAccessoriesById(id, false);

            if (!accessory.isUnlocked)
            {
                ButtonSelect.SetActive(false);
                ButtonBuy.GetComponentInChildren<TextMeshProUGUI>().text = accessory.price.ToString();
                ButtonBuy.SetActive(true);
            }
            else
            {
                ButtonBuy.SetActive(false);
                if (accessory.id == LocalData.instance.GetCurrentIdAccessories())
                {
                    ButtonSelect.SetActive(false);
                }
                else
                {
                    ButtonSelect.SetActive(true);
                }
            }
        }

        SelectedUI.transform.parent = accessoriesItemDic[accessory.id].transform;
        SelectedUI.transform.position = accessoriesItemDic[accessory.id].transform.position;
    }

    public void BuyAccessories()
    {
        int coin = LocalData.instance.GetCoin();

        Accessory accessory = lst_accessoriesData.Find(i => i.id == idCurrentItemSelect);

        if (coin >= accessory.price&& !lst_idAccessoriesOwnedData.Contains(accessory.id))
        {
            accessory.isUnlocked = true;
            lst_idAccessoriesOwnedData.Add(accessory.id);

            LocalData.instance.SetIdAccessoriesOwnedData(lst_idAccessoriesOwnedData);

            coin -= accessory.price;

            LocalData.instance.SetCoin(coin);

            ButtonBuy.SetActive(false);
            ButtonSelect.SetActive(true);
        }
    }

    public void EquipAccessories()
    {
        ButtonSelect.SetActive(false);

        LocalData.instance.SetCurrentIdAccessories(idCurrentItemSelect);
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

            Animator animator = modelReview.GetComponent<Animator>();
            animator.SetInteger("RandomIdle", UnityEngine.Random.Range(0, 5));
        }
        else
        {
            if (modelReview == null)
            {
                modelReview = Instantiate(CharacterManager.instance.GetPrefabCharacterById(currentIdData), tranformModelReview);
            }

            Animator animator = modelReview.GetComponent<Animator>();
            animator.SetInteger("RandomIdle", UnityEngine.Random.Range(0, 5));
        }
    }
}
