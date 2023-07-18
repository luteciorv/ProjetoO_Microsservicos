# Projeto O - .NET Microsservices

### Descrição
        * Projeto do curso: https://www.youtube.com/watch?v=ByYyk8eMG6c sobre microsserviços
        * A ideia do curso gira em torno de um ambiente de microsserviços, onde 2 principais são implementados: Play.Catalog e Play.Inventory
        * O projeto utiliza o banco de dados MongoDb, Packages personalizados, Mensageria (MassTransit e RabbitMQ) e API REST
        * O objetivo desse projeto é um jogador poder operar os seus itens, podendo também adicioná-los e recuperá-los de seu inventário

### Implementações
        - Conexão com o banco de dados MongoDb (Docker)
        - Packages personalizados (Generalização do Repository e do MongoDb)
        - Mapeamento manual de entidade para DTOs e vice-versa
        - Entidades com comportamento
        - Mensageria com MassTransit e RabbitMQ (Docker)
        - API REST

### Telas do Projeto
	* Pastas
![](Images/pastas.png?raw=true)

        * Swagger - Play.Catalog
![](Images/swagger-play-catalog.png?raw=true)

        * Swagger - Play.Inventory
![](Images/swagger-play-inventory.png?raw=true)