using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSounds : MonoBehaviour
{
    [SerializeField]
    AudioSource[] sounds;

    [SerializeField]
    float pitchVariationRange = 0;

    Gun gun;
    // Start is called before the first frame update
    void Start()
    {
        gun = GetComponent<Gun>();
        gun.onShoot += ShotFired;

        sounds = GetComponents<AudioSource>();
        if (sounds.Length <= 0)
        {
            return;
        }
    }

   

    void ShotFired()
    {
        
        int randNum = Random.Range(0, sounds.Length);
        float pitchVariation = Random.Range(-pitchVariationRange, pitchVariationRange);

        //AudioSource a = gameObject.AddComponent<AudioSource>();
        //a.clip = sounds[randNum].clip;
        //a.spatialBlend = sounds[randNum].spatialBlend;
        sounds[randNum].pitch = 1 + pitchVariation;
        sounds[randNum].Play();
        //Destroy(a, a.clip.length);
    }
}
