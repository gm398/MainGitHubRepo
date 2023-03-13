using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPTIK : MonoBehaviour
{
    [SerializeField] float stepDistance = 1f;
    [SerializeField] float stepSpeed = 5f;
    float footOffset;
    [SerializeField] float hipHeight = 1f;
    [SerializeField] Transform body;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] IKFootSolver otherLeg;
    float lerp = 1f;
    Vector3 oldPos, newPos, currentPos;
    float lastGroundedTime;
    bool isGrounded;

    public bool isMoving = false;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        oldPos = transform.position;
        newPos = transform.position;
        currentPos = transform.position;

        footOffset = transform.localPosition.x;

        rb = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = currentPos;

        // Check if foot is grounded
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, -Vector3.up, out RaycastHit hit, hipHeight, groundLayer))
        {
            isGrounded = true;
            lastGroundedTime = Time.time;

            // Check if foot should move
            if (Vector3.Distance(newPos, hit.point) > stepDistance && !otherLeg.isMoving && !isMoving)
            {
                lerp = 0;
                newPos = hit.point;
                isMoving = true;
                oldPos = newPos;
            }
        }
        else
        {
            // If foot is not grounded, set its position to the last grounded position
            if (Time.time - lastGroundedTime < 0.1f)
            {
                currentPos = oldPos;
            }

            isGrounded = false;
        }

        if (isGrounded && lerp < 1)
        {
            // Modify step height based on distance from ground
            float distanceFromGround = Mathf.Max(0.1f, hipHeight - hit.distance);
            float stepHeight = distanceFromGround * 0.2f;

            Vector3 footPos = Vector3.Lerp(oldPos, newPos, lerp);
            footPos.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            currentPos = footPos;
            lerp += Time.deltaTime * stepSpeed;
        }
        else if (isGrounded)
        {
            oldPos = newPos;
            isMoving = false;
        }
    }
}