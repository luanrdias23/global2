using GlobalQuiz.Domain;
using GlobalQuiz.Domain.Interfaces;
using GlobalQuiz.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlobalQuiz.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRepository _repository;
        private readonly QuestionPredictionService _predictionService;

        public QuestionController(IQuestionRepository repository)
        {
            _repository = repository;
            _predictionService = new QuestionPredictionService();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Question question)
        {
            // Predizer dificuldade
            var predictedDifficulty = _predictionService.PredictDifficulty(question.Text, question.Category);
            question.Difficulty = (int)Math.Round(predictedDifficulty); // Conversão explícita

            await _repository.AddAsync(question);
            return CreatedAtAction(nameof(GetById), new { id = question.Id }, question);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var question = await _repository.GetByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }
    }
}
