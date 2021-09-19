using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace MediaPlayerApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<string> listPaths = new List<string>();
        List<string> listName = new List<string>();
        string[] paths = null;
        string[] fileNames = null;
        //Stack paths = new Stack();
        //Stack FileNames = new Stack();

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;
            if (open.ShowDialog() == DialogResult.OK)
            {// Set the ListBox to display items in multiple columns.
                ListBoxOfSongs.MultiColumn = false;
                // Set the selection mode to multiple and extended.
                //ListBoxOfSongs.SelectionMode = SelectionMode.MultiExtended;

                // Shutdown the painting of the ListBox as items are added.
                ListBoxOfSongs.BeginUpdate();

                ListBoxOfSongs.Items.Clear();

                //fileNames = open.SafeFileNames;
                // paths =  open.FileNames;
                // foreach (var item in open.SafeFileNames)
                {
                    // if (!listName.Contains(item))
                    {
                        foreach (var item in open.SafeFileNames)
                        {
                            listName.Add(item);
                        }
                        // listName.Add(item);
                        //Array.Clear(fileNames, 0, fileNames.Length);
                        fileNames = listName.ToArray();

                        listPaths.AddRange(open.FileNames);
                        // Array.Clear(paths, 0, paths.Length);
                        paths = listPaths.ToArray();
                    }
                }
                foreach (var item in fileNames)
                {
                    ListBoxOfSongs.Items.Add(item);
                }
                // Allow the ListBox to repaint and display the new items.
                ListBoxOfSongs.EndUpdate();
                MessageBox.Show("Items: " + ListBoxOfSongs.Items.Count.ToString());
                MessageBox.Show("paths length: " + paths.Length.ToString());
            }
        }

        string path = @"C:\Users\114\Desktop\MediaSavedFiles";

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (paths != null)
            {
                string playListName = "myPlayList";
                pl = WindowsMediaPlayer.playlistCollection.newPlaylist(playListName);

                //System.IO.Directory.CreateDirectory(path);

                for (int i = 0; i < paths.Length; i++)
                {
                    //string copeidFile = path + @"\" + fileNames[i];
                    //File.Copy(paths[i], copeidFile, true);
 
                    WMPLib.IWMPMedia m1 = WindowsMediaPlayer.newMedia(paths[i]);

                    pl.appendItem(m1);
                }
                MessageBox.Show("done!");
            }
            else { MessageBox.Show("Nothing to save!"); }
        }
        //public static FileStream Create(byte[] audio, string outputName)
        //{
        //    using (FileStream bytesToAudio = File.Create(outputName + ".wav"))
        //    {
        //        bytesToAudio.Write(audio, 0, audio.Length);
        //        Stream audioFile = bytesToAudio;
        //        bytesToAudio.Close();
        //        //return audioFile;
        //    }
        //}

        private void btnRestore_Click(object sender, EventArgs e)
        {
            //ClearFunc();
            ListBoxOfSongs.Items.Clear();
            listPaths.Clear();
            listName.Clear();
            OpenFileDialog open = new OpenFileDialog();

            //    for (int i = 0; i < num; i++)
            //    {
            //        ListBoxOfSongs.Items.Add(WindowsMediaPlayer.playlistCollection.getByName(open.SafeFileName).Item(i));
            //    }

            if (open.ShowDialog() == DialogResult.OK)
            {
                lb2.Text = (open.SafeFileName );
                lb2.Visible = true;
                //var pListArray = WindowsMediaPlayer.playlistCollection.getByName(open.FileName);
                //var pll=  File.ReadAllLines(open.FileName);
                // ListBoxOfSongs.Items.Add(File.ReadAllLines(open.FileName));
                //StreamReader playlist = new StreamReader(open.FileName);

                //while (playlist.Peek() >= 0)
                //{
                //    ListBoxOfSongs.Items.Add(playlist.ReadLine());
                //}
                WindowsMediaPlayer.URL = open.FileName;
               // paths = new string[WindowsMediaPlayer.currentPlaylist.count];
                

                for (int i = 0; i < WindowsMediaPlayer.currentPlaylist.count; i++)
                {
                    WMPLib.IWMPMedia media = WindowsMediaPlayer.currentPlaylist.get_Item(i);
                    // Play the media item.
                    // WindowsMediaPlaye.Ctlcontrols.playItem(media);
                    //mess media.sourceURL;
                    ListBoxOfSongs.Items.Add(media.name.ToString());
                    listName.Add(media.name.ToString());
                    fileNames = listName.ToArray();

                    listPaths.Add(media.sourceURL);
                }
                paths = listPaths.ToArray();

            }

            //var currPlayList = WindowsMediaPlayer.playlistCollection.newPlaylist(open.SafeFileName);

            //ListBoxOfSongs.Items.Add(currPlayList.Item[0]);


            //byte[] audiobyte = File.ReadAllBytes(paths[0] );
            //using (FileStream fs = File.Create(path + "\\" + "Music.wav"))
            //{
            //    fs.Write(audiobyte, 0, audiobyte.Length);
            //    WindowsMediaPlayer.URL = path + "\\" + "Music.wav";
            //}

            //System.Media.SoundPlayer player = new System.Media.SoundPlayer(path + "\\" + "Music.wav");
            //player.Play();
        }

        private void listBoxOfSongs_SelectedIndexChanged(object sender, EventArgs e)
        {
            IWMPMedia med = WindowsMediaPlayer.currentMedia;
            if (ListBoxOfSongs.SelectedIndex != -1)
            {
                WindowsMediaPlayer.URL = paths[ListBoxOfSongs.SelectedIndex];
            }
            if (ListBoxOfSongs.SelectedIndex == -1)
            {
                // WindowsMediaPlayer.playlistCollection.remove(collection);
                
                //WindowsMediaPlayer.mediaCollection.remove(med, true);
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            if (ListBoxOfSongs.SelectedItem != null)
            {
                MessageBox.Show("deleted index: " + ListBoxOfSongs.SelectedIndex.ToString());
                //for (int i =0; i< ListBoxOfSongs.Selected)
                
                listName.RemoveAt(ListBoxOfSongs.SelectedIndex);
                fileNames = listName.ToArray();
                //Array.Clear(fileNames, ListBoxOfSongs.SelectedIndex, 1);
                listPaths.RemoveAt(ListBoxOfSongs.SelectedIndex);
                paths = listPaths.ToArray();
                //Array.Clear(paths, ListBoxOfSongs.SelectedIndex, 1);
                // ListBoxOfSongs.BeginUpdate();
                MessageBox.Show("path length: " + paths.Length.ToString());
                ListBoxOfSongs.Items.RemoveAt(ListBoxOfSongs.SelectedIndex);
               // ListBoxOfSongs.EndUpdate();
            }
            else
            {
                ClearFunc();
            }
        }
        public void ClearFunc()
        {
            ListBoxOfSongs.Items.Clear();
            listPaths.Clear();
            paths = listPaths.ToArray();
            listName.Clear();
            fileNames = listName.ToArray();
            //Array.Clear(fileNames, 0, fileNames.Length);

            //if (paths != null)
            //{
            //    Array.Clear(paths, 0, paths.Length);
            //}

            lb2.ResetText();
            WindowsMediaPlayer.Ctlcontrols.stop();
            WindowsMediaPlayer.URL = null;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            lb.Visible = true;
            lb.Text = "yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy";
            lb.BackColor = Color.Red;
            //lb.Location = new Point(191, 489);
            lb.Location = new Point(162, 491);
            Controls.Add(lb);
        }

        readonly Label lb = new Label();
        WMPLib.IWMPPlaylist pl;




    }
}
