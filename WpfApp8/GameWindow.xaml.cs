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
using System.Windows.Threading;

namespace WpfApp8
{
    /// <summary>
    /// Логика взаимодействия для Game.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        static Random r = new Random();
        private int score = 0;
        private string filePath = @"C:\Users\User\source\repos\WpfApp8\WpfApp8\users.json";

        private TextBlock timerTextBlock;
        private TextBlock highScoreTextBlock;
        private DispatcherTimer gameTimer;
        private TimeSpan timeLeft;
        private Game game;
        private List<Thing> things;

        private bool gameEnded;
        public GameWindow()
        {
            InitializeComponent();
            InitializeUIElements();
            SetupGameTimer();
            DisplayUIElementsOn(myCanvas);
            game = new Game();

            gameplace.Fill = game.backgroundImage;

            player1.Fill = game.basket.Imagee;
            StartGameTimer();
        }
        public class Game
        {
            public Basket basket;
            public ImageBrush backgroundImage;
            public Game()
            {
                basket = new Basket();

                backgroundImage = new ImageBrush(); // new background image brush - will be used to show background
                backgroundImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/pngeggBackground.jpg"));

            }

            public void Start()
            {

            }
        }
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            // Get the x and y coordinates of the mouse pointer.
            System.Windows.Point position = e.GetPosition(this);
            double pX = position.X;
            // Sets the Height/Width of the circle to the mouse coordinates.
            Canvas.SetLeft(player1, pX - 250);
        }
        private void InitializeUIElements()
        {
            timerTextBlock = new TextBlock
            {
                FontSize = 30,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Black),
                HorizontalAlignment = HorizontalAlignment.Right
            };

            highScoreTextBlock = new TextBlock
            {
                Text = $"Score: {score}",
                FontSize = 24,
                FontWeight = FontWeights.Bold,
                Foreground = new SolidColorBrush(Colors.Black),
                HorizontalAlignment = HorizontalAlignment.Right
            };
        }

        public void StartGameTimer()
        {
            if (!gameTimer.IsEnabled)
            {
                gameTimer.Start();
            }
        }
        private void DisplayUIElementsOn(Canvas parentCanvas)
        {
            if (!parentCanvas.Children.Contains(timerTextBlock))
                parentCanvas.Children.Add(timerTextBlock);


            if (!parentCanvas.Children.Contains(highScoreTextBlock))
                parentCanvas.Children.Add(highScoreTextBlock);


            UpdateUIElementsPosition(parentCanvas);
        }
        private void UpdateUIElementsPosition(Canvas parentCanvas)
        {
            double topMargin = 5;
            double rightPosition = parentCanvas.ActualWidth - timerTextBlock.ActualWidth;

            Canvas.SetTop(timerTextBlock, topMargin);
            Canvas.SetLeft(timerTextBlock, rightPosition);

            topMargin = 50;
            rightPosition = parentCanvas.ActualWidth - highScoreTextBlock.ActualWidth;

            Canvas.SetTop(highScoreTextBlock, topMargin);
            Canvas.SetLeft(highScoreTextBlock, rightPosition);

        }
        private void SetupGameTimer()
        {
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromSeconds(1);
            gameTimer.Tick += GameTimer_Tick;
            timeLeft = TimeSpan.FromSeconds(20);
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            things = new List<Thing>();
            gameTimer.Tick += MakeThings; // run the make presents function

            if (timeLeft.TotalSeconds > 0)
            {
                timeLeft = timeLeft.Add(TimeSpan.FromSeconds(-1));
                timerTextBlock.Text = $"Time {timeLeft.TotalSeconds}";
            }
            else if (!gameEnded)
            {
                gameEnded = true;
                gameTimer.Stop();
                var result = MessageBox.Show("Час закінчився! Гра завершена. Бажаєте почати спочатку?", "Гра завершена", MessageBoxButton.YesNo, MessageBoxImage.Question);
                
                User newuser = new User(UserNameWindow.userName, score);
                ResaultsWindow res = new ResaultsWindow(score);
                newuser.SaveToJSON(filePath);
                Close();
                res.ShowDialog();

                if (result == MessageBoxResult.Yes)
                {
                }
                else if (result == MessageBoxResult.No)
                {
                    Application.Current.Shutdown();
                }
            }
        }
        private void MakeThings(object sender, EventArgs e)
        {
            Thing thing = new RedFood();
            Rectangle newRec = new Rectangle { };

            if (r.Next(100) < 20)
            {
                Objects objectType = (Objects)r.Next(9);
                thing = CreateThing(objectType);
                things.Add(thing);
            }
            if (thing == null)
            {
                return;
            }
            newRec = new Rectangle
            {
                Tag = "drops",
                Width = 60,
                Height = 60,
                Fill = thing.Imagee,
                Opacity = 1,
            };
            Canvas.SetTop(newRec, r.Next(60, 150) * -1);
            Canvas.SetLeft(newRec, r.Next(10, 450));
            // once the location is set now we can add it to the canvas
            myCanvas.Children.Add(newRec);
        }
        
        private Thing CreateThing(Objects objectType)
        {
            //жалке подобіє поліморфізма
            switch (objectType)
            {
                case Objects.Apple | Objects.Tomato:
                    return new RedFood();
                case Objects.Cucumber:
                    return new GreenFood();
                case Objects.Banana | Objects.Orange | Objects.Pumpking:
                    return new YellowFood();
                case Objects.Ball | Objects.Bear | Objects.Car:
                    return new Toy();
                default:
                    return null;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            timeLeft = timeLeft.Add(TimeSpan.FromSeconds(-timeLeft.TotalSeconds));//Button's Clicked == Time's up
        }
    }
}
