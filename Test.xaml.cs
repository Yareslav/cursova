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
using System.Windows.Shapes;
using System.Timers;
using System.Windows.Threading;

namespace real
{
    public partial class Test : Window
    {
        List<StatisticsClass> statistics = new List<StatisticsClass>();
        int rightAnswers = 0;
        int wrongAnswers = 0;
        int currentStep = 0;
        Timer timer =new Timer();
        Time time = new Time();
        DateTime timeSTart;
        TestVariant[] mass= TestVariants.getVariants;
        bool animation = false;
        public Test(List<StatisticsClass> statistics)
        {
            InitializeComponent();
            this.statistics = statistics;
            GenerateOptions();
            timer.Interval = 1000;
            timer.Elapsed += time.Increase;
            timer.Start();
            timeSTart = new DateTime();
        }
        private void Submit(object sender, RoutedEventArgs e)
        {
            if (animation) return;
            animation = true;
            var booleans= (mass[currentStep].type == "radio") ? RadioButtonCheck() : CheckBoxCheck();
            //is answer given
            if (!booleans[0]) return;
            //is right answer
            if (booleans[1]) rightAnswers++;
            else wrongAnswers++;
            //timeout
            var timeout = new Timer();
            timeout.Interval = 1000;
            timeout.Elapsed += TimeoutFunc;
            timeout.Start();
            void TimeoutFunc(Object source, ElapsedEventArgs e)
            {
                Dispatcher.Invoke(() =>
                {
                    if (currentStep == mass.Length - 1) FinishTest();
                    else
                    {
                        currentStep++;
                        GenerateOptions();
                        animation = false;
                    }
                    timeout.Stop();
                });
            }
        }
        private void FinishTest()
        {
            timer.Stop();
            statistics.Add(new StatisticsClass(rightAnswers,wrongAnswers, timeSTart, time.GetTotal()));
            Decorate();
            //!!timeout
            var timeout = new Timer();
            timeout.Interval = 3000;
            timeout.Elapsed += Event;
            timeout.Start();
           
            void Event(Object source, ElapsedEventArgs e)
            {
                Dispatcher.Invoke(() =>
                {
                    timeout.Stop();
                    var window = new MainWindow(statistics);
                    window.Show();
                    Close();
                });
            }
        }
        void Decorate()
        {
            Result.Visibility =Visibility.Visible;
            int totalQuestions = rightAnswers + wrongAnswers;
            decimal rightAnswerPercentage = rightAnswers * 100 / totalQuestions;
            string resultText = "";
            string resultImage = "";
            if (rightAnswerPercentage > 75)
            {
                resultImage = "good";
                resultText = "great . Your logical thinking is advanced . Carry on boy";
            }
            else if (rightAnswerPercentage>40)
            {
                resultImage = "normal";
                resultText = "good . Your logic works normally . But you definitely need to upgrate it";
            }
            else
            {
                resultImage = "bad";
                resultText = "bad . Don`t be sad . Try again";
            }
            ResultText.Text=$"you have answered on {rightAnswers} of {totalQuestions} . This is {resultText}";
            ResultImage.Source= new BitmapImage(new Uri($"C:/My/Programesoft/C#/cursova/real/real/images/{resultImage}.png"));
        }
        private void GenerateOptions()
        {
            var obj = mass[currentStep];
            Order.Text = $"{currentStep+1}/{mass.Length}";
            ImageElement.Source =new BitmapImage(new Uri(obj.source));
            Question.Text = obj.question;
            Controls.Children.Clear();
            var gridPosition = new int[,] {{0,0},{0,1},{1,0},{1,1}};
            for (var i = 0; i <4; i++)
            {
                if (obj.type == "radio")
                {
                    var element = new RadioButton();
                    element.Content = obj.variants[i];
                    FastAdding(element);
                } else
                {
                    var element = new CheckBox();
                    element.Content = obj.variants[i];
                    FastAdding(element);
                }
                void FastAdding(UIElement element)
                {
                    Grid.SetColumn(element, gridPosition[i, 0]);
                    Grid.SetRow(element, gridPosition[i, 1]);
                    Controls.Children.Add(element);
                }
            }
        }
        private bool[] RadioButtonCheck()
        {
            var children = Controls.Children;
            int selectedButtonIndex = 0;
            bool isAnswerPresents()
            {
                bool isChecked = false;
                for (var i = 0; i < children.Count; i++)
                {
                    if ((bool)((RadioButton)children[i]).IsChecked)
                    {
                        selectedButtonIndex = i;
                        isChecked = true;
                    }
                }
                return isChecked;
            }
            void MakeColorfull()
            {
                for (var i = 0; i < children.Count; i++)
                {
                    var button = (RadioButton)children[i];
                    button.Background = GetColor(i == mass[currentStep].rightAnswers[0]);
                }
            }
            if (!isAnswerPresents()) return new bool[] {false};
            MakeColorfull();
            return new bool[] {true,selectedButtonIndex== mass[currentStep].rightAnswers[0]};
        }
        private bool[] CheckBoxCheck()
        {
            var children = Controls.Children;
            var indexes = new List<int>();
            bool isAnswerPresents()
            {
                bool isChecked = false;

                for (var i = 0; i < children.Count; i++)
                {
                    if ((bool)((CheckBox)children[i]).IsChecked)
                    {
                        indexes.Add(i);
                        isChecked = true;
                    }
                }
                return isChecked;
            }
            bool isAnswerCorrect()
            {
                bool correct = true;
                var currentList = mass[currentStep];
                if (currentList.rightAnswers.Length != indexes.Count) correct = false;
                else
                {
                    indexes.Sort();
                    var rightAnswersClone = (int[])currentList.rightAnswers.Clone();
                    Array.Sort(rightAnswersClone);
                    for (var i = 0; i < indexes.Count; i++)
                    {
                        if (indexes[i] != rightAnswersClone[i]) correct = false;
                    }
                }
                return correct;
            }
            void MakeColorfull()
            {
                for (var i = 0; i < children.Count; i++)
                {
                    var button = (CheckBox)children[i];
                    button.Background = GetColor(mass[currentStep].rightAnswers.Contains(i));
                }
            }
            if (!isAnswerPresents()) return new bool[] { false };
            MakeColorfull();
            return new bool[] {true, isAnswerCorrect() };
        }
        private SolidColorBrush GetColor(bool condition)
        {
            return new SolidColorBrush((Color)ColorConverter.ConvertFromString(condition ? Colors.green : Colors.yellow));
        }
    }
}
