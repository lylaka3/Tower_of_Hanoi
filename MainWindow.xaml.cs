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
using System.Threading;
using System.Threading.Tasks;

namespace Pyramid
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isup = false;
        Rectangle stickup;
        StackPanel pirup;
        int n = 3;
        public MainWindow()
        {
            InitializeComponent();
            Fill();
            Chooser.SelectedIndex = 2;
        }
        public void Fill()
        {
            Brush[] colors = new Brush[] { Brushes.Red, Brushes.Blue, Brushes.Yellow, Brushes.Green, Brushes.Purple };
            Rectangle[] r1 = new Rectangle[n];
            Stick1.Height = 50;
            Stick2.Height = 50 + 30 * n;
            Stick3.Height = 50 + 30 * n;

            for (int i = 0; i < n; i++)
            {
                r1[i] = new Rectangle
                {
                    Height = 30,
                    Width = 50 + i * 30,
                    Fill = colors[i]
                };
            }
            for (int i = 0; i < n; i++)
                Pir1.Children.Add(r1[i]);
        }
        public void ChooseN(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            TextBlock selectedItem = (TextBlock)comboBox.SelectedItem;
            for (int i = 0; i < n; i++) Pir1.Children.Remove(Pir1.Children[1]);
            n = Convert.ToInt32(selectedItem.Text);        
            Fill();
        }

        public void Up(StackPanel pir, Rectangle stick)
        {
            if (pir.Children.Count != 1)
            {
                try 
                { 
                    var ring = pir.Children[1]; 
                    pir.Children.Remove(ring);
                    pir.Children.Insert(0, ring);
                    isup = !isup;
                    pirup = pir;
                    stickup = stick;
                }
                catch (ArgumentOutOfRangeException) { }
                
            }
        }
        public void Down(StackPanel pir, Rectangle stick)
        {
            if (isup)
            {
                var ring = pirup.Children[0];
                Rectangle a = (Rectangle)ring;
                try
                { 
                    Rectangle b = (Rectangle)pir.Children[1];
                    if ((a.Width < b.Width) || (pirup == pir))
                    {
                        pirup.Children.Remove(ring);
                        stickup.Height += 30;
                        pir.Children.Insert(1, ring);
                        stick.Height -= 30;
                        isup = false;
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    pirup.Children.Remove(ring);
                    stickup.Height += 30;
                    pir.Children.Insert(1, ring);
                    stick.Height -= 30;
                    isup = false;
                }                          
            }
            if ((pir != Pir1) && (pir.Children.Count == n + 1))
            {
                MessageBox.Show("Победа!");
                this.Close();
            }
        }
        public void PirChoose(StackPanel pir, Rectangle stick)
        {
            Chooser.Visibility = Visibility.Hidden;
            if (isup) Down(pir, stick);            
            else Up(pir, stick);
        }
        public void PirChoose1(object sender, RoutedEventArgs e)
        {
            PirChoose(Pir1, Stick1);
        }

        private void PirChoose2(object sender, MouseButtonEventArgs e)
        {
            PirChoose(Pir2, Stick2);
        }
        private void PirChoose3(object sender, MouseButtonEventArgs e)
        {
            PirChoose(Pir3, Stick3);
        }
        /*public async void Solve(int n, int x, int y)
        {
            StackPanel[] pirs = new StackPanel[] { Pir1, Pir2, Pir3 };
            Rectangle[] sticks = new Rectangle[] { Stick1, Stick2, Stick3 };
            //Thread.Sleep(800);
            if (n == 1)
            {
                await Task.Run(() => Up(pirs[x - 1], sticks[x - 1]));
                await Task.Run(() => Down(pirs[y - 1], sticks[y - 1]));
            }
            else
            {
                await Task.Run(() => Solve(n, x, 6 - x - y));
                await Task.Run(() => Up(pirs[x - 1], sticks[x - 1]));
                await Task.Run(() => Down(pirs[y - 1], sticks[y - 1]));
                await Task.Run(() => Solve(n, 6 - x - y, y));
            }
        }*/
        public async void SolveButton(object sender, RoutedEventArgs e)
        {
            //await Task.Run(() => Solve(n, 1, 3));            
        }
    }
}
