using System.Resources;
using System.Collections;
using System.Collections.Generic;

namespace PL.Helpers
{
    public static class ResourceConvert
    {
        public static List<string> ToList(string pathToResx)
        {
            List<string> resources = new List<string>();
            using (ResXResourceReader reader = new ResXResourceReader(pathToResx))
            {
                foreach (DictionaryEntry item in reader)
                {
                    resources.Add((string)item.Value);
                }
            }
            return resources;
        }
    }
}
