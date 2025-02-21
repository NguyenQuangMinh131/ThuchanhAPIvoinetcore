using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ThuchanhAPIvoinetcore.Model;
using ett1_web_api.Data;
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

        // API tạo sinh viên mới
        [HttpPost("CreateStudent")]
        public async Task<IActionResult> CreateStudent([FromBody] Student student)
        {
            _logger.LogInformation("\ud83d\udccc Nhận request tạo Student: {@Student}", student);

            if (student == null)
            {
                _logger.LogWarning("\u26a0\ufe0f Dữ liệu student bị null!");
                return BadRequest(new { message = "Invalid student data" });
            }

            try
            {
                _context.Students.Add(student);
                await _context.SaveChangesAsync();

                _logger.LogInformation("\u2705 Student đã được thêm vào DB: {@Student}", student);
                return Ok(new { message = "Student added successfully", student });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "\u274c Lỗi khi thêm Student vào DB.");
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }
    }
}