using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace help
{
    public partial class Front : Form
    {
        private CancellationTokenSource cts;

        public Front()
        {
            InitializeComponent();

            // Configuração de cores e estilos
            this.BackColor = Color.FromArgb(245, 245, 245);
            Color buttonColor = Color.FromArgb(70, 130, 180);
            foreach (var control in this.Controls)
            {
                if (control is Button button)
                {
                    button.BackColor = buttonColor;
                    button.ForeColor = Color.White;
                    button.FlatStyle = FlatStyle.Flat;
                }
            }

            // Configurações das caixas de texto
            txtResultado.ScrollBars = ScrollBars.Vertical;
            txtEntrada.ReadOnly = true;
            txtAviso.ReadOnly = true;
            txtResultado.ReadOnly = true;
            txtSalvo.ReadOnly = true;
            txtTempoExecucao.ReadOnly = true;
            txtFrase.Enabled = false;
            txtPalavrasNovas.ScrollBars = ScrollBars.Vertical;
            btnValidar.Enabled = false;

        }

        // Método para habilitar ou desabilitar botões durante a execução
        private void ToggleButtons(bool enable)
        {
            btnBuscar.Enabled = enable;
            btnBubble.Enabled = enable;
            btnHeap.Enabled = enable;
            btnSalvar.Enabled = enable;
            btnLimpar.Enabled = enable;
            btnLimparPalavras.Enabled = enable;
            txtFrase.Enabled = enable;
            txtPalavrasNovas.Enabled = enable;
            btnValidar.Enabled = enable;
            btnSalvarPalavrasNovas.Enabled = enable;
            btnParar.Enabled = !enable;
            btnParar.BackColor = enable ? Color.FromArgb(70, 130, 180) : Color.Red;

        }


        // Variável de controle para saber se o algoritmo foi executado
        private bool algoritmoExecutado = false;

        // Evento chamado quando o texto de txtSalvar muda (arquivo carregado ou salvo)
        private void txtSalvar_TextChanged(object sender, EventArgs e)
        {
            // Desabilita textFrase e btnValidar até que o algoritmo de ordenação tenha sido executado
            if (!algoritmoExecutado)
            {
                txtFrase.Enabled = false;
                txtPalavrasNovas.Enabled = false; // Desabilita a edição do campo textFrase
                btnValidar.Enabled = false;
                btnLimparPalavras.Enabled = false;
                btnLimpar.Enabled = false;
                btnSalvarPalavrasNovas.Enabled = false; // Desabilita o botão de validação

            }
            else
            {
                txtFrase.Enabled = true;
                txtPalavrasNovas.Enabled = true; // Habilita a edição do campo textFrase
                btnValidar.Enabled = true;
                btnLimparPalavras.Enabled = true;
                btnLimpar.Enabled = true;
                btnSalvarPalavrasNovas.Enabled = true; // Habilita o botão de validação
            }
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar se txtResultado está preenchido
                if (string.IsNullOrWhiteSpace(txtResultado.Text))
                {
                    MessageBox.Show("O campo de resultados está vazio. Por favor, carregue o arquivo primeiro.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Verificar se txtFrase está preenchido
                if (string.IsNullOrWhiteSpace(txtFrase.Text))
                {
                    MessageBox.Show("O campo de frase está vazio. Por favor, insira uma frase para validar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Criar o dicionário a partir do conteúdo do txtResultado
                HashSet<string> dicionario = new HashSet<string>(
                    txtResultado.Text.Split(new[] { ' ', '\n', '\r', '.', ',', ';' }, StringSplitOptions.RemoveEmptyEntries),
                    StringComparer.OrdinalIgnoreCase
                );

                // Certificar-se de que txtFrase é um RichTextBox
                if (!(txtFrase is RichTextBox rtbFrase))
                {
                    MessageBox.Show("O campo de frase precisa ser um RichTextBox para marcação colorida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Dividir palavras da frase
                string[] palavrasFrase = txtFrase.Text
                    .Split(new[] { ' ', '.', ',', ';', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                // Preparar campos
                rtbFrase.Clear();
                txtPalavrasNovas.Clear();
                List<string> palavrasNovas = new List<string>();

                // Desabilitar edição do RichTextBox durante a validação
                rtbFrase.ReadOnly = true;

                // Processar palavras
                foreach (string palavra in palavrasFrase)
                {
                    int startIndex = rtbFrase.TextLength;
                    rtbFrase.AppendText(palavra + " ");

                    // Verificar a palavra
                    if (System.Text.RegularExpressions.Regex.IsMatch(palavra, @"[^a-zA-Z]"))
                    {
                        // Palavras inválidas (caracteres fora do padrão esperado) -> Laranja
                        rtbFrase.Select(startIndex, palavra.Length);
                        rtbFrase.SelectionColor = Color.Orange;
                    }
                    else if (dicionario.Contains(palavra))
                    {
                        // Palavras existentes no dicionário -> Verde
                        rtbFrase.Select(startIndex, palavra.Length);
                        rtbFrase.SelectionColor = Color.Green;
                    }
                    else
                    {
                        // Palavras não existentes no dicionário -> Vermelho (com marcação de fundo)
                        rtbFrase.Select(startIndex, palavra.Length);
                        rtbFrase.SelectionColor = Color.Black;
                        rtbFrase.SelectionBackColor = Color.Red;
                        palavrasNovas.Add(palavra);
                    }
                }

                // Exibir palavras que não existem no dicionário
                txtPalavrasNovas.Text = string.Join(" - ", palavrasNovas);

                // Mostrar mensagem de conclusão
                MessageBox.Show("Validação concluída. O campo de frase foi bloqueado para edição. Limpe o campo para validar novamente.", "Validação Concluída", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Bloquear o campo txtFrase após validação
                txtFrase.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao validar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnBuscar_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtEntrada.Text = openFileDialog.FileName;

                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                if (fileInfo.Length > 800 * 1024)
                {
                    txtAviso.Text = "O arquivo é grande. O processamento pode demorar.";
                }
                else
                {
                    txtAviso.Text = "Arquivo pronto para ordenação.";
                }

                // Carregar conteúdo do arquivo em partes menores
                try
                {
                    using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                    {
                        txtResultado.Clear();
                        char[] buffer = new char[1024 * 1024]; // 1MB de buffer
                        int bytesRead;

                        while ((bytesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            txtResultado.AppendText(new string(buffer, 0, bytesRead));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar o arquivo: " + ex.Message);
                }
            }
        }


        private async void btnBubble_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEntrada.Text))
            {
                MessageBox.Show("Por favor, selecione um arquivo primeiro.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(txtEntrada.Text) + "_Ordenado_Bubble" + Path.GetExtension(txtEntrada.Text);

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ToggleButtons(false);
                txtTempoExecucao.Text = "Em andamento...";
                txtAviso.Text = "";
                txtResultado.Text = ""; // Limpa o txtResultado no início

                cts = new CancellationTokenSource();
                CancellationToken token = cts.Token;
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                var progressTask = Task.Run(async () =>
                {
                    while (!token.IsCancellationRequested && stopwatch.IsRunning)
                    {
                        TimeSpan elapsed = stopwatch.Elapsed;
                        txtTempoExecucao.Invoke((MethodInvoker)(() =>
                            txtTempoExecucao.Text = $"Em andamento... {elapsed.Minutes}m {elapsed.Seconds}s"));
                        await Task.Delay(1000, token);
                    }
                }, token);

                try
                {
                    await Task.Run(() =>
                    {
                        BubbleSort.SortAndSave(txtEntrada.Text, saveFileDialog.FileName, token);
                    }, token);

                    stopwatch.Stop();
                    TimeSpan totalElapsed = stopwatch.Elapsed;
                    txtTempoExecucao.Text = $"Tempo de execução (Bubble Sort): {totalElapsed.Minutes}m {totalElapsed.Seconds}s {totalElapsed.Milliseconds}ms";
                    txtResultado.Text = File.ReadAllText(saveFileDialog.FileName); // Atualiza com o resultado ordenado
                    txtSalvo.Text = "Arquivo salvo em: " + saveFileDialog.FileName;
                    MessageBox.Show("Arquivo ordenado usando Bubble Sort e salvo.");
                }
                catch (OperationCanceledException)
                {
                    stopwatch.Stop();
                    txtTempoExecucao.Text = "Operação cancelada.";
                    txtAviso.Text = "Operação interrompida pelo usuário.";
                }
                finally
                {
                    ToggleButtons(true);
                    cts.Dispose();
                }
            }
        }

        private async void btnHeap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEntrada.Text))
            {
                MessageBox.Show("Por favor, selecione um arquivo primeiro.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            saveFileDialog.FileName = Path.GetFileNameWithoutExtension(txtEntrada.Text) + "_Ordenado_Heap" + Path.GetExtension(txtEntrada.Text);

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ToggleButtons(false); // Desabilita botões
                txtTempoExecucao.Text = "Em andamento...";
                txtAviso.Text = "";
                txtResultado.Text = ""; // Limpa o txtResultado no início

                cts = new CancellationTokenSource();
                CancellationToken token = cts.Token;
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                var progressTask = Task.Run(async () =>
                {
                    while (!token.IsCancellationRequested && stopwatch.IsRunning)
                    {
                        TimeSpan elapsed = stopwatch.Elapsed;
                        txtTempoExecucao.Invoke((MethodInvoker)(() =>
                            txtTempoExecucao.Text = $"Em andamento... {elapsed.Minutes}m {elapsed.Seconds}s"));
                        await Task.Delay(1000, token);
                    }
                }, token);

                try
                {
                    await Task.Run(() =>
                    {
                        HeapSort.SortAndSave(txtEntrada.Text, saveFileDialog.FileName, token);
                    }, token);

                    stopwatch.Stop();
                    TimeSpan totalElapsed = stopwatch.Elapsed;
                    txtTempoExecucao.Text = $"Tempo de execução (Heap Sort): {totalElapsed.Minutes}m {totalElapsed.Seconds}s {totalElapsed.Milliseconds}ms";
                    txtResultado.Text = File.ReadAllText(saveFileDialog.FileName);
                    txtSalvo.Text = "Arquivo salvo em: " + saveFileDialog.FileName;
                    MessageBox.Show("Arquivo ordenado usando Heap Sort e salvo.");
                }
                catch (OperationCanceledException)
                {
                    stopwatch.Stop();
                    txtTempoExecucao.Text = "Operação cancelada.";
                    txtAviso.Text = "Operação interrompida pelo usuário.";
                }
                finally
                {
                    ToggleButtons(true); // Habilita botões novamente
                    cts.Dispose();
                }
            }
        }



        private void btnParar_Click(object sender, EventArgs e)
        {
            cts?.Cancel(); // Cancela a operação atual
            ToggleButtons(true); // Reabilita os botões
        }

        private void txtSalvo_TextChanged(object sender, EventArgs e) { }
        private void txtResultado_TextChanged(object sender, EventArgs e) { }
        private void Front_Load(object sender, EventArgs e) { }

        private void textFrase_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLimparPalavras_Click_1(object sender, EventArgs e)
        {
            txtFrase.Clear();           // Limpa o conteúdo do campo de frase
            txtFrase.ReadOnly = false;  // Reabilita o campo de frase para edição
            txtPalavrasNovas.Clear();   // Limpa o campo de palavras novas
            MessageBox.Show("Limpeza feita com sucesso. O campo está liberado para nova validação.", "Limpeza Concluída", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void btnSalvarPalavrasNovas_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Verificar se há palavras novas para salvar
                if (string.IsNullOrWhiteSpace(txtPalavrasNovas.Text))
                {
                    MessageBox.Show("Não há palavras novas para salvar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Obter as palavras existentes no txtResultado
                List<string> palavrasExistentes = txtResultado.Text
                    .Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => p.Trim())
                    .Where(p => !string.IsNullOrWhiteSpace(p))
                    .ToList();

                // Obter as palavras novas do txtPalavrasNovas usando o separador " - "
                List<string> palavrasNovas = txtPalavrasNovas.Text
                    .Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => p.Trim())
                    .Where(p => !string.IsNullOrWhiteSpace(p))
                    .ToList();

                // Lista para armazenar palavras confirmadas
                List<string> palavrasAConfirmar = new List<string>();

                // Confirmar cada palavra nova com o usuário
                foreach (var palavra in palavrasNovas)
                {
                    DialogResult result = MessageBox.Show(
                        $"Deseja adicionar a palavra '{palavra}' ao início da lista?",
                        "Confirmação",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (result == DialogResult.Yes)
                    {
                        palavrasAConfirmar.Add(palavra);
                    }
                }

                // Adicionar as palavras confirmadas ao início da lista existente
                if (palavrasAConfirmar.Count > 0)
                {
                    // Inserir palavras confirmadas no início
                    palavrasExistentes.InsertRange(0, palavrasAConfirmar);

                    // Garantir que não existam duplicatas, mantendo a ordem
                    palavrasExistentes = palavrasExistentes
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .ToList();

                    // Atualizar o TextBox com a lista atualizada
                    txtResultado.Text = string.Join(Environment.NewLine, palavrasExistentes);

                    // Salvar a lista atualizada no arquivo carregado anteriormente
                    if (!string.IsNullOrWhiteSpace(txtEntrada.Text) && File.Exists(txtEntrada.Text))
                    {
                        File.WriteAllLines(txtEntrada.Text, palavrasExistentes);
                        MessageBox.Show("Palavras novas salvas no arquivo com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Arquivo de origem não encontrado. Certifique-se de ter carregado o arquivo corretamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Nenhuma palavra nova foi adicionada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar palavras novas: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpar_Click_1(object sender, EventArgs e)
        {

            txtResultado.Clear();
            txtAviso.Clear();
            txtSalvo.Clear();
            txtTempoExecucao.Clear();
            MessageBox.Show("Limpeza feita com sucesso, pode fazer nova consulta.");

        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtResultado.Text))
            {
                MessageBox.Show("Não há dados para salvar.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog.FileName, txtResultado.Text);
                MessageBox.Show("Arquivo salvo com sucesso.");
                txtSalvo.Text = "Arquivo salvo em: " + saveFileDialog.FileName;
            }
        }
    }
}
