using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IdolFever.UI;
using TMPro;
using System;

namespace IdolFever.Server
{
    public class ManOfUI : MonoBehaviour
    {
        public DailyManager database;

        //change button image after press
        public Sprite Btn_ImageBefore;
        public Sprite Btn_ImageAfter;

        //fills
        public Image fillRounds;
        public Image fillMultiplayer;
        public Image fillAll;


        //buttons main
        public GameObject multi_Btn;
        public GameObject rounds_Btn; 
        public GameObject all_Btn;

        //multi panel objects
        public GameObject multiPanel;
        public GameObject multiPanel_Btn;
        public TextMeshProUGUI multi_infoTXT;

        //texts
        public TextMeshProUGUI RoundsProgress;
        public TextMeshProUGUI multiplayerProgress;
        public TextMeshProUGUI allProgress;

        


        void Start()
        {
            //set progress bar == 0
            fillRounds.fillAmount = 0.0f;
            fillMultiplayer.fillAmount = 0.0f;
            fillAll.fillAmount = 0.0f;

            //null the text
            RoundsProgress.text = "";
            multiplayerProgress.text = "";
            allProgress.text = "";
            //UpdateFills();



            StaticDataStorage.nowTime = (getDate() + "");
        }
        // Update is called once per frame

        public void UpdateFills()
        {
            // write values
            Debug.LogWarning("updateFills triggerd");
            fillRounds.fillAmount = StaticDataStorage.roundPlayed / 5.0f;
            RoundsProgress.text = StaticDataStorage.roundPlayed + "/5";

            if (Int32.Parse(StaticDataStorage.nowTime) >= Int32.Parse(StaticDataStorage.nextMulti))//if can do task in that time (within 24hrs)
            {
                //do task
                fillMultiplayer.fillAmount = StaticDataStorage.roundMulti / 2.0f;
                multiplayerProgress.text = StaticDataStorage.roundMulti + "/2";
            }
            else
            {
                //locked for collect
                fillMultiplayer.fillAmount = 0.0f;
                multiplayerProgress.text =  "";
             
            }

            if(StaticDataStorage.roundPlayed >= 5 && StaticDataStorage.roundMulti >= 2)
            {
                // all tasks are done
                fillAll.fillAmount = 1f;
                allProgress.text = "done";

            }
            else
            {
                if(StaticDataStorage.roundPlayed >= 5 || StaticDataStorage.roundMulti >= 2)
                {
                    // just 1 task is done
                    fillAll.fillAmount = 0.5f;
                    allProgress.text = "1/2";
                }


            }

            //decisions
            if (StaticDataStorage.roundMulti >= 2)
            {     
                multi_Btn.GetComponent<Image>().sprite = Btn_ImageAfter;
            }
            else
            {                    
                multi_Btn.GetComponent<Image>().sprite = Btn_ImageBefore;
                Debug.Log("before IMG");
            }      
        }

        

        public void OnButt_multi()
        {
            multiPanel.SetActive(true);

            if(Int32.Parse(StaticDataStorage.nowTime) >= Int32.Parse(StaticDataStorage.nextMulti))
            {
                //display the progress

                if (StaticDataStorage.roundMulti >= 2)
                {
                    //you can collect
                    multi_infoTXT.text = "100 gems";
                    multiPanel_Btn.GetComponent<Button>().interactable = true;
                    multiPanel_Btn.SetActive(true);

                }
                else
                {
                    //you need to finish task
                    multi_infoTXT.text = "You have yet to finish your Mission!";
                    multiPanel_Btn.GetComponent<Button>().interactable = false;

                }


            }
            else
            {
                //display wait time
                int h = (Int32.Parse(StaticDataStorage.nextMulti) - Int32.Parse(StaticDataStorage.nowTime)) / 3600;

                multi_infoTXT.text = "locked "  + h + "h";
                multiPanel_Btn.GetComponent<Button>().enabled = false;
                multiplayerProgress.text = "";
                fillMultiplayer.fillAmount = 0.0f;


            }
            

        }


        public void On_Collect(int task)
        { // task range: 1,2,3

            int gems = 0;


            if (task == 1)//Multi
            {
                gems = 100;
                StaticDataStorage.roundMulti = 0;
                StaticDataStorage.nextMulti = (Int32.Parse(StaticDataStorage.nowTime) + 84600).ToString(); // nextMulti will be now + 24h
                StartCoroutine(database.UpdateTaskUTC(StaticDataStorage.nextMulti, task));
            }
            else if (task == 2)//Round
            {
                gems = 200;
                StaticDataStorage.roundPlayed = 0;
                StaticDataStorage.nextRound = (Int32.Parse(StaticDataStorage.nowTime) + 84600).ToString();
                StartCoroutine(database.UpdateTaskUTC(StaticDataStorage.nextRound, task));
            }
            else//all done
            {
                gems = 300;
                StaticDataStorage.roundPlayed = 0;
                StaticDataStorage.nextAll = (Int32.Parse(StaticDataStorage.nowTime) + 84600).ToString(); 
                StartCoroutine(database.UpdateTaskUTC(StaticDataStorage.nextAll, task));
            }

            StaticDataStorage.gems += gems;
            StartCoroutine(database.UpdateGems(StaticDataStorage.gems));
            StartCoroutine(database.UpdateProgress(StaticDataStorage.roundPlayed, StaticDataStorage.roundMulti));

            Debug.Log("gems = " + gems);
        }


        int getDate()
        {
            System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            Int32 cur_time = (Int32)(System.DateTime.UtcNow - epochStart).TotalSeconds;
            Debug.LogWarning("nowTime: " + cur_time);

            return cur_time;
        }


    }
}