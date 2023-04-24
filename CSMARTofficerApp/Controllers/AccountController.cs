//using AspNetCore;
using CSMARTofficerApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CSMARTofficerApp.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        csmartContext db = new csmartContext();
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();

        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(OfficerLoginCred officer)
        {

            /*var off = db.OfficerLoginCreds.Where(x => x.UserId == officer.UserId && x.Password == officer.Password).Count();
            if (off > 0)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Message ="Oops! Wrong credentials. Try again.";
                return View();
            }*/

            if (ModelState.IsValid)
            {
                var off = db.OfficerLoginCreds.Where(x => x.UserId == officer.UserId).SingleOrDefault();
                if (off != null)
                {
                    bool isValid = (off.UserId == officer.UserId && off.Password == officer.Password);
                    if (isValid)
                    {
                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, officer.UserId) },
                            CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,principal);
                        HttpContext.Session.SetString("UserId", officer.UserId);
                        return RedirectToAction("Dashboard", "Account");
                    }
                    else
                    {
                        ViewBag.Message = "Invalid Password!";
                        //TempData["errorMessage"] = "Invalid Password!";
                        return View(officer);
                    }
                }
                else
                {
                    ViewBag.Message = "Username not found!";
                    //TempData["errorMessage"] = "Username not found!";
                    return View(officer);
                }
            }
            else
            {
                return View(officer);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var storedCookies = Request.Cookies.Keys;
            foreach(var cookies in storedCookies)
            {
                Response.Cookies.Delete(cookies);
            }
            return RedirectToAction("Login", "Account");
        }

        public IActionResult OfficerData(string searchBy, string search)
        {
            //var res = db.Officers.ToList();
            //return View(res);

            if (searchBy == "OfficerKey")
            {
                return View(db.Officers.Where(x=>x.OfficerKey.StartsWith(search) || search==null).ToList());
            }
            else if (searchBy == "FirstName")
            {
                return View(db.Officers.Where(x => x.FirstName.StartsWith(search) || search == null).ToList());
            }
            else if(searchBy == "LastName")
            {
                return View(db.Officers.Where(x => x.LastName.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.Officers.Where(x => x.AgencyCode.StartsWith(search) || search == null).ToList());
            }

        }
        public IActionResult OfficerAgencyData(string searchBy, string search)
        {
            //var dta = db.OfficerAgencies.ToList();
            //return View(dta);

            if (searchBy == "AgencyCode")
            {
                return View(db.OfficerAgencies.Where(x => x.AgencyCode.StartsWith(search) || search == null).ToList());
            }
            else if (searchBy == "AgencyName")
            {
                return View(db.OfficerAgencies.Where(x => x.AgencyName.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(db.OfficerAgencies.Where(x => x.AgencyState.StartsWith(search) || search == null).ToList());
            }
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult DeleteOfficer(string id)
        {
            //var res = db.Officers.Where(x => x.OfficerKey == id).FirstOrDefault();
            //db.Officers.Remove(res);
            //db.SaveChanges();
            //var list = db.Officers.ToList();
            //return View("OfficerData",list);

            var off = db.Officers.SingleOrDefault(e => e.OfficerKey == id);
            db.Officers.Remove(off);
            db.SaveChanges();
            return RedirectToAction("OfficerData");
        }

        //Code throwing error
        public IActionResult DeleteAgency(string id)
        {
            var em = db.OfficerAgencies.SingleOrDefault(x => x.AgencyCode == id);
            db.OfficerAgencies.Remove(em);//Part giving error
            db.SaveChanges();
            return RedirectToAction("OfficerAgencyData");
        }
        public IActionResult EditOfficer(string id)
        {
            var off = db.Officers.SingleOrDefault(e => e.OfficerKey ==id);
            var result = new Officer()
            {
                OfficerKey = off.OfficerKey,
                FirstName = off.FirstName,
                LastName = off.LastName,
                PhoneNumber = off.PhoneNumber,
                BadgeNumber = off.BadgeNumber,
                AgencyCode = off.AgencyCode
            };
            return View(result);
        }

        [HttpPost]
        public IActionResult EditOfficer(Officer model) {
            if (ModelState.IsValid)
            {
                var off = new Officer()
                {
                    OfficerKey = model.OfficerKey,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    BadgeNumber = model.BadgeNumber,
                    AgencyCode = model.AgencyCode
                };

                db.Officers.Update(off);
                db.SaveChanges();
                return RedirectToAction("OfficerData");
            }
            else { 
                return View(model); 
            }
        }

        
        public IActionResult EditAgency(string id)
        {
            var off = db.OfficerAgencies.SingleOrDefault(e => e.AgencyCode == id);
            var result = new OfficerAgency()
            {
                AgencyCode = off.AgencyCode,
                AgencyName = off.AgencyName,
                AgencyState = off.AgencyState,
                Pincode = off.Pincode
            };
            return View(result);
        }

        [HttpPost]
        public IActionResult EditAgency(OfficerAgency model)
        {
            if (ModelState.IsValid)
            {
                var off = new OfficerAgency()
                {
                    AgencyCode = model.AgencyCode,
                    AgencyName = model.AgencyName,
                    AgencyState = model.AgencyState,
                    Pincode = model.Pincode
                };
                db.OfficerAgencies.Update(off);
                db.SaveChanges();
                return RedirectToAction("OfficerAgencyData");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddOfficer() {
            return View();
        }

        [HttpPost]
        public IActionResult AddOfficer(Officer model)
        {
            var existingKeys = db.Officers.Select(u => u.OfficerKey).ToList();
            if (ModelState.IsValid && !(existingKeys.Contains(model.OfficerKey)))
            {
                var off = new Officer() {
                    OfficerKey = model.OfficerKey,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    BadgeNumber = model.BadgeNumber,
                    AgencyCode = model.AgencyCode
                };
                db.Officers.Add(off);
                db.SaveChanges();
                return RedirectToAction("OfficerData");
            }
            else if(existingKeys.Contains(model.OfficerKey)){
                ModelState.AddModelError("OfficerKey", "This officer key already exists");
                return View(model);
            }
            else
            {
                //ViewBag.Message = "Officer Key can't be empty";
                //TempData["error1"] = "Officer Key cannot be empty";
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult AddAgency()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAgency(OfficerAgency model)
        {
            var existingKeys = db.OfficerAgencies.Select(u => u.AgencyCode).ToList();
            if (ModelState.IsValid && !(existingKeys.Contains(model.AgencyCode)))
            {
                var ag = new OfficerAgency()
                {
                    AgencyCode = model.AgencyCode,
                    AgencyName = model.AgencyName,
                    AgencyState = model.AgencyState,
                    Pincode = model.Pincode
                };
                db.OfficerAgencies.Add(ag);
                db.SaveChanges();
                return RedirectToAction("OfficerAgencyData");
            }
            else if (existingKeys.Contains(model.AgencyCode))
            {
                ModelState.AddModelError("AgencyCode", "This agency code already exists");
                return View(model);
            }
            else
            {
                //ViewBag.Message = "Agency Code can't be empty";
                //TempData["error2"] = "Agency Code cannot be empty";
                return View(model);
            }
        }

        public IActionResult ErrorPage()
        {
            return View();
        }
    }
}
