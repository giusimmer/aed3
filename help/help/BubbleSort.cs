using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading;

public static class BubbleSort
{
    public static void SortAndSave(string filePath, string sortedFilePath, CancellationToken cancellationToken)
    {
        // Carregar o conteúdo do arquivo no dicionário
        List<string> dicionario = CarregarDicionario(filePath);
        if (dicionario.Count == 0)
        {
            throw new Exception("O arquivo está vazio ou não foi encontrado.");
        }

        // Iniciar o cronômetro para medir o tempo de ordenação
        Stopwatch stopwatch = Stopwatch.StartNew();
        OrdenarBolha(dicionario, cancellationToken);
        stopwatch.Stop();

        // Salvar o dicionário ordenado no novo arquivo
        SalvarDicionario(sortedFilePath, dicionario);
        Console.WriteLine($"Tempo de execução com Bubble Sort para {filePath}: {stopwatch.ElapsedMilliseconds} ms");

        // Exibir o conteúdo ordenado do arquivo
        ExibirConteudoOrdenado(sortedFilePath);
    }

    private static List<string> CarregarDicionario(string caminho)
    {
        if (File.Exists(caminho))
        {
            return new List<string>(File.ReadAllLines(caminho));
        }
        else
        {
            Console.WriteLine($"Arquivo {caminho} não encontrado.");
            return new List<string>();
        }
    }

    private static void SalvarDicionario(string caminho, List<string> dicionario)
    {
        File.WriteAllLines(caminho, dicionario);
    }

    private static void OrdenarBolha(List<string> vetor, CancellationToken cancellationToken)
    {
        int n = vetor.Count;
        for (int i = 0; i < n - 1; i++)
        {
            bool trocou = false;
            for (int j = 0; j < n - i - 1; j++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (string.Compare(vetor[j], vetor[j + 1], StringComparison.OrdinalIgnoreCase) > 0)
                {
                    var temp = vetor[j];
                    vetor[j] = vetor[j + 1];
                    vetor[j + 1] = temp;
                    trocou = true;
                }
            }
            if (!trocou)
                break;
        }
    }

    private static void ExibirConteudoOrdenado(string sortedFilePath)
    {
        if (File.Exists(sortedFilePath))
        {
            var lines = File.ReadAllLines(sortedFilePath);
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
        else
        {
            Console.WriteLine("O arquivo ordenado não foi encontrado.");
        }
    }
}
