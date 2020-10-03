using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PIckupIndicator : MonoBehaviour
{
    public RawImage mouseMarker;
    public Interaction interaction;

    public Color HighlightColour;
    public Color NeutralColour;
    public Color ChargeColour;

    float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (interaction.ChargeLevel > 0f && interaction.IsHoldingObject)
        {
            float scale = Mathf.Lerp(.5f, .1f, interaction.ChargeLevel / 1.5f);
            mouseMarker.transform.localScale = Vector3.one * scale;
            mouseMarker.color = Color.Lerp(NeutralColour, ChargeColour, interaction.ChargeLevel / 1.5f);
        }
        else
        {
            bool over = interaction.mouseOver && interaction.CurrentState == Interaction.InteractionState.Default;
            time = Mathf.Clamp(time + (over ? Time.deltaTime * 2f : -Time.deltaTime), 0f, .25f);
            mouseMarker.transform.localScale = Vector3.one * (time * 2f + .5f);
            mouseMarker.color = Color.Lerp(NeutralColour, HighlightColour, time * 4f);
        }
    }
}
