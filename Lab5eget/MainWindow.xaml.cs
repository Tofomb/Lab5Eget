﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;


namespace Lab5eget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<ProfileData> UserList = new List<ProfileData>();
        public List<ProfileData> AdminList = new List<ProfileData>();
        public string emailPattern = @"\b[a-z\d]+@[a-z\d]+.[a-z\d]+\b";
        public string namePattern = @"\w";

        public MainWindow()
        {

            InitializeComponent();

        }
        // Loading Program

        private void LoadMainFrame(object sender, RoutedEventArgs e)
        {
            string[] opener;
            System.IO.FileStream UserFilar = System.IO.File.OpenWrite(@"UserGallery.txt");
            UserFilar.Close();
            
            opener = System.IO.File.ReadAllLines(@"UserGallery.txt");
            for (int ii=0; ii<opener.Length; ii = ii + 2)
            {
                ProfileData user = new ProfileData();
                user.NameData = opener[ii];
                user.EmailData = opener[ii+1];
                UserList.Add(user);
                UserListBox.Items.Add(opener[ii]);
            }
            System.IO.FileStream AdminFilar = System.IO.File.OpenWrite(@"AdminGallery.txt");
            AdminFilar.Close();
            opener = System.IO.File.ReadAllLines(@"AdminGallery.txt");

            for (int ii = 0; ii < opener.Length; ii = ii + 2)
            {
                ProfileData admin = new ProfileData();
                admin.NameData = opener[ii];
                admin.EmailData = opener[ii + 1];
                AdminList.Add(admin);
                AdminListBox.Items.Add(opener[ii]);
            }


            ChangeButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
            MakeAdminButton.IsEnabled = false;
            MakeUserButton.IsEnabled = false;
        }

        // Creating New User

        private void Creating(object sender, RoutedEventArgs e)
        {
            var match = Regex.Matches(EmailBox.Text.ToLower(), emailPattern);
            var namematch = Regex.Matches(NameBox.Text.ToLower(), namePattern);
            if (match.Count == 1)
            {
                if (namematch.Count > 0)
                {
                    var LookInUserList = from Looker in UserList
                                         where Looker.EmailData == EmailBox.Text.ToLower()
                                         select Looker;
                    var LookInAdminList = from Looker in AdminList
                                          where Looker.EmailData == EmailBox.Text.ToLower()
                                          select Looker;

                    if (LookInUserList.Count() + LookInAdminList.Count() == 0)
                    {
                        ProfileData user = new ProfileData();
                        user.NameData = NameBox.Text;
                        user.EmailData = EmailBox.Text.ToLower();
                        UserList.Add(user);
                        UserListBox.Items.Add(NameBox.Text);
                        NameBox.Text = "";
                        EmailBox.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("That Email is already in use!");
                    }
                }
                else
                {
                    MessageBox.Show("Thats not a name!");
                }
            }
            else
            {
                MessageBox.Show("Invalid Email Format");
            }
        }


        private void SelectionUserListBox(object sender, SelectionChangedEventArgs e)
        {

            // Selecting User
            AdminListBox.SelectedIndex = -1;
            if (UserListBox.SelectedIndex > -1 && UserListBox.SelectedIndex < UserList.Count)
            {
                EmailDisplay.Text = UserList[UserListBox.SelectedIndex].EmailData + "  -  " + UserList[UserListBox.SelectedIndex].NameData;
                NameBox.Text = UserList[UserListBox.SelectedIndex].NameData;
                EmailBox.Text = UserList[UserListBox.SelectedIndex].EmailData;
                ChangeButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
                MakeAdminButton.IsEnabled = true;
            }
            else
            {
                ChangeButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
                MakeAdminButton.IsEnabled = false;
                EmailDisplay.Text = "Display Selected Profils Email and Name";
            }

        }

        private void SelectionAdminListBox(object sender, SelectionChangedEventArgs e)
        {

            // Selecting Admin
            UserListBox.SelectedIndex = -1;
            if (AdminListBox.SelectedIndex > -1 && AdminListBox.SelectedIndex < AdminList.Count)
            {
                EmailDisplay.Text = AdminList[AdminListBox.SelectedIndex].EmailData + "  -  " + AdminList[AdminListBox.SelectedIndex].NameData; ;
                NameBox.Text = AdminList[AdminListBox.SelectedIndex].NameData;
                EmailBox.Text = AdminList[AdminListBox.SelectedIndex].EmailData;
                ChangeButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
                MakeUserButton.IsEnabled = true;
            }
            else
            {
                ChangeButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
                MakeUserButton.IsEnabled = false;
                EmailDisplay.Text = "Display Selected Profils Email";
            }

        }


        private void Deleting(object sender, RoutedEventArgs e)
        {
            // Deleting User
            if (UserListBox.SelectedIndex > -1)
            {

                var NewList = from c in UserList
                              where c.EmailData != UserList[UserListBox.SelectedIndex].EmailData
                              select c;
                UserList = NewList.ToList();
                UserListBox.Items.Remove(UserListBox.Items[UserListBox.SelectedIndex]);
            }
            // Deleting Admin
            else if (AdminListBox.SelectedIndex > -1)
            {
                var NewList = from c in AdminList
                              where c.EmailData != AdminList[AdminListBox.SelectedIndex].EmailData
                              select c;
                AdminList = NewList.ToList();
                AdminListBox.Items.Remove(AdminListBox.Items[AdminListBox.SelectedIndex]);
            }

        }
        // Change
        private void Changer(object sender, RoutedEventArgs e)
        {
            var match = Regex.Matches(EmailBox.Text.ToLower(), emailPattern);
            if (match.Count == 1)
            {
                IEnumerable<ProfileData> LookInUserList;
                IEnumerable<ProfileData> LookInAdminList;
                if (AdminListBox.SelectedIndex > -1)
                {
                    LookInUserList = from Looker in UserList
                                         where Looker.EmailData == EmailBox.Text.ToLower()
                                        // where Looker.EmailData != UserList[UserListBox.SelectedIndex].EmailData
                                         select Looker;
                    LookInAdminList = from Looker in AdminList
                                          where Looker.EmailData == EmailBox.Text.ToLower()
                                          where Looker.EmailData != AdminList[AdminListBox.SelectedIndex].EmailData
                                          select Looker;
                }
                else
                {
                    LookInUserList = from Looker in UserList
                                         where Looker.EmailData == EmailBox.Text.ToLower()
                                         where Looker.EmailData != UserList[UserListBox.SelectedIndex].EmailData
                                         select Looker;
                    LookInAdminList = from Looker in AdminList
                                          where Looker.EmailData == EmailBox.Text.ToLower()
                                      //    where Looker.EmailData != AdminList[AdminListBox.SelectedIndex].EmailData
                                          select Looker;
                }

                if ((LookInUserList.Count() + LookInAdminList.Count()) == 0)
                {

                    if (UserListBox.SelectedIndex > -1)
                    {
                        UserList[UserListBox.SelectedIndex].EmailData = EmailBox.Text.ToLower();
                        UserList[UserListBox.SelectedIndex].NameData = NameBox.Text;
                        UserListBox.Items[UserListBox.SelectedIndex] = NameBox.Text;
                        EmailBox.Text = "";
                        NameBox.Text = "";
                    }
                    else if (AdminListBox.SelectedIndex > -1)
                    {
                        AdminList[AdminListBox.SelectedIndex].EmailData = EmailBox.Text.ToLower();
                        AdminList[AdminListBox.SelectedIndex].NameData = NameBox.Text;
                        AdminListBox.Items[AdminListBox.SelectedIndex] = NameBox.Text;
                        EmailBox.Text = "";
                        NameBox.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Email already in use");
                }
            }
            else
            {
                MessageBox.Show("Not a valid Email!");
            }

        /*    CreateUserButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
            ChangeButton.IsEnabled = false;
            ConfirmButton.IsEnabled = true; */


        }
   /*     private void Confirming(object sender, RoutedEventArgs e)
        {
            if (UserListBox.SelectedIndex > -1)
            {
                UserList[UserListBox.SelectedIndex].EmailData = EmailBox.Text;
                UserList[UserListBox.SelectedIndex].NameData = NameBox.Text;
                UserListBox.Items[UserListBox.SelectedIndex] = NameBox.Text;
            }
            else if (AdminListBox.SelectedIndex > -1)
            {
                AdminList[AdminListBox.SelectedIndex].EmailData = EmailBox.Text;
                AdminList[AdminListBox.SelectedIndex].NameData = NameBox.Text;
                AdminListBox.Items[AdminListBox.SelectedIndex] = NameBox.Text;
            }

            CreateUserButton.IsEnabled = true;
            DeleteButton.IsEnabled = true;
            ConfirmButton.IsEnabled = false;
        } */

        private void MakingAdmin(object sender, RoutedEventArgs e)
        {

            //Create Copy
            ProfileData admin = new ProfileData();
            admin.NameData = UserList[UserListBox.SelectedIndex].NameData;
            admin.EmailData = UserList[UserListBox.SelectedIndex].EmailData;
            AdminList.Add(admin);
            AdminListBox.Items.Add(UserList[UserListBox.SelectedIndex].NameData);
            //Delete Original
            var NewList = from c in UserList
                          where c.EmailData != UserList[UserListBox.SelectedIndex].EmailData
                          select c;
            UserList = NewList.ToList();
            UserListBox.Items.Remove(UserListBox.Items[UserListBox.SelectedIndex]);
        }

        private void MakingUser(object sender, RoutedEventArgs e)
        {

            //Create Copy
            ProfileData user = new ProfileData();
            user.NameData = AdminList[AdminListBox.SelectedIndex].NameData;
            user.EmailData = AdminList[AdminListBox.SelectedIndex].EmailData;
            UserList.Add(user);
            UserListBox.Items.Add(AdminList[AdminListBox.SelectedIndex].NameData);
            //Delete Original
            var NewList = from c in AdminList
                          where c.EmailData != AdminList[AdminListBox.SelectedIndex].EmailData
                          select c;
            AdminList = NewList.ToList();
            AdminListBox.Items.Remove(AdminListBox.Items[AdminListBox.SelectedIndex]);
        }
        
        private void SavingFile(object sender, RoutedEventArgs e)
        {
            string saveUserStrign = "";
            ProfileData user = new ProfileData();
            for (int ii = 0; ii < UserList.Count() ; ii++)
            {
                saveUserStrign = saveUserStrign + UserList[ii].NameData + "\n" + UserList[ii].EmailData + "\n";
            }
            System.IO.File.WriteAllText(@"UserGallery.txt", saveUserStrign);
            string saveAdminStrign = "";
            ProfileData admin = new ProfileData();
            for (int ii = 0; ii < AdminList.Count(); ii++)
            {
                saveAdminStrign = saveAdminStrign + AdminList[ii].NameData + "\n" + AdminList[ii].EmailData + "\n";
            }
            System.IO.File.WriteAllText(@"AdminGallery.txt", saveAdminStrign);
        }
    }


    public class ProfileData
    {
        public string NameData { get; set; }
        public string EmailData { get; set; }

    }
}

