using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFire : MonoBehaviour
{

    public float maxHealth = 500;
    public float currentHealth = 500;
    private float regenPerSecond = 50;
    private float deathScale = 0.05f;
    public bool isFireStopped = false;
    private float originalLightIntensity;
    private Vector3 damageModifier;
    private float lightIntensityModifier;
    private Vector3 originalScale;
    private Light lightSource;
    private float lastDamageTime = 0;
    private float regenDelay = 1f;
    private AudioSource audioSource;
    private float originalVolume;
    private float volumeModifier;
    // Start is called before the first frame update
    void Start()
    {
        this.maxHealth *= (1 + this.deathScale);
        this.currentHealth += maxHealth * (this.deathScale);
        this.lightSource = GetComponentInChildren<Light>();
        this.audioSource = GetComponent<AudioSource>();
        this.originalScale = transform.localScale;
        originalLightIntensity = lightSource.intensity;
        damageModifier.x = originalScale.x / maxHealth;
        damageModifier.y = originalScale.y / maxHealth;
        damageModifier.z = originalScale.z / maxHealth;
        lightIntensityModifier = originalLightIntensity / maxHealth;
        this.originalVolume = this.audioSource.volume;
        this.volumeModifier = this.originalVolume / maxHealth;

        this.UpdateScale(this.currentHealth);

    }

    // Update is called once per frame
    void Update()
    {
        if (isFireStopped)
        {
            return;
        }

        if (transform.localScale.x < this.deathScale)
        {
            this.EndFire();
        }

        // do regen
        if (Time.time > (lastDamageTime + regenDelay))
        {
            currentHealth = Mathf.Min(maxHealth, currentHealth + (regenPerSecond * transform.localScale.x * Time.deltaTime));
        }

        // update scale
        this.UpdateScale(this.currentHealth);
    }

    void WaterCollision(float damage)
    {
        if (this.isFireStopped)
        {
            return;
        }
        currentHealth -= damage;
        this.lastDamageTime = Time.time;
        //transform.localScale -= new Vector3((damage * damageModifier.x), (damage * damageModifier.y), (damage * damageModifier.z));
        //lightSource.intensity -= damage * lightIntensityModifier;
    }

    void EndFire()
    {
        ParticleSystem[] particleSystemArray = this.GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem particleSystem in particleSystemArray)
        {
            particleSystem.Stop();
        }

        lightSource.enabled = false;
        audioSource.enabled = false;
        isFireStopped = true;
    }

    void StartFire()
    {
        ParticleSystem[] particleSystemArray = this.GetComponents<ParticleSystem>();
        foreach (ParticleSystem particleSystem in particleSystemArray)
        {
            particleSystem.Play();
        }
        lightSource.enabled = true;
        audioSource.enabled = true;
        isFireStopped = false;
    }

    void UpdateScale(float health)
    {
        transform.localScale = new Vector3(health * damageModifier.x, health * damageModifier.y, health * damageModifier.z);
        lightSource.intensity = health * lightIntensityModifier;
        this.audioSource.volume = health * this.volumeModifier;

    }
}
