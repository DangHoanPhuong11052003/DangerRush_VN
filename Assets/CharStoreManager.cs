using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class CharacterItem
{
    public int id;
    public string name;
    public int price;
    public Sprite icon;
    public bool isUnlocked;
    public GameObject modelReview;

    public CharacterItem()
    {
        id = -1;
    }
}

public class CharStoreManager : MonoBehaviour
{
    [SerializeField] private List<CharacterItem> characterItems = new List<CharacterItem>();
    [SerializeField] private GameObject ButtonBuy;
    [SerializeField] private GameObject ButtonSelect;
    [SerializeField] private GameObject IteamUIPerfab;
    [SerializeField] private Transform transformItems;

    private List<CharacterData> characterDatas = new List<CharacterData>();
    private List<CharacterItem> characterItemsData=new List<CharacterItem>();
    private CharacterItem currentItemSeleted=new CharacterItem();

    private Dictionary<int, GameObject> arrayItems = new Dictionary<int, GameObject>();

    private bool isFistStart = true;

    // Start is called before the first frame update
    private void Start()
    {
        characterItemsData = characterItems;
        characterDatas=LocalData.instance.GetCharacterData();

        UpdateDataStore();

        UpdateUIStore();

        SelectedItem(LocalData.instance.GetCurrentChar());

        isFistStart = false;

    }
    private void OnEnable()
    {
        if(!isFistStart)
        {
            SelectedItem(LocalData.instance.GetCurrentChar());
        }
    }

    private void UpdateDataStore()
    {
        if (characterDatas.Count==0)
        {
            foreach (var item in characterItems)
            {
                characterDatas.Add(new CharacterData(item.id, item.isUnlocked));
            }
            LocalData.instance.SetCharacterData(characterDatas);

        }
        else
        {
            foreach (var item in characterItems)
            {
                int index = characterDatas.FindIndex(c => c.id == item.id);
                if (index != -1)
                {
                    characterItemsData[characterItemsData.FindIndex(c => c.id == item.id)].isUnlocked = characterDatas[index].isUnlock;
                }
                else
                {
                    characterDatas.Add(new CharacterData(item.id, item.isUnlocked));
                }
            }
        }
    }

    private void UpdateUIStore()
    {
        foreach(var item in characterItemsData)
        {
            GameObject itemUI = Instantiate(IteamUIPerfab, transformItems);
            ItemsCharUI itemsCharUI= itemUI.GetComponent<ItemsCharUI>();
            itemsCharUI.SetData(item, this);

            arrayItems.Add(item.id, itemUI);
        }
    }

    public void SelectedItem(int id)
    {
        if (currentItemSeleted.id== id)
        {
            currentItemSeleted.modelReview.GetComponent<Animator>().SetInteger("RandomIdle", UnityEngine.Random.Range(0, 5));
            return;
        }

        foreach (var item in characterItemsData)
        {
            if (item.id == id)
            { 
                if (currentItemSeleted.modelReview != null)
                {
                    currentItemSeleted.modelReview.SetActive(false);
                }

                currentItemSeleted = item;
                currentItemSeleted.modelReview.SetActive(true);
                currentItemSeleted.modelReview.GetComponent<Animator>().SetInteger("RandomIdle", UnityEngine.Random.Range(0, 5));

                if (!currentItemSeleted.isUnlocked)
                {
                    ButtonBuy.GetComponentInChildren<TextMeshProUGUI>().text = currentItemSeleted.price.ToString();
                    ButtonBuy.SetActive(true);
                }
                else
                {
                    ButtonBuy.SetActive(false);
                    if (currentItemSeleted.id == LocalData.instance.GetCurrentChar())
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

    public void BuyCharacter()
    {
        int coin = LocalData.instance.GetCoin();

        if (coin < currentItemSeleted.price)
        {
            return;
        }
        else
        {
            ButtonBuy.SetActive(false);

            int index= characterItemsData.FindIndex(item => item.id == currentItemSeleted.id);

            currentItemSeleted.isUnlocked = true;
            characterItemsData[index] = currentItemSeleted;

            characterDatas.Find(item=>item.id == currentItemSeleted.id).isUnlock=true;

            LocalData.instance.SetCharacterData(characterDatas);

            coin -= currentItemSeleted.price;
            LocalData.instance.SetCoin(coin);
        }
    }

    public void SeleteCharacter()
    {
        ButtonSelect.SetActive(false);
        LocalData.instance.SetCurrentChar(currentItemSeleted.id);
    }
}
