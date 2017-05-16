using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour {

	public Slider healthSlider;
	public Slider foodSlider;
	public Slider waterSlider;
	public Slider staminaSlider;

	public int maxHealth;
	public int healthFallRate;
	public int maxWaterLevel;
	public int waterFallRate;
	public int maxFoodLevel;
	public int foodFallRate;
	public int maxStamina;
	private int staminaFallRate;
	public int staminaFallMult;
	private int staminaRegainRate;
	public int staminaRegainMult;

	[Header("Temperature Settings")]
	public float freezingTemp;
	public float currentTemp;
	public float normalTemp;
	public float heatTemp;
	public Text tempNumber;


	private CharacterController charController;
	private FirstPersonController playerController;

	void Start(){
		healthSlider.maxValue = maxHealth;
		healthSlider.value = maxHealth / 2;

		waterSlider.maxValue = maxWaterLevel;
		waterSlider.value = maxWaterLevel / 2;

		foodSlider.maxValue = maxFoodLevel;
		foodSlider.value = maxFoodLevel / 2;

		staminaSlider.maxValue = maxStamina;
		staminaSlider.value = maxStamina;
		staminaFallRate = 1;
		staminaRegainRate = 1;

		charController = GetComponent<CharacterController> ();
		playerController = GetComponent<FirstPersonController> ();



	}

	void UpdateTemp(){
		tempNumber.text = currentTemp.ToString ("0.0");
	}

	void Update(){
		//Temperature section
		if (currentTemp <= freezingTemp) {
			tempNumber.color = Color.cyan;
			UpdateTemp ();
		} else if (currentTemp >= heatTemp - 0.1) {
			tempNumber.color = Color.red;
			UpdateTemp ();
		} else {
			tempNumber.color = Color.white;
			UpdateTemp ();
		}

		if (currentTemp >= normalTemp) {
			currentTemp -= Time.deltaTime / 50;
		} else if (currentTemp <= normalTemp) {
			currentTemp += Time.deltaTime / 50;
		}
		//Health control
		if (foodSlider.value <= 0 && waterSlider.value <= 0) {
			healthSlider.value -= Time.deltaTime / healthFallRate * 2;
		} else if (foodSlider.value <= 0 || waterSlider.value <= 0 || currentTemp<=freezingTemp || currentTemp >= heatTemp) {
			healthSlider.value -= Time.deltaTime / healthFallRate;
		}

		if (healthSlider.value <= 0) {
			CharacterDeath ();
		}

		//food system
		if (foodSlider.value >= 0) {
			foodSlider.value -= Time.deltaTime / foodFallRate;
		} else if (foodSlider.value <= 0) {
			foodSlider.value = 0;
		} 
		if (foodSlider.value >= maxFoodLevel) {
			foodSlider.value = maxFoodLevel;
		}

		//water system
		if (waterSlider.value >= 0) {
			if (charController.velocity.magnitude > 0 && Input.GetKey (KeyCode.LeftShift) && playerController.m_RunSpeed==playerController.m_RunSpeedNorm) {
				waterSlider.value -= Time.deltaTime / waterFallRate * 2;
			} else {
				waterSlider.value -= Time.deltaTime / waterFallRate;
			}
		} else if (waterSlider.value <= 0) {
			waterSlider.value = 0;
		} 
		if (waterSlider.value >= maxWaterLevel) {
			waterSlider.value = maxWaterLevel;
		}

		//Stamina system
		if (charController.velocity.magnitude > 0 && Input.GetKey (KeyCode.LeftShift) && playerController.m_RunSpeed==playerController.m_RunSpeedNorm) {
			staminaSlider.value -= Time.deltaTime / staminaFallRate * staminaFallMult;
			currentTemp += Time.deltaTime / 30;
		} else {
			staminaSlider.value += Time.deltaTime / staminaRegainRate * staminaRegainMult;
		}

		if (staminaSlider.value >= maxStamina) {
			staminaSlider.value = maxStamina;
		} else if (staminaSlider.value <= 0) {
			playerController.m_RunSpeed = playerController.m_WalkSpeed;
			staminaSlider.value = 0;
		} else if (staminaSlider.value >= maxStamina-1) {
			playerController.m_RunSpeed = playerController.m_RunSpeedNorm;
		}

	}

	void CharacterDeath(){
		Debug.Log ("Player is dead");
	}

}
