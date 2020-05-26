using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityStandardAssets.CrossPlatformInput;

public class ImageManager : MonoBehaviour
{

    [SerializeField]
    private Text videoText;

    [SerializeField]
    private Button mainScreenButton;

    [SerializeField]
    GameObject textVideoObject;


    private ARTrackedImageManager mImageManager;
    //public ARSession aR;

    private void Start()
    {
        //aR.enabled = false;
        mainScreenButton.onClick.AddListener(() => { LoadScene("MainScreen"); });
        mImageManager = GetComponent<ARTrackedImageManager>();
        mImageManager.trackedImagePrefab.gameObject.SetActive(false);
        mImageManager.trackedImagesChanged += OnImagesChanged;




    }

    private void OnDestroy()
    {
        mImageManager.trackedImagesChanged -= OnImagesChanged;
    }


    void OnImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {

        //Whenever the device finds an image 
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            // Display the title of the video 
            if (mImageManager.trackedImagePrefab.gameObject.activeSelf == false)
            {
                videoText.text = "";
                trackedImage.gameObject.SetActive(false);
            }
            else
            {
                //videoText.text = trackedImage.referenceImage.name;
                //trackedImage.gameObject.SetActive(true);
                // Give the initial image a default scale
                trackedImage.transform.localScale =
                    new Vector3(-trackedImage.referenceImage.size.x, 0.005f, -trackedImage.referenceImage.size.y);
            }

        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            // Display the title of the video 
            if(mImageManager.trackedImagePrefab.gameObject.activeSelf == false)
            {
                videoText.text = "";
                trackedImage.gameObject.SetActive(false);
            }
            else
            {
                videoText.text = trackedImage.referenceImage.name;
                trackedImage.gameObject.SetActive(true);
                // Give the initial image a default scale
                trackedImage.transform.localScale =
                    new Vector3(-trackedImage.referenceImage.size.x, 0.005f, -trackedImage.referenceImage.size.y);
            }

        }
    }

    private void LoadScene(string name) => SceneManager.LoadScene(name);
    bool isTextActive = true;
    int count = 0;
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("TitleText"))
        {

            if (isTextActive)
            {
                textVideoObject.SetActive(!isTextActive);
                isTextActive = false;

            }
            else if (!isTextActive )
            {
                textVideoObject.SetActive(!isTextActive);
                isTextActive = true;
            }
        }
        else if (CrossPlatformInputManager.GetButtonDown("AR"))
        {

            if (count == 0)
            {
                mImageManager.trackedImagePrefab.gameObject.SetActive(true);
                videoText.text = "Scanning...";
                count = 1;
            }
            else
            {

                mImageManager.trackedImagePrefab.gameObject.SetActive(false);
                videoText.text = "";
                count = 0;
            }
            //aR.enabled = false;
            
            
        }
    }

}
