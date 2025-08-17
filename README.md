# Tic Tac Toe
тестовое задание : https://docs.google.com/document/d/19W7mZq51gZLAmcSCANrBz8aSoQP85vns/edit?tab=t.0

<pre>
<b>Инструкция по запуску</b>
-скачать архив "Tic Tac Toe.rar" из папки "game_build";
-разархивировать архив;
-запустить исполняемый файл "Tic Tac Toe.exe";
-после подключения к серверу можно играть;
-для быстрой игры нажать кнопку "Случайный соперник". 
Если другой игрок нажмёт эту кнопку, то игра сведёт вместе этих игроков, и можно начинать игру;
-также можно создать комнату и передать её код другому игроку, если хотите поиграть с товарищем.

<b>Краткое описание архитектурных решений</b>
Основная логика игры находится в папке Core.
Её основа - класс Game (начать игру, сделать ход), 
который использует класс GameBoard(игровое поле), 
в свою очередь использующий класс BoardCell (клетка поля).
Все действия идут через метод MoveTurn(...) класса Game. 
Интерфейс и сетевое взаимодействие реагируют на события (начало, окончание игры) 
или изменения реактивных свойств из Core-классов (последний ход, содержимое клетки поля).
В папке Network - все классы с сетевым взаимодействием через PUN2.
В папке View - классы, отображающие данные на UI.

<b>Используемые технологии</b>
Сетевой фрэймворк - PUN2
DI контейнер - VContainer
Реактивное взаимодействие - UniRx
</pre>
<br><br>
<b>Скриншоты из игры</b>
<img width="1457" height="787" alt="image" src="https://github.com/user-attachments/assets/eaf39bba-5f12-47a2-b7e2-d315e2f49be0" />
<img width="1457" height="787" alt="image" src="https://github.com/user-attachments/assets/4916fe00-e083-4d79-8150-e5d78e693f01" />
<img width="1457" height="787" alt="image" src="https://github.com/user-attachments/assets/65200d46-7260-47fa-9ba9-bb4fb3e25e8b" />
<img width="1457" height="787" alt="image" src="https://github.com/user-attachments/assets/f74d66f0-ba16-4fe3-97d1-cd3670d40760" />
<img width="1457" height="787" alt="image" src="https://github.com/user-attachments/assets/4d427db5-db81-4078-aa7f-1d6155e78b1b" />
<img width="1457" height="787" alt="image" src="https://github.com/user-attachments/assets/f2133138-57b6-4260-9ef5-f9034f537053" />
<img width="1457" height="787" alt="image" src="https://github.com/user-attachments/assets/91e83592-a97d-4bc6-b090-3bb01b731389" />
<img width="1457" height="787" alt="image" src="https://github.com/user-attachments/assets/e17d68b3-fcab-45ad-83f6-e2f0dbcf6e88" />







