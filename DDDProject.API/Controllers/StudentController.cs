using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DDDProject.API.Configurations;
using DDDProject.Application.Repositories;
using System;

namespace DDDProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public StudentsController(IStudentRepository studentRepository, IWebHostEnvironment hostEnvironment)
        {
            _studentRepository = studentRepository;
            _hostEnvironment = hostEnvironment;
        }

        [HttpPost("upload-profile-picture")]
        public async Task<IActionResult> UploadProfilePicture([FromForm] UploadImageRequest request, [FromQuery] Guid studentId)
        {
            if (request?.File == null || request.File.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(request.File.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
            {
                return BadRequest("Invalid file type. Please upload an image.");
            }

            var filePath = Path.Combine(_hostEnvironment.WebRootPath, "profile-pictures", Guid.NewGuid() + fileExtension);
            var directoryPath = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }

            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student == null)
            {
                return NotFound("Student not found.");
            }

            student.ProfilePicturePath = "/profile-pictures/" + Path.GetFileName(filePath);
            await _studentRepository.UpdateAsync(student);

            return Ok(new { Message = "Profile picture uploaded successfully.", ProfilePicturePath = student.ProfilePicturePath });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetStudentById([FromQuery] Guid id)
        {
            var stu = await _studentRepository.GetByIdAsync(id);
            if (stu == null)
            {
                return NotFound();
            }

            return Ok(stu);

        }
    }
}
