using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BasicToolTip : ToolTip
{
    [SerializeField] BasicToolTipAsset ttpAsset;

    public override string GenerateToolTip()
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("<size=36>").Append(ttpAsset.TtpName).Append("</size>").AppendLine();
        builder.Append("<size=15>").Append(ttpAsset.TtpDesc).Append("</size>");

        return builder.ToString();
    }
}
