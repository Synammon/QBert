using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QBert
{
    public class HighScoreList
    {
        public List<HighScore> HighScores { get; set; } = new List<HighScore>();

        public HighScoreList()
        {
            HighScores.Add(new HighScore() { Score = 10000, Name = "AAA" });
            HighScores.Add(new HighScore() { Score = 9000, Name = "BBB" });
            HighScores.Add(new HighScore() { Score = 8000, Name = "CCC" });
            HighScores.Add(new HighScore() { Score = 7000, Name = "DDD" });
            HighScores.Add(new HighScore() { Score = 6000, Name = "EEE" });
            HighScores.Add(new HighScore() { Score = 5000, Name = "FFF" });
            HighScores.Add(new HighScore() { Score = 4000, Name = "GGG" });
            HighScores.Add(new HighScore() { Score = 3000, Name = "HHH" });
            HighScores.Add(new HighScore() { Score = 2000, Name = "III" });
            HighScores.Add(new HighScore() { Score = 1000, Name = "JJJ" });

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
                IncludeFields = true
            };

            string json = JsonSerializer.Serialize(this, options);
            string fileName = "..\\..\\..\\Content\\HighScores.json";
            File.WriteAllText(fileName, json);
            fileName = "Content\\HighScores.json";
            File.WriteAllText(fileName, json);
        }
    }
}
