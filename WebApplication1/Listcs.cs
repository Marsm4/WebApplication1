namespace Apiapp
{
    public class Listcs
    {
        public  List<Habits> Habits = new List<Habits>
        {
            new Habits(1, "Спорт", "Становая тяга 10 повторений, вес 50 кг", 0),
            new Habits(2, "Программирование", "Сделать 10 сложных задач на LeetCode", 25),
            new Habits(3, "Саморефлексия", "В течение месяца делать саморефлексию", 30),
            new Habits(4, "Проект", "Каждый день 30 минут заниматься Unity", 40),
            new Habits(5, "Работа", "Сделать 20 заказов", 50)
        };

        public void AddHabit(Habits habit)
        {
            Habits.Add(habit);
        }
    }
}
