Sua Database Completa de RO em 1 Clique

Cansado de editar a **Dicas (Tipbox)** e o **Guia de Aventura** do Ragnarok na mão, linha por linha? O **Tipbox Generator** faz o trabalho pesado por você.

### O que ele faz
Ele pega os arquivos que você **já tem** no seu servidor e no seu client e transforma tudo numa database completa e navegável dentro do jogo — sincronizando **monstros, itens, cartas, armas e armaduras** de forma automática.

Com ele o jogador consulta, direto na janela de Dicas, coisas como:
- **Monstros:** stats completos (HP, ATK, DEF, raça, elemento, tamanho), lista de **drops com porcentagem real**, drops de MVP e **em quais mapas o monstro aparece** (com quantidade e navegação por clique)
- **Cartas e Itens:** descrição, efeitos, e **quais monstros dropam** aquele item
- **Guia de Aventura:** abas de **Monster, Weapon e Armor** montadas automaticamente, organizadas por level e tipo

Tudo aparece bonitinho como nas imagens: com imagem do card/coleção, links clicáveis e navegação de mapa.

### Por que é fácil de usar
A interface é uma janela só. Você **seleciona os arquivos** e clica em **"Generate"**. Pronto.

Os arquivos que ele lê são os padrões do seu emulador e client:
- `num2cardillustnametable.txt` e `iteminfo.lua/lub` (client)
- `mob_db.yml`, `item_db_etc.yml`, `item_db_equip.yml`, `item_db_usable.yml` (server)
- a pasta de **spawn** dos mobs

E ele **entrega prontos** os arquivos `tipbox.lub`, `guide_monster.lub`, `guide_weapon.lub` e `guide_armor.lub`.

### Recursos que fazem a diferença
- **Suporte a ANSI e UTF-8** por arquivo (checkbox "is ANSI?") — sem dor de cabeça com acentuação/encoding
- **Barra de progresso** e status em tempo real durante a geração
- Opção **"dropable only"**: gera a database só com itens que realmente dropam, deixando tudo limpo e relevante
- **Salva suas configurações** — os caminhos ficam guardados pra próxima vez
