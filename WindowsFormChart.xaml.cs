using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.DataVisualization.Charting;

namespace Utils
{
    /// <summary>
    /// Interaction logic for WindowsFormChart.xaml
    /// </summary>
    public partial class WindowsFormChart : UserControl
    {
        public WindowsFormChart()
        {
            InitializeComponent();

            initialize();
        }

        private void initialize()
        {
            var chart = wfchart;

            // ChartAreaとSeriesの作成と追加
            chart.ChartAreas.Add(new ChartArea());
            chart.Series.Add(new Series());
        }

        #region "Dataプロパティ"
        public static readonly DependencyProperty _data
            = DependencyProperty.Register(nameof(Data), typeof(IEnumerable<Point>), typeof(WindowsFormChart),
                new UIPropertyMetadata(new List<Point>(), null, new CoerceValueCallback(onDataUpdated)));

        [Bindable(true)]
        public IEnumerable<Point> Data
        {
            get
            {
                return (IEnumerable<Point>)GetValue(_data);
            }
            set
            {
                SetValue(_data, value);
            }
        }

        private static object onDataUpdated(DependencyObject d, object baseValue)
        {
            try
            {
                var chart = (d as WindowsFormChart)?.wfchart;
                chart?.Series[0].Points.Clear();
                foreach (var p in (IEnumerable<Point>)baseValue)
                {
                    chart?.Series[0].Points.AddXY(p.X, p.Y);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return baseValue;
        }
        #endregion

        #region "Titleプロパティ"
        public static readonly DependencyProperty _title
            = DependencyProperty.Register(nameof(Title), typeof(string), typeof(WindowsFormChart),
                new UIPropertyMetadata(null, new PropertyChangedCallback(onTitleChanged)));

        [Bindable(true)]
        public string Title
        {
            get
            {
                return (string)GetValue(_title);
            }
            set
            {
                SetValue(_title, value);
            }
        }

        private static void onTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chart = (d as WindowsFormChart)?.wfchart;
            chart.Titles.Clear();
            if (e.NewValue != null)
            {
                chart.Titles.Add((string)e.NewValue);
            }
        }
        #endregion


        #region "AxisXMinimumプロパティ"
        public static readonly DependencyProperty _axisXMinimun
            = DependencyProperty.Register(nameof(AxisXMinimum), typeof(double), typeof(WindowsFormChart),
                new UIPropertyMetadata(double.NaN, new PropertyChangedCallback(onAxisXMinimumChanged)));

        [Bindable(true)]
        public double AxisXMinimum
        {
            get
            {
                return (double)GetValue(_axisXMinimun);
            }
            set
            {
                SetValue(_axisXMinimun, value);
            }
        }

        private static void onAxisXMinimumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chart = (d as WindowsFormChart)?.wfchart;
            if (chart?.ChartAreas.Any() ?? false)
            {
                chart.ChartAreas[0].AxisX.Minimum = (double)e.NewValue;
            }
        }
        #endregion

        #region "AxisXMaximumプロパティ"
        public static readonly DependencyProperty _axisXMaximum
            = DependencyProperty.Register(nameof(AxisXMaximum), typeof(double), typeof(WindowsFormChart),
                new UIPropertyMetadata(double.NaN, new PropertyChangedCallback(onXMaximumChanged)));

        [Bindable(true)]
        public double AxisXMaximum
        {
            get
            {
                return (double)GetValue(_axisXMaximum);
            }
            set
            {
                SetValue(_axisXMaximum, value);
            }
        }

        private static void onXMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chart = (d as WindowsFormChart)?.wfchart;
            if (chart?.ChartAreas.Any() ?? false)
            {
                chart.ChartAreas[0].AxisX.Maximum = (double)e.NewValue;
            }
        }
        #endregion

        #region "AxisYMinimumプロパティ"
        public static readonly DependencyProperty _axisYMinimun
            = DependencyProperty.Register(nameof(AxisYMinimum), typeof(double), typeof(WindowsFormChart),
                new UIPropertyMetadata(double.NaN, new PropertyChangedCallback(onAixsYMinimumChanged)));

        [Bindable(true)]
        public double AxisYMinimum
        {
            get
            {
                return (double)GetValue(_axisYMinimun);
            }
            set
            {
                SetValue(_axisYMinimun, value);
            }
        }

        private static void onAixsYMinimumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chart = (d as WindowsFormChart)?.wfchart;
            if (chart?.ChartAreas.Any() ?? false)
            {
                chart.ChartAreas[0].AxisY.Minimum = (double)e.NewValue;
            }
        }
        #endregion

        #region "AixsYMaximumプロパティ"
        public static readonly DependencyProperty _axisYMaximum
            = DependencyProperty.Register(nameof(AixsYMaximum), typeof(double), typeof(WindowsFormChart),
                new UIPropertyMetadata(double.NaN, new PropertyChangedCallback(onAixsYMaximumChanged)));

        [Bindable(true)]
        public double AixsYMaximum
        {
            get
            {
                return (double)GetValue(_axisYMaximum);
            }
            set
            {
                SetValue(_axisYMaximum, value);
            }
        }

        private static void onAixsYMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chart = (d as WindowsFormChart)?.wfchart;
            if (chart?.ChartAreas.Any() ?? false)
            {
                chart.ChartAreas[0].AxisY.Maximum = (double)e.NewValue;
            }
        }
        #endregion

        #region "ChartTypeプロパティ"
        public static readonly DependencyProperty _chartType
            = DependencyProperty.Register(nameof(ChartType), typeof(SeriesChartType), typeof(WindowsFormChart),
                new UIPropertyMetadata(new SeriesChartType(), new PropertyChangedCallback(onChartTypeChanged)));

        [Bindable(true)]
        public SeriesChartType ChartType
        {
            get
            {
                return (SeriesChartType)GetValue(_chartType);
            }
            set
            {
                SetValue(_chartType, value);
            }
        }

        private static void onChartTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chart = (d as WindowsFormChart)?.wfchart;
            if (chart?.Series.Any() ?? false)
            {
                chart.Series[0].ChartType = (SeriesChartType)e.NewValue;
            }
        }
        #endregion

        #region "Colorプロパティ"
        public static readonly DependencyProperty _color
            = DependencyProperty.Register(nameof(Color), typeof(System.Drawing.Color), typeof(WindowsFormChart),
                new UIPropertyMetadata(new System.Drawing.Color(), new PropertyChangedCallback(onColorChanged)));

        [Bindable(true)]
        public System.Drawing.Color Color
        {
            get
            {
                return (System.Drawing.Color)GetValue(_color);
            }
            set
            {
                SetValue(_color, value);
            }
        }

        private static void onColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chart = (d as WindowsFormChart)?.wfchart;
            if (chart?.Series.Any() ?? false)
            {
                chart.Series[0].Color = (System.Drawing.Color)e.NewValue;
            }
        }
        #endregion

        #region "BorderWidthプロパティ"
        public static readonly DependencyProperty _borderWidth
            = DependencyProperty.Register(nameof(BorderWidth), typeof(int), typeof(WindowsFormChart),
                new UIPropertyMetadata(1, new PropertyChangedCallback(onBorderWidthChanged)));

        [Bindable(true)]
        public int BorderWidth
        {
            get
            {
                return (int)GetValue(_borderWidth);
            }
            set
            {
                SetValue(_borderWidth, value);
            }
        }

        private static void onBorderWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chart = (d as WindowsFormChart)?.wfchart;
            if (chart?.Series.Any() ?? false)
            {
                chart.Series[0].BorderWidth = (int)e.NewValue;
            }
        }
        #endregion

        #region "MarkerColorプロパティ"
        public static readonly DependencyProperty _markerColor
            = DependencyProperty.Register(nameof(MarkerColor), typeof(System.Drawing.Color), typeof(WindowsFormChart),
                new UIPropertyMetadata(new System.Drawing.Color(), new PropertyChangedCallback(onMarkerColorChanged)));

        [Bindable(true)]
        public System.Drawing.Color MarkerColor
        {
            get
            {
                return (System.Drawing.Color)GetValue(_markerColor);
            }
            set
            {
                SetValue(_markerColor, value);
            }
        }

        private static void onMarkerColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chart = (d as WindowsFormChart)?.wfchart;
            if (chart?.Series.Any() ?? false)
            {
                chart.Series[0].MarkerColor = (System.Drawing.Color)e.NewValue;
            }
        }
        #endregion

        #region "MarkerStyleプロパティ"
        public static readonly DependencyProperty _markerStyle
            = DependencyProperty.Register(nameof(MarkerStyle), typeof(MarkerStyle), typeof(WindowsFormChart),
                new UIPropertyMetadata(new MarkerStyle(), new PropertyChangedCallback(onMarkerStyleChanged)));

        [Bindable(true)]
        public MarkerStyle MarkerStyle
        {
            get
            {
                return (MarkerStyle)GetValue(_markerStyle);
            }
            set
            {
                SetValue(_markerStyle, value);
            }
        }

        private static void onMarkerStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chart = (d as WindowsFormChart)?.wfchart;
            if (chart?.Series.Any() ?? false)
            {
                chart.Series[0].MarkerStyle = (MarkerStyle)e.NewValue;
            }
        }
        #endregion

        #region "MarkerSizeプロパティ"
        public static readonly DependencyProperty _markerSize
            = DependencyProperty.Register(nameof(MarkerSize), typeof(int), typeof(WindowsFormChart),
                new UIPropertyMetadata(5, new PropertyChangedCallback(onMarkerSizeChanged)));

        [Bindable(true)]
        public int MarkerSize
        {
            get
            {
                return (int)GetValue(_markerSize);
            }
            set
            {
                SetValue(_markerSize, value);
            }
        }

        private static void onMarkerSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chart = (d as WindowsFormChart)?.wfchart;
            if (chart?.Series.Any() ?? false)
            {
                chart.Series[0].MarkerSize = (int)e.NewValue;
            }
        }
        #endregion

        #region "AxisXIsLogarithmicプロパティ"
        public static readonly DependencyProperty _axisXIsLogarithmic
            = DependencyProperty.Register(nameof(AxisXIsLogarithmic), typeof(bool), typeof(WindowsFormChart),
                new UIPropertyMetadata(false, new PropertyChangedCallback(onAxisXIsLogarithmicChanged)));

        [Bindable(true)]
        public bool AxisXIsLogarithmic
        {
            get
            {
                return (bool)GetValue(_axisXIsLogarithmic);
            }
            set
            {
                SetValue(_axisXIsLogarithmic, value);
            }
        }

        private static void onAxisXIsLogarithmicChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chart = (d as WindowsFormChart)?.wfchart;
            if (chart?.Series.Any() ?? false)
            {
                chart.ChartAreas[0].AxisX.IsLogarithmic = (bool)e.NewValue;
            }
        }
        #endregion

        #region "AxisYIsLogarithmicプロパティ"
        public static readonly DependencyProperty _axisYIsLogarithmic
            = DependencyProperty.Register(nameof(AxisYIsLogarithmic), typeof(bool), typeof(WindowsFormChart),
                new UIPropertyMetadata(false, new PropertyChangedCallback(onAxisYIsLogarithmicChanged)));

        [Bindable(true)]
        public bool AxisYIsLogarithmic
        {
            get
            {
                return (bool)GetValue(_axisYIsLogarithmic);
            }
            set
            {
                SetValue(_axisYIsLogarithmic, value);
            }
        }

        private static void onAxisYIsLogarithmicChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chart = (d as WindowsFormChart)?.wfchart;
            if (chart?.Series.Any() ?? false)
            {
                chart.ChartAreas[0].AxisY.IsLogarithmic = (bool)e.NewValue;
            }
        }
        #endregion

        #region "AxisXLabelStyleFormatプロパティ"
        public static readonly DependencyProperty _axisXLabelStyleFormat
            = DependencyProperty.Register(nameof(AxisXLabelStyleFormat), typeof(string), typeof(WindowsFormChart),
                new UIPropertyMetadata(null, new PropertyChangedCallback(onAxisXLabelStyleFormatChanged)));

        [Bindable(true)]
        public string AxisXLabelStyleFormat
        {
            get
            {
                return (string)GetValue(_axisXLabelStyleFormat);
            }
            set
            {
                SetValue(_axisXLabelStyleFormat, value);
            }
        }

        private static void onAxisXLabelStyleFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chart = (d as WindowsFormChart)?.wfchart;
            if (chart?.Series.Any() ?? false)
            {
                chart.ChartAreas[0].AxisX.LabelStyle.Format = (string)e.NewValue;
            }
        }
        #endregion

        #region "AxisYLabelStyleFormatプロパティ"
        public static readonly DependencyProperty _axisYLabelStyleFormat
            = DependencyProperty.Register(nameof(AxisYLabelStyleFormat), typeof(string), typeof(WindowsFormChart),
                new UIPropertyMetadata(null, new PropertyChangedCallback(onAxisYLabelStyleFormatChanged)));

        [Bindable(true)]
        public string AxisYLabelStyleFormat
        {
            get
            {
                return (string)GetValue(_axisYLabelStyleFormat);
            }
            set
            {
                SetValue(_axisYLabelStyleFormat, value);
            }
        }

        private static void onAxisYLabelStyleFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var chart = (d as WindowsFormChart)?.wfchart;
            if (chart?.Series.Any() ?? false)
            {
                chart.ChartAreas[0].AxisY.LabelStyle.Format = (string)e.NewValue;
            }
        }
        #endregion
    }
}
