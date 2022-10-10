using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTarget : MonoBehaviour
{
    private const float LookThreshold = 3;
    private const float DefaultSize = .2f;
    private const float GrowFactor = .15f;
    private static readonly Color DefaultColor = new Color(.28f, .56f, .56f);
    private static readonly Color FinalColor = new Color(.34f, .13f, .94f);

    private float lookDuration;
    private Material material;

    [SerializeField]
    private GameObject xrRig;
    [SerializeField]
    private GameObject moveTarget;

    public bool IsLookedAt { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        this.material = this.gameObject.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsLookedAt)
        {
            this.lookDuration += Time.deltaTime;
            if (this.lookDuration >= LookThreshold)
            {
                this.MoveXrRig();
            }
        }
        else
        {
            this.lookDuration = Mathf.Max(0, this.lookDuration - Time.deltaTime);
        }
        this.UpdateViewState();
    }

    private void MoveXrRig()
    {
        this.xrRig.transform.position = this.moveTarget.transform.position;
        this.lookDuration = 0;
    }

    private void UpdateViewState()
    {
        float scale = (DefaultSize + GrowFactor * this.lookDuration / LookThreshold);
        Color color = Color.Lerp(DefaultColor, FinalColor, this.lookDuration / LookThreshold);

        this.transform.localScale = Vector3.one * scale;
        this.material.color = color;
    }
}
