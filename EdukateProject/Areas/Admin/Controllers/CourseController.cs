using EdukateProject.Context;
using EdukateProject.Helpers;
using EdukateProject.Models;
using EdukateProject.ViewModels.CourseViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EdukateProject.Areas.Admin.Controllers;
[Area("Admin")]
//[Authorize(Roles = "Admin")]

public class CourseController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly string _folderPath;

    public CourseController(AppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
        _folderPath = Path.Combine(_environment.WebRootPath, "img");
    }

    public async Task<IActionResult> Index()
    {
        var courses = await _context.Courses.Select(c => new CourseGetVm()
        {
            Id = c.Id,
            Title = c.Title,
            ImagePath = c.ImagePath,
            Rating = c.Rating,
            TeacherName = c.Teacher.Name,
            TeacherSurname = c.Teacher.Surname,
        }).ToListAsync();

        return View(courses);
    }

    public async Task<IActionResult> Create()
    {
        await SendTeacherWithViewBag();
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(CourseCreateVm vm)
    {
        await SendTeacherWithViewBag();
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var isExistTeacher = await _context.Teachers.AnyAsync(t=> t.Id == vm.TeacherId);
        if (!isExistTeacher)
        {
            ModelState.AddModelError("TeacherId", "This Teacher not found");
            return View(vm);
        }

        if (!vm.Image.CheckSize(2))
        {
            ModelState.AddModelError("Image", "Max Size 2 MB");
            return View();
        }

        if (!vm.Image.CheckType("image"))
        {
            ModelState.AddModelError("Image", "Just Image Type");
            return View();
        }

        string uniqueImagePath = await vm.Image.FileUpload(_folderPath);
        Course course = new()
        {
            Title = vm.Title,
            Rating = vm.Rating,
            TeacherId = vm.TeacherId,
            ImagePath = uniqueImagePath,
        };

        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    
    public async Task<IActionResult> Update(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            return NotFound();
        }

        CourseUpdateVm vm = new()
        {
            Id = course.Id,
            Title = course.Title,
            Rating = course.Rating,
            TeacherId = course.TeacherId
        };


        await SendTeacherWithViewBag();
        return View(vm);
    }


    [HttpPost]
    public async Task<IActionResult> Update(CourseUpdateVm vm)
    {

        await SendTeacherWithViewBag();
        if (!ModelState.IsValid)
        {
            return View(vm);
        }

        var course = await _context.Courses.FindAsync(vm.Id);
        if (course == null)
        {
            return NotFound();
        }

        var isExistTeacher = await _context.Teachers.AnyAsync(t => t.Id == vm.TeacherId);
        if (!isExistTeacher)
        {
            ModelState.AddModelError("TeacherId", "This Teacher not found");
            return View(vm);
        }

        if (!vm.Image.CheckSize(2))
        {
            ModelState.AddModelError("Image", "Max Size 2 MB");
            return View();
        }

        if (!vm.Image.CheckType("image"))
        {
            ModelState.AddModelError("Image", "Just Image Type");
            return View();
        }

        course.Title = vm.Title;
        course.TeacherId = vm.TeacherId;
        course.Rating = vm.Rating;
        if(vm.Image != null)
        {
            string pathDel = Path.Combine(_folderPath,course.ImagePath);
            FileHelpers.DeleteFile(pathDel);
            string updateNewImg = await vm.Image.FileUpload(_folderPath);
            course.ImagePath = updateNewImg;
        }


        _context.Courses.Update(course);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));


    }

    public async Task<IActionResult> Delete(int id)
    {
        var deletedCourse = await _context.Courses.FindAsync(id);
        if (deletedCourse == null)
        {
            return NotFound();
        }

        _context.Courses.Remove(deletedCourse);
        await _context.SaveChangesAsync();

        string deletePath = Path.Combine(_folderPath, deletedCourse.ImagePath);
        FileHelpers.DeleteFile(deletePath);
        return RedirectToAction(nameof(Index));
    }


    private async Task SendTeacherWithViewBag()
    {
        var teachers  = await _context.Teachers.ToListAsync();
        ViewBag.Teachers = teachers;
    }
    
}
