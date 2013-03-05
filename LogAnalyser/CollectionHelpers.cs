using System;
using System.Collections.Generic;
using System.Text;

namespace LogAnalizer
{
    public class CollectionHelpers
    {
        static public void MergeFirstIntoSecond(SortedDictionary<string, int> what, SortedDictionary<string, int> destination)
        { 
            foreach(KeyValuePair<string, int> srcPair in what)
            {
                if (!destination.ContainsKey(srcPair.Key))
                    destination.Add(srcPair.Key, srcPair.Value);
                else
                    destination[srcPair.Key] += srcPair.Value;
            }
        }
    }
}
