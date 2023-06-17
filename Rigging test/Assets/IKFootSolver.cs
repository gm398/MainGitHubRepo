using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKFootSolver : MonoBehaviour
{
    [SerializeField] float stepDistance = 1f;
    [SerializeField] float maxStepDistance = 1f;
    [SerializeField] float stepHeight = .5f;
    [SerializeField] float stepSpeed = 5f;
    Vector3 footOffset;
    [SerializeField] float hipHeight = .6f;
    [SerializeField] Transform body;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] IKFootSolver otherLeg1;
    [SerializeField] IKFootSolver otherLeg2;
    float lerp = 1f;
    Vector3 oldPos, newPos, currentPos;

    public bool isMoving = false;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        oldPos = transform.position;
        newPos = transform.position;
        currentPos = transform.position;

        footOffset = transform.localPosition;
        footOffset.y += 1;

        rb = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = currentPos;
        //using body.forward because the rig is rotated
        if (Physics.Raycast(body.position + footOffset, -Vector3.up, out RaycastHit hit, hipHeight, groundLayer))
        {
            newPos = hit.point;
            bool legsMoving = (!otherLeg1.isMoving && !otherLeg2.isMoving) || Vector3.Distance(oldPos, hit.point) > maxStepDistance;
            if (Vector3.Distance(oldPos, hit.point) > stepDistance && legsMoving && !isMoving)
            {
                lerp = 0;
                
                isMoving = true;
                oldPos = newPos;
            }
            
        }
        

        if(lerp < 1)
        {
            Vector3 footPos = Vector3.Lerp(oldPos, newPos, lerp);
            footPos.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            currentPos = footPos;
            lerp += Time.fixedDeltaTime * stepSpeed;
        }
        else
        {
            isMoving = false;
        }
    }
}
