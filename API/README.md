Tecnologias utilizadas:
1 - back end:
	- .Net Core 3.1
    - C#
	- EntityFramework Core
	- Sql Server 2014, mas poderá ser usado qualquer versão

2 - Front End
	- Angular 11
	- Html
	- Css
	- Typescript/Javascript
	- Angular Material
	- Bootstrap

Essenciais:
 - Visual studio 2019 : https://visualstudio.microsoft.com/pt-br/vs/
 - Vs code para front pois é mais leve
 - Sql Server 2014: https://www.microsoft.com/en-US/download/details.aspx?id=53167 
	- Baixe o arquivo sql server com o maior tamanho e instale e configure sua instância e senha
	- Pode ser usado qualquer versão banco de dados sql server pois estou usando o code first

Configurando o Back End
1 - Após instalar os programas acima, abra o projeto
2 - Back.sln
3 - Verifique se o projeto WebAPI está definido como projeto de inicialização
	- Caso não, clique com o botão direito do mouse sobre ele e clique sobre "Definir como Projeto de Inicialização"
	- Verifique se o VS2019 instalou as dependências do projeto
4 - Dentro do projeto WebAPI, localize o arquivo: appsettings.json
5 - Dentro do arquivo "appsettings.json" localize: "DefaultConnection" e insira a string de conexão do seu banco de dados sql  
6 - Abra o menu: Ferramentas > Gerenciador de Pacotes do NuGet > Console do Gerenciador de Pacotes
7 - Digite: Update-Database
	- O VS2019 vai criar o banco de dados para você
8 - Rode o projeto, abrirá uma página do navegador com a documentação da API pelo SWAGGER
9 - API está pronta para uso

Configurando o Front End
1 - Abra o cmd na pasta raiz do projeto "andre-app" e execute npm install
2 - Após instalado todas as dependências do projeto, vá para passo 3.
3 - npm start para iniciar o projeto
4 - Será aberto uma página no seu navegador com a tela de login
5 - Front end pronto para uso.