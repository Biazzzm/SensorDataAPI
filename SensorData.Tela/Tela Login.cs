using SensorData.Data;
using SensorDataAPI.ViewModels;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SensorData.Models;
using Newtonsoft.Json;
namespace SensorData.Tela
{
    public partial class TelaLogin : Form
    {
        private int userId;
        private string passwordLogin;
        public TelaLogin()
        {
            InitializeComponent();
            senhaCheckBox1.CheckedChanged += senhaCheckBox1_CheckedChanged;


        }



        public void PreencherEmail(string email)
        {
            EmailtextBox.Text = email;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string email = EmailtextBox.Text;
            string senha = SenhaTextBox.Text;

            bool sucessoLogin = await AutenticadorAsync(email, senha);

        }


        public async Task<bool> AutenticadorAsync(string email, string senha)
        {
            try
            {
                string url = "https://127.0.0.1:7155/v1/api/users/login";

                var loginModel = new LoginViewModel { Email = email, Password = senha };

                var json = System.Text.Json.JsonSerializer.Serialize(loginModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true,
                    AllowAutoRedirect = false
                };

                using (var client = new HttpClient(handler))
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<LoginResponseModel>(responseBody);
                        int userId = result.UserId;
                        passwordLogin = senha;

                        if (userId != 0) // Garantir que o UserId não é 0
                        {
                            // Passando o userId para o SistemaForm
                            SistemaForm sistema = new SistemaForm(userId, passwordLogin);
                            this.Hide();
                            sistema.ShowDialog();
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Erro: UserId retornou como 0.");
                            return false;
                        }
                    }
                    else
                    {
                        var responseBody = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Erro no login: {response.StatusCode}, Detalhes: {responseBody}");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                MessageBox.Show($"Erro ao tentar se autenticar: {ex.Message}");
                return false;
            }
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            TelaCadastro telaCadastro = new TelaCadastro();
            this.Hide();
            telaCadastro.ShowDialog();

        }
    }
}

