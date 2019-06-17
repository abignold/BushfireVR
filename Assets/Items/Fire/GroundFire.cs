using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFire : MonoBehaviour
{

    public int health = 500;
    public int regenPerSecond = 1;
    public bool isFireStopped = false;
    private float originalLightIntensity;
    private float resistance = 15;
    private Vector3 damageModifier;
    private float lightIntensityModifier;
    private Vector3 originalScale;
    private Light lightSource;

    // Start is called before the first frame update
    void Start()
    {

        lightSource = GetComponentInChildren<Light>();
        originalScale = transform.localScale;
        originalLightIntensity = lightSource.intensity;
        damageModifier.x = originalScale.x / health;
        damageModifier.y = originalScale.y / health;
        damageModifier.z = originalScale.z / health;
        lightIntensityModifier = originalLightIntensity / health;

    }

    // Update is called once per frame
    void Update()
    {
        if (isFireStopped)
        {
            return;
        }
        if (transform.localScale.x < (originalScale.x * 0.10))
        {
            //Destroy(this.gameObject);
            this.EndFire();
            return;
        }
        if (transform.localScale.x <= originalScale.x)
        {
            transform.localScale += new Vector3(regenPerSecond * damageModifier.x * Time.deltaTime, regenPerSecond * damageModifier.x * Time.deltaTime, regenPerSecond * damageModifier.x * Time.deltaTime);
            lightSource.intensity += regenPerSecond * lightIntensityModifier * Time.deltaTime;
        }
    }

    void WaterCollision(float damage)
    {
        damage /= resistance;
        transform.localScale -= new Vector3((damage * damageModifier.x), (damage * damageModifier.y), (damage * damageModifier.z));
        lightSource.intensity -= damage * lightIntensityModifier;
    }

    void EndFire()
    {
        ParticleSystem[] particleSystemArray = this.GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem particleSystem in particleSystemArray)
        {
            particleSystem.Stop();
        }

        lightSource.enabled = false;

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
        isFireStopped = false;
    }
}
