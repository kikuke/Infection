using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationArchive : MonoBehaviour
{
    public static List<Dictionary<string, object>> operationArchiveList;
    // Start is called before the first frame update
    private void Awake()
    {
        operationArchiveList = JsonReader.Read(PathManager.FindPath("OperationArchive"));
    }
}
