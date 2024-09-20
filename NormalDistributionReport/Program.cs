using NormalDistributionReport.Services;
using System;

namespace NormalDistributionReport
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var reportService = new ReportService();

                Console.WriteLine("Escolha o idioma para o relatório:");
                Console.WriteLine("1 - Português (pt-PT)");
                Console.WriteLine("2 - Inglês (en-US)");

                string language = "";
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        language = "pt-PT";
                        break;
                    case "2":
                        language = "en-US";
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Gerando relatório em inglês (en-US) por padrão.");
                        language = "en-US";
                        break;
                }

                reportService.GenerateReport(language);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro não tratado: {ex.Message}");
                Console.WriteLine($"Tipo de exceção: {ex.GetType().FullName}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
            }
            finally
            {
                Console.WriteLine("Processo de geração do relatório concluído.");
                Console.WriteLine("Pressione qualquer tecla para sair...");
                Console.ReadKey();
            }
        }
    }
}

