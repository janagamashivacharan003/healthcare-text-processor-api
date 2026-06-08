namespace HealthcareNLP.Api.Models;

public class MedicalTextRequest
{
    public string Text { get; set; } = string.Empty;
    public string PatientId { get; set; } = string.Empty;
}

public class ExtractionResult
{
    public string PatientId { get; set; } = string.Empty;
    public List<string> Diagnoses { get; set; } = new();
    public List<string> Medications { get; set; } = new();
    public List<string> Symptoms { get; set; } = new();
    public string Summary { get; set; } = string.Empty;
    public DateTime ProcessedAt { get; set; } = DateTime.UtcNow;
}