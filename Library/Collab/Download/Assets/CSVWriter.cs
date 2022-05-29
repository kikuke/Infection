using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;

class CSVWriter
{
    public static void Write(List<Dictionary<string, object>> list, string path)//범용
    {
        StreamWriter csvwriter = new StreamWriter("Assets/Resources/" + path + ".csv");

        for (var i = 0; i < list.Count; i++)
        {
            int j;
            if (i == 0)
            {
                j = 0;
                foreach (KeyValuePair<string, object> kv in list[i])
                {
                    if (j != 0)
                        csvwriter.Write(",");
                    csvwriter.Write(kv.Key);
                    j++;
                }
                csvwriter.Write("\r\n");
            }
            j = 0;
            foreach (KeyValuePair<string, object> kv in list[i])//for문 돌리기
            {
                if (j != 0)
                    csvwriter.Write(",");
                csvwriter.Write(kv.Value);
                j++;
            }
            csvwriter.Write("\r\n");
        }
        csvwriter.Close();
    }
}
