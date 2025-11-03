using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasaheelProject.Data;
using TasaheelProject.Data.Viewmodel;
using TasaheelProject.Models;

namespace TasaheelProject.Controllers
{
    public class AccountController : Controller
    {


        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;


        public AccountController(ApplicationDbContext d, UserManager<ApplicationUser> u, RoleManager<IdentityRole> r, SignInManager<ApplicationUser> s) 
        {
            this.db = d;
            this.userManager = u;
            this.roleManager = r;
            this.signInManager = s;
        
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewmodel us)
        {
            if (!ModelState.IsValid)
                return View("~/Views/Home/Login.cshtml", us);
            var user=await userManager.FindByEmailAsync(us.Email);

            if (user == null)
            {
                ModelState.AddModelError("","البريد الالكتروني غير موجود");
                return View("~/Views/Home/Login.cshtml", us);
            }

            if (!user.IsActive)
            {
                ModelState.AddModelError("", "الحساب غير مفعل");
                return View("~/Views/Home/Login.cshtml", us);
            }
            var result = await signInManager.PasswordSignInAsync(user,us.Password,false,false);

            if (result.Succeeded)
            {
                //توجيه المستخدم
                if (await userManager.IsInRoleAsync(user, "Citizen"))
                    return RedirectToAction("CitizenHome", "Citizin");

                else if (await userManager.IsInRoleAsync(user, "Employee"))
                    return RedirectToAction("EmployeeHome", "Employees");

                else if (await userManager.IsInRoleAsync(user, "Admin"))
                    return RedirectToAction("AdminHome", "Admin");

                return RedirectToAction("HomePage", "Home");
            }
            ModelState.AddModelError("","فشل في تسجيل الدخول تحقق من كلمة المرور و البريد");
            return View("~/Views/Home/Login.cshtml", us);

        }


        [HttpPost]
       
        public async Task<IActionResult> RegisterCitizen(CitizinViewmodel U) 
        {
            if (!ModelState.IsValid) 
                return View(U);
            
                var uc = new ApplicationUser
                {
                    Email = U.Email,
                    IsActive = true,
                    UserName = U.Email,
                    RoleType = "Citizen",

                };
            //انشاء كلمة المرور
            var result=await userManager.CreateAsync(uc,U.Password);
            if (result.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync("Citizen"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Citizen"));
                }
                await userManager.AddToRoleAsync(uc, "Citizen");

                var Citizin = new CitizenProfile()
                {
                    CitizenId=uc.Id,
                    NationalId = U.NationalId,
                    FullName = U.FullName,
                    DateOfBirth = U.DateOfBirth,
                    PhoneNumber = U.PhoneNumber,
                    ApplicationUserId = uc.Id,

                };

                db.Citizens.Add(Citizin);
                await db.SaveChangesAsync();
                return RedirectToAction("Login","Home");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
            return View(U);
        }


        [HttpGet]
        public IActionResult RegisterEmployee()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterEmployee(EmployeeViewmodel U)
        {
            if (!ModelState.IsValid)
                return View(U);

            var uc = new ApplicationUser
            {
                Email = U.Email,
                IsActive = true,
                UserName = U.Name,
                RoleType = "Employee",

            };
            //انشاء كلمة المرور
            var result = await userManager.CreateAsync(uc, U.password);
            if (result.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync("Employee"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Employee"));
                }
                await userManager.AddToRoleAsync(uc, "Employee");

                var Citizin = new EmployeeProfile()
                {
                    NationalId = U.NationalId,
                    HireDate = U.HireDate,
                    JobTitle = U.JobTitle,
                    //BranchId = U.BranchId,
                    EmployeeNumber = U.EmployeeNumber,
                    EmployeeId = uc.Id,

                };

                db.Employees.Add(Citizin);
                await db.SaveChangesAsync();
                //return RedirectToAction(nameof(Login));
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
            return View(U);
        }


        [HttpGet]
        public IActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAdmin(AdminViewmodel U)
        {
            if (!ModelState.IsValid)
                return View(U);

            var uc = new ApplicationUser
            {
                Email = U.Email,
                IsActive = true,
                UserName = U.Name,
                RoleType = "Admin",

            };
            //انشاء كلمة المرور
            var result = await userManager.CreateAsync(uc, U.Password);
            if (result.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }
                await userManager.AddToRoleAsync(uc, "Admin");

               

                
                await db.SaveChangesAsync();
                //return RedirectToAction(nameof(Login));
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
            return View(U);
        }
    }
}

