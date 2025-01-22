namespace novaTentativa.Models
{
    public class Cantor_Model
    {
        public string? Artist { get; set; }
        public string? Song { get; set; }
        public int DurationMs { get; set; }
        public bool Explicit { get; set; }
        public int Year { get; set; }
        public int Popularity { get; set; }
        public double Danceability { get; set; }
        public double Energy { get; set; }
        public int Key { get; set; }
        public double Loudness { get; set; }
        public int Mode { get; set; }
        public double Speechiness { get; set; }
        public double Acousticness { get; set; }
        public double Instrumentalness { get; set; }
        public double Liveness { get; set; }
        public double Valence { get; set; }
        public double Tempo { get; set; }
        public string? Genre { get; set; }
    }
}
