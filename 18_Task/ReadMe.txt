﻿18 - Условные операторы и циклы
ДЗ: Бой с боссом

Легенда:
Вы - герой и у вас есть несколько умений, 
которые вы можете использовать против Босса. 
Вы должны уничтожить босса и только после этого будет вам покой. 

Формально:
Перед вами Босс, у которого есть определенное количество жизней и атака. 
Атака может быть как всегда одной и той же, 
так и определяться рандомом в начале раунда. 
У Босса обычная атака. 
Босс должен иметь возможность убить героя.

У героя есть 4 умения
1. Обычная атака
2. Огненный шар, который тратит ману
3. Взрыв. Можно вызывать, только если был использован огненный шар. 
Для повторного применения надо повторно использовать огненный шар.
4. Лечение. Восстанавливает здоровье и ману, 
но не больше их максимального значения. 
Можно использовать ограниченное число раз.

Если пользователь ошибся с вводом команды или не выполнилось условие, 
то герой пропускает ход и происходит атака Босса
Программа завершается только после смерти босса или смерти пользователя, 
а если у вас возможно одновременно убить друг друга, то надо сообщить о ничье. 