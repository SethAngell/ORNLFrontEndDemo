using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DropDownOptions : MonoBehaviour
{

    // Json classes
    public class dropdownOptions
    {
        public List<string> nameOptions;
    }

    public Dropdown dropdown;
    public GameObject TopEight;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(options());
        TopEight = GameObject.Find("TopEightCanvas");
    }

    // Update is called once per frame
    void Update()
    {

    }


}
