using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    public int Health { get; set; }

    public AudioClip[] playerSound;

    public Image damageScreen;
    private bool damaged = false;
    Color damageColor = new Color(255f, 30f, 30f, 0.2f);
    private float smoothColor = 1f;

    public int coins;

    //get handle to rigidbody
    private Rigidbody2D _rigidbody;
    //variable for jumpForce
    [SerializeField]
    private float _jumpForce = 5.0f;
    private bool _resetJump = false;
    [SerializeField]
    private float _speed = 2.5f;

    private bool _grounded = false;

    //handle to playerAnimation
    private Animator _anim;
    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;

    public GameObject levelGameOver;

    private LevelManager levelManager;

    [Header("Music Clip")]
    public AudioClip jumpClip;
    public AudioClip groundedClip;
    public AudioClip attackClip;
    public AudioClip attackFlameClip;
    public AudioClip hitClip;
    public AudioClip powerupClip;

    [Header("Music")]
    public AudioSource jumpAS;
    public AudioSource groundedAS;
    public AudioSource attackAS;
    public AudioSource attackFlameAS;
    public AudioSource hitAS;
    public AudioSource powerupAS;

    void Init()
    {
        //assign handle to rigidbody
        _rigidbody = GetComponent<Rigidbody2D>();

        _anim = GetComponentInChildren<Animator>();

        //assign handle to playerAnimation
        _playerAnim = GetComponent<PlayerAnimation>();

        _playerSprite = transform.GetComponentInChildren<SpriteRenderer>();

        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();

        Health = 2;

        damaged = false;

        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    void Start()
    {
        Init();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Health > 0)
        {
            Movement();

            //is left click && grounded attack
            //if (Input.GetMouseButtonDown(0) && IsGrounded())
            if (CrossPlatformInputManager.GetButtonDown("B_Button"))
            {
                _playerAnim.Attack();

                if (_swordArcSprite.enabled)
                {
                    attackFlameAS.Play();
                    //AudioSource.PlayClipAtPoint(playerSound[5], Camera.main.transform.position);
                }
                else
                {
                    attackAS.Play();
                    //AudioSource.PlayClipAtPoint(playerSound[1], Camera.main.transform.position);
                }

                //if (PlayerPrefs.GetInt("Muted") == 0)
                //{
                //}
            }

            //if (IsGrounded() && _rigidbody.velocity.magnitude > 2f && !m_FootstepAudioSource.isPlaying)
            //{
            //    print(m_CurrentFootstepSoundIndex);
            //    AudioManager.Singleton.PlayFootstepSound(m_FootstepAudioSource, ref m_CurrentFootstepSoundIndex);
            //}
        }

        if (_anim.GetBool("IsDied"))
        {
            if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
            {
                StartCoroutine("PlayerSimulated");
                //if (IsGrounded())
                //{
                //    StartCoroutine("PlayerSimulated");
                //}
            }

        }

        try
        {
            if (damaged)
            {
                damageScreen.color = damageColor;

                hitAS.Play();
                //if (PlayerPrefs.GetInt("Muted") == 0)
                //{
                //    AudioSource.PlayClipAtPoint(playerSound[4], Camera.main.transform.position);
                //}
            }
            else
            {
                damageScreen.color = Color.Lerp(damageScreen.color, Color.clear, smoothColor * Time.deltaTime);
            }
        }
        catch (Exception err)
        {
            // leave blank
        }

        damaged = false;

        int reset = PlayerPrefs.GetInt("RESET_PLAYER");
        if (reset == 1)
        {
            _anim.SetBool("IsDied", false);
            Init();

            PlayerPrefs.SetInt("RESET_PLAYER", 0);
            PlayerPrefs.Save();
        }
    }

    private bool _detectGrounded = false;

    void Movement()
    {
        try
        {
            //horizontal input for left/right
            float move = Input.GetAxisRaw("Horizontal");
            //float move = CrossPlatformInputManager.GetAxis("Horizontal");

            _grounded = IsGrounded();


            if (!_grounded)
            {
                PlayerPrefs.SetInt("IsGrounded", 0);
                _detectGrounded = true;
            }
            else
            {
                PlayerPrefs.SetInt("IsGrounded", 1);

                if (_detectGrounded)
                {
                    groundedAS.Play();
                    _detectGrounded = false;
                }
            }

            PlayerPrefs.Save();

            //if move is greater than 0 facing right
            //if move is less than 0 facing left
            if (move > 0)
            {
                Flip(true);
            }
            else if (move < 0)
            {
                Flip(false);
            }

            //if space key && grounded == true

            //if ((Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            if (CrossPlatformInputManager.GetButtonDown("A_Button") && IsGrounded())
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
                StartCoroutine(ResetJumpRoutine());

                //tell animator to jump
                _playerAnim.Jump(true);
                _playerAnim.Move(0);

                jumpAS.Play();
            }
            else
            {
                _rigidbody.velocity = new Vector2(move * _speed, _rigidbody.velocity.y);

                if (IsGrounded())
                {
                    _playerAnim.Move(move);
                }
                else
                {
                    _playerAnim.Jump(true);
                    _playerAnim.Move(0);
                }
            }
        }
        catch (Exception err)
        {
            // leave blank
        }
    }



    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, 1 << 8);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);
    
        if (hitInfo.collider != null)
        {
            if (!_resetJump)
            {
                //set animator jump bool to false
                _playerAnim.Jump(false);
                return true;
            }
        }

        return false;
    }

    void Flip(bool faceRight)
    {
        if (faceRight)
        {
            //_playerSprite.flipX = false;
            _swordArcSprite.flipX = false;

            Vector3 newPos = _playerSprite.transform.localScale;
            newPos.x = -1.0f;
            _playerSprite.transform.localScale = newPos;
        }
        else
        {
            //_playerSprite.flipX = true;
            _swordArcSprite.flipX = true;

            Vector3 newPos = _playerSprite.transform.localScale;
            newPos.x = 1.0f;
            _playerSprite.transform.localScale = newPos;
        }
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void Damage()
    {
        if (Health < 1)
        {
            return;
        }
        else
        {
            damaged = true;
        }

        Health--;
        //_playerAnim.Hit();
        UIManager.Instance.RemoveLives(Health);
        
        if (Health < 1)
        {
            PlayerPrefs.SetString("waitDate", System.DateTime.Now.AddMinutes(30).ToString());
            PlayerPrefs.Save();

            _playerAnim.Death();

            levelGameOver.SetActive(true);

            //levelManager.LoadLoseMenuAfterDelay();
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;

        if (PlayerPrefs.GetInt("Muted") == 0)
        {
            AudioSource.PlayClipAtPoint(playerSound[3], Camera.main.transform.position);
        }

        UIManager.Instance.UpdateCoinCount(coins);
    }

    public void AddHealth(int amount)
    {
        if (Health != 2)
        {
            Health += amount;
        }

        powerupAS.Play();
        //if (PlayerPrefs.GetInt("Muted") == 0)
        //{
        //    AudioSource.PlayClipAtPoint(playerSound[6], Camera.main.transform.position);
        //}

        UIManager.Instance.AddLives(Health);
    }

    public void AddSpeed(int amount)
    {
        _speed = amount;
        _jumpForce = 7.5f;

        powerupAS.Play();
        //if (PlayerPrefs.GetInt("Muted") == 0)
        //{
        //    AudioSource.PlayClipAtPoint(playerSound[6], Camera.main.transform.position);
        //}

        StartCoroutine(ResetPowerUps());
    }

    public void EnableFlame()
    {
        _swordArcSprite.enabled = true;

        powerupAS.Play();
        //if (PlayerPrefs.GetInt("Muted") == 0)
        //{
        //    AudioSource.PlayClipAtPoint(playerSound[6], Camera.main.transform.position);
        //}

        StartCoroutine(ResetPowerUps());
    }

    IEnumerator ResetPowerUps()
    {
        yield return new WaitForSeconds(5.0f);
        PlayerPrefs.SetInt("EnableFlame", 0);
        _speed = 2.5f;
        _jumpForce = 8.8f;
        _swordArcSprite.enabled = false;
    }

    IEnumerator PlayerSimulated()
    {
        yield return new WaitForSeconds(0.5f);
        _rigidbody.simulated = false;
        _anim.SetBool("IsDied", false);
    }
}