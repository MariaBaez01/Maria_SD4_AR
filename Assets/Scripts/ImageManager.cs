using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ImageManager : MonoBehaviour
{

    [SerializeField]
    private Text videoText;

    [SerializeField]
    private Button mainScreenButton;

    private ARTrackedImageManager mImageManager;

    private void Awake()
    {
        mainScreenButton.onClick.AddListener(() => { LoadScene("MainScreen"); });
        mImageManager = GetComponent<ARTrackedImageManager>();

    }

    void OnEnable()
    {
        mImageManager.trackedImagesChanged += OnImagesChanged;
    }

    void OnDisable()
    {
        mImageManager.trackedImagesChanged -= OnImagesChanged;
    }


    void OnImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        //Whenever the device finds an image 
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            // Display the title of the video 
            videoText.text = trackedImage.referenceImage.name;
            // Give the initial image a default scale
            trackedImage.transform.localScale =
                new Vector3(-trackedImage.referenceImage.size.x, 0.005f, -trackedImage.referenceImage.size.y);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            // Display the title of the video 
            videoText.text = trackedImage.referenceImage.name;
            // Give the initial image a default scale
            trackedImage.transform.localScale =
                new Vector3(-trackedImage.referenceImage.size.x, 0.005f, -trackedImage.referenceImage.size.y);
        }
    }

    private void LoadScene(string name) => SceneManager.LoadScene(name);

}
