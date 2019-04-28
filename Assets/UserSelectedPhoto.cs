using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using Jobj = JSONObject;

public class UserSelectedPhoto : MonoBehaviour, IPointerClickHandler
{

    public class RelatedLabels
    {
        List<string> topLabels = new List<string>();

        public string getFirst()
        {
            return topLabels[0];
        }
        public string getSecond()
        {
            return topLabels[1];
        }
        public string getThird()
        {
            return topLabels[2];
        }
    }

    // This Canvas
    public Canvas SelectedPhotoCanvas;
    public RawImage SelectedPhoto;
    public Text FirstLabel;
    public Text SecondLabel;
    public Text ThirdLabel;

    // AssignedPhoto
    public RawImage AttachedImage;



    // Start is called before the first frame update
    void Start()
    {
        SelectedPhotoCanvas.GetComponent<Canvas>().enabled = false;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
      
        StartCoroutine(UpdateCanvas());
    }

    public IEnumerator UpdateCanvas()
    {
        if (SelectedPhotoCanvas.GetComponent<Canvas>().enabled == false) { SelectedPhotoCanvas.GetComponent<Canvas>().enabled = true; }
        string PhotoID = AttachedImage.GetComponent<PhotoID>().getIDNumber();
        string URL = "http://localhost:8080/toplabels?photoid=" + PhotoID;
        // Creates and sends HTTPGet Request to middle tier
        UnityWebRequest request = UnityWebRequest.Get(URL);
        yield return request.SendWebRequest();
        // Creates empty handmade artisian class
        Jobj RawData = new Jobj(request.downloadHandler.text);
        string tempString = RawData.GetField("topLabels").ToString();
        tempString = tempString.Replace("[", "");
        tempString = tempString.Replace("]", "");
        tempString = tempString.Replace("\"", "");
        string[] LabelList = tempString.Split(',');
        FirstLabel.text = LabelList[0];
        SecondLabel.text = LabelList[1];
        ThirdLabel.text = LabelList[2];
        SelectedPhoto.texture = AttachedImage.texture;
        SelectedPhotoCanvas.GetComponent<Canvas>().enabled = true;
    }

    

}
