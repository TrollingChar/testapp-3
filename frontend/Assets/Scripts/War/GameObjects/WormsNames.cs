﻿namespace W3 {

    internal class WormsNames {

        private static string[] names = {
            "Тарг",
            "Имба",
            "Айс",
            "Травокур",
            "Эскобар",
            "Гуф",
            "Лирой Дженкинс",
            "Чувак",
            "Сыч",
            "Анон",
            "Ваншот",
            "Флэш",
            "Саргас",
            "Мако",
            "Кирс",
            "Баркс",
            "Трарт",
            "Ллалл",
            "Нуп",
            "Лойс",
            "Спуди",
            "Рашка",
            "Драшка",
            "Клем",
            "Гугр",
            "Гарячко",
            "Санджы",
            "Кек",
            "Рыж",
            "Шарло",
            "Пупсень",
            "Вупсень",
            "Царь Носок",
            "Юриваныч",
            "Единая Россия"
        };


        public static string random () {
            return names[RNG.Int(names.Length)];
        }
    }

}
