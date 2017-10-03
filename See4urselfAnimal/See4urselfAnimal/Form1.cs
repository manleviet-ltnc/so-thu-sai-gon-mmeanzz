﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace See4urselfAnimal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPick_Click(object sender, EventArgs e)
        {
            lstAnimalList.Items.Add(lstNewAnimals.SelectedItem);
        }

        private void ListBox_MouseDown(object sender, MouseEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            int index = lb.IndexFromPoint(e.X, e.Y);
            if (index!=-1)
                lb.DoDragDrop(lb.Items[index].ToString(), DragDropEffects.Copy);
        }

        private void ListBox_DragEnter(object sender,DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.Move;
        }

        private void lstAnimalList_DragDrop(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.Text))
            {
                ListBox lb = (ListBox)sender;
                lb.Items.Add(e.Data.GetData(DataFormats.Text));
            }
        }

        private void Save(object sender,EventArgs e)
        {
            // Open file
            StreamWriter write = new StreamWriter("AnimalsList.txt");
            if (write == null) return;
            foreach (var item in lstAnimalList.Items)
                write.WriteLine(item.ToString());
            write.Close();
        }

 
        private void mnuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuLoad_Click(object sender, EventArgs e)
        {          
                StreamReader reader = new StreamReader("NewAnimals.txt");
                if (reader == null) return;
                string input;
                while ((input = reader.ReadLine()) != null)
                {
                    lstNewAnimals.Items.Add(input);
                }
                reader.Close();

                using (StreamReader rs = new StreamReader("AnimalsList.txt"))
                {
                    input = null;
                    while ((input = rs.ReadLine()) != null)
                    {
                        lstAnimalList.Items.Add(input);
                    }
                }         
        }

        private static string GetMonthToString(string m)
        {
            string result = "";
            switch (m)
            {
                case "1": result = "Jan"; break;
                case "2": result = "Feb"; break;
                case "3": result = "Mar"; break;
                case "4": result = "Apr"; break;
                case "5": result = "May"; break;
                case "6": result = "Jun"; break;
                case "7": result = "Jul"; break;
                case "8": result = "Aug"; break;
                case "9": result = "Sep"; break;
                case "10":result = "Oct"; break;
                case "11":result = "Nov"; break;
                case "12":result = "Dec"; break;
            }
            return result;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = string.Format("Now is {0}:{1}:{2} {3}-{4}-{5}",
                                        DateTime.Now.Hour,
                                        DateTime.Now.Minute,
                                        DateTime.Now.Second,
                                        GetMonthToString((DateTime.Now.Month).ToString()),
                                        DateTime.Now.Day,
                                        DateTime.Now.Year);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }
}
