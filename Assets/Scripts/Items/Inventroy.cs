using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventroy : MonoBehaviour
{
    public List<Item> _items = new List<Item>();

    [SerializeField] int _iSlot = 0;
    [SerializeField] int _nextSlot = 0;
    SpriteRenderer _rend;
    bool _rotate = false;

    public SpriteRenderer _parentRend;
    
    public void Awake()
    {
        _nextSlot =  _iSlot;
    }

    public void Update()
    {
        if(_rotate)
        {
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (transform.localScale.x != 1)
            {
                angle += 180;
            }
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * 10f);
        }

        transform.localScale = new Vector3(_parentRend.flipX ? -1:1, 1, 1);

        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(_items.Count != 0)
            {
                EquipItem(_nextSlot);
            }
            else
            {
                Debug.Log("no item in inventroy");
            }
        }
        
        if(Input.GetKeyDown(KeyCode.X))
        {
            if(_items[_iSlot] != null)
            {
                _items[_iSlot].Use();
            }
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            if(_items[_iSlot] != null)
            {
                RemoveItem(_items[_iSlot]);
            }
        }
    }

    public void AddItem(Item item)
    {
        Item newItem = Instantiate(item);
        newItem.transform.SetParent(transform);
        newItem.transform.localPosition = Vector3.zero;
        newItem.transform.localRotation = Quaternion.identity;
        newItem.transform.localScale = new Vector3(1,1,1);
        _items.Add(newItem);
        newItem.gameObject.SetActive(false);
    }

    public void EquipItem(int slot)
    {
        if(_items.Count != 0)
        {
            _items[_iSlot % _items.Count].gameObject.SetActive(false);
            _iSlot = slot % _items.Count;
            _items[_iSlot].gameObject.SetActive(true);
            transform.rotation = Quaternion.Euler(0,0,0);
            _rotate = _items[_iSlot]._itemRotate;
            _nextSlot = (_iSlot + 1) % _items.Count;
         }
    }

    public void RemoveItem(Item item)
    {
        if(_items.Count != 0)
        {
            _items.Remove(item);
            item.gameObject.SetActive(false);
        }
    }
}
