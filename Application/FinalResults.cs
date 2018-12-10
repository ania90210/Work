﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Application
{
    class FinalResults
    {
        ListViewItem result = new ListViewItem();
        double HeartRate;

        public void FinalResult(List<double> resultsPillow1, List<double> resultsPillow2, List<double> resultsECG, ListView listView1, int Window, bool eChair)
        {
            string comment = "";

            for (int i = 0; i < resultsECG.Count; i++) //resultsPillow
            {

                HeartRate = resultsECG[i];
                if (eChair)
                {
                   /* if (resultsPillow1[i] == 0 && resultsPillow2[i] == 0) // jezeli nikt nie siedzi
                    {
                        comment = "Brak ruchu";
                        HeartRate = 0;
                    }*/

                    if (resultsPillow1[i] >= 1 && resultsPillow2[i] >= 1) // movement && resultsPillow2[i] >= 10
                    {
                        if (HeartRate >= 55 && HeartRate <= 90) comment = "Wszystko OK";
                        else if (HeartRate >= 45 && HeartRate < 55) comment = "Puls za niski";
                        else if (HeartRate > 90 && HeartRate < 100) comment = "Puls za wysoki - STRES";
                        else if (HeartRate >= 100) comment = "ALERT!";

                        else if (HeartRate == 0 || (HeartRate < 45 && HeartRate > 0)) comment = "Zmiana pozycji";
                        else if (HeartRate < 0 ) { comment = "Zmiana pozycji"; HeartRate = 0; }
                        // else if (HeartRate < 0 && HeartRate + 200 < 45) { comment = "Zmiana pozycji"; HeartRate = 0; }
                        //else if (HeartRate < 0) { comment = "ALERT!"; HeartRate = HeartRate + 200; } //Arytmia serca
                    }
                    else if (resultsPillow1[i] < 1 && resultsPillow2[i] < 1) // no movement
                    {
                        if (HeartRate >= 55 && HeartRate <= 90) comment = "Wszystko OK";
                        else if (HeartRate >= 45 && HeartRate < 55) comment = "Puls za niski - SEN";
                        else if (HeartRate > 90 && HeartRate < 100) comment = "Puls za wysoki - STRES";

                        else if (HeartRate == 0 || (HeartRate < 45 && HeartRate > 0) || HeartRate >= 100) comment = "ALERT!";
                        else if (HeartRate < 0) { comment = "ALERT!"; HeartRate = HeartRate + 200; }
                    }
                }
                else
                {
                    if (HeartRate >= 55 && HeartRate <= 90) comment = "Wszystko OK";
                    else if (HeartRate >= 40 && HeartRate < 55) comment = "Puls za niski";
                    else if (HeartRate > 90 && HeartRate < 100) comment = "Puls za wysoki";
                    else if (HeartRate == 0) { comment = "ALERT!"; HeartRate = 0; }
                    else if ((HeartRate < 45 && HeartRate > 0) || HeartRate >= 100) comment = "ALERT!";
                    else if (HeartRate < 0) { comment = "ALERT!"; HeartRate = HeartRate + 200; }
                }


                string WindowTime = " [" + i * Window + " - " + (i + 1) * Window + "s]";
                if (HeartRate == 0 || comment == "Zmiana pozycji") result = new ListViewItem(new[] { (i + 1).ToString() + WindowTime, " ?  bpm", comment });
                else result = new ListViewItem(new[] { (i + 1).ToString() + WindowTime, HeartRate.ToString() + " bpm", comment });
               
                listView1.Items.Add(result);
                result.ForeColor = (comment == "Wszystko OK" || comment == "Zmiana pozycji") ? Color.ForestGreen : Color.Red;
            }
        }
    }
}
