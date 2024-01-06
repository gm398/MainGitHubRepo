using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRandomizer : MonoBehaviour
{
    [SerializeField]
    AudioSource[] sounds;

    [SerializeField]
    float pitchVariationRange = 0;
    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponents<AudioSource>();
        if(sounds.Length <= 0)
        {
            return;
        }
        Vector2 x = Random.insideUnitCircle;
        int randNum = Random.Range(0, sounds.Length);
        float pitchVariation = Random.Range(-pitchVariationRange, pitchVariationRange);
        sounds[randNum].pitch += pitchVariation;
        sounds[randNum].Play();


    }

   
}
