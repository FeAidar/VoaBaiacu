# VoaBaiacu
Aprendendo a utilizar o Github e fazendo um HyperCasual no processo.

#Jogo HyperCasual feito na Unity, a ideia é ajudar um baiacu que quer voar.
Pra isso o jogador desenha linhas na tela para ajudar o peixe a tomar impulso e se manter no ar.
O jogo continua até o momento em que o jogador não consegue salvar o Baiacu da água poluída.

#Elementos:
--Linhas 
= são desenhadas pelo jogador, arrastando o dedo na tela.
= Limite de três linhas na tela por vez
= O tamanho também é limitado
= Elas desaparecem após três segundos caso não haja colisão.
= Elas desaparecem imediatamente ao colidirem com o Baiacu ou Bomba
= Adicionam um valor pequeno de pontos ao score se o Baiacu as toca

--Bombas
= São aleatóriamente spawnadas
= Possuem peso maior e velocidade menor que o Baiacu
= Interagem com todos os elementos que o Baiacu interage
= Se cairem na àgua, tornam o mar poluído
= Se explodirem no ar, adicionam grande quantia de pontos ao score.

--Boias
= São aleatóriamente spawnadas
= Não se movimentam
= Se o Baiacu conseguir passar pelo centro delas, adicionam grande quantia de pontos ao score.

--Mar
= Quando limpo, apenas aumenta o impulso do Baiacu.
= Quando poluído, caso o jogador não tenha power-ups, causa morte e fim de jogo.



Ganho de pontos:
+ 10 Pontos por toque do Baiacu em linhas desenhas
+ 250 Pontos por passagem do Baiacu dentro de Bóias
+ 300 Pontos por bomba detonada no ar
- 10 Pontos por toque do Baiacu na água do mar

Highscore:
O jogo registra sua última pontuação mais alta e o notifica quando consegue ultrapassá-la.

Variação de Dinâmica
O jogo simula a passagem de um dia, tendo mudanças de background e de dinâmica de Spawn de Bóias e Bombas.
Ao fim do dia, o ciclo recomeça.
-- Duração de um ciclo > 10 Minutos

--Manhã
= Bomba > 30 Segundos com variação de 15% desse tempo
= Boia  > 20 Segundos com variação de 10% desse tempo

-- Tarde
= Bomba > 10 Segundos com variação de 15% desse tempo
= Boia  > 35 Segundos com variação de 05% desse tempo

-- Noite
= Bomba > 40 Segundos com variação de 30% desse tempo
= Boia  > 15 Segundos com variação de 10% desse tempo



Condição de morte:
Quando uma bomba cai no mar, ela poluí a água. Se o Baiacu tocar a àgua nesse estado, ele morre.



Duração do Mar Poluído:
20 segundos desde o momento que peixes mortos boiam na água.


Power-Ups:
+ 3 Máscaras de Toxinas, assim o Baiacu pode tocar a água poluída sem risco de morte.
+ 3 Paraquedas, assim ele se torna mais leve e lento, facilitando evitar água.


Condição de Conquista do Power-Up
A cada mil pontos do último power-up, o jogador vai receber aleatóriamente um benefício.


Condição de Perda do Power-Up
-1 Máscara quando toca o mar poluído
-1 Paraquedas quando toca uma linha desenhada



