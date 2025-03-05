using Newtonsoft.Json;
using SensorData.Models;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Text.Json;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO.Ports;

namespace SensorData.Tela
{
    public partial class SistemaForm : Form
    {
        private int userId;
        private string passwordLogin;
       

        public SistemaForm(int userId, string passwordLogin)
        {
            InitializeComponent();
            this.userId = userId;
            this.passwordLogin = passwordLogin;
            MessageBox.Show($"UserId carregado com sucesso: {userId}");
            CarregarUsuario();
            senhaCheckBox1.CheckedChanged += senhaCheckBox1_CheckedChanged;
            Console.WriteLine($"userId recebido: {userId}\n senha recebida: {passwordLogin}");
            CarregarContatosEmergencia(userId);
           

        }

        



        private void CarregarUsuario()
        {
            try
            {
                string url = $"http://apicheiro-dev.eba-bctbmw7j.us-east-1.elasticbeanstalk.com/v1/api/users/{userId}";

                HttpClientHandler handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true,
                    SslProtocols = SslProtocols.Tls12
                };

                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = TimeSpan.FromSeconds(30);

                    var response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = response.Content.ReadAsStringAsync().Result;

                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        var user = System.Text.Json.JsonSerializer.Deserialize<UserModel>(responseBody, options);

                        if (user != null)
                        {

                            // Preenche os campos com os dados do usuário
                            NametextBox.Text = user.Name ?? "";
                            EmailtextBox.Text = user.Email ?? "";
                            ChatIdBox1.Text = user.ChatId ?? "";
                            SenhaTextBox.Text = passwordLogin;

                            MessageBox.Show("Dados carregados com sucesso!");
                        }
                        else
                        {
                            MessageBox.Show("Resposta da API vazia ou não compatível.");
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Erro na requisição. Código: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateAsync(userId, NametextBox.Text, EmailtextBox.Text, SenhaTextBox.Text, ChatIdBox1.Text);
            CarregarUsuario();
        }

        private void dataGridView1_Load(object sender, EventArgs e)
        {
            CarregarContatosEmergencia(userId);

            // Criação da coluna de botão "Salvar"
            if (!dataGridView1.Columns.Contains("Salvar"))
            {
                DataGridViewButtonColumn saveButtonColumn = new DataGridViewButtonColumn
                {
                    Name = "Salvar",
                    HeaderText = "Ações",
                    Text = "Salvar",
                    UseColumnTextForButtonValue = true
                };
                dataGridView1.Columns.Add(saveButtonColumn);
            }
        }

        public async Task<bool> UpdateAsync(int userId, string name, string email, string password, string ChatId)
        {
            string url = $"http://apicheiro-dev.eba-bctbmw7j.us-east-1.elasticbeanstalk.com/v1/api/users/{userId}";

            // Aqui você pode obter a senha atual do usuário para usá-la caso o campo de senha esteja vazio
            string senhaAtual = ""; // Carregar a senha atual do usuário de algum lugar ou usar a senha salva

            // Verifica se o campo de senha está vazio e, caso esteja, não altera a senha
            string senhaParaAlterar = string.IsNullOrEmpty(password) ? senhaAtual : password;

            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true,
                SslProtocols = SslProtocols.Tls12
            };

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(30);

                var user = new
                {
                    Name = name,
                    Email = email,
                    Password = senhaParaAlterar,
                    ChatId = ChatId
                };

                var json = System.Text.Json.JsonSerializer.Serialize(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = client.PutAsync(url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Usuário alterado com sucesso");
                    return true;
                }
                else
                {
                    MessageBox.Show($"Erro na requisição: {response.StatusCode}");
                    return false;
                }
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

        private void CarregarContatosEmergencia(int UserId)
        {
            var url = $"http://apicheiro-dev.eba-bctbmw7j.us-east-1.elasticbeanstalk.com/v1/api/contacts/{UserId}";

            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true,
                SslProtocols = SslProtocols.Tls12
            };

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(30);

                var response = client.GetAsync(url).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = response.Content.ReadAsStringAsync().Result;

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    List<ContactModel> contatos = System.Text.Json.JsonSerializer.Deserialize<List<ContactModel>>(responseBody, options);

                    var bindingList = new BindingList<ContactModel>(contatos);
                    dataGridView1.DataSource = bindingList;


                    if (dataGridView1.Columns.Contains("UserId"))
                    {
                        dataGridView1.Columns["UserId"].Visible = false;
                    }

                    if (dataGridView1.Columns.Contains("User"))
                    {
                        dataGridView1.Columns["User"].Visible = false;
                    }

                    if (!dataGridView1.Columns.Contains("Salvar"))
                    {
                        DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn
                        {
                            Name = "Salvar",
                            HeaderText = "Ações",
                            Text = "Salvar",
                            UseColumnTextForButtonValue = true
                        };
                        dataGridView1.Columns.Add(buttonColumn);
                    }

                    if (!dataGridView1.Columns.Contains("Deletar"))
                    {
                        DataGridViewButtonColumn deleteButtonColumn = new DataGridViewButtonColumn
                        {
                            Name = "Delete",
                            HeaderText = "Ações",
                            Text = "Delete",
                            UseColumnTextForButtonValue = true
                        };
                        dataGridView1.Columns.Add(deleteButtonColumn);
                    }
                }
                else
                {
                    MessageBox.Show($"Erro na requisição. Código: {response.StatusCode}");
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            var novoContato = new ContactModel
            {
                Id = 0,
                Name = "",  // Você pode definir valores iniciais ou deixar vazios
                Email = "",
                ChatId = "",
                UserId = userId
            };

            // Adiciona o novo contato à lista vinculada
            var bindingList = dataGridView1.DataSource as BindingList<ContactModel>;
            if (bindingList != null)
            {
                bindingList.Add(novoContato);
            }
        }



        public async Task<bool> AdicionarContatoNaAPI(ContactModel contato)
        {
            try
            {
                string url = "http://apicheiro-dev.eba-bctbmw7j.us-east-1.elasticbeanstalk.com/v1/api/contacts";

                string json = System.Text.Json.JsonSerializer.Serialize(contato);


                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var client = new HttpClient())
                {

                    HttpResponseMessage response = client.PostAsync(url, content).Result;

                    if (response.IsSuccessStatusCode)
                    {

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                // Aqui você pode logar ou tratar o erro
                Console.WriteLine($"Erro: {ex.Message}");
                return false;
            }
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Salvar"].Index && e.RowIndex >= 0)
            {
                // Obtém o objeto 'contato' associado à linha
                var contato = dataGridView1.Rows[e.RowIndex].DataBoundItem as ContactModel;

                if (contato != null)
                {
                    bool resultado;

                    if (contato.Id > 0)
                    {
                        resultado = AtualizarContatoAsync(contato).Result;

                        if (resultado)
                        {
                            MessageBox.Show("Contato atualizado com sucesso!");
                        }
                        else
                        {
                            MessageBox.Show("Erro ao atualizar o contato.");
                        }
                    }

                    else
                    {
                        resultado = AdicionarContatoNaAPI(contato).Result;

                        if (resultado)
                        {
                            MessageBox.Show("Contato salvo com sucesso!");
                        }
                        else
                        {
                            MessageBox.Show("Erro ao salvar o contato.");
                        }
                    }

                    // Atualiza o DataGrid após salvar ou atualizar
                    CarregarContatosEmergencia(userId);
                }
            }
        }


        public async Task<bool> AtualizarContatoAsync(ContactModel contato)
        {
            string url = $"http://apicheiro-dev.eba-bctbmw7j.us-east-1.elasticbeanstalk.com/v1/api/contacts/{userId}/{contato.Id}";



            using (var client = new HttpClient())
            {
                // Criando o objeto de dados do contato para ser enviado na atualização
                var contatoData = new
                {
                    Name = contato.Name,
                    Email = contato.Email,
                    ChatId = contato.ChatId
                };

                // Serializa o objeto para JSON
                var json = System.Text.Json.JsonSerializer.Serialize(contatoData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Envia a requisição PUT para atualizar o contato
                var response = client.PutAsync(url, content).Result;

                // Retorna true se a atualização for bem-sucedida, caso contrário, false
                if (response.IsSuccessStatusCode)
                {

                    return true;
                }

                else
                {

                    return false;
                }
            }
        }

        public async Task<bool> DeleteAsync(int userId, int id)
        {
            var url = $"http://apicheiro-dev.eba-bctbmw7j.us-east-1.elasticbeanstalk.com/v1/api/contacts/{userId}/{id}";

            try
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = client.DeleteAsync(url).Result;

                    // Verifica se a requisição foi bem-sucedida
                    return response.IsSuccessStatusCode;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir contato: {ex.Message}");
                return false;
            }



        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // Verifica se uma linha está selecionada
            {
                int userId = this.userId;
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                var sucesso = DeleteAsync(userId, id).Result;

                if (sucesso)
                {
                    MessageBox.Show("Contato excluído com sucesso!");
                    CarregarContatosEmergencia(userId);
                    // Atualize o DataGrid após a exclusão
                }
                else
                {
                    MessageBox.Show("Erro ao excluir contato.");
                }
            }
            else
            {
                MessageBox.Show("Selecione uma linha para excluir.");
            }
        }

        private void dataGridView2_Load(object sender, DataGridViewCellEventArgs e)
        {
            CarregarAlertas(userId).Wait();

        }
        private async Task CarregarAlertas(int userId)
        {
            try
            {
                var url = $"http://apicheiro-dev.eba-bctbmw7j.us-east-1.elasticbeanstalk.com/v1/api/alerts/{userId}";

                using (var client = new HttpClient())
                {
                    var response = client.GetAsync(url).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseBody = response.Content.ReadAsStringAsync().Result;
                        var alertas = System.Text.Json.JsonSerializer.Deserialize<List<AlertaModel>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        // Verifica se a lista de alertas é nula ou vazia
                        if (alertas == null || !alertas.Any())
                        {
                            alertas = new List<AlertaModel>(); // Cria uma lista vazia


                        }

                        // Converte o horário UTC para Brasília (GMT-3)
                        TimeZoneInfo brasiliaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                        foreach (var alerta in alertas)
                        {
                            alerta.DataHora = TimeZoneInfo.ConvertTimeFromUtc(alerta.DataHora, brasiliaTimeZone);
                        }


                        // Atualiza o DataGrid com a lista de alertas
                        dataGridView2.DataSource = new BindingList<AlertaModel>(alertas);

                        // Oculta colunas desnecessárias
                        if (dataGridView2.Columns.Contains("UserId"))
                        {
                            dataGridView2.Columns["UserId"].Visible = false;
                        }

                        if (dataGridView2.Columns.Contains("User"))
                        {
                            dataGridView2.Columns["User"].Visible = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Erro ao carregar alertas: " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }



        private async void AdicionarAlerta()
        {
            try
            {
                // Atualiza o DataGrid com os alertas mais recentes
                await CarregarAlertas(userId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar alertas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdicionarAlerta();
            CriarGrafico();

        }

        private async void CriarGrafico()
        {
            // Criar um novo gráfico
            Chart chart = new Chart();
            chart.Size = new System.Drawing.Size(300, 200); // Ajuste o tamanho conforme necessário

            // Posicionar no lado direito da tabPage3
            int xPos = tabPage3.Width - chart.Width - 10; // Margem de 20 pixels da borda
            int yPos = (tabPage3.Height - chart.Height) / 2; // Centralizar verticalmente

            chart.Location = new System.Drawing.Point(xPos, yPos);


            // Criar a área do gráfico
            ChartArea chartArea = new ChartArea("MainArea");
            chart.ChartAreas.Add(chartArea);

            // Criar uma série de dados
            Series series = new Series("Alertas")
            {
                ChartType = SeriesChartType.Column // Tipo do gráfico
            };

            var alertas = GetAlertasAsync(userId).Result;


            var alertasOrdenados = alertas.OrderBy(a => a.DataHora).ToList();

            // Adicionar os dados ao gráfico
            foreach (var alerta in alertasOrdenados)
            {
                series.Points.AddXY(alerta.DataHora.ToString("dd/MM/yyyy HH:mm"), alerta.SensorValue);
            }

            chart.Series.Add(series);

            // Adicionar o gráfico à TabPage3 dentro do TabControl3
            tabPage3.Controls.Add(chart);

            tabPage3.Resize += (s, e) =>
            {
                chart.Location = new System.Drawing.Point(tabPage3.Width - chart.Width - 20, (tabPage3.Height - chart.Height) / 2);
            };

            // Ajustar posicionamento ao redimensionar a aba
            tabPage3.Resize += (s, e) =>
            {
                chart.Location = new System.Drawing.Point(tabPage3.Width - chart.Width - 20, (tabPage3.Height - chart.Height) / 2);
            };
        }

        private async Task<List<AlertaModel>> GetAlertasAsync(int userId)
        {
            try
            {
                // Fazendo a requisição GET para a API
                string url = $"http://apicheiro-dev.eba-bctbmw7j.us-east-1.elasticbeanstalk.com/v1/api/alerts/{userId}";

                var client = new HttpClient();

                HttpResponseMessage response = client.GetAsync(url).Result;

                response.EnsureSuccessStatusCode();

                // Deserializando o JSON
                string responseBody = response.Content.ReadAsStringAsync().Result;
                var alertas = JsonConvert.DeserializeObject<List<AlertaModel>>(responseBody);

                return alertas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao obter dados da API: {ex.Message}");
                return new List<AlertaModel>(); // Retorna uma lista vazia em caso de erro
            }
        }

        
    }
}
        
    

    

