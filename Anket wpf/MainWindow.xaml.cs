using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Anket_wpf
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Person>? People { get; set; }

        public ObservableCollection<string?>? Names { get; set; }


        public Person CreatePerson()
        {
            Person person = new()
            {
                Name = txtbox_name.Text,
                Surname = txtbox_surname.Text,
                Email = txtbox_email.Text,
                Phone = txtbox_tel.Text,
                BDate = bdatepicker.Text
            };
            return person;
        }

        public void NamePicker()
        {
            foreach (var item in People)
            {
                Names.Add(item.Name);
            }
        }

        public void ResetData()
        {
            txtbox_email.Clear();
            txtbox_name.Clear();
            txtbox_surname.Clear();
            txtbox_tel.Clear();
            //bdatepicker.Clear();
        }

        private void GetNamesFromPeople(ObservableCollection<Person>? people)
        {
            foreach (var item in people)
            {
                Names.Add(item.Name);
            }
        }

        public void PersonRemover(string p)
        {
            foreach (var item in Names)
            {
                if (item == p)
                {
                    Names.Remove(item);
                    break;
                }
            }

            foreach (var item in People)
            {
                if (item.Name == p)
                {
                    People.Remove(item);
                    break;
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            People = new();
            Names = new();

            NamePicker();

            DataContext = this;
        }

        private void btn_main_Click(object sender, RoutedEventArgs e)
        {
            if (btn_main.Content.ToString() == "Add")
            {
                Person temp = CreatePerson();
                People.Add(temp);
                Names.Add(temp.Name);
                ResetData();
            }

            else
            {
                if (listbox_people.SelectedItem is string p)
                {
                    PersonRemover(p);

                    Person temp = CreatePerson();

                    People.Add(temp);
                    Names.Add(temp?.Name);
                    ResetData();
                }
            }
        }

        private void listbox_people_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btn_main.Content = "Edit";
            foreach (var item in People)
            {
                if (listbox_people.SelectedItem == item.Name)
                {
                    txtbox_email.Text = item.Email;
                    txtbox_name.Text = item.Name;
                    txtbox_surname.Text = item.Surname;
                    txtbox_tel.Text = item.Phone;
                    bdatepicker.Text = item.BDate;
                    break;
                }
            }
        }

        private void btn_reset_Click(object sender, RoutedEventArgs e)
        {
            ResetData();
        }

        private void listbox_people_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_main.Content = "Add";
            ResetData();
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var json = JsonSerializer.Serialize(People);
                File.WriteAllText($"{txtbox_file.Text}.json", json);
                //listbox_people.Items.Clear();
                MessageBox.Show("saved");
                People.Clear();
                listbox_people.ItemsSource = People;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_load_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var json = File.ReadAllText($"{txtbox_file.Text}.json");
                People = JsonSerializer.Deserialize<ObservableCollection<Person>>(json);
                GetNamesFromPeople(People);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void GroupBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btn_main.Content = "Add";
            ResetData();
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            btn_main.Content = "Add";
            ResetData();
        }

        private void listbox_people_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (listbox_people.SelectedItem is string n)
                PersonRemover(n);
        }
    }
}