using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZUNGAS.Core.Singleton;
using TMPro;
using DG.Tweening;

public class PlayerController : Singleton<PlayerController>
{
    //publics
    [Header("Lerp")]
    public Transform target;
    public float lerpSpeed = 1;

    public float speed = 1f;

    public string tagToCheck = "Enemy";
    public string tagToCheckLine = "EndLine";

    public GameObject endScreen;

    [Header("Text")]
    public TextMeshPro uiTextPowerUp;
    
    public bool invencible;

    [Header("Coin Setup")]
    public GameObject coinCollector;

    [Header("Animation")]
    public AnimatorManager animatorManager;

    [Header("VFX")]
    public ParticleSystem vfxDeath;
    public ParticleSystem vfxWin;

    [Header("Limits")]
    public float limit = 4;
    public Vector2 limitVector = new Vector2(-4, 4);

    [SerializeField] private BounceHelper _bouceHelper;

    [Header("Bounce")]
    public float scaleBounce = 1;
    public float scaleDuration = 1f;
    public Ease ease = Ease.OutBack;


    //privates
    private bool _canRun;
    private Vector3 _pos;
    private float _currentSpeed;
    private Vector3 _startPosition;
    private float _baseSpeedToAnimation = 7f;

    private void Start()
    {
        _startPosition = transform.position;
        ResetSpeed();
        Spawn();
    }

    public void Bounce()
    {
        if(_bouceHelper != null)
            _bouceHelper.Bounce();
    }

    void Update()
    {
        if (!_canRun) return;

        _pos = target.position;
        _pos.y = transform.position.y;
        _pos.z = transform.position.z;

        if (_pos.x < limitVector.x) _pos.x = -limitVector.x;
        else if (_pos.x > limitVector.y) _pos.x = limitVector.y;

        transform.position = Vector3.Lerp(transform.position, _pos, lerpSpeed * Time.deltaTime);
        transform.Translate(transform.forward * _currentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == tagToCheck)
        {
            if (!invencible)
            {
                MoveBack();
                EndGame();
                if(vfxDeath != null) vfxDeath.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == tagToCheckLine)
        {
            EndGame();
            vfxWin.Play();
        }
    }

    private void MoveBack()
    {
        transform.DOMoveZ(-1f, .2f).SetRelative();
    }

    private void EndGame(AnimatorManager.AnimationType animationType = AnimatorManager.AnimationType.DEAD)
    {
        _canRun = false;
        endScreen.SetActive(true);
        animatorManager.Play(animationType);
    }

    public void StartToRun()
    {
        _canRun = true;
        animatorManager.Play(AnimatorManager.AnimationType.RUN, _currentSpeed / _baseSpeedToAnimation);
    }

    #region POWER UPS
    public void SetPowerUpText(string s)
    {
        uiTextPowerUp.text = s;
    }
    
    public void PowerUpSpeedUp(float f)
    {
        _currentSpeed = f;
    }
    
    public void ResetSpeed()
    {
        _currentSpeed = speed;
    }


    public void SetInvencible(bool b = true)
    {
        invencible = b;
    }

    public void ChangeHeight(float amount, float duration, float animationDuration, Ease ease)
    {
        /*var p = transform.position;
        p.y = _startPosition.y + amount;
        transform.position = p;*/


        transform.DOMoveY(_startPosition.y + amount, animationDuration).SetEase(ease);
        Invoke(nameof(ResetHeight), duration);

    }

    public void ResetHeight()
    {
        transform.DOMoveY(_startPosition.y, .5f);
    }


    public void ChangeCoinCollectorSize(float amount)
    {
        coinCollector.transform.localScale = Vector3.one * amount;
    }
    #endregion 

    private void Spawn()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(scaleBounce, scaleDuration).SetEase(ease);
    }
}
