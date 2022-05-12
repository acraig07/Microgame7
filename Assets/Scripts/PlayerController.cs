using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D _playerRigidBody;
    Vector2 _input;
    public float _speed;
    
    public Animator _anim;
    public SpriteRenderer _rend;

    int _lookDir = 0;// 0 down, 1 left or right, 2 up
    bool _moving = false;

    public float _maxHealth;
    public float _health;
    public Image _healthUI;

    public int _maxMoney;
    public int _money;
    public Text _moneyText;

    public float _attack;
    public int _level = 1;
    public float _experience;
    public float _expToNext;
    public AnimationCurve _expCurve = new AnimationCurve();
    public Text _expText;

    public float _iframeTime = 0.6f;
    public float _iframe;
    public GameObject _meleeCollider;
    public GameObject _gameOverUi;
    bool _gameOver;
    private void Awake()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _rend = GetComponent<SpriteRenderer>(); 
        _expToNext = CalculateExp(_level);
        for (int i = 1; i <= 30; i++)
        {
            _expCurve.AddKey(i, CalculateExp(i));
        }
        _health = _maxHealth;
        _money = _maxMoney;
        _iframe = _iframeTime;
        _gameOver = false;
    }

    public float CalculateExp(int level)
    {
        float expNeeded;
        expNeeded = level * 100f;
        return expNeeded;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        _playerRigidBody.AddForce(_input * _speed * Time.deltaTime);

        _moving = (_input.x != 0 || _input.y != 0);

        if(_iframe > 0)
            _iframe -= Time.deltaTime;

        if(_input.y < 0)
        {
            _lookDir = 0;
            _meleeCollider.transform.localPosition = new Vector3(0,0,0);
            _meleeCollider.transform.localScale = new Vector3(1,1,1);

        }
        else if(_input.x > 0)
        {
            _lookDir = 1;
            _rend.flipX = false;
            _meleeCollider.transform.localPosition = new Vector3(0.4f,0.3f,0);
            _meleeCollider.transform.localScale = new Vector3(1,1,1);
        }
        else if(_input.x < 0)
        {
            _lookDir = 1;
            _rend.flipX = true;
            _meleeCollider.transform.localPosition = new Vector3(-0.4f,0.3f,0);
            _meleeCollider.transform.localScale = new Vector3(1,1,1);
        }
        else if(_input.y > 0)
        {
            _lookDir = 2;
            _meleeCollider.transform.localPosition = new Vector3(0f,0.75f,0);
            _meleeCollider.transform.localScale = new Vector3(1,1,1);
        }

        _anim.SetInteger("dir", _lookDir);
        _anim.SetBool("moving", _moving);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwingAttack();
        }

        //if (Input.GetKeyDown(KeyCode.J))
            //AddExp(20);
        
        _healthUI.fillAmount = _health/_maxHealth;
        _moneyText.text = "Coins: " + _money.ToString();
        _expText.text = "Level: " + _level.ToString() + " Exp: " + _experience.ToString() + "/" + _expToNext.ToString();

        if(_gameOver && Input.anyKeyDown)
        {
            SceneManager.LoadScene("SampleScene");
            Time.timeScale = 1f;
        }
    }

    public void SwingAttack()
    {
        _anim.SetBool("attacking", true);
        Invoke("ResetAttack", 0.1f);
    }

    void ResetAttack()
    {
        _anim.SetBool("attacking", false);
    }

    public void Heal(float amt)
    {
        _health += amt;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    public void Damaage(float amt)
    {   
        if(_iframe <= 0)
        {
            _health -= amt;
            _iframe = _iframeTime;
            if (_health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        GameOver();
    }

    public void AddMoney(int amt)
    {
        _money += amt;
    }

    public void AddExp(float amt)
    {
        _experience += amt;
        if(_experience >= _expToNext)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        _level++;
        _experience -= _expToNext;
        _attack = _attack + 5f;
        _speed = _speed + 50f;
        Heal(_maxHealth);
        _expToNext = CalculateExp(_level);
    }

    private void GameOver()
    {
        _gameOver = true;
        _gameOverUi.SetActive(true);
        Time.timeScale = 0f;
    }
}
