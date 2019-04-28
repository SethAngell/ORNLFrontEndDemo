//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Networking;
//using UnityEngine.UI;

//public class API : MonoBehaviour
//{

//    // JSON Classes
//    public class userInfo
//    {
//        public int index;
//        public string content;
//        public string filePath;
//    }

//    public class dropdownOptions
//    {
//        public List<string> nameOptions;
//    }

//    public class Photograph
//    {
//        public string photoID;
//        public string photoPath;
//        public string wncode;
//        public string label;
//        public float score;

//        public Photograph( List<string> photoAttributes)
//        {
//            photoID = photoAttributes[0];
//            photoPath = photoAttributes[1];
//            wncode = photoAttributes[2];
//            label = photoAttributes[3];
//            score = float.Parse(photoAttributes[4]);
//        }

//        public string getFilePath ()
//        {
//            return photoPath;
//        }
//    }



//    public class TopEightTags
//    {
//        public List<List<string>> TotalCollection;
//        public Photograph oneImage;
//        public Photograph twoImage;
//        public Photograph threeImage;
//        public Photograph fourImage;
//        public Photograph fiveImage;
//        public Photograph sixImage;
//        public Photograph sevenImage;
//        public Photograph eightImage;
//        List<Photograph> photoPackages;

//        public TopEightTags ( List<List<string>> topEight)
//        {
//            TotalCollection = topEight;
//            oneImage = new Photograph(topEight[0]);
//            twoImage = new Photograph(topEight[1]);
//            threeImage = new Photograph(topEight[2]);
//            fourImage = new Photograph(topEight[3]);
//            fiveImage = new Photograph(topEight[4]);
//            sixImage = new Photograph(topEight[5]);
//            sevenImage = new Photograph(topEight[6]);
//            eightImage = new Photograph(topEight[7]);
//            photoPackages = new List<Photograph>
//            {
//                oneImage, twoImage,
//                threeImage, fourImage,
//                fiveImage, sixImage,
//                sevenImage, eightImage
//            };
//        }

//        public string getPhotoPackagesPath (int index)
//        {
//            return photoPackages[index].getFilePath();
//        }

//    }

//    // Game Objects
//    public Canvas TopEight;
//    public Image oneImage;
//    public Image twoImage;
//    public Image threeImage;
//    public Image fourImage;
//    public Image fiveImage;
//    public Image sixImage;
//    public Image sevenImage;
//    public Image eightImage;
   

//    public void Start()
//    {
//        //responseText.gameObject.SetActive(false);
//        //dropdown.gameObject.SetActive(false);
//        //StartCoroutine(options());

//    }

//    public void Update()
//    {
//        //int selection = dropdown.value + 1;
//        //this.Request(selection);
//    }

//    public void Request( int option )
//    {
//        // Sends to numertor so that we can yield until the request is filled
//        StartCoroutine(OnResponse( option ) );

//    }

//    private IEnumerator options()
//    {
//        UnityWebRequest request = UnityWebRequest.Get("http://localhost:8080/options");
//        yield return request.SendWebRequest();
//        dropdownOptions DO = new dropdownOptions();
//        DO = JsonUtility.FromJson<dropdownOptions>(request.downloadHandler.text);
//        dropdown.ClearOptions();
//        dropdown.AddOptions(DO.nameOptions);
//        dropdown.gameObject.SetActive(true);

//    }

//    private IEnumerator OnResponse ( int userSelection )
//    {
//        string URL = "http://localhost:8080/toplabels";

//        // Creates and sends HTTPGet Request to middle tier
//        UnityWebRequest request = UnityWebRequest.Get(URL);
//        yield return request.SendWebRequest();
//        // Creates empty handmade artisian class
//        // Loads JSON data from GET request into this object
//        TopEightTags TagTable = JsonUtility.FromJson<TopEightTags>(request.downloadHandler.text);
//        // Pulls just the content for posting
//        //string formattedString = dataBase.content;
//        // Posts to our box
//        //responseText.text = formattedString;
//        //responseText.gameObject.SetActive(true);

//        for (int x = 0; x < 8; x++) 
//        { 
//            string filePath = ("file:///users/sethworkprofile/photos" + TagTable.getPhotoPackagesPath(x));

//        }

//        transform.GetChild(0).GetComponent<Image>().sprite = 
//        transform.GetChild(
//        string filePath = "file:///users/sethworkprofile/photos" + dataBase.filePath;
//        WWW imageLoader = new WWW(filePath);
//        while (!imageLoader.isDone)
//            yield return null;
//        systemImage.texture = imageLoader.texture;



//    }

//}
