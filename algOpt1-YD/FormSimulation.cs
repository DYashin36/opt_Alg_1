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
        }

        private void InitializeGrids()
        {
            // Вершины
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

            // Связи
            impulseGrid.Columns.Clear();
            impulseGrid.ColumnCount = 3;
            impulseGrid.Columns[0].Name = "From";
            impulseGrid.Columns[0].ReadOnly = true;
            impulseGrid.Columns[1].Name = "To";
            impulseGrid.Columns[1].ReadOnly = true;
            impulseGrid.Columns[2].Name = "Impulse";
            impulseGrid.Columns[2].ValueType = typeof(double);

            impulseGrid.CellValueChanged += Grid_CellValueChanged;
            impulseGrid.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (impulseGrid.IsCurrentCellDirty)
                    impulseGrid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };

            // Инициализация значений 0
            foreach (var v in graph.Vertices)
                vertexValueGrid.Rows.Add(v.Label, 0.0);
            foreach (var e in graph.Edges)
                impulseGrid.Rows.Add(e.From.Label, e.To.Label, 0.0);
        }

        private void InitializeChartAndSeries()
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add(new ChartArea("Default"));
            simBox.Items.Clear();

            foreach (var v in graph.Vertices)
            {
                v.Value = 0;

                var series = new Series(v.Label)
                {
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 2,
                    Name = v.Label
                };
                chart1.Series.Add(series);
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
            if(e.RowIndex < 0) return;

            // Если изменились значения, не трогаем серию, просто обновляем вершину или связь
            if (sender == vertexValueGrid && e.ColumnIndex == 1)
            {
                var row = vertexValueGrid.Rows[e.RowIndex];
                string vertexName = row.Cells[0].Value?.ToString();
                if (!double.TryParse(row.Cells[1].Value?.ToString(), out double val)) return;
                var vertex = graph.Vertices.FirstOrDefault(v => v.Label == vertexName);
                if (vertex != null)
                    vertex.Value = val;
            }
            else if (sender == impulseGrid && e.ColumnIndex == 2)
            {
                var row = impulseGrid.Rows[e.RowIndex];
                string fromName = row.Cells[0].Value?.ToString();
                string toName = row.Cells[1].Value?.ToString();
                if (!double.TryParse(row.Cells[2].Value?.ToString(), out double val)) return;
                var edge = graph.Edges.FirstOrDefault(ed => ed.From.Label == fromName && ed.To.Label == toName);
                if (edge != null)
                    edge.Weight = val;
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

        private void StepSimulation()
        {
            var newValues = new Dictionary<IVertex, double>();

            foreach (var v in graph.Vertices)
            {
                if (!v.Visible) continue;

                double sum = 0;
                foreach (var e in graph.Edges)
                    if (e.To == v && e.Visible)
                        sum += e.From.Value * e.Weight;

                sum += GetImpulseForVertex(v);

                newValues[v] = sum;
            }

            foreach (var v in newValues.Keys)
                v.Value = newValues[v];

            step++;

            foreach (var v in graph.Vertices)
                chart1.Series[v.Label].Points.AddXY(step, v.Value);
        }

        private void btnWeightsSet_Click(object sender, EventArgs e)
        {
            gridVisible = !gridVisible;
            vertexValueGrid.Visible = gridVisible;
            impulseGrid.Visible = gridVisible;
            Width = gridVisible ? 1278 : 677;
        }

        private double GetImpulseForVertex(IVertex v)
        {
            double sum = 0.0;
            foreach (DataGridViewRow row in impulseGrid.Rows)
            {
                if (row.IsNewRow) continue;
                string fromName = row.Cells[0].Value?.ToString();
                string toName = row.Cells[1].Value?.ToString();
                if (toName == v.Label && double.TryParse(row.Cells[2].Value?.ToString(), out double w))
                {
                    var fromVertex = graph.Vertices.FirstOrDefault(x => x.Label == fromName);
                    if (fromVertex != null)
                        sum += fromVertex.Value * w;
                }
            }
            return sum;
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
        }

        private void ApplyGridValues()
        {
            // Применяем значения вершин
            foreach (DataGridViewRow row in vertexValueGrid.Rows)
            {
                if (row.IsNewRow) continue;
                string vertexName = row.Cells[0].Value?.ToString();
                if (!double.TryParse(row.Cells[1].Value?.ToString(), out double val)) continue;
                var vertex = graph.Vertices.FirstOrDefault(v => v.Label == vertexName);
                if (vertex != null)
                    vertex.Value = val;
            }

            // Применяем значения связей
            foreach (DataGridViewRow row in impulseGrid.Rows)
            {
                if (row.IsNewRow) continue;
                string fromName = row.Cells[0].Value?.ToString();
                string toName = row.Cells[1].Value?.ToString();
                if (!double.TryParse(row.Cells[2].Value?.ToString(), out double val)) continue;
                var edge = graph.Edges.FirstOrDefault(ed => ed.From.Label == fromName && ed.To.Label == toName);
                if (edge != null)
                    edge.Weight = val;
            }
        }
    }
}
