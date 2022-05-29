using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatController : MonoBehaviour
{
    public void LevelUpButton()
    {
        MainInformationManager.Rank(true);
    }

    public void GoldUpButton()
    {
        MainInformationManager.Gold(1000);
    }

    public void AllClearButton()
    {
        MainInformationManager.mainInformationlist[13]["Player"] = "TRUE";
        MainInformationManager.mainInformationlist[14]["Player"] = "TRUE";
        JsonWriter.Write(MainInformationManager.mainInformationlist, PathManager.FindPath("MainInformation"));
    }
}
