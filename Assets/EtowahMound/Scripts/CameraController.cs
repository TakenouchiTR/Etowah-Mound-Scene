using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int layerMask;
    LookTarget currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = 1 << 6;
    }

    // Update is called once per frame
    void Update()
    {
        this.PerformLookTargetRaycast();
    }

    private void PerformLookTargetRaycast()
    {
        RaycastHit hit;
        LookTarget hitTarget = null;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            hitTarget = hit.collider.gameObject.GetComponent<LookTarget>();
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }
        this.ChangeLookTarget(hitTarget);
    }

    private void ChangeLookTarget(LookTarget target)
    {
        if (this.currentTarget != null)
        {
            this.currentTarget.IsLookedAt = false;
        }
        if (target != null)
        {
            target.IsLookedAt = true;
        }
        this.currentTarget = target;
    }
}
