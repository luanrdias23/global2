using Microsoft.ML.Data;

namespace GlobalQuiz.Domain.ML
{
    // Estrutura de dados para treinamento
    public class QuestionTrainingData
    {
        [LoadColumn(0)] public string Text { get; set; }
        [LoadColumn(1)] public string Category { get; set; }
        [LoadColumn(2)] public float Difficulty { get; set; }
    }

    // Resultado da predição
    public class QuestionPrediction
    {
        [ColumnName("Score")]
        public float PredictedDifficulty { get; set; }
    }
}
