using System.Reflection.Metadata.Ecma335;

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
                Console.WriteLine();
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
            string Txt { get; set; }
            bool BuyCheck {  get; set; }
            bool Equip { get; set; }
        }

        public interface IDefenseItem
        {
            string Name { get; set; }
            int Price { get; set; }
            int Defense { get; set; }
            string Txt { get; set; }
            bool BuyCheck { get; set; }
            bool Equip { get; set; }
        }

        public class AttackItem : IAttackItem
        {
            public string Name { get; set; }
            public int Price { get; set; }
            public int Attack { get; set; }
            public string Txt { get; set; }
            public bool BuyCheck { get; set; }
            public bool Equip { get; set; }


            public AttackItem(string name, int attack, string txt, int price, bool BuyCheck)
            {
                Name = name;
                Price = price;
                Attack = attack;
                Txt = txt;
                BuyCheck = false;
            }
        }

        public class DefenseItem : IDefenseItem
        {
            public string Name { get; set; }
            public int Price { get; set; }
            public int Defense { get; set; }
            public string Txt { get; set; }
            public bool BuyCheck { get; set; }
            public bool Equip { get; set; }

            public DefenseItem(string name, int defense, string txt, int price, bool BuyCheck)
            {
                Name = name;
                Price = price;
                Defense = defense;
                Txt = txt;
                BuyCheck = false;
            }
        }

        public class ItemManager
        {
            public List<AttackItem> attackItems = new List<AttackItem>();
            public List<DefenseItem> defenseItems = new List<DefenseItem>();

            public ItemManager()
            {
                attackItems.Add(new AttackItem("낡은 검", 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600, false));
                attackItems.Add(new AttackItem("청동 도끼", 5, "어디선가 사용됐던 거 같은 도끼입니다.", 1500, false));
                attackItems.Add(new AttackItem("스파르타의 창", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2500, false));

                defenseItems.Add(new DefenseItem("수련자 갑옷", 5, "수련에 도움을 주는 갑옷입니다.", 1000, false));
                defenseItems.Add(new DefenseItem("무쇠 갑옷", 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000, false));
                defenseItems.Add(new DefenseItem("스파르타의 갑옷", 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, false));
            }

            public void ShowItems()
            {
                foreach (var item in attackItems)
                {
                    Console.WriteLine("- {0} | 공격력 +{1} | {2} | {3}", item.Name, item.Attack, item.Txt, BuyOrNot(item.Price, item.BuyCheck));
                }
                foreach (var item in defenseItems)
                {
                    Console.WriteLine("- {0} | 방어력 +{1} | {2} | {3}", item.Name, item.Defense, item.Txt, BuyOrNot(item.Price, item.BuyCheck));
                }
            }

            private string BuyOrNot(int price, bool buy)
            {
                return buy == true ? "구매완료" : $"{price} G";
            }
        }

        public class Inventory
        {

        }

        public class Store
        {
            Player player = new Player();
            ItemManager itemManager = new ItemManager();

            bool caseCheck = false;
            public void StoreMenu()
            {
                ItemManager itemManager = new ItemManager();
                Console.WriteLine();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]\n" + player.Gold);
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                itemManager.ShowItems();
                Console.WriteLine();
                if(caseCheck == false)
                    Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        caseCheck = true;
                        break;
                    case "0":
                        caseCheck = false;
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("정확한 답변을 입력해주세요");
                        break;
                }
            }

            

        }


        public class InGame
        {
            Player player = new Player();
            Store store = new Store();
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
                            store.StoreMenu();
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
