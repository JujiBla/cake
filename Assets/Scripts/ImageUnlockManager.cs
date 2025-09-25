using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ImageUnlockManager : MonoBehaviour
{
    public GameObject image1; 
    public GameObject image2;
    public GameObject image3;
    public GameObject image4;
    public GameObject image5;
    public GameObject image6;
    public GameObject image7;

    public GameObject frame1;
    public GameObject frame2;
    public GameObject frame3;
    public GameObject frame4;
    public GameObject frame5;
    public GameObject frame6;
    public GameObject frame7;


    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        
        int count = InfoTracker.instance.currentFruit;
        //Debug.Log("current count " + count);

        image1.SetActive(scene.name == "level_8_backhome");
        image2.SetActive(scene.name == "level_8_backhome");
        image3.SetActive(scene.name == "level_8_backhome");
        image4.SetActive(count >= 120);
        image5.SetActive(count >= 180);
        image6.SetActive(count >= 240);
        image7.SetActive(count >= 300);

        frame1.SetActive(scene.name != "level_8_backhome");
        frame2.SetActive(scene.name != "level_8_backhome");
        frame3.SetActive(scene.name != "level_8_backhome");
        frame4.SetActive(count <= 120);
        frame5.SetActive(count <= 180);
        frame6.SetActive(count <= 240);
        frame7.SetActive(count <= 300);
    }
}