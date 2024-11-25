namespace help
{
    partial class Front
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            txtEntrada = new TextBox();
            txtResultado = new TextBox();
            txtAviso = new TextBox();
            btnBuscar = new Button();
            btnBubble = new Button();
            btnHeap = new Button();
            btnParar = new Button();
            txtSalvo = new TextBox();
            txtTempoExecucao = new TextBox();
            txtFrase = new RichTextBox();
            btnValidar = new Button();
            txtPalavrasNovas = new TextBox();
            btnLimparPalavras = new Button();
            btnSalvarPalavrasNovas = new Button();
            btnLimpar = new Button();
            btnSalvar = new Button();
            SuspendLayout();
            // 
            // txtEntrada
            // 
            txtEntrada.Location = new Point(57, 27);
            txtEntrada.Name = "txtEntrada";
            txtEntrada.ReadOnly = true;
            txtEntrada.Size = new Size(654, 27);
            txtEntrada.TabIndex = 0;
            // 
            // txtResultado
            // 
            txtResultado.Location = new Point(57, 105);
            txtResultado.Multiline = true;
            txtResultado.Name = "txtResultado";
            txtResultado.ReadOnly = true;
            txtResultado.ScrollBars = ScrollBars.Vertical;
            txtResultado.Size = new Size(654, 144);
            txtResultado.TabIndex = 1;
            txtResultado.TextChanged += txtResultado_TextChanged;
            // 
            // txtAviso
            // 
            txtAviso.Location = new Point(57, 72);
            txtAviso.Name = "txtAviso";
            txtAviso.ReadOnly = true;
            txtAviso.Size = new Size(654, 27);
            txtAviso.TabIndex = 2;
            // 
            // btnBuscar
            // 
            btnBuscar.Location = new Point(717, 25);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(94, 29);
            btnBuscar.TabIndex = 3;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            btnBuscar.Click += btnBuscar_Click;
            // 
            // btnBubble
            // 
            btnBubble.Location = new Point(57, 307);
            btnBubble.Name = "btnBubble";
            btnBubble.Size = new Size(129, 73);
            btnBubble.TabIndex = 4;
            btnBubble.Text = "BubbleSort";
            btnBubble.UseVisualStyleBackColor = true;
            btnBubble.Click += btnBubble_Click;
            // 
            // btnHeap
            // 
            btnHeap.Location = new Point(582, 307);
            btnHeap.Name = "btnHeap";
            btnHeap.Size = new Size(129, 73);
            btnHeap.TabIndex = 6;
            btnHeap.Text = "HeapSort";
            btnHeap.UseVisualStyleBackColor = true;
            btnHeap.Click += btnHeap_Click;
            // 
            // btnParar
            // 
            btnParar.BackColor = Color.Red;
            btnParar.Enabled = false;
            btnParar.Location = new Point(717, 70);
            btnParar.Name = "btnParar";
            btnParar.Size = new Size(94, 29);
            btnParar.TabIndex = 9;
            btnParar.Text = "Parar";
            btnParar.UseVisualStyleBackColor = false;
            btnParar.Click += btnParar_Click;
            // 
            // txtSalvo
            // 
            txtSalvo.Location = new Point(57, 408);
            txtSalvo.Name = "txtSalvo";
            txtSalvo.ReadOnly = true;
            txtSalvo.Size = new Size(654, 27);
            txtSalvo.TabIndex = 10;
            txtSalvo.TextChanged += txtSalvo_TextChanged;
            // 
            // txtTempoExecucao
            // 
            txtTempoExecucao.Location = new Point(57, 255);
            txtTempoExecucao.Name = "txtTempoExecucao";
            txtTempoExecucao.ReadOnly = true;
            txtTempoExecucao.Size = new Size(654, 27);
            txtTempoExecucao.TabIndex = 11;
            // 
            // txtFrase
            // 
            txtFrase.Location = new Point(936, 52);
            txtFrase.Name = "txtFrase";
            txtFrase.Size = new Size(393, 303);
            txtFrase.TabIndex = 12;
            txtFrase.Text = "";
            txtFrase.TextChanged += textFrase_TextChanged;
            // 
            // btnValidar
            // 
            btnValidar.Location = new Point(936, 361);
            btnValidar.Name = "btnValidar";
            btnValidar.Size = new Size(393, 29);
            btnValidar.TabIndex = 13;
            btnValidar.Text = "Validar";
            btnValidar.UseVisualStyleBackColor = true;
            btnValidar.Click += btnValidar_Click;
            // 
            // txtPalavrasNovas
            // 
            txtPalavrasNovas.Location = new Point(936, 396);
            txtPalavrasNovas.Multiline = true;
            txtPalavrasNovas.Name = "txtPalavrasNovas";
            txtPalavrasNovas.Size = new Size(393, 79);
            txtPalavrasNovas.TabIndex = 14;
            // 
            // btnLimparPalavras
            // 
            btnLimparPalavras.Location = new Point(936, 14);
            btnLimparPalavras.Name = "btnLimparPalavras";
            btnLimparPalavras.Size = new Size(393, 32);
            btnLimparPalavras.TabIndex = 15;
            btnLimparPalavras.Text = "Limpar Editor";
            btnLimparPalavras.Click += btnLimparPalavras_Click_1;
            // 
            // btnSalvarPalavrasNovas
            // 
            btnSalvarPalavrasNovas.Location = new Point(936, 481);
            btnSalvarPalavrasNovas.Name = "btnSalvarPalavrasNovas";
            btnSalvarPalavrasNovas.Size = new Size(393, 40);
            btnSalvarPalavrasNovas.TabIndex = 16;
            btnSalvarPalavrasNovas.Text = "Salvar Palavras Novas";
            btnSalvarPalavrasNovas.Click += btnSalvarPalavrasNovas_Click_1;
            // 
            // btnLimpar
            // 
            btnLimpar.Location = new Point(717, 255);
            btnLimpar.Name = "btnLimpar";
            btnLimpar.Size = new Size(94, 29);
            btnLimpar.TabIndex = 17;
            btnLimpar.Text = "Limpar";
            btnLimpar.UseVisualStyleBackColor = true;
            btnLimpar.Click += btnLimpar_Click_1;
            // 
            // btnSalvar
            // 
            btnSalvar.Location = new Point(717, 408);
            btnSalvar.Name = "btnSalvar";
            btnSalvar.Size = new Size(94, 29);
            btnSalvar.TabIndex = 18;
            btnSalvar.Text = "Salvar";
            btnSalvar.UseVisualStyleBackColor = true;
            btnSalvar.Click += btnSalvar_Click_1;
            // 
            // Front
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1379, 533);
            Controls.Add(btnSalvar);
            Controls.Add(btnLimpar);
            Controls.Add(txtPalavrasNovas);
            Controls.Add(btnValidar);
            Controls.Add(txtFrase);
            Controls.Add(txtTempoExecucao);
            Controls.Add(txtSalvo);
            Controls.Add(btnParar);
            Controls.Add(btnHeap);
            Controls.Add(btnBubble);
            Controls.Add(btnBuscar);
            Controls.Add(txtAviso);
            Controls.Add(txtResultado);
            Controls.Add(txtEntrada);
            Controls.Add(btnLimparPalavras);
            Controls.Add(btnSalvarPalavrasNovas);
            Name = "Front";
            Text = "AED3";
            Load += Front_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtEntrada;
        private TextBox txtResultado;
        private TextBox txtAviso;
        private Button btnBuscar;
        private Button btnBubble;
        private Button btnHeap;
        private Button btnParar;
        private TextBox txtSalvo;
        private TextBox txtTempoExecucao;
        private RichTextBox txtFrase;
        private Button btnValidar;
        private Button btnLimparPalavras;
        private TextBox txtPalavrasNovas;
        private Button btnSalvarPalavrasNovas;
        private Button btnLimpar;
        private Button btnSalvar;
    }
}
