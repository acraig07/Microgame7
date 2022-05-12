using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject _chestParent;
    PlayerController _player;

    GameController _cont;
    bool _populated = false;
    void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _cont = FindObjectOfType<GameController>();
    }

    public void PopulateChest()
    {
        ChestItem chestItem;
        for(int i = 0; i < 3; i++)
        {
            int r = Random.Range(0,100);
            if(r < 3 && _cont._legendaryItems.Count != 0)
            {
                chestItem = Instantiate(_cont.GetRandomItem(_cont._legendaryItems));
                chestItem.transform.SetParent(_chestParent.transform);
                chestItem.transform.localPosition = new Vector3((i*1f)-1f,0,0);
            }
            else if (r < 10 && _cont._rareItems.Count != 0)
            {
                chestItem = Instantiate(_cont.GetRandomItem(_cont._rareItems));
                chestItem.transform.SetParent(_chestParent.transform);
                chestItem.transform.localPosition = new Vector3((i*1f)-1f,0,0);
            }
            else if (r < 50 && _cont._uncommonItems.Count != 0)
            {
                chestItem = Instantiate(_cont.GetRandomItem(_cont._uncommonItems));
                chestItem.transform.SetParent(_chestParent.transform);
                chestItem.transform.localPosition = new Vector3((i*1f)-1f,0,0);
            }
            else if (_cont._commonItems.Count != 0)
            {
                chestItem = Instantiate(_cont.GetRandomItem(_cont._commonItems));
                chestItem.transform.SetParent(_chestParent.transform);
                chestItem.transform.localPosition = new Vector3((i*1f)-1f,0,0);
            }
            else
            {
                Debug.Log("No Items in Chest");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!_populated && collision.gameObject.CompareTag("Player"))
        {
            PopulateChest();
            _populated = true;
        }
    }
}
