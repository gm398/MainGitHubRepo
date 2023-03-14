using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    [SerializeField] float lerpSpeed = 10f;
    [SerializeField] float maxAimOffset = 3f;
    Transform player;
    Vector3 aimingAt = new Vector3(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        aimingAt = player.GetComponent<PlayerController>().GetAimingAt();
    }
    

    void FixedUpdate()
    {
        aimingAt = player.GetComponent<PlayerController>().GetAimingAt();
        Vector3 target = player.position + Vector3.ClampMagnitude(aimingAt - player.position,  maxAimOffset);
        this.transform.position = Vector3.Lerp(this.transform.position, target, lerpSpeed * Time.fixedDeltaTime);
    }
}
