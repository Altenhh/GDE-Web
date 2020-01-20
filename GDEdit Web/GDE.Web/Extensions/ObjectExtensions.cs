using System;
using Newtonsoft.Json;

namespace GDE.Web.Extensions
{
    public static class ObjectExtensions
    {
        public static void Dump(this object obj)
        {
            Console.WriteLine(obj.DumpString());
        }

        public static string DumpString(this object obj) => 
            JsonConvert.SerializeObject(obj, Formatting.Indented);
    }
}