using HealthcareNLP.Api.Models;

namespace HealthcareNLP.Api.Services;

public interface ITextExtractionService
{
    ExtractionResult Extract(MedicalTextRequest request);
    string Summarize(string text);
}

public class TextExtractionService : ITextExtractionService
{
    private static readonly HashSet<string> KnownDiagnoses = new(StringComparer.OrdinalIgnoreCase)
    {
        "diabetes", "hypertension", "pneumonia", "asthma", "cancer",
        "anemia", "arthritis", "depression", "anxiety", "migraine"
    };

    private static readonly HashSet<string> KnownMedications = new(StringComparer.OrdinalIgnoreCase)
    {
        "metformin", "lisinopril", "atorvastatin", "amoxicillin", "ibuprofen",
        "aspirin", "omeprazole", "losartan", "amlodipine", "paracetamol"
    };

    private static readonly HashSet<string> KnownSymptoms = new(StringComparer.OrdinalIgnoreCase)
    {
        "fever", "cough", "fatigue", "headache", "nausea", "vomiting",
        "chest pain", "shortness of breath", "dizziness", "swelling"
    };

    public ExtractionResult Extract(MedicalTextRequest request)
    {
        var words = request.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var result = new ExtractionResult { PatientId = request.PatientId };

        foreach (var word in words)
        {
            var clean = word.Trim('.', ',', ';', ':').ToLower();
            if (KnownDiagnoses.Contains(clean)) result.Diagnoses.Add(clean);
            if (KnownMedications.Contains(clean)) result.Medications.Add(clean);
            if (KnownSymptoms.Contains(clean)) result.Symptoms.Add(clean);
        }

        result.Diagnoses = result.Diagnoses.Distinct().ToList();
        result.Medications = result.Medications.Distinct().ToList();
        result.Symptoms = result.Symptoms.Distinct().ToList();
        result.Summary = Summarize(request.Text);

        return result;
    }

    public string Summarize(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return string.Empty;
        var sentences = text.Split('.', StringSplitOptions.RemoveEmptyEntries);
        return sentences.Length <= 2 ? text.Trim() : string.Join(". ", sentences.Take(2)).Trim() + ".";
    }
}