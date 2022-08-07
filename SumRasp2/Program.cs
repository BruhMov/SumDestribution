namespace SumRasp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double Summa = 10000;
            List<double> Sum = new List<double>();
            Sum.Add(1000);
            Sum.Add(2000);
            Sum.Add(3000);
            Sum.Add(5000);
            Sum.Add(8000);
            Sum.Add(5000);

            var result = SumDestribution("ПРОП", Summa, Sum);
            Console.WriteLine(result.Item1);
            foreach (var item in result.Item2)
            {
                Console.WriteLine(item);
            };
        }
        public static Tuple<string, List<double>> SumDestribution(string DestribMethod, double Summa, List<double> Sum)
        {            
            List<double> Output = new List<double>();
            switch (DestribMethod)
            {
                case "ПРОП":
                    double remainder = 0;
                    foreach (double item in Sum)
                    {
                        // Находим какую часть суммы составляет определенный элемент из списка сумм
                        var Part = item / Sum.Sum();
                        // Для определения остатка создаем две переменные
                        double ValueBeforeRounding = (Part * Summa);
                        double ValueAfterRounding = Math.Round(ValueBeforeRounding, 2);
                        // Теперь добавляем остаток округления элемента в переменную
                        remainder += ValueBeforeRounding - ValueAfterRounding;
                        Output.Add(ValueAfterRounding);
                    }
                    // К последнему элементу выводного списка добавляем остаток
                    Math.Round(Output[Output.Count - 1] += remainder);
                    return Tuple.Create(DestribMethod, Output);

                case "ПЕРВ":
                    // Для того, чтобы метод понял какое количество суммы осталось распределить,
                    // объявляем переменную, из которой будем выводить уже распределенную сумму
                    double actualSumma = Summa;
                    foreach (var item in Sum)
                    {
                        if(actualSumma > item)
                        {
                            // Логика та же:
                            // Находим часть элемента
                            var part = item / Summa;
                            // Получаем распределенное значение
                            var Value = Math.Round(part * Summa,2);
                            Output.Add(Value);
                            // Отнимаем от оставшейся суммы распределенный элемент
                            actualSumma -= Value;
                        }
                        if(actualSumma <= item)
                        {
                            // В случае, если оставшаяся сумма меньше элемента для распределения
                            // или равна ему, записываем весь остаток суммы в элемент вывода и отнимаем от остатка
                            Output.Add(actualSumma);
                            actualSumma -= actualSumma;
                        }
                    }
                    return Tuple.Create(DestribMethod, Output);

                case "ПОСЛ":
                    actualSumma = Summa;
                    // Та же логика, только для удобства переворачиваем входной список сумм
                    Sum.Reverse();
                    foreach (var item in Sum)
                    {
                        if (actualSumma > item)
                        {
                            var part = item / Summa;
                            var Value = Math.Round(part * Summa,2);
                            Output.Add(Value);
                            actualSumma -= Value;
                        }
                        else
                        {
                            Output.Add(actualSumma);
                            actualSumma -= actualSumma;
                        }
                    }
                    // А затем распределенные суммы возвращаем в изначальный порядок входных сумм
                    Output.Reverse();
                    return Tuple.Create(DestribMethod, Output);
                default:
                    return Tuple.Create("Ошибка в строке распределения!", Output);
            }


            
            

        }
    }
}