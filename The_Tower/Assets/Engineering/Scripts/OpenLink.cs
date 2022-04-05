using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
    [SerializeField]
    string link = "https://docs.google.com/spreadsheets/d/1JB2h2myWdbFI_iQSMg3VNQ3tXydzMjERHFgHsdUGlng/edit?usp=sharing";
    public void OpenURL()
    {
        Application.OpenURL(link);
    }
}
