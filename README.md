# RedisMsgPackApi
**Artigo sobre cache distribuído com serialização utilizando formato MessagePack**

Medium [link](https://medium.com/@elvis-dias/c-utilizando-messagepack-com-redis-50303d5e91d0).

## Api RedisMsgPackApi
Api http desenvolvida para provas de conceito sobre a utilização do formato MessagePack para salvar objetos serializados no Redis, cache distribuído.
Documentação sobre MessagePack pode ser obtida no [link](https://msgpack.org/).

### Como iniciar
Projeto desenvolvido em dotnet core 6. Para iniciar execute o comando abaixo:
```bash
$ dotnet run --project  ./src/RedisMsgPackApi.csproj 
```

### Testando a API
Serviço configurado para realizar bind na porta 5253. Exemplo de GET abaixo:
```bash
$ curl -s --request GET 'http://localhost:5253/user/123-pack/msgpack' | json_pp
{
   "appName" : "Lotlux",
   "email" : "apassfield0@craigslist.org",
   "firstName" : "Arvie",
   "gender" : "Male",
   "id" : "123-pack",
   "ipAddress" : "207.30.182.147",
   "lastName" : "Passfield",
   "souce" : "From Redis"
}
```

```bash
$ curl -s --request GET 'http://localhost:5253/user/123-json/jsonSerializer' | json_pp
{
   "appName" : "Lotlux",
   "email" : "apassfield0@craigslist.org",
   "firstName" : "Arvie",
   "gender" : "Male",
   "id" : "123-json",
   "ipAddress" : "207.30.182.147",
   "lastName" : "Passfield",
   "souce" : "From Redis"
}
```
