# Microservice.Mail
Repositório de microsserviço Email. usado como exemplo o SMTP do gmail.

* Projeto Web API com Swagger
   * usado AutoMapper, MailKit, Npgsql.EntityFrameworkCore.PostgreSQL

* Projeto de teste com xUNIT
   * usado AutoFixture, Moq, Shouldly
   * Configurado mapper na injeção de dependência do teste sem mock.

Modificar o arquivo appsettings.json com a porta correta da base de dados, e o e-mail/senha para o serviço SMTP.  
Execute o comando update-database para criação do Database.
