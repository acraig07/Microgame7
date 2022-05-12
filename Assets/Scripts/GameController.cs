using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<ShopItem> _shopItems = new List<ShopItem>();
    public List<ChestItem> _chestItems = new List<ChestItem>();

    public List<ChestItem> _commonItems = new List<ChestItem>();
    public List<ChestItem> _uncommonItems = new List<ChestItem>();
    public List<ChestItem> _rareItems = new List<ChestItem>();
    public List<ChestItem> _legendaryItems = new List<ChestItem>();

    public GameObject[] _spwanPoints;
    public List<GameObject> _enemies;
    public GameObject _enemy;
    public float _timeBetweenSpaw = 8f;
    float _cooldown;
    
    void Awake()
    {
        _spwanPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        _cooldown = _timeBetweenSpaw;

        for(int i = 0; i < _chestItems.Count; i++)
        {
            switch (_chestItems[i]._item._itemRarity)
            {
                case Item._rarity.common:
                    _commonItems.Add(_chestItems[i]);
                    break;
                case Item._rarity.uncommon:
                    _uncommonItems.Add(_chestItems[i]);
                    break;
                case Item._rarity.rare:
                    _rareItems.Add(_chestItems[i]);
                    break;
                case Item._rarity.legendary:
                    _legendaryItems.Add(_chestItems[i]);
                    break;    
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_cooldown > 0)
            _cooldown -= Time.deltaTime;
        else
            SpawnEnemy();
    }

    public ShopItem GetRandomItem(List<ShopItem> l)
    {
        int index = Random.Range(0, l.Count);
        ShopItem item = l[index];
        return item;
    }

    public ChestItem GetRandomItem(List<ChestItem> l)
    {
        int index = Random.Range(0, l.Count);
        ChestItem item = l[index];
        return item;
    }

    GameObject GetEnemy()
    {
        for(int i = 0; i < _enemies.Count; i++)
        {
            if(!_enemies[i].activeInHierarchy)
            {
                return _enemies[i];
            }
        }
        GameObject en = Instantiate(_enemy, transform.position, Quaternion.identity);
        _enemies.Add(en);
        en.SetActive(false);
        return en;
    }
    void SpawnEnemy()
    {
        GameObject obj = GetEnemy();
        obj.transform.position = _spwanPoints[Random.Range(0, _spwanPoints.Length)].transform.position;
        obj.SetActive(true);
        //Instantiate(_enemy, _spwanPoints[Random.Range(0, _spwanPoints.Length)].transform.position, Quaternion.identity);
        _cooldown = _timeBetweenSpaw;
    }
}
