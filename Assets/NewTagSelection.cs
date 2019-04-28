using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NewTagSelection : MonoBehaviour, IPointerClickHandler
{

    public Canvas TopEightCanvas;
    public Text currentTag;

    public void OnPointerClick(PointerEventData eventData)
    {
        string newTag = currentTag.text;
        newTag = newTag.Replace(" ", "");
        TopEightCanvas.GetComponent<API_V2>().SelectedPhotoTag(newTag);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
