using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBert
{
    public class LevelDef
    {
        public int Level { get; set; }
        public int Round { get; set; }
        public List<Color> TopColors { get; set; } = new List<Color>();
        public Color LeftColor { get; set; }
        public Color RightColor { get; set; }
        public bool WrapAround { get; set; } = false;
        public List<int> Discs { get; set; } = new List<int>();

        public LevelDef() 
        { 
        }
    }
}
