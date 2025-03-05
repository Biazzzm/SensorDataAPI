namespace SensorData.Tela
{
    partial class SistemaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            senhaCheckBox1 = new CheckBox();
            ChatIdBox1 = new TextBox();
            button1 = new Button();
            label6 = new Label();
            label5 = new Label();
            SenhaTextBox = new TextBox();
            EmailtextBox = new TextBox();
            NametextBox = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            tabPage2 = new TabPage();
            button3 = new Button();
            button2 = new Button();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            tabPage3 = new TabPage();
            dataGridView2 = new DataGridView();
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            sqlCommand2 = new Microsoft.Data.SqlClient.SqlCommand();
            sqlCommand3 = new Microsoft.Data.SqlClient.SqlCommand();
            sqlCommand4 = new Microsoft.Data.SqlClient.SqlCommand();
            button4 = new Button();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Location = new Point(115, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(685, 437);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(senhaCheckBox1);
            tabPage1.Controls.Add(ChatIdBox1);
            tabPage1.Controls.Add(button1);
            tabPage1.Controls.Add(label6);
            tabPage1.Controls.Add(label5);
            tabPage1.Controls.Add(SenhaTextBox);
            tabPage1.Controls.Add(EmailtextBox);
            tabPage1.Controls.Add(NametextBox);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(label2);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(677, 409);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Sua Conta";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // senhaCheckBox1
            // 
            senhaCheckBox1.AutoSize = true;
            senhaCheckBox1.Location = new Point(346, 206);
            senhaCheckBox1.Name = "senhaCheckBox1";
            senhaCheckBox1.Size = new Size(101, 19);
            senhaCheckBox1.TabIndex = 18;
            senhaCheckBox1.Text = "Mostrar senha";
            senhaCheckBox1.UseVisualStyleBackColor = true;
            senhaCheckBox1.CheckedChanged += senhaCheckBox1_CheckedChanged;
            // 
            // ChatIdBox1
            // 
            ChatIdBox1.Location = new Point(112, 259);
            ChatIdBox1.Name = "ChatIdBox1";
            ChatIdBox1.Size = new Size(218, 23);
            ChatIdBox1.TabIndex = 17;
            // 
            // button1
            // 
            button1.BackColor = Color.LightSeaGreen;
            button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = SystemColors.ButtonFace;
            button1.Location = new Point(255, 311);
            button1.Name = "button1";
            button1.Size = new Size(75, 39);
            button1.TabIndex = 16;
            button1.Text = "EDITAR";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(32, 259);
            label6.Name = "label6";
            label6.Size = new Size(64, 30);
            label6.TabIndex = 14;
            label6.Text = "   ChatId \r\n(Telegram)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(112, 29);
            label5.Name = "label5";
            label5.Size = new Size(218, 45);
            label5.TabIndex = 13;
            label5.Text = "SEUS DADOS";
            // 
            // SenhaTextBox
            // 
            SenhaTextBox.Location = new Point(112, 206);
            SenhaTextBox.Name = "SenhaTextBox";
            SenhaTextBox.Size = new Size(218, 23);
            SenhaTextBox.TabIndex = 12;
            SenhaTextBox.UseSystemPasswordChar = true;
            // 
            // EmailtextBox
            // 
            EmailtextBox.Location = new Point(112, 157);
            EmailtextBox.Name = "EmailtextBox";
            EmailtextBox.Size = new Size(218, 23);
            EmailtextBox.TabIndex = 11;
            // 
            // NametextBox
            // 
            NametextBox.Location = new Point(112, 108);
            NametextBox.Name = "NametextBox";
            NametextBox.Size = new Size(218, 23);
            NametextBox.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(42, 206);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 9;
            label4.Text = "Senha";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(42, 157);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 8;
            label3.Text = "E-mail";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(42, 111);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 7;
            label2.Text = "Nome";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(button3);
            tabPage2.Controls.Add(button2);
            tabPage2.Controls.Add(label1);
            tabPage2.Controls.Add(dataGridView1);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(677, 409);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Contatos";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(541, 17);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 3;
            button3.Text = "Deletar";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new Point(460, 17);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 2;
            button2.Text = "+ Novo";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(204, 13);
            label1.Name = "label1";
            label1.Size = new Size(250, 30);
            label1.TabIndex = 1;
            label1.Text = "Contatos de Emergência";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(75, 46);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(537, 347);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(button4);
            tabPage3.Controls.Add(dataGridView2);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(677, 409);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Alertas e Incidentes";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(26, 34);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.Size = new Size(347, 349);
            dataGridView2.TabIndex = 0;
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // sqlCommand2
            // 
            sqlCommand2.CommandTimeout = 30;
            sqlCommand2.EnableOptimizedParameterBinding = false;
            // 
            // sqlCommand3
            // 
            sqlCommand3.CommandTimeout = 30;
            sqlCommand3.EnableOptimizedParameterBinding = false;
            // 
            // sqlCommand4
            // 
            sqlCommand4.CommandTimeout = 30;
            sqlCommand4.EnableOptimizedParameterBinding = false;
            // 
            // button4
            // 
            button4.Location = new Point(298, 6);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 1;
            button4.Text = "Atualizar Alertas";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // SistemaForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(922, 511);
            Controls.Add(tabControl1);
            Name = "SistemaForm";
            Text = "CheiroDeFumaça";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private Label label1;
        private DataGridView dataGridView1;
        private DataGridView dataGridView2;
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand2;
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand3;
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand4;
        private Label label5;
        private TextBox SenhaTextBox;
        private TextBox EmailtextBox;
        private TextBox NametextBox;
        private Label label4;
        private Label label3;
        private Label label2;
        private Button button1;
        private Label label6;
        private TextBox ChatIdBox1;
        private CheckBox senhaCheckBox1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}