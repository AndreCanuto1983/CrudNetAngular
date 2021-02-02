Tecnologias utilizadas:
1 - back end:
	- .Net Core 3.1
    - C#
	- EntityFramework Core
	- Sql Server 2018, mas poderá ser usado qualquer versão

Essenciais:
 - Visual studio 2018
 - Vs code para front pois é mais leve
 - Baixe o arquivo sql server, instale e configure sua instância e senha
 - Pode ser usado qualquer versão banco de dados sql server pois estou usando o code first

Configurando o Back End
1 - Após instalar os programas acima, abra o projeto
2 - Back.sln
3 - Verifique se o projeto WebAPI está definido como projeto de inicialização
	- Caso não, clique com o botão direito do mouse sobre ele e clique sobre "Definir como Projeto de Inicialização"
	- Verifique se o VS2019 instalou as dependências do projeto
4 - Dentro do projeto WebAPI, localize o arquivo: appsettings.json
    - Dentro do arquivo "appsettings.json" localize: "DefaultConnection" e insira a string de conexão do seu banco de dados sql  
6 - Abra o menu: Ferramentas > Gerenciador de Pacotes do NuGet > Console do Gerenciador de Pacotes
7 - Digite: Update-Database
	- O VS2019 vai criar o banco de dados para você, desde que a string de conexão estiver correta
8 - Rode o projeto, abrirá uma página do navegador com a documentação da API pelo SWAGGER
9 - API está pronta para uso

