using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Jobj = JSONObject;
using System.IO;

public class API_V2 : MonoBehaviour
{
    AsyncOperation asyncLoadLevel;

    // JSON Classes
    public class userInfo
    {
        public int index;
        public string content;
        public string filePath;
    }

    public class dropdownOptions
    {
        public List<string> nameOptions;
    }

    public class Photograph
    {
        public string photoID;
        public string photoPath;
        public string wncode;
        public string label;
        public float score;

        public Photograph(List<string> photoAttributes)
        {
            photoID = photoAttributes[0];
            photoPath = photoAttributes[1];
            wncode = photoAttributes[2];
            label = photoAttributes[3];
            score = float.Parse(photoAttributes[4]);
        }

        public string getFilePath()
        {
            return photoPath;
        }

        public string getPhotoID()
        {
            return photoID;
        }
    }

    public class testTag
    {
        public List<List<string>> testTable;

        public void printSomething()
        {
            Debug.Log(testTable[0]);
        }

    }

    public class masterDatabase
    {
        public List<List<string>> photoDump;
    }



    public class TopEightTags
    {
        public List<List<string>> TotalCollection;
        public Photograph oneImage;
        public Photograph twoImage;
        public Photograph threeImage;
        public Photograph fourImage;
        public Photograph fiveImage;
        public Photograph sixImage;
        public Photograph sevenImage;
        public Photograph eightImage;
        List<Photograph> photoPackages;

        public TopEightTags(List<List<string>> topEight)
        {
            TotalCollection = topEight;
            oneImage = new Photograph(topEight[0]);
            twoImage = new Photograph(topEight[1]);
            threeImage = new Photograph(topEight[2]);
            fourImage = new Photograph(topEight[3]);
            fiveImage = new Photograph(topEight[4]);
            sixImage = new Photograph(topEight[5]);
            sevenImage = new Photograph(topEight[6]);
            eightImage = new Photograph(topEight[7]);
            photoPackages = new List<Photograph>
            {
                oneImage, twoImage,
                threeImage, fourImage,
                fiveImage, sixImage,
                sevenImage, eightImage
            };
        }

        public string getPhotoPackagesPath(int index)
        {
            return photoPackages[index].getFilePath();
        }

        public string getPhotoId(int index)
        {
            return photoPackages[index].getPhotoID();
        }

    }

    // Game Objects
    public Canvas TopEight;
    public Canvas TagOptions;
    public RawImage oneImage;
    public RawImage twoImage;
    public RawImage threeImage;
    public RawImage fourImage;
    public RawImage fiveImage;
    public RawImage sixImage;
    public RawImage sevenImage;
    public RawImage eightImage;
    public Image testREgular;
    public Dropdown dropdown;

    public Dictionary<string, Sprite> MDBSprites = new Dictionary<string, Sprite>();

   

    public void Start()
    {
        TopEight.GetComponent<Canvas>().enabled = false; 
        StartCoroutine(options());
        dropdown.onValueChanged.AddListener(delegate
        {
            newTag(dropdown);
        });
        Init();
    }

    public void Init()
    {
        OnResponse("assaultrifle");
    }

    public void SelectedPhotoTag(string tag)
    {
        StartCoroutine(OnResponse(tag));
    }





    private IEnumerator OnResponse(string tag)
    {

        TopEight.GetComponent<Canvas>().enabled = false;
        string URL = "http://localhost:8080/tagTable?tag=" + tag;
        // Creates and sends HTTPGet Request to middle tier
        UnityWebRequest request = UnityWebRequest.Get(URL);
        yield return request.SendWebRequest();
        // Creates empty handmade artisian class
        // Loads JSON data from GET request into this object
        Jobj TagTable = new Jobj(request.downloadHandler.text);
        List<List<string>> JsonDump = new List<List<string>>();
        for (int x = 0; x < 8; x++)
        {
            string tempVariable = TagTable.GetField("tagTableData")[x].ToString();
            tempVariable = tempVariable.Replace("[", "");
            tempVariable = tempVariable.Replace("]", "");
            tempVariable = tempVariable.Replace("\"", "");
            List<string> reformattedString = tempVariable.Split(new char[] { ',' }).ToList();
          
            JsonDump.Add(reformattedString);
        }
        TopEightTags TopEightTable = new TopEightTags(JsonDump);

        // Pulls just the content for posting
        //string formattedString = dataBase.content;
        // Posts to our box
        //responseText.text = formattedString;
        //responseText.gameObject.SetActive(true);

        for (int x = 0; x < JsonDump.Capacity; x++)
        {
            //MACOS PATH
            //string filePath = ("file:///users/sethworkprofile/photos" + TopEightTable.getPhotoPackagesPath(x)); 
            //string filePath = ("file:///\"C://Users/SRA6535/Desktop/photos/photo/photos/amazing junk food/1. 3456px-%28pizza%29_by_david_adam_kess_%28pic.1%29.jpg\"");
            string filePath = "file:///C:/Users/SRA6535/Desktop/photos/photos/photos" + TopEightTable.getPhotoPackagesPath(x);
            RawImage CurrentImage = TopEight.transform.GetChild(x).GetComponent<Canvas>().transform.GetChild(0).GetComponent<RawImage>(); ;
            CurrentImage.GetComponent<RawImage>().enabled = true;
            WWW imageLoader = new WWW(filePath);
            while (!imageLoader.isDone) { yield return null; }
            Texture2D AdjustedTexture = new Texture2D(128, 128, TextureFormat.DXT1, false);
            //int width;
            //int height;
            //if ( imageLoader.texture.width > imageLoader.texture.height )
            //{
            //    double IWidth = imageLoader.texture.width;
            //    double IHeight = imageLoader.texture.height;
            //    double aspectRatio = IHeight / IWidth;
            //    Debug.Log(aspectRatio);
            //    int newHeight = Convert.ToInt32(aspectRatio * 150);
            //    AdjustedTexture = new Texture2D(150, newHeight, TextureFormat.DXT1, false);
            //    CurrentImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Convert.ToInt32(IWidth), Convert.ToInt32(IHeight));


            //} else if (imageLoader.texture.width < imageLoader.texture.height) 
            //{
            //    double IWidth = imageLoader.texture.width;
            //    double IHeight = imageLoader.texture.height;
            //    double aspectRatio = IWidth / IHeight;
            //    Debug.Log(aspectRatio);
            //    int newWidth = Convert.ToInt32(aspectRatio * 150);
            //    AdjustedTexture = new Texture2D(newWidth, 150, TextureFormat.DXT1, false);
              

            //}
            //else
            //{
            //    AdjustedTexture = new Texture2D(150, 150, TextureFormat.DXT1, false);
            //    height = 150;
            //    width = 150;
            //}
            imageLoader.LoadImageIntoTexture(AdjustedTexture);
            CurrentImage.texture = AdjustedTexture;
            CurrentImage.GetComponent<PhotoID>().setIDNumber(TopEightTable.getPhotoId(x));
            //Vector2 Dimensions = new Vector2(width, height);
            //CurrentImage.GetComponent<RectTransform>().sizeDelta = Dimensions;
            //CurrentImage.SetNativeSize();



            //Rect rec = new Rect(0, 0, texture.width, texture.height);
            //Sprite spriteToUse = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
            //CurrentImage.sprite = MDBSprites[TopEightTable.getPhotoId(x)];
            imageLoader.Dispose();
            imageLoader = null;
        }
        TopEight.GetComponent<Canvas>().enabled = true;

        // // Start a download of the given URL
        // WWW imageLoader2 = new WWW("file:///users/sethworkprofile/photos" + TopEightTable.getPhotoPackagesPath(1));
        // // wait until the download is done
        // yield return imageLoader2;
        // // Create a texture in DXT1 format
        // //Texture2D texture = new Texture2D(imageLoader2.texture.width, imageLoader2.texture.height, TextureFormat.DXT1, false);

        // // assign the downloaded image to sprite
        // //imageLoader2.LoadImageIntoTexture(texture);
        // //Rect rec = new Rect(0, 0, 200, newAspectRatio(texture.width, texture.height));
        // //Sprite spriteToUse = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
        // testREgular.sprite = spriteToUse;
        // testREgular.SetNativeSize();

        // imageLoader2.Dispose();
        //imageLoader2 = null;

    }

    private IEnumerator options()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost:8080/tagoptions");
        yield return request.SendWebRequest();
        dropdownOptions DO = new dropdownOptions();
        DO = JsonUtility.FromJson<dropdownOptions>(request.downloadHandler.text);
        dropdown.ClearOptions();
        dropdown.AddOptions(DO.nameOptions);
        dropdown.gameObject.SetActive(true);

    }

    public void newTag(Dropdown change)
    {
        StartCoroutine(OnResponse(change.options[change.value].text));
    }

    private IEnumerator spriteGeneration()
    {
        string URL = "http://localhost:8080/SceneLoader";
        // Creates and sends HTTPGet Request to middle tier
        UnityWebRequest request = UnityWebRequest.Get(URL);
        yield return request.SendWebRequest();
        // Creates empty handmade artisian class
        // Loads JSON data from GET request into this object
        Jobj TagTable = new Jobj(request.downloadHandler.text);
        Debug.Log(request.downloadHandler.text);
        List<List<string>> JSONDump = new List<List<string>>();
        for ( int x = 0; x < TagTable.GetField("masterDatabase").Count ; x++)
        {
            Debug.Log(x);
            string tempVariable = TagTable.GetField("masterDatabase")[x].ToString();
            tempVariable = tempVariable.Replace("[", "");
            tempVariable = tempVariable.Replace("]", "");
            tempVariable = tempVariable.Replace("\"", "");
            List<string> reformattedString = tempVariable.Split(new char[] { ',' }).ToList();

            JSONDump.Add(reformattedString);
        }

        foreach (List<string> i in JSONDump) 
        {
            string filePath = ("file:///users/sethworkprofile/photos" + i[1]);
            WWW imageLoader = new WWW(filePath);
            while (!imageLoader.isDone) { yield return null; }
            Texture2D texture = new Texture2D(imageLoader.texture.width, imageLoader.texture.height, TextureFormat.DXT1, false);
            imageLoader.LoadImageIntoTexture(texture);
            Rect rec = new Rect(0, 0, texture.width/100, texture.height/100);
            Sprite spriteToUse = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 1);
            string currentKey = "910" + (string)i[0];
            Debug.Log(currentKey);
            MDBSprites[currentKey] = spriteToUse;
            imageLoader.Dispose();
            imageLoader = null;
        }

    }


}
