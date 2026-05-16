using UnityEngine;
using System.Collections;

public class urlbutton : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://steamcommunity.com/sharedfiles/filedetails/?id=2407981384&searchtext=100%25+Achievement+Guide%3A+Nier+-+Automata");
        Debug.Log("is this working?");
    }

}
