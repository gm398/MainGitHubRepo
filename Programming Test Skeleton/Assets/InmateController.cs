using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InmateController : MonoBehaviour
{
    [SerializeField] Material[] skins;
    [SerializeField] GameObject bloodSplatter;

    AudioSource[] hitSounds;
    Animator anim;

    NavMeshAgent agent;
    Transform target;

    bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        hitSounds = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive)
        { return; }
        agent.SetDestination(target.position);
    }

    public void Spawn()
    {
        isActive = true;
        this.GetComponent<CapsuleCollider>().enabled = true;
        if (skins.Length > 0)
        {
            GetComponentInChildren<SkinnedMeshRenderer>().material = skins[Random.Range(0, skins.Length)];
        }
    }
    public void Die()
    {

        isActive = false;
        agent.isStopped = true;
        this.GetComponent<CapsuleCollider>().enabled = false;
        hitSounds[Random.Range(0, hitSounds.Length)].Play();
        anim.SetBool("isDead", true);
        Invoke("Deactivate", 3f);
    }

    void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public bool IsActive() { return isActive; }

    
}
