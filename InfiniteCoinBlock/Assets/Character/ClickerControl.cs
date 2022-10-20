using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ObserverSystem;
using DG.Tweening;

public class ClickerControl : Observer
{
    private static ClickerControl _instance = null;
    public static ClickerControl Instance => _instance;

    PlayerHealth _playerHealth;
    public Builder _builder;
    public Brick brick;
    [SerializeField] public Animator anim;
    bool click = false;
   [SerializeField] float defaultAnimSpeed = 1f;
    //float clickAnimSpeed = 2f;
    float currentAnimSpeed;
    bool speedyActive = false;
    [SerializeField] ParticleSystem finalConfetti;
    float tempSpeed;

    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        finalConfetti.Stop();
        _playerHealth = GetComponent<PlayerHealth>();
        //staminaSlider
        currentAnimSpeed = defaultAnimSpeed;
        tempSpeed = defaultAnimSpeed;
        anim.SetFloat("JumpSpeed", currentAnimSpeed);
        Globals.currrentAnimSpeed = currentAnimSpeed;
        ObserverManager.Instance.RegisterObserver(this, SubjectType.GameState);
    }

    public override void OnNotify(NotificationType notificationType) 
    {
        switch (notificationType)
        {
            case NotificationType.Start:
                {
                    StartState();
                }
                break;
            case NotificationType.End:
                {

                }
                break;
            case NotificationType.Win:
                {
                    WinState();
                }
                break;
            case NotificationType.Fail:
                {
              
                }
                break;

        }
    }
    void StartState()
    {
        anim.SetTrigger("Jump");
        _builder.BuildStart();
    }

    void WinState()
    {
        finalConfetti.Play();
        VibratoManager.Instance.LongHeavyViration();
        anim.SetTrigger("win");
        PlayerPrefs.SetInt("BrickUpgradeLevel", 0);
        PlayerPrefs.SetInt("StaminaUpgradeLevel", 0);
        PlayerPrefs.SetInt("ClickAnimLevel", 0);
    }
    public void ClickButton()
    {
        if (!_playerHealth.fallActive)
        {
            StartCoroutine(Accelerator());
        }
    }
    IEnumerator Accelerator()
    {
        speedyActive = true;
        click = false;
        yield return null;
        currentAnimSpeed = Globals.clickAnimSpeed;
        anim.SetFloat("JumpSpeed", currentAnimSpeed);
        Globals.currrentAnimSpeed = currentAnimSpeed;
        
        _playerHealth.HealthDownStart();

        click = true;
        float counter = 0;
        while (counter < 0.3f && click)
        {
            counter += Time.deltaTime;

            yield return null;
        }
        if (click)
        {
            speedyActive = false;
            currentAnimSpeed = defaultAnimSpeed;
            anim.SetFloat("JumpSpeed", currentAnimSpeed);
            Globals.currrentAnimSpeed = currentAnimSpeed;
        //DoGetValueScale(UpgradeManager.Instance.brickMoneyText.transform, true, 0.75f, 1, 0.5f, Ease.OutElastic);
            _playerHealth.CoolDownStart();
        }
    }
    public void HitBrick()
    {
        UpgradeManager.Instance.CoinPerSecondView();
        if (speedyActive)
        {
            VibratoManager.Instance.MediumViration();
        }
        else
        {
            VibratoManager.Instance.LightViration();
        }
        StartCoroutine(MultiHit());
        brick.HitBrick();
        if (Globals.currrentAnimSpeed != tempSpeed)
        {
            DoGetValueScale(UpgradeManager.Instance.brickMoneyText.transform, true, 0.75f, 1, 0.5f, Ease.OutElastic);
        }
        tempSpeed = Globals.currrentAnimSpeed;
    }
    IEnumerator MultiHit()
    {
        GameManager.Instance.MoneyUpdate(Globals.brickPerHit * Globals.coinPerBrick);

        int counter = 0;
        while (counter<Globals.brickPerHit)
        {
            counter++;
            Globals.buildActive = true;
            yield return new WaitForSeconds((float)currentAnimSpeed / (5 * Globals.brickPerHit));
        }
    }

    public Tween DoGetValueScale(Transform tr, bool active, float value, float lastValue, float duration, DG.Tweening.Ease type)
    {
        //Vector3 firstScale = tr.localScale;
        Tween tween = DOTween.To
            (() => value, x => value = x, lastValue, duration).SetEase(type).OnUpdate(delegate ()
            {
                tr.localScale = Vector3.one * value;
            });
        return tween;
    }
}
