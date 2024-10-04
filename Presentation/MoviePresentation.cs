using MovieApp.Controller;
using MovieApp.Model;
using MovieApp.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Presentation
{
    internal class MoviePresentation
    {
        private static MovieController _movieController;

        public static void StartMovieApp()
        {
            SerializerDeserializer movieFileService = new SerializerDeserializer();
            _movieController = new MovieController(movieFileService);

            List<Movie> movies = _movieController.LoadMovies();
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\nmovie app menu =>");
                Console.WriteLine("1) display movies");
                Console.WriteLine("2) add movie");
                Console.WriteLine("3) clear all movies");
                Console.WriteLine("4) exit");
                Console.Write("enter your choice => ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayMovies(movies);
                        break;
                    case "2":
                        AddNewMovie(movies);
                        break;
                    case "3":
                        ClearAllMovies(movies);
                        break;
                    case "4":
                        exit = true;
                        Console.WriteLine("exiting..");
                        break;
                    default:
                        Console.WriteLine("invalid choice try again");
                        break;
                }
            }
        }

        public static void DisplayMovies(List<Movie> movies)
        {
            if (movies.Count == 0)
            {
                Console.WriteLine("no movies found");
                return;
            }

            Console.WriteLine("movies list => ");
            foreach (var movie in movies)
            {
                if (movie != null)
                {
                    Console.WriteLine($"Id: {movie.MovieId}, Name: {movie.Name}, Genre: {movie.Genre}, Year: {movie.Year}");
                }
            }
        }

        public static void AddNewMovie(List<Movie> movies)
        {
            if (_movieController.IsMovieListFull(movies))
            {
                Console.WriteLine("movie limit reached only 5 movies allowed");
                return;
            }

            Console.Write("enter movie ID => ");
            int id = Convert.ToInt32(Console.ReadLine());

            Console.Write("enter movie name => ");
            string name = Console.ReadLine();

            Console.Write("enter genre of movie genre means (action,drama,horror) => ");
            string genre = Console.ReadLine();

            Console.Write("enter year => ");
            int year = Convert.ToInt32(Console.ReadLine());

            Movie newMovie = new Movie(id, name, genre, year);
            bool isAdded = _movieController.AddMovie(movies, newMovie);

            if (isAdded)
            {
                Console.WriteLine("movie added successfully");
            }
            else
            {
                Console.WriteLine("failed to add movie and movie limit reached");
            }
        }

        public static void ClearAllMovies(List<Movie> movies)
        {
            _movieController.ClearMovies(movies);
            Console.WriteLine("all movies cleared");
        }
    }
}
