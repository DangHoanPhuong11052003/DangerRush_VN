using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharStoreManager : MonoBehaviour
{
    [SerializeField] private CharacterData lst_charactersAssetData;
    [SerializeField] private GameObject ButtonBuy;
    [SerializeField] private GameObject ButtonSelect;
    [SerializeField] private GameObject IteamUIPerfab;
    [SerializeField] private Transform transformItems;
    [SerializeField] private Transform SelectUI;
    [SerializeField] private TextMeshProUGUI nameTextUI;
    [SerializeField] private Transform modelReviewTranform;

    private Dictionary<int,GameObject> dic_modelsReview = new Dictionary<int,GameObject>();
    private List<Character> charactersDataLst = new List<Character>();
    private List<int> lst_idCharactersOwned=new List<int>();
    private int idCharacterSeleted;

    private Dictionary<int, Transform> arrayItems = new Dictionary<int, Transform>();

    // Start is called before the first frame update
    private void Awake()
    {
        UpdateDataStore();
        CreateModelsReview();
        UpdateUIStore();

        SelectedItem(LocalData.instance.GetCurrentChar());
    }

    private void OnEnable()
    {
        SelectedItem(LocalData.instance.GetCurrentChar());
    }

    private void UpdateDataStore()
    {
        charactersDataLst.Clear();
        lst_idCharactersOwned =LocalData.instance.GetCharacterOwnedData();

        foreach (var item in lst_charactersAssetData.characters)
        {
            if (lst_idCharactersOwned.Contains(item.id))
            {
                Character character = new Character(item);
                character.isUnlocked = true;

                charactersDataLst.Add(character);
            }
            else
            {
                charactersDataLst.Add(new Character(item));
            }
        }
    }

    private void CreateModelsReview()
    {
        foreach (var item in charactersDataLst)
        {
            GameObject newModel = Instantiate(item.model, modelReviewTranform);
            newModel.SetActive(false);
            dic_modelsReview.Add(item.id, newModel);
        }
    }

    private void UpdateUIStore()
    {
        foreach(var item in charactersDataLst)
        {
            GameObject itemUI = Instantiate(IteamUIPerfab, transformItems);
            ItemsCharUI itemsCharUI= itemUI.GetComponent<ItemsCharUI>();
            itemsCharUI.SetData(item, this);

            arrayItems.Add(item.id, itemUI.transform);
        }
    }

    public void SelectedItem(int idChar)
    {
        if (idCharacterSeleted == idChar)
        {
            dic_modelsReview[idChar].SetActive(true);
            dic_modelsReview[idChar].GetComponent<Animator>().SetInteger("RandomIdle", UnityEngine.Random.Range(0, 5));
            return;
        }

        Character character= charactersDataLst.Find(x=>x.id==idChar);
        if(character != null)
        {
            //show model char
            dic_modelsReview[idCharacterSeleted].SetActive(false);
            dic_modelsReview[idChar].SetActive(true);
            idCharacterSeleted= idChar;
            dic_modelsReview[idChar].GetComponent<Animator>().SetInteger("RandomIdle", UnityEngine.Random.Range(0, 5));

            //set information
            nameTextUI.text = character.name;

            if (!character.isUnlocked)
            {
                ButtonBuy.GetComponentInChildren<TextMeshProUGUI>().text = character.price.ToString();
                ButtonBuy.SetActive(true);
            }
            else
            {
                ButtonBuy.SetActive(false);
                if (character.id == LocalData.instance.GetCurrentChar())
                {
                    ButtonSelect.SetActive(false);
                }
                else
                {
                    ButtonSelect.SetActive(true);
                }
            }
        }

        SelectUI.SetParent(arrayItems[idChar]);
        SelectUI.transform.position = arrayItems[idChar].position;
    }

    public void BuyCharacter()
    {
        int coin = LocalData.instance.GetCoin();
        Character character = charactersDataLst.Find(x => x.id == idCharacterSeleted);

        if (coin < character.price&&lst_idCharactersOwned.Contains(character.id))
        {
            //notifi play don't have enough fishbone
            return;
        }
        else
        {
            ButtonBuy.SetActive(false);
            ButtonSelect.SetActive(true);

            character.isUnlocked = true;
            lst_idCharactersOwned.Add(character.id);

            LocalData.instance.SetCharacterOwnedData(lst_idCharactersOwned);

            coin -= character.price;
            LocalData.instance.SetCoin(coin);
        }
    }

    public void SeleteCharacter()
    {
        ButtonSelect.SetActive(false);
        LocalData.instance.SetCurrentChar(idCharacterSeleted);
    }
}
