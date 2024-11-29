using System;
using System.Collections.Generic;

[Serializable]
public class ApplicationData
{
    public List<PinInfo> pinInfos;

    public ApplicationData()
    {
        pinInfos = new List<PinInfo>();
    }
}