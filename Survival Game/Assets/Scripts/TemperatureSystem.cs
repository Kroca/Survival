using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureSystem : MonoBehaviour {

	public TimeSystem timeSystem;
	public Text tempText;
	public float envTemp;
	private Player player;
	private int dayNightTrigger; // 0 - night; 1 - day; 2 - morning/evening;
	private bool isSmoothChanged;
	private float newTemp;

	void Start(){
		dayNightTrigger = 1;
		isSmoothChanged = true;
		envTemp = Random.Range (20f, 50f);
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}
	void Update(){
		
		if (timeSystem.currentTimeOfDay <= 0.2 || timeSystem.currentTimeOfDay >= 0.8) {
			if (dayNightTrigger != 0) {
				dayNightTrigger = 0;
				newTemp = Random.Range (-40.0f, -15f);	
				isSmoothChanged = false;
				Debug.Log ("yeah night");
			}
		} else if (timeSystem.currentTimeOfDay <= 0.35 || timeSystem.currentTimeOfDay >= 0.65) {
			if (dayNightTrigger != 2) {
				dayNightTrigger = 2;
				newTemp = Random.Range (-15f, 20f);
				isSmoothChanged = false;
				Debug.Log ("yeah morning/evening");
			}
		} else {
			if (dayNightTrigger != 1) {
				dayNightTrigger = 1;
				newTemp = Random.Range (20f, 50f);
				isSmoothChanged = false;
				Debug.Log ("yeah day");
			}
		}

		if (isSmoothChanged == false) {
			if ((int)envTemp < (int)newTemp) {
				envTemp += Random.Range (0.0001f, 0.005f)*1200f/timeSystem.secondsInFullDay;
			} else if ((int)envTemp > (int)newTemp) {
				envTemp -= Random.Range (0.0001f, 0.005f)*1200f/timeSystem.secondsInFullDay;
			} else {
				isSmoothChanged = true;
			}
		} else {
			envTemp += Random.Range (-0.05f, 0.05f);
		}	
		tempText.text = envTemp.ToString ("0");

		if (envTemp >= 30f) {
			tempText.color = Color.red;
			player.currentTemp+=Random.Range(0.0001f, 0.005f)/2f;
		} else if (envTemp <= 0f) {
			tempText.color = Color.cyan;
			player.currentTemp-=Random.Range(0.0001f, 0.005f)/2f;
		} else {
			tempText.color = Color.white;
		}
	}
}
