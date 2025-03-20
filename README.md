# SensorDataAPI

Este repositório contém a API que recebe e processa dados dos sensores MQ-2 e MQ-4 , incluindo informações de gás e fumaça enviadas por dispositivos Arduino. A API é responsável por registrar os dados enviados pelo sensor, bem como associar essas informações ao e-mail do usuário para rastreamento e notificações.

## Funcionalidades

- **Recebe Dados de Sensores**: A API recebe dados do sensor MQ-2 (gás e fumaça) enviados pelo Arduino, junto com o e-mail do usuário.
- **Armazenamento e Processamento**: A API armazena os dados no banco de dados em memória e realiza o processamento necessário para a geração de alertas ou registros.
- **Notificação**: Possibilidade de enviar notificações para o usuário caso um valor crítico de sensor seja detectado.
- **Integração com o Sistema de Lâmpadas**: Integra-se ao sistema de lâmpadas Yeelight, alterando as cores em resposta aos dados recebidos do sensor.

## Como Executar a API

### Instalação

1. Clone o repositório para o seu computador:

    ```bash
    git clone[https://github.com/Biazzzm/SensorDataAPI.git]
    cd SensorDataAPI
    ```

2. Instale as dependências:

    ```bash
    npm install
    ```

3. Inicie a API:

    ```bash
    npm start
    ```

### Endpoints

A API possui o seguinte endpoint principal:

- **POST /api/sensordata/post-sensor-data/{email}**  
  Recebe os dados do sensor MQ-2 e o e-mail do usuário.

  **Corpo da requisição**:
  ```json
  {
   "id": 0,
  "sensorValue": 0,
  "userId": 0,
  "timestamp": "2025-02-12T19:17:04.635Z"
  }

