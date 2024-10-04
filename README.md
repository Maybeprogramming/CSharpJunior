#  Основы программирования

<details>
<summary>01 - Задача Переменные </summary>
Попрактикуйтесь в создании переменных, 
объявить 10 переменных разных типов. 
Напоминание: переменные именуются с маленькой буквы, 
если название состоит из нескольких слов, 
то комбинируем их следующим образом - названиеПеременной. 
Также имя всегда должно отражать суть того, что хранит переменная. 
Для сдачи ДЗ требуется сдать код, 
который вы можете загрузить на https://gist.github.com/
или https://pastebin.com/ 
Это не сайт https://github.com/ где надо будет разбираться с работой git, 
а те сайты, на которые можно скопировать код
</details>

<details>
<summary>02 - Задача Что выведется в консоль и почему? </summary>
int a = 10;
int b = 38;
int c = (31 – 5 * a) / b;
Console.WriteLine(c);

> ВАЖНО!!! Не запускать код и попытаться подумать головой. 
> Также надо написать ответ “Почему?”

Приоритет арифметических операций:
В скобках сначала выполнится умножение 5 * 10 = 50,
далее вычитание 31-50 = -19,
и деление результата из скобок -19/38 = 0
Почему 0? Потому что тип переменной с это int - целочисленный, 
и все значения после запятой игнорируются/пропускаются
</details>

<details>
<summary>03 - Задача Работа со строками</summary>
  Вы задаете вопросы пользователю, 
по типу "как вас зовут", 
"какой ваш знак зодиака" и тд, 
после чего, по данным, которые он ввел, 
формируете небольшой текст о пользователе. 
"Вас зовут Алексей, вам 21 год, вы водолей и работаете на заводе."
</details>

<details>
<summary>04 - Задача Картинки</summary>
  На экране, в специальной зоне, выводятся картинки, 
по 3 в ряд (условно, ничего рисовать не надо). 
Всего у пользователя в альбоме 52 картинки. 
Код должен вывести, сколько полностью заполненных рядов 
можно будет вывести, и сколько картинок будет сверх меры.

В качестве решения ожидаются объявленные переменные 
с необходимыми значениями и, основываясь на значениях переменных, 
вывод необходимых данных. 
По задаче требуется выполнить простые математические действия.
</details>

<details>
<summary>05 - Задача Перстановка местами значений</summary>
  Даны две переменные. 
Поменять местами значения двух переменных. 
Вывести на экран значения переменных до перестановки и после.
К примеру, есть две переменные имя и фамилия, 
они сразу инициализированные, но данные не верные, перепутанные. 
Вот эти данные и надо поменять местами через код.
</details>

<details>
<summary>06 - Задача Магазин кристаллов</summary>
  Легенда:
Вы приходите в магазин и хотите купить за своё золото кристаллы. 
В вашем кошельке есть какое-то количество золота, 
продавец спрашивает у вас, 
сколько кристаллов вы хотите купить? 
После сделки у вас остаётся какое-то количество золота 
и появляется какое-то количество кристаллов.

Формально:
При старте программы пользователь вводит начальное количество золота. 
Потом ему предлагается купить какое-то количество 
кристаллов по цене N(задать в программе самому). 
Пользователь вводит число и его золото конвертируется в кристаллы. 
Остаток золота и кристаллов выводится на экран.

Проверять на то, что у игрока достаточно денег ненужно.
</details>

<details>
<summary>07 - Задача Поликлиника </summary>
  Легенда:

Вы заходите в поликлинику и видите огромную очередь из старушек, вам нужно рассчитать время ожидания в очереди.

Формально:
Пользователь вводит кол-во людей в очереди.
Фиксированное время приема одного человека всегда равно 10 минутам.
Пример ввода: Введите кол-во старушек: 14
Пример вывода: "Вы должны отстоять в очереди 2 часа и 20 минут."
</details>

#  Условные операторы и циклы

<details>
<summary>08 - Задача Освоение циклов</summary>
  При помощи циклов вы можете повторять один и тот же код множество раз.
Напишите простейшую программу, 
которая выводит указанное(установленное) 
пользователем сообщение заданное количество раз. 
Количество повторов также должен ввести пользователь.
</details>

<details>
<summary>09 - Задача Контроль выхода</summary>
  Написать программу, которая будет выполняться до тех пор, пока не будет введено слово exit.
Помните, в цикле должно быть условие, которое отвечает за то, когда цикл должен завершиться.
Это нужно, чтобы любой разработчик взглянув на ваш код, понял четкие границы вашего цикла.
</details>

<details>
<summary>10 - Задача Последовательность</summary>
  Нужно написать программу (используя циклы, обязательно пояснить выбор 
вашего цикла), чтобы она выводила следующую 
последовательность 5 12 19 26 33 40 47 54 61 68 75 82 89 96
Нужны переменные для обозначения чисел в условии цикла.

ОТВЕТ:
Выбрал цикл "for" так как есть четкое начало, конец и шаг итераций.
Т.е. для нас понятно когда цикл начнётся, когда закончится и с каким шагом будет выполняться.
</details>

<details>
<summary>11 - Задача Сумма чисел</summary>
  С помощью Random получить число number, которое не больше 100. 
Найти сумму всех положительных чисел меньше number (включая число), 
которые кратные 3 или 5. 
К примеру, это числа 3, 5, 6, 9, 10, 12, 15 и т.д.)
</details>

<details>
<summary>12 - Задача Конвертер валют</summary>
  Написать конвертер валют (3 валюты).

У пользователя есть баланс в каждой из представленных валют. 
Он может попросить сконвертировать часть баланса 
с одной валюты в другую. Тогда у него с баланса 
одной валюты снимется X и зачислится на баланс другой Y. 
Курс конвертации должен быть просто прописан в программе.
По имени переменной курса конвертации должно быть понятно, 
из какой валюты в какую валюту конвертируется.
Должна выполняться однотипная операция 
или везде умножение "*" 
или деление "/". 
Для чего это нужно подробнее позже узнаете в разделе "Функции". 
Но придётся объявить коэффициенты на все случаи.

Программа должна завершиться тогда, когда это решит пользователь.

Дополнительно: 
Если решение строится на switch, 
то принято работать с константами (в остальных случаях объявляются переменные). 
Для каждого case следует объявить константу.
Пример:
const string CommandExit = "exit";

case CommandExit:
break;

Константы объявляются перед блоком переменных и 
отделяются от них пустой строкой. 
Константы именуются с большой буквы. 
Если константа создана для связки 
консольное меню + switch (case) 
к имени константы добавляется Command или Menu 
- это передает суть константы, превращая ее в существительное, 
а не глагол и улучшает читаемость кода.
</details>

<details>
<summary>13 - Задача Консольное меню</summary>
  При помощи всего, что вы изучили, создать приложение, 
которое может обрабатывать команды. 
Т.е. вы создаете меню, ожидаете ввода нужной команды, 
после чего выполняете действие, которое присвоено этой команде.

Примеры команд (требуется 4-6 команд, придумать самим):

SetName – установить имя;
ChangeConsoleColor- изменить цвет консоли;
SetPassword – установить пароль;
WriteName – вывести имя (после ввода пароля);
Esc – выход из программы.

Программа не должна завершаться после ввода, 
пользователь сам должен выйти из программы при помощи команды.
</details>

<details>
<summary>14 - Задача Вывод имени</summary>
  Вывести имя в прямоугольник из символа, который введет сам пользователь.

Вы запрашиваете имя, после запрашиваете символ, 
а после отрисовываете в консоль его имя в прямоугольнике из его символов.

Пример:

Alexey

%

%%%%%%
% Alexey %
%%%%%%

Примечание:

Длину строки можно всегда узнать через свойство Length
string someString = “Hello”;
Console.WriteLine(someString.Length); //5
</details>

<details>
<summary>15 - Задача Программа под паролем</summary>
  Создайте переменную типа string, 
в которой хранится пароль для доступа к тайному сообщению. 
Пользователь вводит пароль, 
далее происходит проверка пароля на правильность, 
и если пароль неверный, то попросите его ввести пароль ещё раз. 
Если пароль подошёл, выведите секретное сообщение.

Если пользователь неверно ввел пароль 3 раза, программа завершается
</details>

<details>
<summary>16 - Задача Кратные числа</summary>
  Дано N (1 ≤ N ≤ 27). 
Найти количество трехзначных натуральных чисел, 
которые кратны N. 
Операции деления (/, %) не использовать. 
А умножение не требуется.
Число N всего одно, его надо получить в нужном диапазоне.
</details>

<details>
<summary>17 - Задача Степень двойки</summary>
  Найдите минимальную степень двойки, превосходящую заданное число.
К примеру, для числа 4 будет 2 в степени 3, то есть 8. 4<8.
Для числа 29 будет 2 в степени 5, то есть 32. 29<32.
В консоль вывести число (лучше получить от Random), 
степень и само число 2 в найденной степени.
</details>

<details>
<summary>18 - Задача Скобочное выражение </summary>
  Дана строка из символов '(' и ')'. 
Определить, является ли она корректным скобочным выражением. 
Определить максимальную глубину вложенности скобок.

Пример “(()(()))” - строка корректная и максимум глубины равняется 3.
Пример не верных строк: "(()", "())", ")(", "(()))(()"

Для перебора строки по символам можно использовать цикл foreach, 
к примеру будет так foreach (var symbol in text)
Или цикл for(int i = 0; i < text.Length; i++) и 
дальше обращаться к каждому символу внутри цикла как text[i]
Цикл нужен для перебора всех символов в строке.
</details>

<details>
<summary>19 - Задача Бой с боссом </summary>
  Легенда: 
Вы – теневой маг(можете быть вообще хоть кем) и 
у вас в арсенале есть несколько заклинаний, 
которые вы можете использовать против Босса. 
Вы должны уничтожить босса и только после этого будет вам покой.

Формально: 
перед вами босс, у которого есть определенное кол-во жизней и 
определенный ответный урон. 
У вас есть 4 заклинания для нанесения урона боссу. 
Программа завершается только после смерти босса или смерти пользователя.

Пример заклинаний

Рашамон – призывает теневого духа для нанесения атаки (Отнимает 100 хп игроку)

Хуганзакура (Может быть выполнен только после призыва теневого духа), наносит 100 ед. урона

Межпространственный разлом – позволяет скрыться в разломе и восстановить 250 хп. 
Урон босса по вам не проходит

Заклинания должны иметь схожий характер, 
то есть иметь как одиночное действие, 
так и какие-то условия выполнения (пример - Хуганзакура). 
Одно заклинание влияет на другое, тоже нужно для практики. 
Босс должен иметь возможность убить пользователя, возможна и ничья.

Не переусложняйте задачу излишними взаимосвязями. 
Вы ещё сможете реализовать творческие задумки далее по курсу. 
Например, "Гладиаторские бои" в разделе ООП 
(Знакомство с классами позволит писать лаконичней более сложный функционал)
</details>

#  Массивы

<details>
<summary>20 - Задача Работа с конкретными строками/столбцами </summary>
  Дан двумерный массив.
Вычислить сумму второй строки и произведение первого столбца. 
Вывести исходную матрицу и результаты вычислений.
</details>

<details>
<summary>21 - Задача Наибольший элемент</summary>
  Найти наибольший элемент матрицы A(10,10) и 
записать ноль в те ячейки, где он находятся. 
Вывести наибольший элемент, исходную и полученную матрицу.
Массив под измененную версию не нужен.
</details>

<details>
<summary>22 - Задача Локальные максимумы </summary>
  Дан одномерный массив целых чисел из 30 элементов.
Найдите все локальные максимумы и вывести их. 
(Элемент является локальным максимумом, если он больше своих соседей)
Крайний элемент является локальным максимумом, если он больше своего соседа.
Программа должна работать с массивом любого размера.
Массив всех локальных максимумов не нужен.
</details>

<details>
<summary>23 - Задача Динамический массив </summary>
  Пользователь вводит числа, и программа их запоминает.
Как только пользователь введёт команду sum, программа выведет сумму всех веденных чисел.

Выход из программы должен происходить только в том случае, если пользователь введет команду exit.
Если введено не sum и не exit, значит это число и его надо добавить в массив.

Программа должна работать на основе расширения массива.

Внимание, нельзя использовать List<T> и Array.Resize
</details>

<details>
<summary>24 - Задача Подмассив повторений чисел</summary>
  В массиве чисел найдите самый длинный подмассив из одинаковых чисел.
Дано 30 чисел. Вывести в консоль сам массив, число, 
которое само больше раз повторяется подряд и количество повторений.
Дополнительный массив не надо создавать.
Пример: {5, 5, 9, 9, 9, 5, 5} - число 9 повторяется большее число раз подряд.
</details>

<details>
<summary>25 - Задача Сортировка чисел</summary>
  Дан массив чисел (минимум 10 чисел). 
Надо вывести в консоль числа отсортированы, от меньшего до большего.
Нельзя использовать Array.Sort. 
Можно найти подходящий алгоритм сортировки и использовать его для задачи.
</details>

<details>
<summary>26 - Задача SPLIT </summary>
  Дана строка с текстом, используя метод строки String.Split() 
получить массив слов, которые разделены пробелом в тексте и 
вывести массив, каждое слово с новой строки.

!!! объявлять переменные для ' ', ',', '.', '\n', '?', ':', '…'
</details>

<details>
<summary>27 - Задача Сдвиг значений массива </summary>
  Дан массив чисел. 
Нужно его сдвинуть циклически на указанное пользователем значение позиций влево, 
не используя других массивов. 
Пример для сдвига один раз: 
{1, 2, 3, 4} => {2, 3, 4, 1}

Можно было ещё исключить отсутствие лишних итераций, 
если пользователь введёт количество сдвигов больше, чем длина массива. 
- Пример: userInput= userInput % numbers.Length; 2) Lenth - Length*
</details>

#  Функции

<details>
<summary>28 - Задача Кадровый учет</summary>
  Будет 2 массива: 
1) фио 
2) должность.

Описать функцию заполнения массивов досье, 
функцию форматированного вывода, 
функцию поиска по фамилии и функцию удаления досье.

Функция расширяет уже имеющийся массив на 1 и дописывает туда новое значение.

Программа должна быть с меню, которое содержит пункты:

- 1) добавить досье
- 2) вывести все досье (в одну строку через “-” фио и должность с порядковым номером в начале)
- 3) удалить досье (Массивы уменьшаются на один элемент. Нужны дополнительные проверки, чтобы не возникало ошибок)
- 4) поиск по фамилии
- 5) выход
</details>

<details>
<summary>29 - Задача UIELEMENT </summary>
  Разработайте функцию, которая рисует некий бар (Healthbar, Manabar) в определённой позиции. 
Она также принимает некий закрашенный процент.
При 40% бар выглядит так:
-[####______]
</details>

<details>
<summary>30 - Задача READINT</summary>
  Написать функцию, которая запрашивает число у пользователя 
(с помощью метода Console.ReadLine() ) и 
пытается сконвертировать его в тип int (с помощью int.TryParse())

Если конвертация не удалась у пользователя запрашивается число 
повторно до тех пор, пока не будет введено верно. 
После ввода, который удалось преобразовать в число, число возвращается.

P.S Задача решается с помощью циклов
P.S Также в TryParse используется модфикатор параметра out
</details>

<details>
<summary>31 - Задача BRAVE NEW WORLD</summary>
  Сделать игровую карту с помощью двумерного массива. 
Сделать функцию рисования карты. 
Помимо этого, дать пользователю возможность перемещаться по карте и 
взаимодействовать с элементами (например пользователь не может пройти сквозь стену)
Все элементы являются обычными символами
</details>

<details>
<summary>32 - Задача КАНЗАС СИТИ ШАФЛ </summary>
  Реализуйте функцию Shuffle, которая перемешивает элементы массива в случайном порядке.
</details>

#  Коллекции

<details>
<summary>33 - Задача Толковый словарь</summary>
  Создать программу, 
которая принимает от пользователя слово и выводит его значение. 
Если такого слова нет, то следует вывести соответствующее сообщение.
</details>

<details>
<summary>34 - Задача Очередь в магазине</summary>
  У вас есть множество целых чисел. 
Каждое целое число - это сумма покупки.
Вам нужно обслуживать клиентов до тех пор, 
пока очередь не станет пуста.
После каждого обслуженного клиента 
деньги нужно добавлять на наш счёт и выводить его в консоль.
После обслуживания каждого клиента 
программа ожидает нажатия любой клавиши, 
после чего затирает консоль и 
по новой выводит всю информацию, только уже со следующим клиентом
</details>

<details>
<summary>35 - Задача Динамический массив продвинутый</summary>
  В массивах вы выполняли задание "Динамический массив"
Используя всё изученное, 
напишите улучшенную версию динамического массива
(не обязательно брать своё старое решение)
Задание нужно, чтобы вы освоились с List и 
прощупали его преимущество.
Проверка на ввод числа обязательна.

Пользователь вводит числа, и программа их запоминает.
Как только пользователь введёт команду sum, программа выведет сумму всех веденных чисел.

Выход из программы должен происходить только в том случае, если пользователь введет команду exit.
</details>

<details>
<summary>36 - Задача Кадровый учет продвинутый</summary>
  В функциях вы выполняли задание "Кадровый учёт"
Используя одну из изученных коллекций, 
вы смогли бы сильно себе упростить код выполненной программы, 
ведь у нас данные, это ФИО и позиция.
Поиск в данном задании не нужен.

1) добавить досье
2) вывести все досье (в одну строку через “-” фио и должность)
3) удалить досье
4) выход
</details>

<details>
<summary>37 - Задача Объединение в одну коллекцию </summary>
  Есть два массива строк. 
Надо их объединить в одну коллекцию, 
исключив повторения, 
не используя Linq. 
Пример: {"1", "2", "1"} + {"3", "2"} => {"1", "2", "3"}
</details>

#  ООП

<details>
<summary>38 - Задача Работа с классами </summary>
  Создать класс игрока, с полями, 
содержащими информацию об игроке и методом, который выводит информацию на экран.
В классе обязательно должен быть конструктор
</details>

<details>
<summary>39 - Задача Работа со свойствами </summary>
  Создать класс игрока, у которого есть поля с его положением в x,y.
Создать класс отрисовщик, с методом, который отрисует игрока.

Попрактиковаться в работе со свойствами.
</details>

<details>
<summary>40 - Задача База данных игроков </summary>
  Реализовать базу данных игроков и методы для работы с ней.
У игрока может быть уникальный номер, ник, уровень, флаг – забанен ли он(флаг - bool).
Реализовать возможность добавления игрока, 
бана игрока по уникальный номеру, 
разбана игрока по уникальный номеру и удаление игрока.
Создание самой БД не требуется, задание выполняется инструментами, 
которые вы уже изучили в рамках курса. 
Но нужен класс, который содержит игроков и её можно назвать "База данных".
</details>

<details>
<summary>41 - Задача Колода карт </summary>
  Есть колода с картами. 
Игрок достает карты, пока не решит, что ему хватит карт 
(может быть как выбор пользователя, так и количество сколько карт надо взять). 
После выводиться вся информация о вытянутых картах.
Возможные классы: Карта, Колода, Игрок.
</details>

<details>
<summary>42 - Задача Хранилище книг </summary>
  Создать хранилище книг.
Каждая книга имеет название, автора и год выпуска (можно добавить еще параметры). 
В хранилище можно добавить книгу, убрать книгу, 
показать все книги и показать книги по указанному параметру 
(по названию, по автору, по году выпуска).
Про указанный параметр, к примеру нужен конкретный автор, 
выбирается показ по авторам, запрашивается у пользователя автор и 
показываются все книги с этим автором.
</details>

<details>
<summary>43 - Задача Магазин </summary>
  Существует продавец, он имеет у себя список товаров, 
и при нужде, может вам его показать, 
также продавец может продать вам товар. 
После продажи товар переходит к вам, и вы можете также посмотреть свои вещи.

Возможные классы – игрок, продавец, товар.
Вы можете сделать так, как вы видите это.
</details>

<details>
<summary>44 - Задача Конфигуратор пассажирских поездов </summary>
  У вас есть программа, которая помогает пользователю составить план поезда.
Есть 4 основных шага в создании плана:
-Создать направление - создает направление для поезда(к примеру Бийск - Барнаул)
-Продать билеты - вы получаете рандомное кол-во пассажиров, которые купили билеты на это направление
-Сформировать поезд - вы создаете поезд и добавляете ему столько вагонов(вагоны могут быть разные по вместительности), сколько хватит для перевозки всех пассажиров.
-Отправить поезд - вы отправляете поезд, после чего можете снова создать направление.
В верхней части программы должна выводиться полная информация о текущем рейсе или его отсутствии.
</details>

<details>
<summary>45 - Задача Гладиаторские бои </summary>
  Создать 5 бойцов, пользователь выбирает 2 бойцов и они сражаются друг с другом до смерти. 
У каждого бойца могут быть свои статы.
Каждый игрок должен иметь особую способность для атаки, которая свойственна только его классу!
Если можно выбрать одинаковых бойцов, то это не должна быть одна и та же ссылка на одного бойца, чтобы он не атаковал сам себя.
Пример, что может быть уникальное у бойцов. 
Кто-то каждый 3 удар наносит удвоенный урон, 
другой имеет 30% увернуться от полученного урона, 
кто-то при получении урона немного себя лечит. 
Будут новые поля у наследников. 
У кого-то может быть мана и это только его особенность.
</details>

<details>
<summary>46 - Задача Супермаркет </summary>
  Написать программу администрирования супермаркетом.
В супермаркете есть очередь клиентов.
У каждого клиента в корзине есть товары, также у клиентов есть деньги.
Клиент, когда подходит на кассу, получает итоговую сумму покупки и старается её оплатить.
Если оплатить клиент не может, то он рандомный товар из корзины выкидывает до тех пор, пока его денег не хватит для оплаты.
Клиентов можно делать ограниченное число на старте программы.
Супермаркет содержит список товаров, из которых клиент выбирает товары для покупки.
</details>

<details>
<summary>47 - Задача Война </summary>
  Есть 2 взвода. 1 взвод страны один, 2 взвод страны два.
Каждый взвод внутри имеет солдат.
Нужно написать программу, которая будет моделировать бой этих взводов.
Каждый боец - это уникальная единица, он может иметь уникальные способности 
или же уникальные характеристики, такие как повышенная сила.
Побеждает та страна, во взводе которой остались выжившие бойцы.
Не важно, какой будет бой, рукопашный, стрелковый.
</details>

<details>
<summary>48 - Задача Аквариум </summary>
  Есть аквариум, в котором плавают рыбы. 
В этом аквариуме может быть максимум определенное кол-во рыб. 
Рыб можно добавить в аквариум или рыб можно достать из аквариума. 
(программу делать в цикле для того, чтобы рыбы могли “жить”)
Все рыбы отображаются списком, у рыб также есть возраст. 
За 1 итерацию рыбы стареют на определенное кол-во жизней и могут умереть. 
Рыб также вывести в консоль, чтобы можно было мониторить показатели.
</details>

<details>
<summary>49 - Задача Зоопарк </summary>
  Пользователь запускает приложение и перед ним находится меню, 
в котором он может выбрать, к какому вольеру подойти. 
При приближении к вольеру, пользователю выводится информация о том, 
что это за вольер, сколько животных там обитает, их пол и какой звук издает животное.
Вольеров в зоопарке может быть много, в решении нужно создать минимум 4 вольера.
</details>

<details>
<summary>50 - Задача Автосервис </summary>
  У вас есть автосервис, в который приезжают люди, чтобы починить свои автомобили.
У вашего автосервиса есть баланс денег и склад деталей.
Когда приезжает автомобиль, у него сразу ясна его поломка, 
и эта поломка отображается у вас в консоли вместе с ценой за починку
(цена за починку складывается из цены детали + цена за работу).
Поломка всегда чинится заменой детали, 
но количество деталей ограничено тем, 
что находится на вашем складе деталей.
Если у вас нет нужной детали на складе, 
то вы можете отказать клиенту, и в этом случае вам придется выплатить штраф.
Если вы замените не ту деталь, то вам придется возместить ущерб клиенту.
За каждую удачную починку вы получаете выплату за ремонт, которая указана в чек-листе починки.
Класс Деталь не может содержать значение “количество”. 
Деталь всего одна, за количество отвечает тот, кто хранит детали. 
При необходимости можно создать дополнительный класс для конкретной детали и работе с количеством.
</details>

#  LINQ

<details>
<summary>51 - Задача Поиск преступника </summary>
  У нас есть список всех преступников.
В преступнике есть поля: ФИО, 
заключен ли он под стражу, рост, вес, национальность.
Вашей программой будут пользоваться детективы.
У детектива запрашиваются данные 
(рост, вес, национальность), и детективу выводятся все преступники, 
которые подходят под эти параметры, 
но уже заключенные под стражу выводиться не должны.
</details>

<details>
<summary>52 - Задача Амнистия </summary>
  В нашей великой стране Арстоцка произошла амнистия!
Всех людей, заключенных за преступление "Антиправительственное", следует исключить из списка заключенных.
Есть список заключенных, каждый заключенный состоит из полей: ФИО, преступление.
Вывести список до амнистии и после.
</details>

<details>
<summary>53 - Задача Анархия в больнице </summary>
  У вас есть список больных(минимум 10 записей)
Класс больного состоит из полей: ФИО, возраст, заболевание.
Требуется написать программу больницы, в которой перед пользователем будет меню со следующими пунктами:
1)Отсортировать всех больных по фио
2)Отсортировать всех больных по возрасту
3)Вывести больных с определенным заболеванием
(название заболевания вводится пользователем с клавиатуры)
</details>

<details>
<summary>54 - Задача Топ игроков сервера </summary>
У нас есть список всех игроков(минимум 10). 
У каждого игрока есть поля: имя, уровень, сила. 
Требуется написать запрос для определения топ 3 игроков по уровню 
и топ 3 игроков по силе, после чего вывести каждый топ.
2 запроса получится.
</details>

<details>
<summary>55 - Задача Определение просрочки </summary>
Есть набор тушенки. У тушенки есть название, год производства и срок годности.
Написать запрос для получения всех просроченных банок тушенки.
Чтобы не заморачиваться, можете думать, что считаем только года, без месяцев.
</details>

<details>
<summary>56 - Задача Отчет о вооружении </summary>
Существует класс солдата. В нём есть поля: имя, вооружение, звание, срок службы(в месяцах).
Написать запрос, при помощи которого получить набор данных состоящий из имени и звания.
Вывести все полученные данные в консоль.
(Не менее 5 записей)
</details>

<details>
<summary>57 - Задача Объединение войск </summary>
Есть 2 списка в солдатами.
Всех бойцов из отряда 1, у которых фамилия начинается на букву Б, требуется перевести в отряд 2.
</details>
