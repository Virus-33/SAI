using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Logic.Resources;
using Logic.Screatures;

namespace SCREEEEE
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            env = new(50, 350, 15);
            Fill();
            flag = false;
        }

        bool flag;
        public Logic.MainClasses.Environment env { get; private set; }

        async void Action()
        {
            while (flag)
            {
                env.Move();
                Update();
                await Task.Delay(100);
            }
        }

        void Update()
        {
            drawer.Children.Clear();

            foreach (BaseScreature bs in env.Creatures)
            {
                Rectangle rec = new();
                rec.Width = 6;
                rec.Height = 6;
                rec.Fill = new SolidColorBrush((System.Windows.Media.Color)new ColorConverter().ConvertFrom(bs.colour));
                drawer.Children.Add(rec);
                Canvas.SetTop(rec, bs.Y);
                Canvas.SetLeft(rec, bs.X);
            }

            foreach (PointBase pb in env.Points)
            {
                Rectangle rec = new();
                rec.Width = 12;
                rec.Height = 12;
                rec.Fill = new SolidColorBrush((System.Windows.Media.Color)new ColorConverter().ConvertFrom(pb.colour));
                drawer.Children.Add(rec);
                Canvas.SetTop(rec, pb.Y);
                Canvas.SetLeft(rec, pb.X);
            }
        }

        void Fill()
        {
            Speed.Content = env.speed.ToString();
            Spread.Content = env.spread.ToString();
            Radius.Content = env.HearRadius.ToString();
        }

        private void speed_Click(object sender, RoutedEventArgs e)
        {
            int temp;
            if (int.TryParse(newSpeed.Text, out temp))
            {
                env.setSpeed(temp);
                Speed.Content = temp.ToString();
            }
        }

        private void cord_Click(object sender, RoutedEventArgs e)
        {
            double temp, temp1;
            if (double.TryParse(baseX.Text, out temp) && double.TryParse(baseY.Text, out temp1))
            {
                env.NewBase(temp, temp1);
            }
        }

        private void spead_Click(object sender, RoutedEventArgs e)
        {
            double temp;
            if (double.TryParse(newSpread.Text, out temp))
            {
                env.setSpred(temp);
                Spread.Content = temp.ToString();
            }
        }

        private void radius_Click(object sender, RoutedEventArgs e)
        {
            int temp;
            if (int.TryParse(newHR.Text, out temp))
            {
                env.setHearRadius(temp);
                Radius.Content = temp.ToString();
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            flag = true;
            Action();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            flag = false;
            drawer.Children.Clear();
            env = new(50, 350, 15);
            Fill();
        }

        private void drawer_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point Pos = Mouse.GetPosition(drawer);
            env.PlaceResource(Pos.X, Pos.Y, 1);
            Update();
        }

        private void drawer_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point Pos = Mouse.GetPosition(drawer);
            env.PlaceResource(Pos.X, Pos.Y, 2);
            Update();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Это - программа симуляции роевого интеллекта.\n\nБольшие квадраты - ресурсы, которые нужны агентам похожего цвета.\n" +
                "Агент меняет цвет на более яркий, если он несёт этот ресурс в данный момент в логово\n\n" +
                "Маленькие квадраты - агенты. Они ищут ресурсы и приносят их в логово.\n" +
                "Агенты слепы. Они ничего не видят и ориентируются лишь по выкрикам своих сородичей. Каждый из них знает, когда кричат расстояние до логова, а когда до ресурса.\n" +
                "Агент может услышать своего сородича лишь на определённом расстоянии" +
                "\n\nБольшой оранжевый квардат - логово. К ней жуки и носят ресурсы. Если долгое время ресурсов не будет, матка в логове умрёт.\n\n" +
                "Нажимайте на тёмно-серую область левой кнопкой мыши для создания жёлтого ресурса, и правой для создания красного.\n" +
                "Вы можете менять параметры скорости агентов, разброса их поворота, радиус слышимости, а также создавать новые логова вместо старых.\n\n" +
                "Нажмите на кнопку \"старт\" для того, чтобы начать симуляцию.\n" +
                "Клавиша \"Стоп\" убьёт всех агентов, удалит ресурсы и поставит логово в начальное положение, перезапустив симуляцию\n\n" +
                "Помните, что агенты очень несовершенны, и в виду слабой нервной системы могут иногда путаться и идти не туда (зов природы, видимо)", "О программе", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
