using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

namespace MyWpfCanvasGrid
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _conter;

        #region OnPropertyChanged Properties

        // Template for a new INotify Changed Property
        // for using CTRL-R-R
        private string xxx;
        public string Xxx
        {
            get { return xxx; }
            set { SetField(ref xxx, value, nameof(Xxx)); }
        }
        #endregion


        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        /******************************/
        /*       Button Events        */
        /******************************/
        #region Button Events

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var p = Properties.Settings.Default;

            // Output and debugging
            string mes = String.Format("#1 StaticGrid={0} Columns={1} Rows={2} ColumXS={3} RowYS={4}", p.StaticGrid, p.Colums, p.Rows, p.ColumXS, p.RowYS);
            Debug.WriteLine(mes);
            mes = String.Format("#1 MyCanvas.ActualWidth={0} MyCanvas.ActualHeight={1}", MyCanvas.ActualWidth,MyCanvas.ActualHeight);
            Debug.WriteLine(mes);

            RecalculateGridValues();
        }

        /// <summary>
        /// Button_Click_Close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            Close();
        }

        #endregion
        /******************************/
        /*      Menu Events          */
        /******************************/
        #region Menu Events

        #endregion
        /******************************/
        /*      Other Events          */
        /******************************/
        #region Other Events

        /// <summary>
        /// MyGrid_SizeChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RecalculateGridValues();
        }

        /// <summary>
        /// TextBox_LostFocus_XS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_LostFocus_XS(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("{0} Enter TextBox_LostFocus_XS()", ++_conter);
            RecalculateGridValues();
        }

        /// <summary>
        /// TextBox_LostFocus_YS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_LostFocus_YS(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("{0} Enter TextBox_LostFocus_YS()", ++_conter);
            RecalculateGridValues();
        }

        /// <summary>
        /// Slider_ValueChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RecalculateGridValues();
        }

        /// <summary>
        /// Window_Closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        #endregion
        /******************************/
        /*      Other Functions       */
        /******************************/
        #region Other Functions

        /// <summary>
        /// RecalculateGridValues
        /// </summary>
        private void RecalculateGridValues()
        {
            if (MyGrid.ActualWidth <= 0)
                return;

            Debug.WriteLine("{0} Enter RecalculateGridValues()",++_conter);

            var p = Properties.Settings.Default;

            if (p.StaticGrid)
            {
                p.Colums = (int)Math.Round(MyGrid.ActualWidth / p.ColumXS);
                p.Rows = (int)Math.Round(MyGrid.ActualHeight / p.RowYS);
            }
            else
            {
                p.ColumXS = MyGrid.ActualWidth / p.Colums;
                p.RowYS = MyGrid.ActualHeight / p.Rows;
            }
        }

        /// <summary>
        /// SetField
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        private void OnPropertyChanged(string p)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(p));
        }

        #endregion
    }

    [System.Windows.Data.ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBooleanConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}
