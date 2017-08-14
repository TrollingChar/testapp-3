using Utils;


namespace War.Objects.GameObjects {

    internal class WormsNames {

        private static readonly string[] _names = {
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
            "Санджы",
            "Кек",
            "Рыж",
            "Шарло",
            "Пупсень",
            "Вупсень",
            "Царь Носок",
            "Пудж",
            "Единая Россия"
        };


        public static string Random () {
            return _names[RNG.Int(_names.Length)];
        }

    }

}
