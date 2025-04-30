using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QBert
{
    public class Levels
    {
        public List<LevelDef> LevelDefs { get; set; } = new List<LevelDef>();

        public Levels() 
        {
            LevelDefs.Add(new LevelDef()
            {
                WrapAround = false,
                Level = 1,
                Round = 1,
                TopColors = new List<Color>() { Color.Red, Color.Blue },
                LeftColor = Color.Yellow,
                RightColor = Color.Purple,
                Discs = new List<int>() { 10, 14 }
            });
            LevelDefs.Add(new LevelDef()
            {
                WrapAround = false,
                Level = 1,
                Round = 2,
                TopColors = new List<Color>() { Color.Cyan, Color.Magenta },
                LeftColor = Color.Gray,
                RightColor = Color.Orange,
                Discs = new List<int>() { 15, 20 }
            });
            LevelDefs.Add(new LevelDef()
            {
                WrapAround = false,
                Level = 1,
                Round = 3,
                TopColors = new List<Color>() { Color.Aqua, Color.Beige },
                LeftColor = Color.AliceBlue,
                RightColor = Color.MediumPurple,
                Discs = new List<int>() { 21, 27 }
            });
            LevelDefs.Add(new LevelDef()
            {
                WrapAround = false,
                Level = 1,
                Round = 4,
                TopColors = new List<Color>() { Color.OldLace, Color.Black },
                LeftColor = Color.DarkGray,
                RightColor = Color.LightGray
            });
            LevelDefs.Add(new LevelDef()
            {
                WrapAround = false,
                Level = 2,
                Round = 1,
                TopColors = new List<Color>() { Color.Cyan, Color.Magenta, Color.YellowGreen },
                LeftColor = Color.Turquoise,
                RightColor = Color.Coral
            });
            LevelDefs.Add(new LevelDef()
            {
                WrapAround = false,
                Level = 2,
                Round = 2,
                TopColors = new List<Color>() { Color.Chocolate, Color.BurlyWood, Color.Ivory },
                LeftColor = Color.Indigo,
                RightColor = Color.Honeydew
            });
            LevelDefs.Add(new LevelDef()
            {
                WrapAround = false,
                Level = 2,
                Round = 3,
                TopColors = new List<Color>() { Color.LemonChiffon, Color.PeachPuff, Color.PaleVioletRed },
                LeftColor = Color.Gray,
                RightColor = Color.Orange
            });
            LevelDefs.Add(new LevelDef()
            {
                WrapAround = false,
                Level = 2,
                Round = 4,
                TopColors = new List<Color>() { Color.SandyBrown, Color.SeaGreen, Color.SkyBlue },
                LeftColor = Color.Sienna,
                RightColor = Color.SteelBlue
            });
            LevelDefs.Add(new LevelDef()
            {
                WrapAround = true,
                Level = 3,
                Round = 1,
                TopColors = new List<Color>() { Color.Teal, Color.Tomato, Color.Olive },
                LeftColor = Color.Navy,
                RightColor = Color.Orange
            });
            LevelDefs.Add(new LevelDef()
            {
                WrapAround = true,
                Level = 3,
                Round = 2,
                TopColors = new List<Color>() { Color.SeaGreen, Color.BurlyWood, Color.RosyBrown },
                LeftColor = Color.SeaShell,
                RightColor = Color.SlateGray
            });
            LevelDefs.Add(new LevelDef()
            {
                WrapAround = true,
                Level = 3,
                Round = 3,
                TopColors = new List<Color>() { Color.OrangeRed, Color.OliveDrab, Color.LightSlateGray },
                LeftColor = Color.ForestGreen,
                RightColor = Color.LightSeaGreen
            });
            LevelDefs.Add(new LevelDef()
            {
                WrapAround = true,
                Level = 3,
                Round = 4,
                TopColors = new List<Color>() { Color.MediumTurquoise, Color.MidnightBlue, Color.SkyBlue },
                LeftColor = Color.MistyRose,
                RightColor = Color.Moccasin
            });

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
                IncludeFields = true
            };

            string json = JsonSerializer.Serialize(this, options);
            string fileName = "..\\..\\..\\Content\\Levels.json";
            File.WriteAllText(fileName, json);
            fileName = "Content\\Levels.json";
            File.WriteAllText(fileName, json);
        }
    }
}
