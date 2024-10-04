using MovieApp.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service
{
    internal class SerializerDeserializer
    {
        private readonly string _filePath;
        public SerializerDeserializer()
        {
            _filePath = ConfigurationManager.AppSettings["myFilePath"];
        }    
         
        //this method loads list of movies from a json file
        public List<Movie> LoadMovies()
        {
            //check if the movie file exists if not return empty list
            if (!File.Exists(_filePath))
            {
                return new List<Movie>();
            }

            //open file for reading
            using (StreamReader reader = new StreamReader(_filePath))
            {
                //read all text from the file into a string
                string json = reader.ReadToEnd();

                //convert json text back into list of movie objects 
                //if fails return empty list instead
                return JsonConvert.DeserializeObject<List<Movie>>(json) ?? new List<Movie>();
            }
        }

        //this method saves list of movies to a json file
        public void SaveMovies(List<Movie> movies)
        {
            //open file for writing this will erase any existing content
            using (StreamWriter writer = new StreamWriter(_filePath, false))
            {
                //convert list of movies into a json string making it pretty easy to read
                string json = JsonConvert.SerializeObject(movies, Newtonsoft.Json.Formatting.Indented);
                writer.Write(json);
            }
        }

        //or

        /*

        //this method loads list of movies from a json file
        public List<Movie> LoadMovies()
        {
            //check if the movie file exists if not return empty list
            if (File.Exists(_filePath))
            {
                //if exists read all the text from the file into a string called "json"
                string json = File.ReadAllText(_filePath);

                //convert json text back into list of movie objects 
                //if fails return empty list instead
                return JsonConvert.DeserializeObject<List<Movie>>(json) ?? new List<Movie>();
            }
            return new List<Movie>();
        }

        //this method saves list of movies to a json file
        public void SaveMovies(List<Movie> movies)
        {
            //convert list of movies into a json string making it pretty easy to read
            string json = JsonConvert.SerializeObject(movies, Newtonsoft.Json.Formatting.Indented);

            //we write this json string to the file
            File.WriteAllText(_filePath, json);
        }
        
        */

    }
}
