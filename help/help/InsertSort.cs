using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace help
{
    public static class InsertSort
    {
        // Lista estática para armazenar os dados carregados
        private static List<string> _dataList = new List<string>();

        // Propriedade para expor e manipular diretamente a lista
        public static List<string> DataList
        {
            get => _dataList;
            set => _dataList = value ?? new List<string>();
        }

        // Método para adicionar uma palavra, mantendo a lista ordenada
        public static string AddWord(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return "A palavra está vazia.";

            word = word.Trim();

            // Verifica duplicata ignorando diferença de maiúsculas e minúsculas
            if (_dataList.Any(w => string.Equals(w, word, StringComparison.OrdinalIgnoreCase)))
                return "A palavra já existe na lista.";

            // Adiciona a palavra e mantém a lista ordenada
            _dataList.Add(word);
            _dataList = _dataList
                .OrderBy(w => w, StringComparer.OrdinalIgnoreCase) // Ordena alfabeticamente
                .ToList();

            return "Palavra adicionada com sucesso.";
        }

        // Método para salvar o conteúdo da lista no arquivo de origem
        public static void SaveToFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                throw new FileNotFoundException("Arquivo de origem não encontrado.");
            }

            File.WriteAllLines(filePath, _dataList);
        }

        // Método para carregar o conteúdo do arquivo na lista
        public static void LoadFromFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                throw new FileNotFoundException("Arquivo de origem não encontrado.");
            }

            _dataList = File.ReadAllLines(filePath)
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Distinct(StringComparer.OrdinalIgnoreCase) // Remove duplicatas
                .OrderBy(line => line, StringComparer.OrdinalIgnoreCase) // Ordena
                .ToList();
        }
    }
}
