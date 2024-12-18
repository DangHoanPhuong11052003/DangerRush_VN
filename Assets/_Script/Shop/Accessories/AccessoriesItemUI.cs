using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessoriesItemUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    private Accessory accessoriesItem;
    private AccessoriesStoreManager AccessoriesStoreManager;

    public void SetData(Accessory accessoriesItem, AccessoriesStoreManager AccessoriesStoreManager)
    {
        this.accessoriesItem = accessoriesItem;
        icon.sprite=accessoriesItem.icon;
        this.AccessoriesStoreManager = AccessoriesStoreManager;
    }

    public void SelectItem()
    {
        AccessoriesStoreManager.SelectItem(accessoriesItem.id);
    }
}
