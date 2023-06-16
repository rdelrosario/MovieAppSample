using System.Collections.Generic;
using Newtonsoft.Json;

namespace MovieTimeApp.Models
{
	public class Movie
    {
        public bool Adult { get; set; }

        [JsonProperty("genre_ids")]
        public List<int> GenreIds { get; set; }

        public int Id { get; set; }
        public string Overview { get; set; }
        public double Popularity { get; set; }

        [JsonProperty("Poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("Release_date")]
        public string ReleaseDate { get; set; }

        public string Title { get; set; }
        public bool Video { get; set; }

        [JsonProperty("Vote_average")]
        public double VoteAverage { get; set; }

        [JsonProperty("Vote_count")]
        public int VoteCount { get; set; }
    }
}

