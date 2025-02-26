using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ThuchanhAPIvoinetcore.Model;
using ett1_web_api.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ThuchanhAPIvoinetcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentContext _context;
        private readonly ILogger<StudentsController> _logger;

        public StudentsController(StudentContext context, ILogger<StudentsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // ✅ API lấy danh sách sinh viên
        [HttpGet("GetStudents")]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }

        // ✅ API lấy sinh viên theo ID
        [HttpGet("GetStudent/{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound(new { message = "Student not found" });

            return Ok(student);
        }

        // ✅ API tạo sinh viên mới
        [HttpPost("CreateStudent")]
        public async Task<IActionResult> CreateStudent([FromBody] Student student)
        {
            _logger.LogInformation("📌 Nhận request tạo Student: {@Student}", student);

            if (student == null)
            {
                _logger.LogWarning("⚠️ Dữ liệu student bị null!");
                return BadRequest(new { message = "Invalid student data" });
            }

            try
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                _logger.LogInformation("✅ Student đã được thêm vào DB: {@Student}", student);
                return Ok(new { message = "Student added successfully", student });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Lỗi khi thêm Student vào DB.");
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }

        // ✅ API cập nhật ảnh thẻ sinh viên (base64)
        [HttpPut("UpdatePhoto/{id}")]
        public async Task<IActionResult> UpdatePhoto(int id, [FromBody] string base64ImageString)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound(new { message = "Student not found" });

            try
            {
                student.Photo = base64ImageString;
                await _context.SaveChangesAsync();

                return Ok(new { message = "Photo updated successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Lỗi khi cập nhật ảnh thẻ.");
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }
    }
}
