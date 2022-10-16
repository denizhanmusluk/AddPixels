using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	ClickerControl _clickerControl;
	[SerializeField] Slider staminaSlider;
	[SerializeField] Image sliderImage;
	[SerializeField] float maxHealth;
	[SerializeField] float currentHealth;

	[SerializeField] Color maxHealthColor, middleHealthColor, minHealthColor;

	bool fillActive = false;
	[SerializeField] float coolDownSpeed = 10;// Upgradeable
	//[SerializeField] float healthDownSpeed = 10;// Upgradeable
	public bool fallActive = false;

	private void Start()
    {
		_clickerControl = GetComponent<ClickerControl>();
		currentHealth = maxHealth;
		CoolDownStart();
	}
	public void CoolDownStart()
    {
		fillActive = true;
		StartCoroutine(CoolDown(Globals.coolDownSpeed));
	}
	public void HealthDownStart()
	{
		fillActive = false;
		StartCoroutine(HealthDown(Globals.healthDownSpeed));
	}
	IEnumerator CoolDown(float _cooldownSpeed)
	{
		float counter = currentHealth;
        while (fillActive && currentHealth<maxHealth && !fallActive)
        {
			counter += _cooldownSpeed * Time.deltaTime;
			currentHealth = counter;
			staminaSlider.value = currentHealth / maxHealth;
			if (staminaSlider.value > 0.5f)
			{
				sliderImage.color = Color.Lerp(middleHealthColor, maxHealthColor, 2 * staminaSlider.value - 1  );

			}
			else
			{
				sliderImage.color = Color.Lerp(minHealthColor, middleHealthColor, 2 * staminaSlider.value);
			}
			yield return null;
        }
		fillActive = false;
	}
	IEnumerator HealthDown(float _healthDownSpeed)
    {
		float counter = currentHealth;
		while (!fillActive && currentHealth > 0 && !fallActive)
		{
			counter -= _healthDownSpeed * Time.deltaTime;
			currentHealth = counter;
			staminaSlider.value = currentHealth / maxHealth;
			if (staminaSlider.value > 0.5f)
			{
				sliderImage.color = Color.Lerp(middleHealthColor, maxHealthColor, 2 * staminaSlider.value - 1);
			}
			else
			{
				sliderImage.color = Color.Lerp(minHealthColor, middleHealthColor, 2 * staminaSlider.value);
			}
			if (currentHealth < 0)
			{
				StartCoroutine(Falling());
			}
			yield return null;
		}
		fillActive = true;
	}
	IEnumerator Falling()
    {
		fallActive = true;
		_clickerControl.anim.SetTrigger("Fall");
		yield return new WaitForSeconds(3f);
		fallActive = false;
		_clickerControl.anim.SetTrigger("Jump");
		CoolDownStart();
	}
}
