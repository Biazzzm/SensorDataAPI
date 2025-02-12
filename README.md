Projeto de Monitoramento de Gás e Fumaça com Arduino e ESP8266

Descrição:
Este projeto é uma solução de monitoramento de gás e fumaça utilizando o Arduino Uno Wi-Fi, ESP8266, e o sensor MQ-2. A solução envia dados do sensor para uma API hospedada na AWS, e aciona alertas visuais e sonoros em caso de detecção de fumaça ou gás. As lâmpadas Yeelight conectadas via Wi-Fi alteram suas cores como parte do alerta, mudando entre vermelho e branco.

O sistema também permite a configuração de IPs das lâmpadas e do e-mail do usuário através de um ponto de acesso gerado pela placa ESP8266.

Funcionalidades:
Detecção de Gás/Fumaça: Utiliza o sensor MQ-2 para detectar a presença de fumaça ou gases perigosos.
Alerta Sonoro: Buzzer emite um som intermitente quando a fumaça é detectada.
Alerta Visual: LEDs e lâmpadas Yeelight mudam de cor em resposta ao sensor.
Integração com API: Envio de dados do sensor para uma API na AWS, que registra a detecção e envia notificações.
Configuração via Ponto de Acesso: A placa ESP8266 cria um ponto de acesso Wi-Fi (ESP8266_Config) para que o usuário possa informar os IPs das lâmpadas e o e-mail para associar o sistema ao usuário.
Como Funciona
Configuração do Wi-Fi: Ao ligar o dispositivo, ele cria uma rede Wi-Fi chamada ESP8266_Config. O usuário conecta-se a essa rede e informa os IPs das lâmpadas Yeelight (até 5 lâmpadas) e o e-mail utilizado para criar o cadastro.

Leitura de Dados do Sensor: O código na placa Arduino monitora o valor do sensor MQ-2. Quando o valor ultrapassa o limite preestabelecido, a placa ativa os alertas (sonoro e visual).

Enviando Dados para a API: Quando o valor do sensor ultrapassa o limite, os dados são enviados para a API. A API registra a detecção e armazena o e-mail do usuário junto aos dados do sensor.

Alteração das Cores das Lâmpadas: As lâmpadas Yeelight conectadas mudam de cor entre vermelho e branco em resposta à detecção de fumaça.

Tela de Configuração (Interface Windows Forms)
O projeto inclui uma interface Windows Forms para o usuário configurar as lâmpadas. Nela, é possível:

Informar os IPs das lâmpadas Yeelight.
Associar o sistema ao e-mail do usuário.
Monitorar os alertas gerados pelo sensor em tempo real.
Como Executar
Carregar o Código na Placa

Faça o upload do código da Placa Uno Wi-Fi e ESP8266 usando a IDE do Arduino.
Para o código da placa Uno, configure as Chaves 3 e 4 no modo ON.
Para o código do ESP8266, configure as Chaves 5, 6 e 7 no modo ON.
Deixe as Chaves 1, 2 e 5 no modo ON para permitir que os dois códigos se comuniquem corretamente.
Conectar-se à Rede Wi-Fi:

Conecte-se ao ponto de acesso gerado pela placa, ESP8266_Config.
Abra o portal de configuração para informar os IPs das lâmpadas e o e-mail.
Configuração da Interface Windows Forms:

Inicie a interface Windows Forms para visualizar e configurar as lâmpadas.
Monitoramento em Tempo Real:

Ao detectar fumaça ou gás, os alertas serão gerados visualmente e sonoramente. As lâmpadas Yeelight mudarão de cor e o sistema enviará os dados para a API.
Requisitos
Arduino Uno Wi-Fi (Atmega328).
ESP8266.
Sensor MQ-2.
Lâmpadas Yeelight (modelo E27, Wi-Fi).
IDE do Arduino.
Bibliotecas para ESP8266 e Arduino.
Software Windows Forms.
Tecnologias Utilizadas
Arduino (Linguagem C).
ESP8266 (Linguagem C++).
API (Node.js, hospedada na AWS).
Windows Forms (C#).
Como Contribuir
Faça um fork deste repositório.
Crie uma branch para a sua feature ou correção (git checkout -b feature/MinhaFeature).
Faça o commit das suas alterações (git commit -am 'Adicionando nova feature').
Push para a branch (git push origin feature/MinhaFeature).
Abra um pull request.
Licença
Este projeto está licenciado sob a licença MIT - veja o arquivo LICENSE para mais detalhes.

