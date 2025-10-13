using AutoMapper;
using College_App.Data.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using Student = WebApplication1.Data.Student;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IMapper _mapper;
        private readonly ICollegeRepository<Student> _studentRepository;


        public StudentController(ILogger<StudentController> logger, CollegeDBContext dBContext, IMapper mapper, ICollegeRepository<Student> studentRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _studentRepository = studentRepository;
        }

        [HttpGet("GetStudentData", Name = "GetStudentData")]
        [ProducesResponseType(StatusCodes.Status200OK)] // OK
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)] // NotAcceptable
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // InternalServerError
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudentDataAsync()
        {
            _logger.LogInformation("GetStudentData called");
            var students = await _studentRepository.GetAllAsync();

            var studentDTOData = _mapper.Map<List<StudentDTO>>(students);

            //Ok - 200
            return Ok(studentDTOData);

        }

        [HttpGet("GetStudentDataById/{id:int}", Name = "GetStudentDataById")]
        [ProducesResponseType(StatusCodes.Status200OK)] // OK
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // BadRequest
        [ProducesResponseType(StatusCodes.Status404NotFound)] // NotFound
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)] // NotAcceptable
        public async Task<ActionResult<StudentDTO>> GetStudentDataByIdAsync(int id)
        {
            //BadRequest - 400
            if (id <= 0)
            {
                _logger.LogWarning($"Invalid ID {id} Given");
                return BadRequest($"Invalid ID {id} Given");
            }
            var student = await _studentRepository.GetByIdAsync(student => student.Id == id);
            //NotFound - 404
            if (student == null)
            {
                _logger.LogError($"Student With Id {id} Not Found");
                return NotFound($"Student With Id {id} Not Found");
            }
            var studentDTO = _mapper.Map<StudentDTO>(student);

            //Ok - 200
            return Ok(studentDTO);

        }


        [HttpGet("GetStudentDataByName/{name:alpha}")]
        [ProducesResponseType(200)] // OK
        [ProducesResponseType(400)] // BadRequest
        [ProducesResponseType(404)] // NotFound
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)] // NotAcceptable
        public async Task<ActionResult<StudentDTO>> GetStudentDataByNameAsync(string name)
        {
            var student = await _studentRepository.GetByNameAsync(student=> student.Name.ToLower().Contains(name.ToLower())); // Deligate or anonymous Method passing to GetByNameAsync

            //NotFound - 404    
            if (student == null)
            {
                return NotFound("Student Not Found");
            }

            //Check if name is null or empty
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name cannot be null or empty");
            }
            var studentDTO = _mapper.Map<StudentDTO>(student);
            return Ok(studentDTO);
        }


        [HttpPost]
        [Route("CreateStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)] // Created
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // BadRequest
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // InternalServerError
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)] // NotAcceptable 
        public async Task<ActionResult<StudentDTO>> CreateStudentAsync([FromBody] StudentDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Student model cannot be null");
            }
            // int newId = _dbcontext.Students.Max(s => s.Id) + 1; // Generate new ID
            Student newStudent = _mapper.Map<Student>(dto);

            var StudentAfterCreation = await _studentRepository.CreateAsync(newStudent);
            dto.Id = StudentAfterCreation.Id; // Update the model with the new ID
            return CreatedAtRoute("GetStudentDataById", new { id = dto.Id }, dto); // Return the created student with a 201 status code

        }

        [HttpPut]
        [Route("UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status204NoContent)] // NoContent
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // BadRequest
        [ProducesResponseType(StatusCodes.Status404NotFound)] // NotFound
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] // InternalServerError
        public async Task<ActionResult<StudentDTO>> UpdateStudentAsync([FromBody] StudentDTO dto)
        {
            if (dto == null || dto.Id <= 0)
            {
                return BadRequest("Student model cannot be null");
            }

            var existingStudent = await _studentRepository.GetByIdAsync(student => student.Id==dto.Id, true);

            // Check if the student exists
            if (existingStudent == null)
            {
                return NotFound("Student Not Found");
            }

            var newRecord = _mapper.Map<Student>(dto);

            await _studentRepository.UpdateAsync(newRecord);

            //existingStudent.Name = model.Name;  
            //existingStudent.Email = model.Email;
            //existingStudent.Address = model.Address;
            //existingStudent.PhoneNumber = model.PhoneNumber;


            return NoContent(); // Return 204 No Content on successful update

        }


        [HttpPatch("{id:int}/UpdateStudentPartially")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateStudentPartiallyAsync(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
            {
                return BadRequest("Patch document cannot be null and id must be greater than zero");
            }

            var existingStudent = await _studentRepository.GetByIdAsync(student => student.Id == id, true);

            if (existingStudent == null)
            {
                return NotFound("Student Not Found");
            }

            // Create a copy to apply the patch and validate before persisting
            var studentDTO = _mapper.Map<StudentDTO>(existingStudent);

            // Apply patch into the copy using the Controller's ModelState
            patchDocument.ApplyTo(studentDTO, ModelState);

            // Validate patch application
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Copy patched fields back to the tracked entity and save
            existingStudent = _mapper.Map<Student>(studentDTO);

            await _studentRepository.UpdateAsync(existingStudent);

            return NoContent();
        }


        [HttpDelete("DeleteStudentDataById/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)] // OK
        [ProducesResponseType(StatusCodes.Status400BadRequest)] // BadRequest
        [ProducesResponseType(StatusCodes.Status404NotFound)] // NotFound
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)] // NotAcceptable
        public async Task<ActionResult<bool>> DeleteStudentDataByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid ID");
            }

            var student = await _studentRepository.GetByIdAsync(student => student.Id == id, true);

            //NotFound - 404
            if (student == null)
            {
                return NotFound("Student Not Found");
            }

            await _studentRepository.DeleteAsync(student);

            return true;
        }


    }
}
