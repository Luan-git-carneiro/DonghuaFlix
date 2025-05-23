# Documentação do Projeto DonghuaFlix

Este documento apresenta a documentação completa do projeto DonghuaFlix, uma plataforma de streaming de vídeo focada em donghua (animação chinesa). A documentação inclui diagramas UML, casos de uso, requisitos funcionais e não funcionais, e uma visão futura do projeto.

## Índice

1. [Visão Geral do Projeto](#visão-geral-do-projeto)
2. [Requisitos](#requisitos)
   - [Requisitos Funcionais](#requisitos-funcionais)
   - [Requisitos Não Funcionais](#requisitos-não-funcionais)
3. [Diagramas UML](#diagramas-uml)
   - [Diagrama de Classes](#diagrama-de-classes)
   - [Diagramas de Sequência](#diagramas-de-sequência)
   - [Diagrama de Componentes](#diagrama-de-componentes)
4. [Diagramas de Caso de Uso](#diagramas-de-caso-de-uso)
   - [Visão Geral](#visão-geral)
   - [Usuário Anônimo](#usuário-anônimo)
   - [Usuário Logado](#usuário-logado)
   - [Administrador](#administrador)
5. [Visão Futura](#visão-futura)
   - [Sistema de Comentários Avançado](#sistema-de-comentários-avançado)
   - [Sistema de Logs e Monitoramento](#sistema-de-logs-e-monitoramento)
   - [Arquitetura Avançada](#arquitetura-avançada)
6. [Conclusão](#conclusão)

## Visão Geral do Projeto

O DonghuaFlix é uma plataforma de streaming de vídeo especializada em donghua (animação chinesa). O projeto utiliza uma arquitetura moderna com backend em .NET Core 8.0 seguindo os princípios de arquitetura limpa/hexagonal, e frontend em React.

A plataforma permite que usuários naveguem no catálogo, busquem donghuas por tipo (filmes e séries) e assistam ao conteúdo. Usuários logados têm funcionalidades adicionais como adicionar aos favoritos, ver histórico de visualização e comentar. Administradores podem gerenciar o conteúdo, fazer upload de vídeos e gerenciar usuários.

## Requisitos

### Requisitos Funcionais

#### RF01 - Gerenciamento de Usuários
- RF01.1: O sistema deve permitir o cadastro de novos usuários
- RF01.2: O sistema deve permitir a autenticação de usuários
- RF01.3: O sistema deve diferenciar entre usuários comuns e administradores
- RF01.4: Administradores devem poder gerenciar contas de usuários

#### RF02 - Catálogo de Donghua
- RF02.1: O sistema deve exibir um catálogo de donghuas disponíveis
- RF02.2: O sistema deve permitir a busca de donghuas por título
- RF02.3: O sistema deve permitir a filtragem de donghuas por categorias (filmes/séries)
- RF02.4: O sistema deve permitir a filtragem de donghuas por gênero
- RF02.5: O sistema deve exibir informações detalhadas sobre cada donghua

#### RF03 - Reprodução de Vídeo
- RF03.1: O sistema deve permitir a reprodução de episódios/filmes
- RF03.2: O sistema deve suportar diferentes qualidades de vídeo
- RF03.3: O sistema deve manter o controle de progresso de visualização

#### RF04 - Funcionalidades para Usuários Logados
- RF04.1: Usuários logados devem poder adicionar donghuas aos favoritos
- RF04.2: Usuários logados devem poder visualizar seu histórico de visualização
- RF04.3: Usuários logados devem poder adicionar comentários aos donghuas/episódios

#### RF05 - Funcionalidades de Administração
- RF05.1: Administradores devem poder adicionar novos donghuas ao catálogo
- RF05.2: Administradores devem poder atualizar informações de donghuas existentes
- RF05.3: Administradores devem poder fazer upload de vídeos
- RF05.4: Administradores devem poder gerenciar episódios de séries

#### RF06 - Funcionalidades Futuras
- RF06.1: Sistema de comentários avançado com moderação
- RF06.2: Sistema de logs de monitoramento para análise de uso

### Requisitos Não Funcionais

#### RNF01 - Desempenho
- RNF01.1: O sistema deve carregar o catálogo em menos de 3 segundos
- RNF01.2: O sistema deve iniciar a reprodução de vídeo em menos de 5 segundos
- RNF01.3: O sistema deve suportar streaming adaptativo para diferentes larguras de banda

#### RNF02 - Escalabilidade
- RNF02.1: O sistema deve suportar pelo menos 1000 usuários simultâneos
- RNF02.2: O sistema deve ser capaz de escalar horizontalmente para atender a demanda crescente

#### RNF03 - Segurança
- RNF03.1: As senhas dos usuários devem ser armazenadas de forma criptografada (BCrypt)
- RNF03.2: O sistema deve implementar autenticação segura
- RNF03.3: O sistema deve proteger rotas administrativas
- RNF03.4: O sistema deve implementar proteção contra ataques comuns (CSRF, XSS, etc.)

#### RNF04 - Usabilidade
- RNF04.1: A interface do usuário deve ser responsiva e funcionar em dispositivos móveis e desktop
- RNF04.2: A navegação deve ser intuitiva e seguir padrões de UX modernos
- RNF04.3: O sistema deve fornecer feedback visual para ações do usuário

#### RNF05 - Disponibilidade
- RNF05.1: O sistema deve estar disponível 24/7 com uptime de pelo menos 99.9%
- RNF05.2: O sistema deve implementar mecanismos de recuperação de falhas

#### RNF06 - Manutenibilidade
- RNF06.1: O código deve seguir padrões de arquitetura limpa/hexagonal
- RNF06.2: O sistema deve ter testes automatizados
- RNF06.3: O sistema deve ser modular para facilitar atualizações e manutenção

## Diagramas UML

### Diagrama de Classes

O diagrama de classes representa a estrutura estática do sistema, mostrando as classes, seus atributos, métodos e relacionamentos.

### Diagramas de Sequência

#### Assistir Vídeo

Este diagrama mostra a sequência de interações quando um usuário assiste a um vídeo, incluindo a atualização do histórico de visualização.

#### Adicionar aos Favoritos

Este diagrama mostra a sequência de interações quando um usuário adiciona um donghua aos seus favoritos.

### Diagrama de Componentes

O diagrama de componentes mostra a organização e dependências entre os componentes do sistema, incluindo o frontend em React e o backend em .NET Core.

## Diagramas de Caso de Uso

### Visão Geral

Este diagrama mostra todos os atores do sistema (Usuário Anônimo, Usuário Logado e Administrador) e suas interações com as funcionalidades do sistema.

### Usuário Anônimo

Este diagrama detalha as funcionalidades disponíveis para usuários não autenticados, como navegar no catálogo, buscar donghuas, filtrar por categoria e assistir conteúdo.

### Usuário Logado

Este diagrama detalha as funcionalidades disponíveis para usuários autenticados, incluindo todas as funcionalidades do usuário anônimo, além de adicionar aos favoritos, ver histórico de visualização, adicionar comentários e gerenciar perfil.

### Administrador

Este diagrama detalha as funcionalidades disponíveis para administradores, incluindo todas as funcionalidades do usuário logado, além de adicionar donghua, atualizar donghua, fazer upload de vídeo, gerenciar episódios e gerenciar usuários.

## Visão Futura

### Sistema de Comentários Avançado

Este diagrama mostra a visão futura do sistema de comentários, incluindo funcionalidades como responder a comentários, curtir comentários, denunciar comentários, e moderação de comentários.

### Sistema de Logs e Monitoramento

Este diagrama mostra a visão futura do sistema de logs e monitoramento, incluindo coleta de métricas de uso, registro de atividades de usuários, monitoramento de desempenho do servidor, detecção de erros e exceções, e análise de padrões de visualização.

### Arquitetura Avançada

Este diagrama mostra a visão futura da arquitetura do sistema, incluindo componentes adicionais como CDN, API Gateway, Cache, NoSQL Database, Message Queue, e serviços de análise de dados e moderação automática.

## Conclusão

O projeto DonghuaFlix apresenta uma arquitetura robusta e moderna, utilizando .NET Core no backend com arquitetura limpa/hexagonal e React no frontend. A documentação apresentada neste documento fornece uma visão completa do sistema, incluindo requisitos, diagramas UML, casos de uso e visão futura.

Os diagramas UML mostram a estrutura estática e dinâmica do sistema, enquanto os diagramas de caso de uso detalham as interações dos diferentes tipos de usuários com o sistema. A visão futura apresenta possíveis evoluções do sistema, incluindo um sistema de comentários avançado, sistema de logs e monitoramento, e uma arquitetura mais robusta e escalável.