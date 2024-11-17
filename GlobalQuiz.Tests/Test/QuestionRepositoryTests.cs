using Moq;
using GlobalQuiz.Domain.Interfaces;
using GlobalQuiz.Domain;

public class QuestionRepositoryTests
{
    private readonly Mock<IQuestionRepository> _repositoryMock;

    public QuestionRepositoryTests()
    {
        _repositoryMock = new Mock<IQuestionRepository>();
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnQuestions()
    {
        _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Question>
        {
            new Question { Id = 1, Text = "What is C#?", Answer = "Programming Language" },
        });

        var questions = await _repositoryMock.Object.GetAllAsync();
        Assert.Single(questions);
    }
}
