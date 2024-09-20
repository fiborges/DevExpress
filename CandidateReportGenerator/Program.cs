using System;
using System.Collections.Generic;
using CandidateReportGenerator.Controllers;

namespace CandidateReportGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
           
            var candidateController = new CandidateController();

            // Define as fases que o recrutador deseja incluir no relatório
            List<int> selectedPhases = new List<int> { 1, 2, 3 };

            candidateController.GenerateReport(selectedPhases);

            Console.WriteLine("Report generation complete.");
        }
    }
}

