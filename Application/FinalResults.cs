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
        System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
        public void FinalResult(List<double> resultsPillow1, List<double> resultsPillow2, List<double> resultsECG, ListView listView1, int Window)
        {
            string wynik = "";

            for (int i = 0; i < resultsECG.Count; i++) //resultsPillow
            {

                double HR = resultsECG[i];
                var result = new ListViewItem();


                if (resultsPillow1[i] == 0 && resultsPillow2[i] == 0) // jezeli nikt nie siedzi
                {
                    wynik = "nikt nie siedzi";
                    HR = 0;
                }

                else if (resultsPillow1[i] >= 2 && resultsPillow2[i] >= 2) // movement && resultsPillow2[i] >= 10
                {
                    if (HR >= 60 && HR <= 90) wynik = "Wszystko OK";
                    else if (HR > 40 && HR < 60) wynik = "Tętno za małe";
                    else if (HR > 90 && HR < 120) wynik = "Tętno za duże";
                    else if (HR == 0 || HR < 40) wynik = "Zmiana pozycji";
                    else if (HR < 0 && HR + 200 < 40) {wynik = "Zmiana pozycji"; HR = 0; }
                    else if (HR < 0) { wynik = "Arytmia serca"; HR = HR + 200; }
                }
               else if (resultsPillow1[i] < 2 && resultsPillow2[i] < 2) // no movement
                {
                    if (HR >= 60 && HR <= 90) wynik = "Wszystko OK";
                    else if (HR > 40 && HR < 60) wynik = "Tętno za małe";
                    else if(HR > 90 && HR < 120) wynik = "Tętno za duże";
                    else if (HR == 0) wynik = "ALERT!";
                    else if (HR < 0) { wynik = "Arytmia serca"; HR = HR + 200; }
                }

                string WindowTime = " [" + i * Window + " - " + (i + 1) * Window + "s]";
                if (HR == 0 || wynik == "Zmiana pozycji") result = new ListViewItem(new[] { (i + 1).ToString() + WindowTime, " ?  bpm", wynik });
                else result = new ListViewItem(new[] { (i + 1).ToString() + WindowTime, HR.ToString() + " bpm", wynik });
                if (!watch.IsRunning)
                    watch.Restart();
                listView1.Items.Add(result);
                watch.Stop();
                Console.WriteLine($"listView1.Items.Add(result); Execution Time: {watch.ElapsedMilliseconds} ms");
                result.ForeColor = (wynik == "Wszystko OK" || wynik == "Zmiana pozycji") ? Color.ForestGreen : Color.Red;
            }
        }
    }
}