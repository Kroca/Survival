using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeSystem : MonoBehaviour {

	[SerializeField] private Light sun;
	public GameObject stars;
	public GameObject clouds;
	[SerializeField] public float secondsInFullDay = 120f;
	public Text timeText;
	[Range(0,1)] [SerializeField] public float currentTimeOfDay = 0.3f;
	private float timeMultiplier = 1f;
	private float sunInitialIntensity;
	private Color fogColor;
	private float time;
//	private Renderer cloudsMat;

	void Start(){
		sunInitialIntensity = sun.intensity;
		fogColor = RenderSettings.fogColor;
//	cloudsMat = clouds.GetComponent<Renderer> ();
	}

	void Update(){
		time = 86400f * currentTimeOfDay;
		UpdateSun ();

		currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

		if (currentTimeOfDay >= 1) {
			currentTimeOfDay = 0;
		}
		timeText.text = (Mathf.Floor(time / 3600f)).ToString("00") + ":" + (time / 60f % 60f).ToString ("00");

	}

	void UpdateSun(){
		sun.transform.localRotation = Quaternion.Euler ((currentTimeOfDay * 360f) - 90, 170, 0);
		stars.transform.localRotation = Quaternion.Euler ((currentTimeOfDay * 360f) - 90, 170, 0);

		float intensityMultiplier = 1f;
		ParticleSystem.MainModule cloudsMain = clouds.GetComponent<ParticleSystem> ().main;

		if (currentTimeOfDay <= 0.25f || currentTimeOfDay >= 0.75f) {
			intensityMultiplier = 0;
			RenderSettings.fogColor = Color.black;
			stars.SetActive (true);
			//cloudsMain.startColor = new ParticleSystem.MinMaxGradient(new Color(50f, 50f, 50f));

		} else if (currentTimeOfDay <= 0.27f) {
			intensityMultiplier = Mathf.Clamp01 ((currentTimeOfDay - 0.23f) * (1 / 0.02f));
		} else if (currentTimeOfDay >= 0.73f) {
			intensityMultiplier = Mathf.Clamp01 (1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
		} else {
			RenderSettings.fogColor = fogColor;
			stars.SetActive (false);
			//cloudsMain.startColor = new ParticleSystem.MinMaxGradient (color.black);
		}

		sun.intensity = sunInitialIntensity * intensityMultiplier;
		RenderSettings.ambientIntensity  = sunInitialIntensity * intensityMultiplier;
		if (RenderSettings.ambientIntensity == 0) {
			RenderSettings.ambientIntensity = 0.2f;
		}

	}
}
