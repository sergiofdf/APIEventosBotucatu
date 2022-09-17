# APIEventosBotucatu

## Projeto Final do Módulo Programação Web III | Web API

O objetivo do projeto é aplicar os conceitos vistos no módulo do curso Top Coders da ADA.

O projeto consiste em uma API para uma CRUD de eventos da cidade em que moro. Essa API permitirá cadastrar, consultar, atualizar e deletar eventos da cidade. Além disso, irá permitir também o CRUD para reservas para os eventos.

> Professora Amanda Mantovani <br>
> https://github.com/AmandaMantovani

<br>

--- 
<br>

📋 Conceitos abordados:
- Desenvolvimento de uma API com "ASP.NET Core Web App (Model-View-Controller)";
- Aplicação de métodos do protocolo HTTP para criar um CRUD;
- REST API;
- Open API;
- ApiController;
- Decorators;
- Passagem de parâmetros por rota, query e body;
- Data annotations para validações de dados;
- Definição de produces e consumes;
- Definição de códigos e mensagens de retorno;
- Action Result;
- Conexão com banco SQL utilizando Dapper;
- Boas práticas de segurança (proteção de connection strings, uso de dynamic parameters, etc);
- Arquitetura em camadas;
- Uso de injeção de dependência;
- Utilização de filtros (ActionFilter e ExceptionFilter) para validações e tratar erros;
- Utilização de DTO e automapper para não mostrar dados desnecessários;
- Definição e aplicação de políticas de CORS;
- Criação e consumo de tokens do tipo JWT para autenticação;
- Definição de autorizações baseado na autenticação (role via claims).

<br>

--- 
<br>

## 🚀 Como executar o programa
- Clonar o repositório em uma pasta local:
    `git clone https://github.com/sergiofdf/APIEventosBotucatu.git`
  
- Abra a solução do projeto com o visual studio, arquivo `APIEventosBotucatu.sln`.

- Dentro do projeto principal da API ('APIEventosBotucatu'), será necessário criar um arquivo `appsettings.json ` com as credenciais para acesso ao banco de dados. Segue o exemplo do modelo do conteúdo do arquivo. Troque os campos ilustrados em português pelos dados para o acesso ao banco de dados.

``` 
"Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=NomeServidor;Database=NomeBancoDeDados; User Id=Usuario; Password=Senha; Encrypt=False"
  },
  "secretKey": "chave para criar token JWT"
}
```


- Execute o projeto com `CTRL + F5`