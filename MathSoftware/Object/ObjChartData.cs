﻿using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathSoftware.Object
{
    public class ObjChartData
    {
        public SeriesCollection _seriesCollection { get; set; }
        public List<String> _lsRow { get; set; }
        public List<String[]> _lsColumn { get; set; }
        public string _chartTitle { get; set; }
        public int _titleSize { get; set; }
        public string _titleColor { get; set; }
        public string _chartType { get; set; }
        public string _verticalAxis { get; set; }
        public int _verticalSize { get; set; }
        public string _colorVerticalAxis { get; set; }
        public string _horizontalAxis { get; set; }
        public bool _showNote { get; set; }
        public bool _showData { get; set; }
        public string _titlePositon { get; set; }
        public string _noteUnitPosition { get; set; }
        public string _noteUnitChart { get; set; }

        public ObjChartData()
        {
            this._seriesCollection = new SeriesCollection();
            this._lsRow = new List<string>();
            this._lsColumn = new List<string[]>();
            this._chartTitle = "";
            this._titleSize = 15;
            this._titleColor = "#000000";
            this._chartType = "";
            this._verticalAxis = "";
            this._verticalSize = 15;
            this._colorVerticalAxis = "#000000";
            this._horizontalAxis = "";
            this._showNote = true;
            this._showData = true;
            this._titlePositon = "";
            this._noteUnitPosition = "";
            this._noteUnitChart = "";
        }
    }
}
