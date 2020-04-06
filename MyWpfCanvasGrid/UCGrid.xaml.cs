using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyWpfCanvasGrid
{
    /// <summary>
    /// Interaktionslogik für UCGrid.xaml
    /// From:
    /// https://www.codeproject.com/Tips/405355/LinesGrid-control-An-easy-way-to-draw-a-grid-in-WP
    /// </summary>
    public partial class UCGrid : UserControl
    {
        public static readonly DependencyProperty ColumnsProperty;
        public int Columns
        {
            get
            {
                return (int)GetValue(ColumnsProperty);
            }

            set
            {
                SetValue(ColumnsProperty, value);
            }
        }
        public static readonly DependencyProperty RowsProperty;
        public int Rows
        {
            get
            {
                return (int)GetValue(RowsProperty);
            }

            set
            {
                SetValue(RowsProperty, value);
            }
        }
        public static readonly DependencyProperty LineThicknessProperty;
        public double LineThickness
        {
            get
            {
                return (double)GetValue(LineThicknessProperty);
            }

            set
            {
                SetValue(LineThicknessProperty, value);
            }
        }
        public static readonly DependencyProperty LineBrushProperty;
        public Brush LineBrush
        {
            get
            {
                return (Brush)GetValue(LineBrushProperty);
            }

            set
            {
                SetValue(LineBrushProperty, value);
            }
        }

        /// <summary>
        /// static Constructor
        /// </summary>
        static UCGrid()
        {
            ColumnsProperty = DependencyProperty.Register("Columns", typeof(int), typeof(UCGrid), new PropertyMetadata(1, Columns_Changed));
            RowsProperty = DependencyProperty.Register("Rows", typeof(int), typeof(UCGrid), new PropertyMetadata(1, Rows_Changed));
            LineThicknessProperty = DependencyProperty.Register("LineThickness", typeof(double), typeof(UCGrid), new PropertyMetadata(1d));
            LineBrushProperty = DependencyProperty.Register("LineBrush", typeof(Brush), typeof(UCGrid), new PropertyMetadata(Brushes.Black));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public UCGrid()
        {
            DataContext = this;
            InitializeComponent();
            Loaded += new RoutedEventHandler(LinesGrid_Loaded);
        }

        /// <summary>
        /// Columns_Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Columns_Changed(object sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as UCGrid).RecreateLines();
        }

        /// <summary>
        /// Rows_Changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Rows_Changed(object sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as UCGrid).RecreateLines();
        }

        /// <summary>
        /// LinesGrid_Loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinesGrid_Loaded(object sender, RoutedEventArgs e)
        {
            RecreateLines();
        }

        /// <summary>
        /// RecreateLines
        /// </summary>
        private void RecreateLines()
        {
            if (ActualWidth == 0 || ActualHeight == 0 || Columns <= 0 || Rows <= 0)
                return;

            double cellWidth = ActualWidth / (LineThickness * Columns);
            verticalLines.Viewbox = new Rect(0, 0, cellWidth, 1);

            double cellHeight = ActualHeight / (LineThickness * Rows);
            horizontalLines.Viewbox = new Rect(0, 0, 1, cellHeight);

            double qv = (1d - LineThickness / ActualWidth) / Columns;
            verticalLines.Viewport = new Rect(0, 0, qv, 1);

            double qh = (1d - LineThickness / ActualHeight) / Rows;
            horizontalLines.Viewport = new Rect(0, 0, 1, qh);
        }
    }
}
