using MovieApp.Model;
using MovieApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Controller
{
    internal class MovieController
    {
        private readonly SerializerDeserializer _movieFileService;
        private const int MaxMovies = 5;

        public MovieController(SerializerDeserializer movieFileService)
        {
            _movieFileService = movieFileService;
        }

        public List<Movie> LoadMovies()
        {
            return _movieFileService.LoadMovies();
        }

        public void SaveMovies(List<Movie> movies)
        {
            _movieFileService.SaveMovies(movies);
        }

        public bool AddMovie(List<Movie> movies, Movie movie)
        {
            if (movies.Count < MaxMovies)
            {
                movies.Add(movie);
                SaveMovies(movies);
                return true;
            }
            return false;
        }

        public void ClearMovies(List<Movie> movies)
        {
            movies.Clear();
            SaveMovies(movies);
        }

        public bool IsMovieListFull(List<Movie> movies)
        {
            return movies.Count >= MaxMovies;
        }
    }
}
