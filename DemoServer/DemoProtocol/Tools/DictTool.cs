using System.Collections.Generic;

namespace DemoProtocol.Tools
{
    public class DictTool
    {
        public static T2 GetValue<T1, T2>(Dictionary<T1, T2> dict, T1 t1)
        {
            T2 t2;
            if (dict.TryGetValue(t1, out t2))
            {
                return t2;
            }
            else
            {
                return default(T2);
            }
     
        }
    }
}
