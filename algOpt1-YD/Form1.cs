using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using algOpt1_YD.Interfaces;
using MathNet.Numerics.LinearAlgebra;

namespace algOpt1_YD
{
    public partial class Form1 : Form
    {
        private Graph graph;
        private ToolTip vertexToolTip = new ToolTip();
        private static int vertexCounter = 0;

        public Form1()
        {
            InitializeComponent();
            graph = new Graph();
            this.DoubleBuffered = true;
            graphPanel.MouseMove += graphPanel_MouseMove;

            graphPanel.Paint += graphPanel_Paint;
            vertexList.ItemCheck += vertexList_ItemCheck;
        }

        private void UpdateUI()
        {
            vertexList.Items.Clear();
            foreach (var v in graph.Vertices)
            {
                vertexList.Items.Add(v.Label, v.Visible);
            }

            // Реальные название (тулТипы) лежат в лейблах, поэтому в комбоБоксы пишем 
            //comboFrom.Items.Clear();
            //comboTo.Items.Clear();
            foreach (var v in graph.Vertices)
            {
                if (!comboFrom.Items.Contains(v.Label)) comboFrom.Items.Add(v.Label);
                if (!comboTo.Items.Contains(v.Label)) comboTo.Items.Add(v.Label);
            }
            

            graphPanel.Invalidate();
        }
        /// <summary>
        /// Вспомогатлеьный класс для сериализации графа
        /// </summary>
        [Serializable]
        public class GraphData
        {
            public List<VertexData> Vertices { get; set; } = new List<VertexData>();
            public List<EdgeData> Edges { get; set; } = new List<EdgeData>();
        }
        /// <summary>
        /// Вспомогательный класс для сериализации вершины
        /// </summary>
        [Serializable]
        public class VertexData
        {
            public string Name { get; set; }
            public string Label { get; set; }
            public Point Position { get; set; }
            public bool Visible { get; set; }
            public int Radius { get; set; }
        }
        /// <summary>
        /// Вспомогательный класс для сериализации связи
        /// </summary>
        [Serializable]
        public class EdgeData
        {
            public string From { get; set; }
            public string To { get; set; }
            public double Weight { get; set; }
        }

        public class Vertex : IVertex 
        {
            public Vertex(string name, Point pos)
            {
                Name = name;
                Position = pos;
            }
        }

        public class Edge : IEdge
        {
            public Edge(IVertex from, IVertex to, double weight)
            {
                From = from;
                To = to;
                Weight = weight;
            }
        }
        
        public class Graph : IGraph
        {
            /// <summary>
            /// Каждая вершина имеет реальное имя (Лейбл) и букву-обозначение. Буква генерируется методом
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            private string GenerateSystemName(int index)
            {
                string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                int n = alphabet.Length;
                string result = "";
                int i = index;

                do
                {
                    result = alphabet[i % n] + result;
                    i = i / n - 1;
                } while (i >= 0);
                

                return result;
            }

            public void AddVertex(string name, Point pos)
            {
                if (Vertices.Any(vert => vert.Label == name))
                    return;

                string sysName = GenerateSystemName(vertexCounter++);
                var v = new Vertex(sysName, pos) { Label = name };
                v.Radius = 10;
                Vertices.Add(v);
            }
            /// <summary>
            /// Размещение по кругу
            /// </summary>
            /// <param name="panelSize"></param>
            public void ArrangeCircle(Size panelSize)
            {
                var verts = Vertices.Where(v => v.Visible).ToList();
                int n = verts.Count;
                if (n == 0) return;

                int centerX = panelSize.Width / 2;
                int centerY = panelSize.Height / 2;

                int maxRadius = verts.Select(v => v.Radius).DefaultIfEmpty(20).Max();
                int padding = maxRadius + 20;

                double radius = Math.Min(centerX, centerY) - padding;

                for (int i = 0; i < n; i++)
                {
                    double angle = 2 * Math.PI * i / n;
                    int x = centerX + (int)(radius * Math.Cos(angle));
                    int y = centerY + (int)(radius * Math.Sin(angle));
                    verts[i].Position = new Point(x, y);
                }
            }
            public void RemoveVertex(string name)
            {
                var v = Vertices.FirstOrDefault(x => x.Name == name);
                if (v != null)
                {
                    Vertices.Remove(v);
                    Edges.RemoveAll(e => e.From == v || e.To == v);
                }
            }

            public void SetVisibility(string name, bool visible)
            {
                var v = Vertices.FirstOrDefault(x => x.Name == name);
                if (v != null) v.Visible = visible;
            }

            public void AddEdge(string from, string to, double weight)
            {
                var v1 = Vertices.FirstOrDefault(v => v.Name == from);
                var v2 = Vertices.FirstOrDefault(v => v.Name == to);
                if (v1 == null || v2 == null) return;

                // запрет петли
                if (v1 == v2) return;

                // запрет обратного ребра
                if (Edges.Any(e => e.From == v2 && e.To == v1)) return;

                // запрет дублирования прямого ребра
                if (Edges.Any(e => e.From == v1 && e.To == v2)) return;

                Edges.Add(new Edge(v1, v2, weight));
            }


            public void RemoveEdge(string from, string to)
            {
                var v1 = Vertices.FirstOrDefault(v => v.Name == from);
                var v2 = Vertices.FirstOrDefault(v => v.Name == to);
                if (v1 != null && v2 != null)
                {
                    Edges.RemoveAll(e => e.From == v1 && e.To == v2);
                }
            }

            public void Draw(Graphics g)
            {
                foreach (var e in Edges)
                {
                    if (!e.Visible) continue;

                    using (Pen pen = new Pen(Color.Black, 2))
                    {
                        if (e.Weight < 0)
                        {
                            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                        }

                        // направление
                        var dir = new PointF(e.To.Position.X - e.From.Position.X, e.To.Position.Y - e.From.Position.Y);
                        float len = (float)Math.Sqrt(dir.X * dir.X + dir.Y * dir.Y);
                        if (len > 0)
                        {
                            dir.X /= len;
                            dir.Y /= len;
                        }

                        var start = new PointF(
                            e.From.Position.X + dir.X * (e.From.Radius),
                            e.From.Position.Y + dir.Y * (e.From.Radius));
                        var end = new PointF(
                            e.To.Position.X - dir.X * (e.To.Radius),
                            e.To.Position.Y - dir.Y * (e.To.Radius));

                        // основная линия
                        g.DrawLine(pen, start, end);

                        // рисуем стрелку "вручную"
                        const float arrowSize = 12f;
                        float angle = (float)Math.Atan2(dir.Y, dir.X);

                        PointF p1 = new PointF(
                            end.X - arrowSize * (float)Math.Cos(angle - Math.PI / 6),
                            end.Y - arrowSize * (float)Math.Sin(angle - Math.PI / 6));
                        PointF p2 = new PointF(
                            end.X - arrowSize * (float)Math.Cos(angle + Math.PI / 6),
                            end.Y - arrowSize * (float)Math.Sin(angle + Math.PI / 6));

                        g.FillPolygon(Brushes.Black, new PointF[] { end, p1, p2 });

                        // вес посередине
                        var mid = new PointF((start.X + end.X) / 2, (start.Y + end.Y) / 2);
                        g.DrawString(e.Weight.ToString("0.00"), SystemFonts.DefaultFont, Brushes.Red, mid);
                    }
                }

                foreach (var v in Vertices)
                {
                    if (!v.Visible) continue;

                    g.FillEllipse(Brushes.Coral,
                        v.Position.X - v.Radius, v.Position.Y - v.Radius,
                        v.Radius * 2, v.Radius * 2);
                    g.DrawEllipse(Pens.Black,
                        v.Position.X - v.Radius, v.Position.Y - v.Radius,
                        v.Radius * 2, v.Radius * 2);

                    var size = g.MeasureString(v.Name, SystemFonts.DefaultFont);
                    g.DrawString(v.Name, SystemFonts.DefaultFont, Brushes.Black,
                        v.Position.X - size.Width / 2,
                        v.Position.Y - size.Height / 2);
                }
            }

            public List<List<string>> FindCycles()
            {
                var cycles = new List<List<string>>();
                var visited = new HashSet<string>();

                foreach (var start in Vertices.Where(v => v.Visible))
                {
                    DFS(start, start, new List<IVertex>(), cycles, visited);
                }

                var unique = new List<List<string>>();
                var seen = new HashSet<string>();

                foreach (var c in cycles)
                {
                    if (c.All(name => Vertices.First(v => v.Name == name).Visible))
                    {
                        var norm = NormalizeCycle(c);
                        if (seen.Add(norm))
                            unique.Add(c);
                    }
                }

                return unique;
            }

            private void DFS(IVertex current, IVertex start, List<IVertex> path,
                             List<List<string>> cycles, HashSet<string> visited)
            {
                if (!current.Visible) return;

                path.Add(current);

                foreach (var e in Edges)
                {
                    if (!e.From.Visible || !e.To.Visible)
                        continue;

                    if (e.From == current)
                    {
                        if (e.To == start && path.Count > 1)
                        {
                            if (path.All(v => v.Visible))
                                cycles.Add(path.Select(v => v.Name).ToList());
                        }
                        else if (!path.Contains(e.To))
                        {
                            DFS(e.To, start, new List<IVertex>(path), cycles, visited);
                        }
                    }
                }
            }

            /// нормализация списка вершин цикла в строку,
            /// чтобы одинаковые циклы в разном порядке не дублировались
            private string NormalizeCycle(List<string> cycle)
            {
                int n = cycle.Count;
                int minIndex = 0;
                for (int i = 1; i < n; i++)
                {
                    if (string.Compare(cycle[i], cycle[minIndex], StringComparison.Ordinal) < 0)
                        minIndex = i;
                }

                var rotated = cycle.Skip(minIndex).Concat(cycle.Take(minIndex));
                return string.Join("->", rotated);
            }

            public double CycleWeight(List<string> cycle)
            {
                double product = 1;
                for (int i = 0; i < cycle.Count; i++)
                {
                    var from = Vertices.First(v => v.Name == cycle[i]);
                    var to = Vertices.First(v => v.Name == cycle[(i + 1) % cycle.Count]);
                    var edge = Edges.First(e => e.From == from && e.To == to);
                    product *= edge.Weight;
                }
                return product;
            }

            public void SaveToFile(string filename)
            {
                var data = new GraphData();

                foreach (var v in Vertices)
                {
                    data.Vertices.Add(new VertexData
                    {
                        Label = v.Label,
                        Name = v.Name,
                        Position = v.Position,
                        Visible = v.Visible,
                        Radius = v.Radius
                    }) ;
                }

                foreach (var e in Edges)
                {
                    data.Edges.Add(new EdgeData
                    {
                        From = e.From.Name,
                        To = e.To.Name,
                        Weight = e.Weight
                    });
                }

                using (FileStream fs = new FileStream(filename, FileMode.Create))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, data);
                }
            }

            public void LoadFromFile(string filename)
            {
                if (!File.Exists(filename)) return;

                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    var data = (GraphData)bf.Deserialize(fs);

                    Vertices.Clear();
                    Edges.Clear();

                    foreach (var vd in data.Vertices)
                    {
                        var v = new Vertex(vd.Name, vd.Position)
                        {
                            Label = vd.Label,
                            Visible = vd.Visible,
                            Radius = vd.Radius
                        };
                        Vertices.Add(v);
                    }

                    foreach (var ed in data.Edges)
                    {
                        var v1 = Vertices.FirstOrDefault(v => v.Name == ed.From);
                        var v2 = Vertices.FirstOrDefault(v => v.Name == ed.To);
                        if (v1 != null && v2 != null)
                        {
                            Edges.Add(new Edge(v1, v2, ed.Weight));
                        }
                    }
                }
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnRemoveEdge_Click(object sender, EventArgs e)
        {
            if (comboFrom.SelectedItem == null || comboTo.SelectedItem == null) return;

            string fromLabel = comboFrom.SelectedItem.ToString();
            string toLabel = comboTo.SelectedItem.ToString();

            var fromVertex = graph.Vertices.FirstOrDefault(v => v.Label == fromLabel);
            var toVertex = graph.Vertices.FirstOrDefault(v => v.Label == toLabel);
            if (fromVertex == null || toVertex == null) return;

            graph.RemoveEdge(fromVertex.Name, toVertex.Name);
            UpdateUI();
        }

        private void btnAddEdge_Click(object sender, EventArgs e)
        {
            if (comboFrom.SelectedItem == null || comboTo.SelectedItem == null) return;

            string fromLabel = comboFrom.SelectedItem.ToString();
            string toLabel = comboTo.SelectedItem.ToString();

            var fromVertex = graph.Vertices.FirstOrDefault(v => v.Label == fromLabel);
            var toVertex = graph.Vertices.FirstOrDefault(v => v.Label == toLabel);
            if (fromVertex == null || toVertex == null) return;

            if (!double.TryParse(txtWeight.Text, out double w)) return;
            if (w < -1 || w > 1)
            {
                MessageBox.Show("Вес должен быть в диапазоне [-1;1]");
                return;
            }

            graph.AddEdge(fromVertex.Name, toVertex.Name, w);
            UpdateUI();

            // очистка выбора
            comboFrom.SelectedIndex = -1;
            comboTo.SelectedIndex = -1;
            txtWeight.Clear();
        }

        private void btnRemoveVertex_Click(object sender, EventArgs e)
        {
            if (vertexList.SelectedItem == null) return;

            string label = vertexList.SelectedItem.ToString();
            var v = graph.Vertices.FirstOrDefault(x => x.Label == label);
            if (v != null)
            {
                graph.RemoveVertex(v.Name);
                graph.ArrangeCircle(graphPanel.Size);
                UpdateUI();
            }
        }

        private void btnAddVertex_Click(object sender, EventArgs e)
        {
            string name = txtVertexName.Text.Trim();
            if (string.IsNullOrEmpty(name)) return;

            // позиция больше не нужна
            graph.AddVertex(name, Point.Empty);

            // автораскладка по окружности
            graph.ArrangeCircle(graphPanel.Size);

            UpdateUI();
        }

        private void btnShowCycles_Click(object sender, EventArgs e)
        {
            int countMinus = 0;
            int countPlus = 0;
            var cycles = graph.FindCycles();
            txtCycles.Clear();

            foreach (var cycle in cycles)
            {
                double w = graph.CycleWeight(cycle);
                string sign = w < 0 ? "-" : "+";
                if (sign == "-") countMinus++;
                else countPlus++;
                string cycleText = string.Join(" -> ", cycle) + " -> " + cycle[0];

                txtCycles.AppendText($"[{cycleText}]; Знак: {sign}\r\n");
            }
            txtCycles.AppendText("Число '-' циклов: " + countMinus + "; Число '+' циклов: " + countPlus);

            // проверка устойчивости
            int negativeCount = cycles.Count(c => graph.CycleWeight(c) < 0);
            if (negativeCount % 2 == 1)
                MessageBox.Show("Граф устойчивый (нечётное число отрицательных циклов).");
            else
                MessageBox.Show("Граф неустойчивый.");

        }

        private void vertexList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void vertexList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var label = vertexList.Items[e.Index].ToString();
            var v = graph.Vertices.FirstOrDefault(x => x.Label == label);
            if (v != null)
                graph.SetVisibility(v.Name, e.NewValue == CheckState.Checked);

            graphPanel.Invalidate();
        }

        private void graphPanel_Paint(object sender, PaintEventArgs e)
        {
            graph.Draw(e.Graphics);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Graph files (*.bin)|*.bin";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                graph.SaveToFile(sfd.FileName);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Graph files (*.bin)|*.bin";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                graph.LoadFromFile(ofd.FileName);
                graphPanel.Invalidate();
                UpdateUI();
            }
        }

        private void btnSimulation_Click(object sender, EventArgs e)
        {
            var simForm = new FormSimulation(graph);
            simForm.Show();
        }

        private void txtVertexName_TextChanged(object sender, EventArgs e)
        {

        }

        private void graphPanel_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (var v in graph.Vertices)
            {
                if (!v.Visible) continue;
                double dx = e.X - v.Position.X;
                double dy = e.Y - v.Position.Y;
                if (dx * dx + dy * dy <= v.Radius * v.Radius)
                {
                    vertexToolTip.SetToolTip(graphPanel, v.Label);
                    return;
                }
            }
            vertexToolTip.SetToolTip(graphPanel, "");
        }

        private void matrixBtn_Click(object sender, EventArgs e)
        {
            //if (IsStableByPerturbations(out double radius))
            //{
            //    MessageBox.Show($"Система устойчива по возмущениям. " +
            //                    $"|M| = {radius:0.###} < 1");
            //}
            //else
            //{
            //    MessageBox.Show($"Система НЕустойчива по возмущениям. " +
            //                    $"|M| = {radius:0.###}");
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            graph.Vertices = new List<IVertex>();
            graph.Edges = new List<IEdge>();
            UpdateUI();
        }

        //public bool IsStableByPerturbations(out double spectralRadius)
        //{
        //    int n = graph.Vertices.Count;
        //    if (n == 0)
        //    {
        //        spectralRadius = 0;
        //        return true;
        //    }

        //    // формируем матрицу смежности
        //    var matrix = Matrix<double>.Build.Dense(n, n, 0);
        //    foreach (var e in graph.Edges.Where(ed => ed.Visible))
        //    {
        //        int fromIndex = graph.Vertices.IndexOf((Vertex)e.From);
        //        int toIndex = graph.Vertices.IndexOf((Vertex)e.To);
        //        matrix[fromIndex, toIndex] = e.Weight;
        //    }

        //    // находим собственные значения
        //    var evd = matrix.Evd();
        //    var eigenValues = evd.EigenValues;

        //    // спектральный радиус
        //    spectralRadius = eigenValues.Select(ev => ev.Magnitude).Max();

        //    // критерий устойчивости
        //    return spectralRadius < 1.0;
        //}
    }

}
