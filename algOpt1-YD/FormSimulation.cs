using algOpt1_YD.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace algOpt1_YD
{
    public partial class FormSimulation : Form
    {
        private IGraph graph;
        private int step = 0;
        private bool gridVisible = false;

        public FormSimulation(IGraph g)
        {
            InitializeComponent();
            graph = g;

            InitializeGrids();
            InitializeChartAndSeries();

            vertexValueGrid.DataError += (s, e) => e.ThrowException = false;
        }

        private void InitializeGrids()
        {
            vertexValueGrid.Visible = false;
            vertexValueGrid.Rows.Clear();
            vertexValueGrid.Columns.Clear();
            vertexValueGrid.ColumnCount = 2;
            vertexValueGrid.Columns[0].Name = "Vertex";
            vertexValueGrid.Columns[0].ReadOnly = true;
            vertexValueGrid.Columns[1].Name = "Value";
            vertexValueGrid.Columns[1].ValueType = typeof(double);

            vertexValueGrid.CellValueChanged += Grid_CellValueChanged;
            vertexValueGrid.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (vertexValueGrid.IsCurrentCellDirty)
                    vertexValueGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };

            foreach (var v in graph.Vertices)
            {
                vertexValueGrid.Rows.Add(v.Label, v.Value);
            }

        }

        private void InitializeChartAndSeries()
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add(new ChartArea("Default"));
            chart1.ChartAreas["Default"].AxisY.Title = "Значение вершины";
            chart1.ChartAreas["Default"].AxisX.Title = "Шаг симуляции";
            simBox.Items.Clear();

            foreach (var v in graph.Vertices)
            {
                var series = new Series(v.Label)
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 2,
                    Name = v.Label
                };
                chart1.Series.Add(series);

                // стартовая точка = текущее значение вершины
                series.Points.AddXY(0, v.Value);

                simBox.Items.Add(v.Label, true);
            }

            simBox.ItemCheck += simBox_ItemCheck;
        }

        private void simBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var vertexName = simBox.Items[e.Index].ToString();
            bool isChecked = e.NewValue == CheckState.Checked;
            if (chart1.Series.IndexOf(vertexName) >= 0)
                chart1.Series[vertexName].Enabled = isChecked;
        }

        private void Grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (sender == vertexValueGrid && e.ColumnIndex == 1)
            {
                var row = vertexValueGrid.Rows[e.RowIndex];
                string vertexName = row.Cells[0].Value?.ToString();
                if (!double.TryParse(row.Cells[1].Value?.ToString(), out double val)) return;
                var vertex = graph.Vertices.FirstOrDefault(v => v.Label == vertexName);
                if (vertex != null)
                    vertex.Value = val;
            }
        }


        private void ResetChart()
        {
            step = 0;
            foreach (var series in chart1.Series)
            {
                series.Points.Clear();
                var vertex = graph.Vertices.FirstOrDefault(v => v.Label == series.Name);
                if (vertex != null)
                    series.Points.AddXY(0, vertex.Value);
            }
        }

        private void btnStep_Click(object sender, EventArgs e)
        {
            if (graph.Vertices.Any(v => double.IsNaN(v.Value)))
            {
                MessageBox.Show("Назначьте значения для всех вершин перед выполнением шага.");
                return;
            }
            StepSimulation();
        }

        private Dictionary<IVertex, double> nextStepImpulses = new Dictionary<IVertex, double>();

        private void InitializeImpulses()
        {
            nextStepImpulses.Clear();
            foreach (var v in graph.Vertices)
            {
                // вершины из грида — источники первого шага
                nextStepImpulses[v] = v.Value;

                // НЕ обнуляем v.Value! Он показывает текущие накопленные значения
                // v.Value = 0; // <- убрать эту строку
            }
        }

        private void StepSimulation()
        {
            // буфер для импульсов на этот шаг
            var impulsesThisStep = new Dictionary<IVertex, double>();
            foreach (var v in graph.Vertices)
                impulsesThisStep[v] = 0;

            // из вершин, которые были источниками на этом шаге (nextStepImpulses),
            // отправляем импульсы по всем исходящим ребрам
            foreach (var e in graph.Edges)
            {
                if (nextStepImpulses.TryGetValue(e.From, out double fromValue) && fromValue != 0)
                {
                    impulsesThisStep[e.To] += fromValue * e.Weight;
                }
            }

            // обновляем значения вершин: суммируем поступившие импульсы
            foreach (var v in graph.Vertices)
            {
                v.Value += impulsesThisStep[v];
            }

            // на следующий шаг источниками будут те вершины, которые получили импульсы сейчас
            nextStepImpulses = new Dictionary<IVertex, double>(impulsesThisStep);

            step++;

            // добавляем точки на график
            foreach (var v in graph.Vertices)
            {
                chart1.Series[v.Label].Points.AddXY(step, v.Value);
            }
        }



        private void btnWeightsSet_Click(object sender, EventArgs e)
        {
            gridVisible = !gridVisible;
            vertexValueGrid.Visible = gridVisible;
            Width = gridVisible ? 928 : 677;
            btnWeightsSet.Text = gridVisible ? "<<" : ">>";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtSteps.Text, out int steps)) return;
            for (int i = 0; i < steps; i++)
                StepSimulation();
        }

        private void cleanBtn_Click(object sender, EventArgs e)
        {
            ResetChart();
        }

        private void FormSimulation_Load(object sender, EventArgs e)
        {

        }

        private void impulseGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void impulseGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void vertexValueGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void readyBtn_Click(object sender, EventArgs e)
        {

            ApplyGridValues();
            ResetChart();
            InitializeImpulses();
        }

        private void ApplyGridValues()
        {
            foreach (DataGridViewRow row in vertexValueGrid.Rows)
            {
                if (row.IsNewRow) continue;
                string vertexName = row.Cells[0].Value?.ToString();
                if (!double.TryParse(row.Cells[1].Value?.ToString(), out double val)) continue;
                var vertex = graph.Vertices.FirstOrDefault(v => v.Label == vertexName);
                if (vertex != null)
                    vertex.Value = val;
            }
        }
    }
}
