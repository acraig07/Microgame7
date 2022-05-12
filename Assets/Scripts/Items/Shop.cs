using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public float _interactDistance;
    [SerializeField]float _distance;

    public GameObject _shopParent;
    PlayerController _player;

    GameController _cont;
    
    void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _cont = FindObjectOfType<GameController>();
        PopulateShop();
    }

    // Update is called once per frame
    void Update()
    {
        _distance = Vector2.Distance(transform.position, _player.transform.position);
        
        if (_distance <= _interactDistance)
        {
            _shopParent.SetActive(true);
        }
        else 
        {
            _shopParent.SetActive(false);
        }
    }

    public void PopulateShop()
    {
        ShopItem shopItem;
        for(int i = 0; i < 3; i++)
        {
            //shopItem = Instantiate(_cont._shopItems[i]);
            shopItem = Instantiate(_cont.GetRandomItem(_cont._shopItems));
            shopItem.transform.SetParent(_shopParent.transform);
            shopItem.transform.localPosition = new Vector3((i*1.5f)-1.5f,0,0);
        }
    }
}
