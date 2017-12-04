using Utils.Random;


namespace Battle.Objects.GameObjects {

    internal class WormsNames {

        private static readonly string[] _names = {
            "Тарг",
            "Имба",
            "Айс",
            "Травокур",
            "Эскобар",
            "Гуф",
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
            "Санджы",
            "Кек",
            "Рыж",
            "Шарло",
            "Пупсень",
            "Вупсень",
            "Царь Носок",
            "Пудж",
            "Гук"
        };


        public static string Random () {
            return _names[RNG.Int(_names.Length)];
        }

    }

}
