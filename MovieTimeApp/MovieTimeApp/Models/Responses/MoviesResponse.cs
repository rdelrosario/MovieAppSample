using System.Collections.Generic;

namespace MovieTimeApp.Models.Responses
{
	public class MoviesResponse
    {
        public int Page { get; set; }
        public List<Movie> Results { get; set; }
    }
}

