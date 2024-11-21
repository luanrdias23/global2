using Microsoft.ML;
using GlobalQuiz.Domain.ML;

namespace GlobalQuiz.Services
{
    public class QuestionPredictionService
    {
        private readonly string _modelPath;
        private readonly MLContext _mlContext;

        public QuestionPredictionService()
        {
            _mlContext = new MLContext();
            _modelPath = Path.Combine(Environment.CurrentDirectory, "wwwroot", "MLModels", "QuestionDifficultyModel.zip");

            if (!File.Exists(_modelPath))
            {
                Console.WriteLine("Modelo não encontrado. Treinando modelo...");
                TrainModel();
            }
        }

        public void TrainModel()
        {
            var trainingDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "question-train.csv");

            var trainingData = _mlContext.Data.LoadFromTextFile<QuestionTrainingData>(
                trainingDataPath, hasHeader: true, separatorChar: ',');

            var pipeline = _mlContext.Transforms.Categorical.OneHotEncoding("CategoryEncoded", "Category")
                .Append(_mlContext.Transforms.Text.FeaturizeText("TextFeatures", "Text"))
                .Append(_mlContext.Transforms.Concatenate("Features", "CategoryEncoded", "TextFeatures"))
                .Append(_mlContext.Regression.Trainers.Sdca());

            var model = pipeline.Fit(trainingData);

            Directory.CreateDirectory(Path.GetDirectoryName(_modelPath));
            _mlContext.Model.Save(model, trainingData.Schema, _modelPath);
            Console.WriteLine($"Modelo treinado e salvo em {_modelPath}");
        }

        public float PredictDifficulty(string text, string category)
        {
            var model = _mlContext.Model.Load(_modelPath, out _);
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<QuestionTrainingData, QuestionPrediction>(model);

            var prediction = predictionEngine.Predict(new QuestionTrainingData
            {
                Text = text,
                Category = category
            });

            return prediction.PredictedDifficulty;
        }
    }
}
