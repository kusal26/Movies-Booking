using DotNetProject.dto;
using DotNetProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.Sockets;

namespace DotNetProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DotNetProjectContext _context;


        public HomeController(ILogger<HomeController> logger, DotNetProjectContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var result=_context.Movies.Include(x=>x.PricingDetails).ToList();
            return View(result);
        }
        public IActionResult Details(int? id, string time, string events)
        {
            if (id == null)
            { return NotFound(); }
            var result=_context.Movies.Include(x => x.PricingDetails).Include(y=>y.showTimings).FirstOrDefault(x=>x.MovieId==id);
            ViewData["EventTime"] = time;
            ViewData["EventName"] = events;
            return View(result);


        }
        
        public IActionResult Book(int id)
        {
            var result = _context.ShowTiming.Include(x => x.MovieHall).Include(x=>x.MovieHall.Timing).Include(y => y.Movies).Where(z => z.MovieId == id).ToList();
            return View(result);
        }

        public IActionResult Seat()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Seat (int? id,Ticket ticket)
        {
            var result = _context.ShowTiming.Include(x => x.Movies).Include(a => a.Movies.PricingDetails).Include(y => y.MovieHall).FirstOrDefault(x => x.TimingId == id);
            ViewBag.AvailableTicket = result?.available_seats;
            return View(ticket);
        }
        [HttpPost]
        public IActionResult Seat(int id,Ticket ticket)
        {
            var result = _context.ShowTiming.Include(x => x.Movies).Include(a=>a.Movies.PricingDetails).Include(y => y.MovieHall).FirstOrDefault(x=>x.TimingId==id);
            if(ticket.NoOfticket==0)
            {
                ModelState.AddModelError("NoOfticket", "Select Ticket first");
                ViewBag.AvailableTicket = result?.available_seats;


            }
            else
            {

            
           

                if (result != null)
                {
                    if (ticket.NoOfticket > result?.available_seats)
                    {
                        ModelState.AddModelError("NoOfticket", "Ticket UnAvailable");
                        ViewBag.AvailableTicket = result.available_seats;



                    }
                    else
                    {
                        var total = ticket.NoOfticket * result.Movies.PricingDetails.Price;
                        result.available_seats -= ticket.NoOfticket;
                        _context.ShowTiming.Update(result);
                        _context.SaveChanges();
                        var bookedModel = new Booking()
                        {
                            BookedDate = DateTime.Now,
                            NoOfBookedSeats = ticket.NoOfticket,
                            Timing_Id = id,
                            PricePerEach = result.Movies.PricingDetails.Price,
                            Totalprice = total,


                        };
                        _context.Booking.Add(bookedModel);
                        _context.SaveChanges();


                        return RedirectToAction("Cart", new { bookingId = bookedModel.Booking_Id});



                    }

                }

            }
            return View();
        }
        public IActionResult Cart(int? bookingId)
        {
         
        

            var result1 = _context.Booking.Include(x=>x.ShowTiming).Include(y=>y.ShowTiming.Movies).Include(y => y.ShowTiming.MovieHall).FirstOrDefault(x => x.Booking_Id == bookingId);

            return View(result1);
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
    }
}