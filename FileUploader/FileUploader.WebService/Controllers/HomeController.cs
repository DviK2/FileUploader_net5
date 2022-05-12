﻿using FileUploader.WebService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FileUploader.Services;
using Microsoft.AspNetCore.Http;

namespace FileUploader.WebService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICsvParser _parser;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ICsvParser parser, ILogger<HomeController> logger)
        {
            _parser = parser;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult UploadFile(IFormFile? uploadedFile)
        {
            if (uploadedFile == null)
                return View("Index");

            var strings = ReadAsList(uploadedFile);
            var structures = _parser.ParseCsv(uploadedFile.OpenReadStream());

            ViewBag.Strings = strings;
            ViewBag.structures = structures;

            return View("Index");
        }

        public List<string> ReadAsList(IFormFile file)
        {
            var result = new List<string>();
            using var reader = new StreamReader(file.OpenReadStream());

            while (reader.Peek() >= 0)
            {
                result.Add(reader.ReadLine());
            }

            return result;
        }
    }
}
