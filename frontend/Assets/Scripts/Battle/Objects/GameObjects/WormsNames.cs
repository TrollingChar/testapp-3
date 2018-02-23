using Utils.Random;


namespace Battle.Objects.GameObjects {

    internal class WormsNames {

        private static readonly string[] Names = {
            "Айс",
            "Анон",
            "Баркс",
            "Ваншот",
            "Вупсень",
            "Гугр",
            "Гук",
            "Гуф",
            "Драшка",
            "Имба",
            "Кек",
            "Кирс",
            "Клем",
            "Мако",
            "Ллалл",
            "Лойс",
            "Нуп",
            "Пудж",
            "Пупсень",
            "Рашка",
            "Рыж",
            "Санджы",
            "Спуди",
            "Саргас",
            "Сыч",
            "Тарг",
            "Травокур",
            "Трарт",
            "Флэш",
            "Царь Носок",
            "Чувак",
            "Шарло",
            "Эскобар",
        };


        public static string Random () {
            return Names[RNG.Int(Names.Length)];
        }

    }

}
