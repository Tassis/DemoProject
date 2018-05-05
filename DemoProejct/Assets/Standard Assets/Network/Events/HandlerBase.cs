using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class HandlerBase
{
    public abstract void OnEvent(Dictionary<byte, object> parameter);
}