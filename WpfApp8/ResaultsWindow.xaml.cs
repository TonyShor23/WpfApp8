using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;




namespace WpfApp8
{
    /// <summary>
    /// Логика взаимодействия для Resaults.xaml
    /// </summary>
    public partial class ResaultsWindow : Window
    {
        List<User> userList = new List<User>();
        public string path = @"C:\Users\User\source\repos\WpfApp8\WpfApp8\users.json";
        public ResaultsWindow(int score)
        {
            InitializeComponent();
            userList = GetUsersFull(path);
            //Console.WriteLine("Added: " + userList[0].Name);
            
            int myId = GetUserID(userList, UserNameWindow.userName);
            //userList = SortYourself(userList);

            scoreValue.Content = score;

            //UserList.Items.Add(usser);
        }

        private int GetUserID(List<User> list, string line)
        {
            foreach (User u in list) 
            {
                string name = u.Name;
                Console.WriteLine("name: "+ name);
            }
            return 0;
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Бажаєте закрити гру?", "Попередження", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result==MessageBoxResult.Yes)
            {
                Close();
                if (File.Exists(path))
                {
                    //File.Delete(path);
                }
            }

        }

        private void userChange_Click(object sender, RoutedEventArgs e)
        {
            UserNameWindow newUser = new UserNameWindow();
            Close();
            newUser.ShowDialog();
        }

        private void playAgain_Click(object sender, RoutedEventArgs e)
        {
            GameWindow game = new GameWindow();
            Close();
            game.ShowDialog();
        }
        private List<User> GetUsersFull(string filePath)
        {
            List<User> users = new List<User>();
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    User user = new User(); // Створюємо новий екземпляр User для кожного рядка JSON
                    user = LoadFromJSON(line);
                    users.Add(user);
                }
                return users;
            }
            //else
            //{
            //    MessageBox.Show("Наразі немає даних для виведення!");
            //}

            return users;

        }
        private User LoadFromJSON(string line)
        {
            string userName = "empty"; // default values
            int score = 0;
            using (JsonDocument document = JsonDocument.Parse(line))
            {
                JsonElement root = document.RootElement;
                userName = root.GetProperty("Name").GetString();
                score = root.GetProperty("Score").GetInt32();
            }
            User usser = new User(userName, score);
            return usser;
        }

    }
}
