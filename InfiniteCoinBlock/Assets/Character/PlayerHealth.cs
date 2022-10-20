using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	ClickerControl _clickerControl;
	[SerializeField] SkinnedMeshRenderer characterMesh;
	[SerializeField] Slider staminaSlider;
	[SerializeField] Image sliderImage;
	[SerializeField] float maxHealth;
	[SerializeField] float currentHealth;

	[SerializeField] Color maxHealthColor, middleHealthColor, minHealthColor;

	public bool fillActive = false;
	[SerializeField] float coolDownSpeed = 10;// Upgradeable
	//[SerializeField] float healthDownSpeed = 10;// Upgradeable
	public bool fallActive = false;
	Material characterMaterial;
	float shaderHeight;
	[SerializeField] ParticleSystem stunParticle;
    private void Awake()
    {
		characterMaterial = characterMesh.material;
	}
    private void Start()
    {
		_clickerControl = GetComponent<ClickerControl>();
		currentHealth = maxHealth;
		CoolDownStart();

		ShaderSet();

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

			ShaderSet();


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

			ShaderSet();

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
		VibratoManager.Instance.HeavyViration();

		StartCoroutine(CoolDown2(35));
		fallActive = true;
		_clickerControl.anim.SetTrigger("Fall");
		stunParticle.Play();
		stunParticle.GetComponent<FollowHeadStunParticle>().StartFollowing();
		yield return new WaitForSeconds(3f);
		stunParticle.Stop();
		_clickerControl.anim.SetTrigger("Jump");
		CoolDownStart();
		yield return new WaitForSeconds(2.5f);
		fallActive = false;
		stunParticle.GetComponent<FollowHeadStunParticle>().followActive = false;
	}
	void ShaderSet()
    {
		shaderHeight = currentHealth / maxHealth;
		characterMaterial.SetFloat("_Height", 50 * shaderHeight);
	}

	IEnumerator CoolDown2(float _cooldownSpeed)
	{
		float counter = currentHealth;
		while (currentHealth < maxHealth)
		{
			counter += _cooldownSpeed * Time.deltaTime;
			currentHealth = counter;
			staminaSlider.value = currentHealth / maxHealth;

			ShaderSet();


			if (staminaSlider.value > 0.5f)
			{
				sliderImage.color = Color.Lerp(middleHealthColor, maxHealthColor, 2 * staminaSlider.value - 1);

			}
			else
			{
				sliderImage.color = Color.Lerp(minHealthColor, middleHealthColor, 2 * staminaSlider.value);
			}
			yield return null;
		}
	}
}
