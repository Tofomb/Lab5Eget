using System;
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

        public MainWindow()
        {

            InitializeComponent();

        }

        private void Creating(object sender, RoutedEventArgs e)
        {
            var match = Regex.Matches(EmailBox.Text.ToLower(), emailPattern);
            if (match.Count == 1)
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
                }
                else
                {
                    MessageBox.Show("That Email is already in use!");
                }
                DebugBox.Text = UserList.Count.ToString() + " detta " + UserListBox.SelectedIndex;
            }
            else
            {
                MessageBox.Show("Invalid Email Format");
            }
        }


        private void SelectionUserListBox(object sender, SelectionChangedEventArgs e)
        {


            AdminListBox.SelectedIndex = -1;
            if (UserListBox.SelectedIndex > -1 && UserListBox.SelectedIndex < UserList.Count)
            {
                EmailDisplay.Text = UserList[UserListBox.SelectedIndex].EmailData;
                ChangeButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
                MakeAdminButton.IsEnabled = true;
            }
            else
            {
                ChangeButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
                MakeAdminButton.IsEnabled = false;
                EmailDisplay.Text = "Display Selected Profils Email";
            }
            string debugging = "";
            for (int ii = 0; ii < UserList.Count; ii++)
            {
                debugging = debugging + "\r" + ii + " " + UserList[ii].EmailData;
            }

            DebugBox.Text = UserList.Count.ToString() + " detta " + UserListBox.SelectedIndex + " " + debugging;

        }
        private void SelectionAdminListBox(object sender, SelectionChangedEventArgs e)
        {
            UserListBox.SelectedIndex = -1;

            if (AdminListBox.SelectedIndex > -1 && AdminListBox.SelectedIndex < AdminList.Count)
            {
                EmailDisplay.Text = AdminList[AdminListBox.SelectedIndex].EmailData;
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

        private void LoadMainFrame(object sender, RoutedEventArgs e)
        {
            ChangeButton.IsEnabled = false;
            DeleteButton.IsEnabled = false;
            ConfirmButton.IsEnabled = false;
            MakeAdminButton.IsEnabled = false;
            MakeUserButton.IsEnabled = false;
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

            //debug
            string debugging = "";
            for (int ii = 0; ii < UserList.Count; ii++)
            {
                debugging = debugging + "\r" + ii + " " + UserList[ii].EmailData;
            }

            DebugBox.Text = UserList.Count.ToString() + " detta " + UserListBox.SelectedIndex + " " + debugging;
        }

        private void Changer(object sender, RoutedEventArgs e)
        {
            var match = Regex.Matches(EmailBox.Text.ToLower(), emailPattern);
            if (match.Count == 1)
            {
                var LookInUserList = from Looker in UserList
                                     where Looker.EmailData == EmailBox.Text.ToLower()
                                     where Looker.EmailData != UserList[UserListBox.SelectedIndex].EmailData
                                     select Looker;
                var LookInAdminList = from Looker in AdminList
                                      where Looker.EmailData == EmailBox.Text.ToLower()
                                      where Looker.EmailData != AdminList[AdminListBox.SelectedIndex].EmailData
                                      select Looker;
                if (LookInUserList.Count() + LookInAdminList.Count() == 0)
                {

                    if (UserListBox.SelectedIndex > -1)
                    {
                        // NameBox.Text = UserList[UserListBox.SelectedIndex].NameData;
                        // EmailBox.Text = UserList[UserListBox.SelectedIndex].EmailData;
                        UserList[UserListBox.SelectedIndex].EmailData = EmailBox.Text.ToLower();
                        UserList[UserListBox.SelectedIndex].NameData = NameBox.Text;
                        UserListBox.Items[UserListBox.SelectedIndex] = NameBox.Text;
                    }
                    else if (AdminListBox.SelectedIndex > -1)
                    {
                        //   NameBox.Text = AdminList[AdminListBox.SelectedIndex].NameData;
                        //  EmailBox.Text = AdminList[AdminListBox.SelectedIndex].EmailData;
                        AdminList[AdminListBox.SelectedIndex].EmailData = EmailBox.Text.ToLower();
                        AdminList[AdminListBox.SelectedIndex].NameData = NameBox.Text;
                        AdminListBox.Items[AdminListBox.SelectedIndex] = NameBox.Text;
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
    }


    public class ProfileData
    {
        public string NameData { get; set; }
        public string EmailData { get; set; }

    }
}

