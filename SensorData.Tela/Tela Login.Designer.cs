namespace SensorData.Tela
{
    partial class TelaLogin
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
            label1 = new Label();
            linkLabel1 = new LinkLabel();
            label5 = new Label();
            senhaCheckBox1 = new CheckBox();
            SenhaTextBox = new TextBox();
            EmailtextBox = new TextBox();
            label4 = new Label();
            label3 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(318, 81);
            label1.Name = "label1";
            label1.Size = new Size(135, 50);
            label1.TabIndex = 1;
            label1.Text = "LOGIN";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            linkLabel1.Location = new Point(427, 258);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(69, 15);
            linkLabel1.TabIndex = 18;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Clique Aqui";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(265, 258);
            label5.Name = "label5";
            label5.Size = new Size(156, 15);
            label5.TabIndex = 17;
            label5.Text = "Ainda não possui cadastro? ";
            // 
            // senhaCheckBox1
            // 
            senhaCheckBox1.AutoSize = true;
            senhaCheckBox1.Location = new Point(502, 225);
            senhaCheckBox1.Name = "senhaCheckBox1";
            senhaCheckBox1.Size = new Size(101, 19);
            senhaCheckBox1.TabIndex = 15;
            senhaCheckBox1.Text = "Mostrar senha";
            senhaCheckBox1.UseVisualStyleBackColor = true;
            senhaCheckBox1.CheckedChanged += senhaCheckBox1_CheckedChanged;
            // 
            // SenhaTextBox
            // 
            SenhaTextBox.Location = new Point(274, 220);
            SenhaTextBox.Name = "SenhaTextBox";
            SenhaTextBox.Size = new Size(218, 23);
            SenhaTextBox.TabIndex = 14;
            SenhaTextBox.UseSystemPasswordChar = true;
            // 
            // EmailtextBox
            // 
            EmailtextBox.Location = new Point(274, 171);
            EmailtextBox.Name = "EmailtextBox";
            EmailtextBox.Size = new Size(218, 23);
            EmailtextBox.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(204, 220);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 12;
            label4.Text = "Senha";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(204, 171);
            label3.Name = "label3";
            label3.Size = new Size(41, 15);
            label3.TabIndex = 11;
            label3.Text = "E-mail";
            // 
            // button1
            // 
            button1.BackColor = Color.MediumAquamarine;
            button1.Location = new Point(417, 288);
            button1.Name = "button1";
            button1.Size = new Size(75, 35);
            button1.TabIndex = 16;
            button1.Text = "Entrar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // TelaLogin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(linkLabel1);
            Controls.Add(label5);
            Controls.Add(senhaCheckBox1);
            Controls.Add(SenhaTextBox);
            Controls.Add(EmailtextBox);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(button1);
            Controls.Add(label1);
            Name = "TelaLogin";
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private LinkLabel linkLabel1;
        private Label label5;
        private CheckBox senhaCheckBox1;
        private TextBox SenhaTextBox;
        private TextBox EmailtextBox;
        private Label label4;
        private Label label3;
        private Button button1;
    }
}