using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Item _item;
    PlayerController _player;
    Inventroy _inventroy;
    SpriteRenderer _rend;

    Text _itemText;
    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _inventroy = FindObjectOfType<Inventroy>();
        _rend = GetComponent<SpriteRenderer>();
        _itemText = GetComponentInChildren<Text>();
        _rend.sprite = _item._itemSprite;
    }

    public void BuyItem()
    {
        if(_player._money >= _item._itemCost)
        {
            _player.AddMoney(-_item._itemCost);
            _inventroy.AddItem(_item);
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        BuyItem();
    }

    private void Update()
    {
        _itemText.text = _item._itemName + "\n" + _item._itemCost;
        _itemText.color = _player._money > _item._itemCost ? Color.green : Color.red;
    }
}

