using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;

class JsonWriter
{
    public static void Write(List<Dictionary<string, object>> list, string path)//범용
    {
        RWJsonCSV.WriteListToJson(list, path);
    }
}
