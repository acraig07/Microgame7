using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float _maxHp;
    [SerializeField]float _hp;
    public float _exp;
    public int _money;
    PlayerController _player;
    public float _iframeTime = 0.3f;
    float _iframe;

    public enum _enemyStates {chase, attack};
    public _enemyStates _currentState;

    Animator _anim;
    Rigidbody2D _enemyRigidbody;
    GameController _cont;

    public float _timeBetweenAttack = 1f;
    float _cools;

    public float _speed;
    int _dir;
    SpriteRenderer _rend;
    public float _attackRange;
    float _distance;

    public GameObject _meleeCollider;
    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _cont = FindObjectOfType<GameController>();
        _anim = GetComponent<Animator>();
        _enemyRigidbody = GetComponent<Rigidbody2D>();
        _rend = GetComponent<SpriteRenderer>();

        _hp = _maxHp;
        _iframe = _iframeTime;
        _cools = _timeBetweenAttack;
    }
    public void Damaage(float amt)
    {
        if (_iframe <= 0)
        {
            _hp -= amt;
            _iframe = _iframeTime;
            if (_hp <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        _player.AddExp(_exp);
        _player.AddMoney(_money);
    }

    // Update is called once per frame
    void Update()
    {
        if(_iframe > 0)
            _iframe -= Time.deltaTime;

        if(_cools > 0)
            _cools -= Time.deltaTime;

        switch (_currentState)
        {
            case (_enemyStates.chase):
                Chase();
                break;
            case (_enemyStates.attack):
                Attack();
                break;
        }
        _anim.SetInteger("dir", _dir);
    }

    void Chase()
    {
        _distance = Vector2.Distance(transform.position, _player.transform.position);

        if(_player.transform.position.y < transform.position.y)
        {
            _dir = 0;
            _meleeCollider.transform.localPosition = new Vector3(0,0,0);
            _meleeCollider.transform.localScale = new Vector3(1,1,1);

        }
        else if(_player.transform.position.x > transform.position.x)
        {
            _dir = 1;
            _rend.flipX = false;
            _meleeCollider.transform.localPosition = new Vector3(0.3f,0.5f,0);
            _meleeCollider.transform.localScale = new Vector3(1,1,1);
        }
        else if(_player.transform.position.x < transform.position.x)
        {
            _dir = 1;
            _rend.flipX = true;
            _meleeCollider.transform.localPosition = new Vector3(-0.3f,0.5f,0);
            _meleeCollider.transform.localScale = new Vector3(1,1,1);
        }
        else if(_player.transform.position.y > transform.position.y)
        {
            _dir = 2;
            _meleeCollider.transform.localPosition = new Vector3(0.4f,0,0);
            _meleeCollider.transform.localScale = new Vector3(1,1,1);
        }

        if (_distance < _attackRange)
        {
            Vector3 direction = _player.transform.position - transform.position;
            _enemyRigidbody.AddForce(direction * _speed * Time.deltaTime);
            if (_cools <= 0)
            {
                _currentState = _enemyStates.attack;
            }
        }
        /*else
        {
            if (_cools <= 0)
            {
                _currentState = _enemyStates.attack;
            }
        }*/
    }

    void Attack()
    {
        _anim.SetTrigger("attacking");
        _cools = _timeBetweenAttack;
        _currentState = _enemyStates.chase;
    }
    ///
}
