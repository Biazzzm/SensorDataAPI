namespace SensorData.Tela
{
    partial class TelaCadastro
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            NametextBox = new TextBox();
            EmailtextBox = new TextBox();
            SenhaTextBox = new TextBox();
            senhaCheckBox1 = new CheckBox();
            button1 = new Button();
            label5 = new Label();
            linkLabel1 = new LinkLabel();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(238, 81);
            label1.Name = "label1";
            label1.Size = new Size(218, 50);
            label1.TabIndex = 0;
            label1.Text = "CADASTRO";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(168, 175);
            label2.Name = "label2";
            label2.Size = new Size(45, 17);
            label2.TabIndex = 1;
            label2.Text = "Nome";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(168, 221);
            label3.Name = "label3";
            label3.Size = new Size(47, 17);
            label3.TabIndex = 2;
            label3.Text = "E-mail";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(168, 270);
            label4.Name = "label4";
            label4.Size = new Size(45, 17);
            label4.TabIndex = 3;
            label4.Text = "Senha";
            // 
            // NametextBox
            // 
            NametextBox.Location = new Point(238, 172);
            NametextBox.Name = "NametextBox";
            NametextBox.Size = new Size(218, 25);
            NametextBox.TabIndex = 4;
            // 
            // EmailtextBox
            // 
            EmailtextBox.Location = new Point(238, 221);
            EmailtextBox.Name = "EmailtextBox";
            EmailtextBox.Size = new Size(218, 25);
            EmailtextBox.TabIndex = 5;
            // 
            // SenhaTextBox
            // 
            SenhaTextBox.Location = new Point(238, 270);
            SenhaTextBox.Name = "SenhaTextBox";
            SenhaTextBox.Size = new Size(218, 25);
            SenhaTextBox.TabIndex = 6;
            SenhaTextBox.UseSystemPasswordChar = true;
            // 
            // senhaCheckBox1
            // 
            senhaCheckBox1.AutoSize = true;
            senhaCheckBox1.Location = new Point(466, 275);
            senhaCheckBox1.Name = "senhaCheckBox1";
            senhaCheckBox1.Size = new Size(115, 21);
            senhaCheckBox1.TabIndex = 7;
            senhaCheckBox1.Text = "Mostrar senha";
            senhaCheckBox1.UseVisualStyleBackColor = true;
            senhaCheckBox1.CheckedChanged += senhaCheckBox1_CheckedChanged;
            // 
            // button1
            // 
            button1.BackColor = Color.MediumAquamarine;
            button1.Location = new Point(381, 338);
            button1.Name = "button1";
            button1.Size = new Size(75, 35);
            button1.TabIndex = 8;
            button1.Text = "Salvar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(238, 309);
            label5.Name = "label5";
            label5.Size = new Size(113, 15);
            label5.TabIndex = 9;
            label5.Text = "Já possui cadastro? ";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            linkLabel1.Location = new Point(346, 307);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(69, 15);
            linkLabel1.TabIndex = 10;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Clique Aqui";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // TelaCadastro
            // 
            AutoScaleDimensions = new SizeF(8F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 510);
            Controls.Add(linkLabel1);
            Controls.Add(label5);
            Controls.Add(senhaCheckBox1);
            Controls.Add(SenhaTextBox);
            Controls.Add(EmailtextBox);
            Controls.Add(NametextBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Name = "TelaCadastro";
            Text = "Cheiro de Fumaça";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox NametextBox;
        private TextBox EmailtextBox;
        private TextBox SenhaTextBox;
        private CheckBox senhaCheckBox1;
        private Button button1;
        private Label label5;
        private LinkLabel linkLabel1;
    }
}
