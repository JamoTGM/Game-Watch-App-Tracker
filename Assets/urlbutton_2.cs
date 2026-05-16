using UnityEngine;
using System.Collections;

public class urlbutton_2 : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://steamcommunity.com/sharedfiles/filedetails/?id=2915376409&searchtext=english");
        Debug.Log("is this working?");
    }

}
