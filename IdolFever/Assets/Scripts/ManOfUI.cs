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
        public ServerDatabase serverdatabase;
        public DailyManager database;

        public TMP_Text gems_txt;

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

        //rounds panel objects
        public GameObject allPanel;
        public GameObject allPanel_Btn;
        public TextMeshProUGUI all_infoTXT;

        //all_TASK panel objects
        public GameObject roundsPanel;
        public GameObject roundsPanel_Btn;
        public TextMeshProUGUI rounds_infoTXT;

        // progress bars of tasks
        public GameObject task1_progressParnet;
        public GameObject task2_progressParnet;
        public GameObject task3_progressParnet;

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
            //update gems
            gems_txt.text = StaticDataStorage.gems + "";

            // write values
            bool task3 = false;
            Debug.LogWarning("updateFills triggerd");


            fillRounds.fillAmount = StaticDataStorage.roundPlayed / 5.0f;
            RoundsProgress.text = StaticDataStorage.roundPlayed + "/5";

            //for progress bar to be visibible or not
            if (Int32.Parse(StaticDataStorage.nowTime) <= Int32.Parse(StaticDataStorage.nextMulti))
                task1_progressParnet.SetActive(false);
            else
                task1_progressParnet.SetActive(true);

            if (Int32.Parse(StaticDataStorage.nowTime) <= Int32.Parse(StaticDataStorage.nextRound))
                task2_progressParnet.SetActive(false);
            else
                task2_progressParnet.SetActive(true);

            if (Int32.Parse(StaticDataStorage.nowTime) <= Int32.Parse(StaticDataStorage.nextAll))
                task3_progressParnet.SetActive(false);
            else
                task3_progressParnet.SetActive(true);



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

            //when task done dont revert
            //if TASK 1 and task 2 ready to be collected
            //if check task2 is collected, task 1 is ready to be collected
            //if check task1 is collected, task 2 is ready to be collected
            //i task 1 and task 2 is collected, task 3 is ready to be collected
            if ((StaticDataStorage.roundPlayed >= 5 && StaticDataStorage.roundMulti >= 2) && Int32.Parse(StaticDataStorage.nowTime) >= Int32.Parse(StaticDataStorage.nextAll) ||
                    (Int32.Parse(StaticDataStorage.nowTime) <= Int32.Parse(StaticDataStorage.nextRound) && StaticDataStorage.roundMulti >= 2 && Int32.Parse(StaticDataStorage.nowTime) >= Int32.Parse(StaticDataStorage.nextAll)) ||
                    (Int32.Parse(StaticDataStorage.nowTime) <= Int32.Parse(StaticDataStorage.nextMulti) && StaticDataStorage.roundPlayed >= 5 && Int32.Parse(StaticDataStorage.nowTime) >= Int32.Parse(StaticDataStorage.nextAll)) ||
                    (Int32.Parse(StaticDataStorage.nowTime) <= Int32.Parse(StaticDataStorage.nextMulti) && Int32.Parse(StaticDataStorage.nowTime) <= Int32.Parse(StaticDataStorage.nextRound) && Int32.Parse(StaticDataStorage.nowTime) >= Int32.Parse(StaticDataStorage.nextAll)))
            {
                //all done
                allProgress.text = "all done";
                fillAll.fillAmount = 1f;
                task3 = true;
            }
            else
            {
                //you need to finish task

                if ((StaticDataStorage.roundPlayed >= 5 || StaticDataStorage.roundMulti >= 2) && Int32.Parse(StaticDataStorage.nowTime) >= Int32.Parse(StaticDataStorage.nextAll) ||
                    (Int32.Parse(StaticDataStorage.nowTime) <= Int32.Parse(StaticDataStorage.nextRound) || StaticDataStorage.roundMulti >= 2 && Int32.Parse(StaticDataStorage.nowTime) >= Int32.Parse(StaticDataStorage.nextAll)) ||
                    (Int32.Parse(StaticDataStorage.nowTime) <= Int32.Parse(StaticDataStorage.nextMulti) || StaticDataStorage.roundPlayed >= 5 && Int32.Parse(StaticDataStorage.nowTime) >= Int32.Parse(StaticDataStorage.nextAll)) ||
                    (Int32.Parse(StaticDataStorage.nowTime) <= Int32.Parse(StaticDataStorage.nextMulti) || Int32.Parse(StaticDataStorage.nowTime) <= Int32.Parse(StaticDataStorage.nextRound) && Int32.Parse(StaticDataStorage.nowTime) >= Int32.Parse(StaticDataStorage.nextAll)))
                {
                    fillAll.fillAmount = 0.5f;
                    allProgress.text = "1/2";
                }
                else
                {
                    fillAll.fillAmount = 0.0f;
                    allProgress.text = "0/2";
                }


                if (Int32.Parse(StaticDataStorage.nowTime) <= Int32.Parse(StaticDataStorage.nextMulti))
                    task1_progressParnet.SetActive(false);
                else
                    task1_progressParnet.SetActive(true);

                if (Int32.Parse(StaticDataStorage.nowTime) <= Int32.Parse(StaticDataStorage.nextRound))
                    task2_progressParnet.SetActive(false);
                else
                    task2_progressParnet.SetActive(true);

                if (Int32.Parse(StaticDataStorage.nowTime) <= Int32.Parse(StaticDataStorage.nextAll))
                    task3_progressParnet.SetActive(false);
                else
                    task3_progressParnet.SetActive(true);

            }

            //decisions for buttons
            if (StaticDataStorage.roundMulti >= 2)
            {     
                multi_Btn.GetComponent<Image>().sprite = Btn_ImageAfter;
            }
            else
            {                    
                multi_Btn.GetComponent<Image>().sprite = Btn_ImageBefore;
                Debug.Log("before IMG");
            }

            if (StaticDataStorage.roundPlayed >= 5)
            {
                rounds_Btn.GetComponent<Image>().sprite = Btn_ImageAfter;
            }
            else
            {
               rounds_Btn.GetComponent<Image>().sprite = Btn_ImageBefore;
                Debug.Log("before IMG");
            }

            if (task3 == true)
            {
                all_Btn.GetComponent<Image>().sprite = Btn_ImageAfter;
            }
            else
            {
                all_Btn.GetComponent<Image>().sprite = Btn_ImageBefore;
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
                multiPanel_Btn.GetComponent<Button>().interactable = false;
                multiplayerProgress.text = "";
                fillMultiplayer.fillAmount = 0.0f;


            }
            

        }


        public void OnButt_rounds()
        {
            roundsPanel.SetActive(true);

            if (Int32.Parse(StaticDataStorage.nowTime) >= Int32.Parse(StaticDataStorage.nextRound))
            {
                //display the progress

                if (StaticDataStorage.roundPlayed >= 5)
                {
                    //you can collect
                    rounds_infoTXT.text = "200 gems";
                    roundsPanel_Btn.GetComponent<Button>().interactable = true;
                    roundsPanel_Btn.SetActive(true);

                }
                else
                {
                    //you need to finish task
                    rounds_infoTXT.text = "You have yet to finish your Mission!";
                    roundsPanel_Btn.GetComponent<Button>().interactable = false;

                }


            }
            else
            {
                //display wait time
                int h = (Int32.Parse(StaticDataStorage.nextRound) - Int32.Parse(StaticDataStorage.nowTime)) / 3600;

                rounds_infoTXT.text = "locked " + h + "h";
                roundsPanel_Btn.GetComponent<Button>().interactable = false;
                RoundsProgress.text = "";
                fillRounds.fillAmount = 0.0f;


            }


        }

        public void OnButt_all()
        {
            allPanel.SetActive(true);

            if (Int32.Parse(StaticDataStorage.nowTime) >= Int32.Parse(StaticDataStorage.nextAll))
            {
                //display the progress

                //when task done dont revert
                if ((StaticDataStorage.roundPlayed >= 5 && StaticDataStorage.roundMulti>=2) && Int32.Parse(StaticDataStorage.nowTime) >= Int32.Parse(StaticDataStorage.nextAll) ||
                    (Int32.Parse(StaticDataStorage.nowTime) <= Int32.Parse(StaticDataStorage.nextRound) && StaticDataStorage.roundMulti>= 2 && Int32.Parse(StaticDataStorage.nowTime) >= Int32.Parse(StaticDataStorage.nextAll)) ||
                    (Int32.Parse(StaticDataStorage.nowTime) <= Int32.Parse(StaticDataStorage.nextMulti) && StaticDataStorage.roundPlayed >= 5 && Int32.Parse(StaticDataStorage.nowTime) >= Int32.Parse(StaticDataStorage.nextAll)) ||
                    (Int32.Parse(StaticDataStorage.nowTime) <= Int32.Parse(StaticDataStorage.nextMulti) && Int32.Parse(StaticDataStorage.nowTime) <= Int32.Parse(StaticDataStorage.nextRound) && Int32.Parse(StaticDataStorage.nowTime) >= Int32.Parse(StaticDataStorage.nextAll)))
                {
                    //you can collect
                    all_infoTXT.text = "300 gems";
                    allPanel_Btn.GetComponent<Button>().interactable = true;
                    allPanel_Btn.SetActive(true);

                }
                else
                {
                    //you need to finish task
                    all_infoTXT.text = "You have yet to finish your Missions!";
                    allPanel_Btn.GetComponent<Button>().interactable = false;

                }


            }
            else
            {
                //display wait time
                int h = (Int32.Parse(StaticDataStorage.nextAll) - Int32.Parse(StaticDataStorage.nowTime)) / 3600;

                all_infoTXT.text = "locked " + h + "h";
                allPanel_Btn.GetComponent<Button>().interactable = false;
                allProgress.text = "";
                fillAll.fillAmount = 0.0f;
                UpdateFills();

            }


        }



        public void On_Collect(int task)
        { // task range: 1,2,3

            int gems = 0;


            if (task == 1)//Multi
            {
                Debug.Log("i got 100 gems");
                gems = 100;
                StaticDataStorage.roundMulti = 0;
                StaticDataStorage.nextMulti = (Int32.Parse(StaticDataStorage.nowTime) + 84600).ToString(); // nextMulti will be now + 24h
                StartCoroutine(database.UpdateTaskUTC(StaticDataStorage.nextMulti, task));
                OnButt_multi();
            }
            else if (task == 2)//Round
            {
                gems = 200;
                StaticDataStorage.roundPlayed = 0;
                StaticDataStorage.nextRound = (Int32.Parse(StaticDataStorage.nowTime) + 84600).ToString();
                StartCoroutine(database.UpdateTaskUTC(StaticDataStorage.nextRound, task));
                OnButt_rounds();
            }
            else//all done
            {
                gems = 300;
                StaticDataStorage.nextAll = (Int32.Parse(StaticDataStorage.nowTime) + 84600).ToString(); 
                StartCoroutine(database.UpdateTaskUTC(StaticDataStorage.nextAll, task));
                OnButt_all();
            }

            StaticDataStorage.gems += gems;
            StartCoroutine(serverdatabase.UpdateGems(StaticDataStorage.gems));
            StartCoroutine(database.UpdateProgress(StaticDataStorage.roundPlayed, StaticDataStorage.roundMulti));
          

            UpdateFills();
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
