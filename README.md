# Algoritmo e Estrutura de Dados 3: Curso de Sistemas de Informação
Implementação de um editor de texto com dicionário de palavras

Implementar um editor de texto com um dicionário de dados.

Implementar em C#;

O editor deverá reconhecer as palavras que foram carregadas ou digitadas pelo usuário no editor;

O editor deverá estar preparado para abrir um arquivo texto já existente ou permitir editar um novo.

Deverá ser implementada rotina que faça a validação de todas as palavras do texto, verificando se as palavras estão ou não no dicionário da aplicação;

A rotina não deverá diferenciar palavras em maiúsculo ou minúsculo;

As palavras que não estiverem no dicionário deverão ser marcadas no editor e deverá ser possível ao usuário incluir ou não cada palavra no dicionário; 

O dicionário deverá ser armazenado em arquivo físico, de forma a poder ser reutilizado entre várias execuções do programa.

A forma de armazenamento ficará à escolha do grupo;

O dicionário deverá ser implementado em um vetor, que deverá ser carregado na inicialização do programa;

Para garantir que o vetor esteja sempre ordenado, deverá ser executada operação de ordenamento do vetor assim que for carregado;

Cada vez que for inserida uma nova palavra, essa deverá ser incluída no final do vetor e deverá ser executada a rotina de ordenação novamente.

Requisitos:

Todos os grupos deverão implementar três algoritmos de ordenação:

Bolha;

Inserção;

Heap;

Deverá ser feita a avaliação de Θ para os dois algoritmos;

Deverão ser realizados testes com várias massas de dados, contendo quantidades de palavras diferentes, e deverá ser realizada uma análise de qual situação é melhor utilizar cada um dos dois algoritmos, em termos de complexidade Θ, tempo de execução e facilidade de implementação.
