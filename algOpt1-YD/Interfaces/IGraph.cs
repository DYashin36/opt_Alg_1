using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algOpt1_YD.Interfaces
{
    public abstract class IGraph
    {
        public List<IVertex> Vertices { get; set; } = new List<IVertex>();
        public List<IEdge> Edges { get; set; } = new List<IEdge>();
    }

    public abstract class IVertex
    {
        public string Label { get; set; }
        public string Name { get; set; }
        public Point Position { get; set; }
        public bool Visible { get; set; } = true;
        public int Radius { get; set; } = 65;
        public double Value { get; set; } = 0;

    }

    public abstract class IEdge
    {
        public IVertex From { get; set; }
        public IVertex To { get; set; }
        public double Weight { get; set; }
        public bool Visible => From.Visible && To.Visible;
    }
}
