using SensorData.Models;
using System.Text;
using System.Text.Json;

namespace SensorData.Tela
{
    public partial class TelaCadastro : Form
    {
        private int userId;
        public TelaCadastro()
        {
            InitializeComponent();

            senhaCheckBox1.CheckedChanged += senhaCheckBox1_CheckedChanged;

        }



        private void senhaCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (senhaCheckBox1.Checked)
            {
                // Exibe a senha
                SenhaTextBox.UseSystemPasswordChar = false;  // Mostra a senha
            }
            else
            {
                // Oculta a senha
                SenhaTextBox.UseSystemPasswordChar = true;  // Oculta a senha
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var user = PostAsync(NametextBox.Text, EmailtextBox.Text, SenhaTextBox.Text);

            if (user != null)
            {
                MessageBox.Show("Usuário criado com sucesso!");

                string emailCadastrado = EmailtextBox.Text;

                TelaLogin login = new TelaLogin();
                login.PreencherEmail(emailCadastrado);
                this.Hide();
                login.ShowDialog();  // Janela modal

            }
            else
            {
                MessageBox.Show("Erro ao salvar contato.");
            }


        }

        public async Task<UserModel> PostAsync(string name, string email, string senha)
        {
            try
            {
                string url = "http://apicheiro-dev.eba-bctbmw7j.us-east-1.elasticbeanstalk.com/v1/api/users";

                var user = new UserModel
                {
                    Name = name,
                    Email = email,
                    Password = senha
                };


                string json = System.Text.Json.JsonSerializer.Serialize(user);


                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {

                        string responseBody = await response.Content.ReadAsStringAsync();


                        var option = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };


                        return JsonSerializer.Deserialize<UserModel>(responseBody, option); ;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Aqui você pode logar ou tratar o erro
                Console.WriteLine($"Erro: {ex.Message}");
                return null;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TelaLogin login = new TelaLogin();
            this.Hide();
            login.ShowDialog();
           
        }
    }


}
