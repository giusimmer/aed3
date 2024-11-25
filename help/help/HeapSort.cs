using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Threading;

public static class HeapSort
{
    public static void SortAndSave(string inputFilePath, string sortedFilePath, CancellationToken cancellationToken)
    {
        // Carrega as palavras do arquivo de entrada
        List<string> dicionario = CarregarDicionario(inputFilePath);

        if (dicionario != null && dicionario.Count > 0)
        {
            // Ordena usando Heap Sort
            Stopwatch stopwatch = Stopwatch.StartNew();
            OrdenarHeap(dicionario, cancellationToken);
            stopwatch.Stop();

            // Salva o dicionário ordenado no arquivo de saída
            SalvarDicionario(sortedFilePath, dicionario);
            Console.WriteLine($"Arquivo '{sortedFilePath}' salvo com {dicionario.Count} palavras ordenadas.");
            Console.WriteLine($"Tempo de execução com Heap Sort: {stopwatch.ElapsedMilliseconds} ms");
        }
        else
        {
            Console.WriteLine("Erro ao carregar o arquivo ou o arquivo está vazio.");
        }
    }

    private static List<string> CarregarDicionario(string caminho)
    {
        try
        {
            if (File.Exists(caminho))
            {
                // Usa HashSet para eliminar palavras duplicadas e adiciona todas as palavras à lista
                return new List<string>(new HashSet<string>(File.ReadAllLines(caminho), StringComparer.OrdinalIgnoreCase));
            }
            else
            {
                Console.WriteLine($"Arquivo não encontrado: {caminho}");
                return null;
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Erro ao ler o arquivo: {ex.Message}");
            return null;
        }
    }

    private static void SalvarDicionario(string caminho, List<string> dicionario)
    {
        try
        {
            File.WriteAllLines(caminho, dicionario);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Erro ao salvar o arquivo: {ex.Message}");
        }
    }

    private static void OrdenarHeap(List<string> vetor, CancellationToken cancellationToken)
    {
        int n = vetor.Count;

        // Constrói o max-heap
        for (int i = n / 2 - 1; i >= 0; i--)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Heapify(vetor, n, i, cancellationToken);
        }

        // Extrai os elementos do heap um por um e ajusta o heap
        for (int i = n - 1; i > 0; i--)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // Move o maior elemento atual para o final do vetor não ordenado
            var temp = vetor[0];
            vetor[0] = vetor[i];
            vetor[i] = temp;

            // Heapify na raiz do heap reduzido
            Heapify(vetor, i, 0, cancellationToken);
        }
    }

    private static void Heapify(List<string> vetor, int n, int i, CancellationToken cancellationToken)
    {
        int maior = i; // Inicializa maior como raiz
        int esquerda = 2 * i + 1;
        int direita = 2 * i + 2;

        cancellationToken.ThrowIfCancellationRequested();

        // Se o filho da esquerda é maior que o maior atual
        if (esquerda < n && string.Compare(vetor[esquerda], vetor[maior], StringComparison.OrdinalIgnoreCase) > 0)
            maior = esquerda;

        // Se o filho da direita é maior que o maior atual
        if (direita < n && string.Compare(vetor[direita], vetor[maior], StringComparison.OrdinalIgnoreCase) > 0)
            maior = direita;

        // Se o maior não é a raiz
        if (maior != i)
        {
            // Troca o maior elemento para a posição da raiz
            var temp = vetor[i];
            vetor[i] = vetor[maior];
            vetor[maior] = temp;

            // Recursivamente heapifica a subárvore afetada
            Heapify(vetor, n, maior, cancellationToken);
        }
    }
}
