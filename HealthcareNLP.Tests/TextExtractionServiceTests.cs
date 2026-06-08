using HealthcareNLP.Api.Models;
using HealthcareNLP.Api.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HealthcareNLP.Tests;

[TestClass]
public class TextExtractionServiceTests
{
    private readonly ITextExtractionService _service = new TextExtractionService();

    [TestMethod]
    public void Extract_IdentifiesDiagnosis()
    {
        var result = _service.Extract(new MedicalTextRequest
        {
            PatientId = "P001",
            Text = "Patient has diabetes and hypertension. Prescribed metformin."
        });

        CollectionAssert.Contains(result.Diagnoses, "diabetes");
        CollectionAssert.Contains(result.Diagnoses, "hypertension");
    }

    [TestMethod]
    public void Extract_IdentifiesMedication()
    {
        var result = _service.Extract(new MedicalTextRequest
        {
            PatientId = "P002",
            Text = "Patient is on aspirin and omeprazole daily."
        });

        CollectionAssert.Contains(result.Medications, "aspirin");
        CollectionAssert.Contains(result.Medications, "omeprazole");
    }

    [TestMethod]
    public void Summarize_ReturnsFirstTwoSentences()
    {
        var text = "Patient presents with fever. Cough noted for 5 days. No prior history.";
        var summary = _service.Summarize(text);
        Assert.IsTrue(summary.Contains("fever"));
        Assert.IsFalse(summary.Contains("No prior history"));
    }

    [TestMethod]
    public void Extract_EmptyText_ReturnsEmptyLists()
    {
        var result = _service.Extract(new MedicalTextRequest { PatientId = "P003", Text = "xyz abc" });
        Assert.AreEqual(0, result.Diagnoses.Count);
        Assert.AreEqual(0, result.Medications.Count);
    }
}