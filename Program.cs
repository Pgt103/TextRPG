namespace TextRPG
{
    internal class Program
    {
        public interface ICharacter
        {
            int Level { get; set; }
            string Name { get; set; }
            string Job { get; set; }
            int Attack { get; set; }
            int Defense { get; set; }
            int Health { get; set; }
            int Gold { get; set; }
        }

        public class Player : ICharacter
        {
            public int Level { get; set; } = 1;
            public string Name { get; set; } = "Chad";
            public string Job { get; set; } = "전사";
            public int Attack { get; set; } = 10;
            public int Defense { get; set; } = 5;
            public int Health { get; set; } = 100;
            public int Gold { get; set; } = 1500;

            public Player()
            {
                
            }

            public void PlayerStat()
            {
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine("LV. " + Level);
                Console.WriteLine(Name + " " + "({0})", Job);
                Console.WriteLine("공격력 : " + Attack);
                Console.WriteLine("방어력 : " + Defense);
                Console.WriteLine("체 력 : " + Health);
                Console.WriteLine("Gold : " + Gold);
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "0":
                        Console.Clear();
                        return;
                }
            }
        }
        
        public interface IAttackItem
        {
            string Name { get; set; }
            int Price { get; set; }
            int Attack { get; set; }
        }

        public interface IDefenseItem
        {
            string Name { get; set; }
            int Price { get; set; }
            int Defense { get; set; }
        }

        public class AttackItem : IAttackItem
        {
            public string Name { get; set; }
            public int Price { get; set; }
            public int Attack { get; set; }

            public AttackItem(string name, int price, int attack)
            {
                Name = name;
                Price = price;
                Attack = attack;
            }
        }

        public class DefenseItem : IDefenseItem
        {
            public string Name { get; set; }
            public int Price { get; set; }
            public int Defense { get; set; }

            public DefenseItem(string name, int price, int defense)
            {
                Name = name;
                Price = price;
                Defense = defense;
            }
        }

        public class InGame
        {
            Player player = new Player();
            public void InGameMenu()
            {
                while (true)
                {
                    Console.WriteLine("1. 상태보기");
                    Console.WriteLine("2. 인벤토리");
                    Console.WriteLine("3. 상점");
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    Console.Write(">> ");
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            player.PlayerStat();
                            break;
                        case "2":
                            break;
                        case "3":
                            break;
                        default:
                            Console.WriteLine("잘못 입력하셨습니다. 다시 입력하세요.");
                            break;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            InGame game = new InGame();
            Player player = new Player();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            game.InGameMenu();
        }
    }
}
