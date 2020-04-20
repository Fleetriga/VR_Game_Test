using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using Valve.VR;
using Valve.VR.InteractionSystem;
using System;

public abstract class ToolTip : MonoBehaviour
{
    [SerializeField] GameObject toolTipGameObject;
    TextMeshProUGUI toolTipText;

    void Awake()
    {
        toolTipText = toolTipGameObject.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        toolTipText.text = GenerateToolTip();
        VRInputs.VRInputsInstance.TrackPadNorthDown += DisplayToolTip;
        VRInputs.VRInputsInstance.TrackPadNorthUp += HideToolTip;
    }

    public virtual void DisplayToolTip(Hand hand, EventArgs args)
    {
        if (GetComponent<Interactable>().attachedToHand)
        {
            toolTipGameObject.SetActive(true);
        }
    }

    public void HideToolTip(Hand hand, EventArgs args)
    {

        toolTipGameObject.SetActive(false);
    }

    public virtual string GenerateToolTip()
    {
        return "";
    }
}

