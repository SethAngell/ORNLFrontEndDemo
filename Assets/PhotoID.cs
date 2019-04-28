using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoID : MonoBehaviour
{
    public string IDNumber;

    public void setIDNumber (string ID)
    {
        IDNumber = ID;
    }

    public string getIDNumber()
    {
        return IDNumber;
    }

}
