using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine;



public class MenuControlScript : MonoBehaviour
{
    //We have 3 panel in main menu
    public GameObject mainPanel;
    public GameObject popUpPanel; 
    public GameObject infoPanel; 

    //this is visual time counter, we can see how meny time left to close info short message 
    public GameObject visualTimeCounter;
    
    //buttons in panel
    public GameObject soundTestButton;
    public GameObject showInfoButton;
    public GameObject popUpButton; 

    //audio source, plays the audio file
    public AudioSource audioSource;

    //we can set the time after which we close the short message panel
    public int timeToDisablePanel = 3;
    
    // sound is take from : freesound.org , Autor: Divinux, This work is licensed under the Creative Commons 0 License.
    public AudioClip testSound;

    // Start is called before the first frame update
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        
        //we want to be sure that we will start in the main menu panel
        disableAllPanel();
        ShowPanel(mainPanel);
    }

    
    public void disableAllPanel()
    {
        mainPanel.SetActive(false);
        popUpPanel.SetActive(false);
        infoPanel.SetActive(false);
    }

    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true); 
    }

    public void showPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    //after click the button we start this function
    public void playSound()
    {
        //we check if the file has loaded
        if (testSound.loadState == AudioDataLoadState.Loaded)
        {
            //if so, we play the sound
            audioSource.PlayOneShot(testSound);
        }
    }

    public void showPopUpWindow()
    {
        ShowPanel(popUpPanel);
        
    }

    public void showInfoPanel()
    {
        ShowPanel(infoPanel);

        //just in case...we don't want click again before the panel disappears
        showInfoButton.GetComponent<Button>().interactable = false;
        soundTestButton.GetComponent<Button>().interactable = false;
        popUpButton.GetComponent<Button>().interactable = false;
        //we start the countdown
        StartCoroutine("countingDownTime", timeToDisablePanel);
    }

    //update timer
    void updateVisualTimer(int counter)
    {
        visualTimeCounter.GetComponent<Text>().text = counter.ToString();
    }

    public void clouseInfoPanleButton()
    {
        disableAllPanel();
        ShowPanel(mainPanel);
    }

    IEnumerator countingDownTime(int time)
    {
        int counter = time;
        while (counter > 0)
        {
            updateVisualTimer(counter);
            //we wait 1 second
            yield return new WaitForSeconds(1);
            counter--;
        }
        //when time is end 
        if(counter == 0)
        {

            disableAllPanel();
            ShowPanel(mainPanel);
            //we are restoring button functionality
            showInfoButton.GetComponent<Button>().interactable = true;
            soundTestButton.GetComponent<Button>().interactable = true;
            popUpButton.GetComponent<Button>().interactable = true;
        }
    }
}
