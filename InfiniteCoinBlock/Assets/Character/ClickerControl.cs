using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ObserverSystem;

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
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        //staminaSlider
        currentAnimSpeed = defaultAnimSpeed;
        anim.SetFloat("JumpSpeed", currentAnimSpeed);

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
        click = false;
        yield return null;
        currentAnimSpeed = Globals.clickAnimSpeed;
        anim.SetFloat("JumpSpeed", currentAnimSpeed);

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
            currentAnimSpeed = defaultAnimSpeed;
            anim.SetFloat("JumpSpeed", currentAnimSpeed);
            _playerHealth.CoolDownStart();
        }
    }

    public void HitBrick()
    {
        StartCoroutine(MultiHit());
        brick.HitBrick();
    }
    IEnumerator MultiHit()
    {
        GameManager.Instance.MoneyUpdate(Globals.coinPerBrick);

        int counter = 0;
        while (counter<Globals.brickPerHit)
        {
            counter++;
            Globals.buildActive = true;
            yield return new WaitForSeconds((float)currentAnimSpeed / (5 * Globals.brickPerHit));
        }
    }
}
