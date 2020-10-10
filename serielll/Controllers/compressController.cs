using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace serielll.Controllers
{
    [Route("api/")]
    [ApiController]
    public class compressController : ControllerBase
    {
        public Huffman.Huffman hf = new Huffman.Huffman();


        // GET: api/compressions
        [HttpGet("compressions")]
        public String Get()
        {
            return "Listado";
        }


        [HttpPost("compress/{name}")]
        public IActionResult compress(string cadena)
        {
            hf.Compress("C:\\Users\\javier\\Desktop\\archivo1.huff", "C:\\Users\\javier\\Desktop\\archivo2.huff");
            
            return Ok();
        }

        [HttpPost("decompress")]
        public IActionResult decompress(string cadena)
        {
            hf.uncompress("C:\\Users\\javier\\Desktop\\archivo2.huff", "C:\\Users\\javier\\Desktop\\archivo3.huff");
      
            return Ok();
        }








        //public IActionResult Post([FromBody] Object peliculas)
        //{
        //        JArray a = JArray.Parse(peliculas.ToString());
        //        //MovieDataBase movieDB = new MovieDataBase();

        //        try
        //        {
        //            foreach (JObject item in a.Children())
        //            {
        //                //var test = Request.Body;
        //                Movie pelicula = new Movie();

        //                string releaseDate = item.GetValue("releaseDate").ToString();
        //                DateTime fecha = DateTime.Parse(releaseDate);
        //                pelicula.Year = fecha.Year;
        //                pelicula.Name = item.GetValue("title").ToString();
        //                pelicula.Directed_by = item.GetValue("director").ToString();
        //                pelicula.Stars = item.GetValue("imdbRating").ToString();
        //                pelicula.Genre = item.GetValue("genre").ToString();

        //                movieDB.AddNewMovie(item.GetValue("title").ToString(), pelicula);
        //            }
        //            //var pel= movieDB.GetMovie();
        //            //return pel.ToArray();

        //            return Ok();
        //        }
        //        catch (Exception)
        //        {
        //            return BadRequest("Error");
        //            throw;
        //        }
        //    }

        //}

        //[HttpGet("{recorrido}")]
        //public IEnumerable<Movie> Get(string recorrido)
        //{
        //    //MovieDataBase movieDB = new MovieDataBase();

        //    switch (recorrido)
        //    {
        //        case "inorden":
        //            var peliculas = movieDB.GetMovie();
        //            return peliculas.ToArray();
        //        case "preorden":
        //            peliculas = movieDB.GetMovie();
        //            return peliculas.ToArray();
        //        case "postorden":
        //            peliculas = movieDB.GetMovie();
        //            return peliculas.ToArray();
        //        default:
        //            peliculas = movieDB.GetMovie();
        //            return peliculas.ToArray();
        //    }
        //    //solo esta el recorrido inorden

    }
}
