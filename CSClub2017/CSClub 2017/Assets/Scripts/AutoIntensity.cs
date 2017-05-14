using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoIntensity : MonoBehaviour {

    public ParticleSystem stars;

    /*
    private ParticleSystem.Particle[] starParticles;
    */

    float sunAngle;
    float starBrightness;
    int maxStars;

    public Gradient nightDayColor;
    public float maxIntensity = 3f;
    public float minIntensity = 0f;
    public float minPoint = -0.1f;

    public float maxAmbient = 1f;
    public float minAmbient = 0f;
    public float minAmbientPoint = -0.2f;


    public Gradient nightDayFogColor;
    public AnimationCurve fogDensityCurve;
    public float fogScale = 1f;

    public float dayAtmosphereThickness = 0.4f;
    public float nightAtmosphereThickness = 0.87f;

    public Vector3 dayRotateSpeed;
    public Vector3 nightRotateSpeed;

    float skySpeed = 1;


    Light mainLight;
    Skybox sky;
    Material skyMat;

	// Use this for initialization
	void Start () {
        mainLight = GetComponent<Light>();
        skyMat = RenderSettings.skybox;

        if (stars == null)
        {
            Debug.Log("Stars not assigned");
            stars = GetComponent<ParticleSystem>();
        }
            

        /*
        if (starParticles == null || starParticles.Length < stars.maxParticles)
            starParticles = new ParticleSystem.Particle[stars.maxParticles];
         */
    }
	
	// Update is called once per frame
	void Update () {
        float tRange = 1 - minPoint;
        float dot = Mathf.Clamp01((Vector3.Dot(mainLight.transform.forward, Vector3.down) - minPoint) / tRange);
        float i = ((maxIntensity - minIntensity) * dot) + minIntensity;

        mainLight.intensity = i;

        tRange = 1 - minAmbientPoint;
        dot = Mathf.Clamp01((Vector3.Dot(mainLight.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
        i = ((maxAmbient - minAmbient) * dot) + minAmbient;
        RenderSettings.ambientIntensity = i;

        mainLight.color = nightDayColor.Evaluate(dot);
        RenderSettings.ambientLight = mainLight.color;

        RenderSettings.fogColor = nightDayFogColor.Evaluate(dot);
        RenderSettings.fogDensity = fogDensityCurve.Evaluate(dot) * fogScale;

        i = ((dayAtmosphereThickness - nightAtmosphereThickness) * dot) + nightAtmosphereThickness;
        skyMat.SetFloat("_AtmosphereThickness", i);

        /*
        sunAngle = dot * Mathf.PI;
        starBrightness = Mathf.Sin(sunAngle + Mathf.PI);
        maxStars = (int)(starBrightness * 1000f);
        
        if (maxStars < 0)
            maxStars = 0;
         */

        if (dot > 0){
            transform.Rotate(dayRotateSpeed * Time.deltaTime * skySpeed);
            if (stars != null)
                stars.Stop();
        }
        else{
            transform.Rotate(nightRotateSpeed * Time.deltaTime * skySpeed);
            if (stars != null)
                stars.Play();//
        }

        if (Input.GetKeyDown(KeyCode.Q)) skySpeed *= 0.5f;
        if (Input.GetKeyDown(KeyCode.E)) skySpeed *= 2f;

	}
    /*
    public void starIntensity()
    {
        if (stars == null)
            return;

        stars.SetParticles(starParticles, 0);
    }
    */
}
