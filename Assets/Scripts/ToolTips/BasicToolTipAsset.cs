using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Basic TTP Asset")]
public class BasicToolTipAsset : ToolTipAsset
{
    [SerializeField] string ttpName;
    [TextArea(15, 20)]
    [SerializeField] string desc;

    public string TtpName { get => ttpName; }
    public string TtpDesc { get => desc; }

}
