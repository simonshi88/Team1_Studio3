using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectRoomType : MonoBehaviour
{
    public Dropdown dropdown;
    AreaCreate areaCreate;
    // Start is called before the first frame update
    void Start()
    {
        areaCreate = GetComponent<AreaCreate>();
        dropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(dropdown); });
    }

    private void DropdownValueChanged(Dropdown dropdown)
    {
        var text = dropdown.options[dropdown.value].text;
        if (text == "Public Area")
            areaCreate.zone = Zones.Category.Public;
        else if (text == "Private Area")
            areaCreate.zone = Zones.Category.Private;
        else
            areaCreate.zone = Zones.Category.Other;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
