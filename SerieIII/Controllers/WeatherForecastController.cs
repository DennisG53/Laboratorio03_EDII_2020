﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerieIII;
using Huffman;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using LABREPO_ED2.Repository;

namespace SerieIII.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}

        public static IWebHostEnvironment _environment;

        public WeatherForecastController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        //object file
        public class FileUploadAPI
        {
            public IFormFile files { get; set; } //name use in postman --> files

        }
        public Huffman.Huffman hf = new Huffman.Huffman();

        private static readonly IFileComprassDataBase FDatabase = new FileCompressDataBase();

        // localhost:51626/weatherforecast/compress/Huffman/?Name=archivo
        [HttpPost("compress/Huffman", Name = "PostFile")]
        public async Task<string> Post(string Name, [FromForm] FileUploadAPI objFile)
        {
            try
            {
                if (objFile.files.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + objFile.files.FileName))
                    {
                        objFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        string name = objFile.files.FileName.ToString();
                        string nameNew = GetNameNew(Name, ".huff");
                        string NewPath = _environment.WebRootPath + "\\Upload\\" + name;
                        //implement algorhitm
                        //completed stack
                        string metrics = "";
                        hf.Compress(@NewPath, @nameNew);
                        metrics = hf.GetFilesMetrics("file", @NewPath, @nameNew);
                        string[] metrics_total = metrics.Split('|', StringSplitOptions.RemoveEmptyEntries);
                        float rc = float.Parse(metrics_total[1]);
                        float fc = float.Parse(metrics_total[2]);
                        float rp = float.Parse(metrics_total[3]);
                        string algorithm = "Huffman";
                        FDatabase.AddNewFile(@NewPath, @nameNew, rc, fc, rp, algorithm);
                        return "\\Upload\\" + objFile.files.FileName;

                    }
                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }

        // localhost:51626/weatherforecast/decompress/?Algorithm=archivo
        [HttpPost("decompress", Name = "PostFile3")]
        public async Task<string> Post(string Algorithm, int x, [FromForm] FileUploadAPI objFile)
        {
            try
            {
                if (objFile.files.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + objFile.files.FileName))
                    {
                        objFile.files.CopyTo(fileStream);
                        fileStream.Flush();
                        fileStream.Close();
                        string name = objFile.files.FileName.ToString();
                        string NewPath = _environment.WebRootPath + "\\Upload\\" + name;
                        if (Algorithm == "Huffman")
                        {
                            if (name.Contains(".huff"))
                            {
                                string namefile = GetFileName(name);
                                string wPath = "decompress" + namefile;
                                hf.uncompress(@NewPath, @wPath);
                                return "Uncompress successful huffman";
                            }
                            return "You need file .huff";
                        }
                        else
                        {
                            return "Algorithm incorrect";
                        }

                    }
                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }

        // localhost:51626/weatherforecast/compressions
        [HttpGet("compressions", Name = "GetFile")]
        /*[Route("weatherforecast/Sodas/")]*/
        public IEnumerable<FileCompress> Get()
        {
            return FDatabase.GetFiles();
        }

        //get new path for file compress
        private string GetNameNew(string name, string ext)
        {
            string newname = name + ext;
            return newname;
        }

        //get new path the decompress
        private string GetFileName(string name)
        {
            string res = "";
            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] != '.')
                {
                    res = res + name[i];
                }
                else if (name[i] == '.')
                {
                    res = res + name[i] + "txt";
                    i = name.Length;
                }
            }
            return res;
        }

    }
}
