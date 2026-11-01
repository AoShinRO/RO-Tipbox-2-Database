# 🌀 Tipbox Generator — RO-DB-Translator

> Sua Database Completa de RO em 1 Clique

![Windows](https://img.shields.io/badge/OS-Windows_WPF-blue?style=for-the-badge&logo=windows)
![.NET](https://img.shields.io/badge/.NET_Framework-C%23-purple?style=for-the-badge&logo=dotnet)
![Ragnarok](https://img.shields.io/badge/Community-rAthena_Emulation-orange?style=for-the-badge)
[![Ask DeepWiki](https://deepwiki.com/badge.svg)](https://deepwiki.com/AoShinRO/RO-DB-Translator)

Cansado de editar a **Dicas (Tipbox)** e o **Guia de Aventura** do Ragnarok na mão, linha por linha? O **Tipbox Generator** faz o trabalho pesado por você.

---

## 🎯 O que ele faz

Ele pega os arquivos que você **já tem** no seu servidor e no seu client e transforma tudo numa database completa e navegável dentro do jogo — sincronizando **monstros, itens, cartas, armas e armaduras** de forma automática.

Com ele o jogador consulta, direto na janela de Dicas, coisas como:

* **Monstros:** stats completos (HP, ATK, DEF, raça, elemento, tamanho), lista de **drops com porcentagem real**, drops de MVP e **em quais mapas o monstro aparece** (com quantidade e navegação por clique)
* **Cartas e Itens:** descrição, efeitos, e **quais monstros dropam** aquele item
* **Guia de Aventura:** abas de **Monster, Weapon e Armor** montadas automaticamente, organizadas por level e tipo

Tudo aparece bonitinho como nas imagens: com imagem do card/coleção, links clicáveis e navegação de mapa.

---

## 🖱️ Por que é fácil de usar

A interface é uma janela só. Você **seleciona os arquivos** e clica em **"Generate"**. Pronto.

### 📥 Arquivos de entrada

Os arquivos que ele lê são os padrões do seu emulador e client:

| Arquivo | Origem |
| :--- | :--- |
| `num2cardillustnametable.txt` | Client |
| `iteminfo.lua` / `iteminfo.lub` | Client |
| `mob_db.yml` | Server |
| `item_db_etc.yml` | Server |
| `item_db_equip.yml` | Server |
| `item_db_usable.yml` | Server |
| Pasta de **spawn** dos mobs | Server |

### 📤 Arquivos gerados

E ele **entrega prontos**:

| Arquivo | Conteúdo |
| :--- | :--- |
| `tipbox.lub` | Database de Dicas (monstros, itens, cartas, drops e mapas) |
| `guide_monster.lub` | Guia de Aventura — aba Monster |
| `guide_weapon.lub` | Guia de Aventura — aba Weapon |
| `guide_armor.lub` | Guia de Aventura — aba Armor |

---

## ⚙️ Recursos que fazem a diferença

* **Suporte a ANSI e UTF-8** por arquivo (checkbox "is ANSI?") — sem dor de cabeça com acentuação/encoding
* **Barra de progresso** e status em tempo real durante a geração
* Opção **"dropable only"**: gera a database só com itens que realmente dropam, deixando tudo limpo e relevante
* **Salva suas configurações** — os caminhos ficam guardados pra próxima vez

---

## 🚀 Build & Deployment

### Pré-requisitos
* Windows 10 / 11
* .NET Framework 4.8+
* Visual Studio 2022

## 💾 Download & Quick Start

### 🚀 Release Pré-Compilada
Se você não quiser compilar o código-fonte manualmente, baixe o executável pronto direto na seção de **[GitHub Releases](https://github.com/AoShinRO/RO-DB-Translator/releases)**.

### Passos para compilação:
1. Clone o repositório:
```bash
   git clone https://github.com/AoShinRO/RO-DB-Translator.git
```
2. Abra o arquivo de solution (`.sln`) no Visual Studio.
3. Defina a configuração de build para `Release`.
4. Compile a solution (`Ctrl + Shift + B`).
5. Os binários compilados estarão na pasta `\bin\Release\`.

---

## ⚖️ Disclaimer / Aviso Legal

Este projeto é uma utilidade open-source desenvolvida estritamente para fins educacionais e desenvolvimento de ambientes de servidores privados (emulação rAthena/Hercules). Não é afiliado, associado, autorizado ou endossado pela Gravity Co., Ragnarok Online ou rAthena.
