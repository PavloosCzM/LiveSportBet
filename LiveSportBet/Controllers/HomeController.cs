using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LiveSportBet.Models;

namespace LiveSportBet.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            MatchModel[] matches = MatchManager.GetInstance().getMatches();
            string[][] data = new string[matches.Length][];
            for (int i = 0; i < matches.Length; i++)
            {
                data[i] = matches[i].GetData();
            }
            ViewBag.matches = data;
            return View();
        }
    }
}
