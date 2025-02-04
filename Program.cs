using System.Reflection.Metadata.Ecma335;
using static TextRPG.Program;

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
            private static Player instance;

            public static Player Instance
            {
                get
                {
                    if (instance == null)
                        instance = new Player();
                    return instance;
                }
            }

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
                Console.WriteLine("공격력 : " + Attack + $"{Plus(Attack)}");
                Console.WriteLine("방어력 : " + Defense + $"{Plus(Defense)}");
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

            private string Plus(int num)
            {
                return num > 10 ? $"(+{num - 10})" : "";
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


            public AttackItem(string name, int attack, string txt, int price, bool buyCheck, bool equipCheck)
            {
                Name = name;
                Price = price;
                Attack = attack;
                Txt = txt;
                BuyCheck = buyCheck;
                Equip = equipCheck;
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

            public DefenseItem(string name, int defense, string txt, int price, bool buyCheck, bool equipCheck)
            {
                Name = name;
                Price = price;
                Defense = defense;
                Txt = txt;
                BuyCheck = buyCheck;
                Equip = equipCheck;
            }
        }

        public class ItemManager
        {
            private static ItemManager instance;

            public List<AttackItem> attackItem = new List<AttackItem>();
            public List<DefenseItem> defenseItem = new List<DefenseItem>();

            private ItemManager()
            {
                attackItem.Add(new AttackItem("1 낡은 검", 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600, false, false));
                attackItem.Add(new AttackItem("2 청동 도끼", 5, "어디선가 사용됐던 거 같은 도끼입니다.", 1500, false, false));
                attackItem.Add(new AttackItem("3 스파르타의 창", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2500, false, false));

                defenseItem.Add(new DefenseItem("4 수련자 갑옷", 5, "수련에 도움을 주는 갑옷입니다.", 1000, false, false));
                defenseItem.Add(new DefenseItem("5 무쇠 갑옷", 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000, false, false));
                defenseItem.Add(new DefenseItem("6 스파르타의 갑옷", 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, false, false));
            }

            public static ItemManager Instance
            {
                get
                {
                    if (instance == null)
                        instance = new ItemManager();
                    return instance;
                }
            }

            public void ShowItem()
            {
                foreach (var item in attackItem)
                {
                    Console.WriteLine("- {0} | 공격력 +{1} | {2} | {3}", item.Name, item.Attack, item.Txt, BuyOrNot(item.Price, item.BuyCheck));
                }
                foreach (var item in defenseItem)
                {
                    Console.WriteLine("- {0} | 방어력 +{1} | {2} | {3}", item.Name, item.Defense, item.Txt, BuyOrNot(item.Price, item.BuyCheck));
                }
            }

            public void BuyItem()
            {
                foreach (var item in attackItem)
                {
                    if(item.BuyCheck == true)
                        Console.WriteLine("- {0} | 공격력 +{1} | {2}", IsEquip(item.Name, item.Equip), item.Attack, item.Txt);
                }
                foreach (var item in defenseItem)
                {
                    if (item.BuyCheck == true)
                        Console.WriteLine("- {0} | 방어력 +{1} | {2}", IsEquip(item.Name, item.Equip), item.Defense, item.Txt);
                }
            }

            private string BuyOrNot(int price, bool buy)
            {
                return buy == true ? "구매완료" : $"{price} G";
            }

            private string IsEquip(string name, bool eq)
            {
                return eq == true ? $"[E]{name}" : name;
            }

        }

        public class Inventory
        {
            Player player = Player.Instance;
            ItemManager itemManager = ItemManager.Instance;

            bool caseCheck;

            public void InventoryMenu()
            {
                caseCheck = false;
                Console.WriteLine();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                itemManager.BuyItem();
                Console.WriteLine();
                if(caseCheck == false)
                    Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        caseCheck = true;
                        Console.Write("장착할 아이템의 번호를 입력하세요 >> ");
                        input = Console.ReadLine();
                        switch (input)
                        {
                            case "1":
                                if (itemManager.attackItem[0].Equip == false)
                                {
                                    itemManager.attackItem[0].Equip = true;
                                    player.Attack += itemManager.attackItem[0].Attack;
                                    break;
                                }
                                else
                                {
                                    itemManager.attackItem[0].Equip = false;
                                    player.Attack = player.Attack;
                                    break;
                                }
                            case "2":
                                if (itemManager.attackItem[1].Equip == false)
                                {
                                    itemManager.attackItem[1].Equip = true;
                                    player.Attack += itemManager.attackItem[1].Attack;
                                    break;
                                }
                                else
                                {
                                    itemManager.attackItem[1].Equip = false;
                                    player.Attack = player.Attack;
                                    break;
                                }
                            case "3":
                                if (itemManager.attackItem[2].Equip == false)
                                {
                                    itemManager.attackItem[2].Equip = true;
                                    player.Attack += itemManager.attackItem[2].Attack;
                                    break;
                                }
                                else
                                {
                                    itemManager.attackItem[2].Equip = false;
                                    player.Attack = player.Attack;
                                    break;
                                }
                            case "4":
                                if (itemManager.defenseItem[0].Equip == false)
                                {
                                    itemManager.defenseItem[0].Equip = true;
                                    player.Defense += itemManager.defenseItem[0].Defense;
                                    break;
                                }
                                else
                                {
                                    itemManager.defenseItem[0].Equip = false;
                                    player.Defense = player.Defense;
                                    break;
                                }
                            case "5":
                                if (itemManager.defenseItem[1].Equip == false)
                                {
                                    itemManager.defenseItem[1].Equip = true;
                                    player.Defense += itemManager.defenseItem[1].Defense;
                                    break;
                                }
                                else
                                {
                                    itemManager.defenseItem[1].Equip = false;
                                    player.Defense = player.Defense;
                                    break;
                                }
                            case "6":
                                if (itemManager.defenseItem[2].Equip == false)
                                {
                                    itemManager.defenseItem[2].Equip = true;
                                    player.Defense += itemManager.defenseItem[0].Defense;
                                    break;
                                }
                                else
                                {
                                    itemManager.defenseItem[2].Equip = false;
                                    player.Defense = player.Defense;
                                    break;
                                }
                            case "0":
                                caseCheck = false;
                                Console.Clear();
                                return;
                            default:
                                Console.Clear();
                                Console.WriteLine("잘못 입력하셨습니다. 뒤로 돌아갑니다.");
                                break;
                        }
                        break;
                    case "0":
                        caseCheck = false;
                        Console.Clear();
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못 입력하셨습니다. 메인으로 돌아갑니다.");
                        break;
                }
            }


        }

        public class Store
        {
            Player player = Player.Instance;
            ItemManager itemManager = ItemManager.Instance;

            bool caseCheck;
            public void StoreMenu()
            {
                caseCheck = false;
                Console.WriteLine();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]\n" + player.Gold);
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                itemManager.ShowItem();
                Console.WriteLine();
                if (caseCheck == false)
                    Console.WriteLine("1. 아이템 구매");
                else
                    Console.WriteLine("구매할 아이템 번호를 눌러주세요");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                string input = Console.ReadLine();

                if (caseCheck == false)
                {
                    switch (input)
                    {
                        case "1":
                            caseCheck = true;
                            Console.Write("구매할 아이템의 번호를 입력하세요 >> ");
                            input = Console.ReadLine();
                            switch (input)
                            {
                                case "1":
                                    if(itemManager.attackItem[0].BuyCheck == false && itemManager.attackItem[0].Price <= player.Gold)
                                    {
                                        itemManager.attackItem[0].BuyCheck = true;
                                        player.Gold -= itemManager.attackItem[0].Price;
                                        Console.Clear();
                                        Console.WriteLine("구매를 완료했습니다.");
                                        Console.WriteLine();
                                        break;
                                    }
                                    else if(itemManager.attackItem[0].Price > player.Gold)
                                    {
                                        Console.WriteLine("Gold가 부족합니다.");
                                        Console.WriteLine();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("이미 구매한 아이템입니다.");
                                        Console.WriteLine();
                                        break;
                                    }
                                case "2":
                                    if (itemManager.attackItem[1].BuyCheck == false && itemManager.attackItem[1].Price <= player.Gold)
                                    {
                                        itemManager.attackItem[1].BuyCheck = true;
                                        player.Gold -= itemManager.attackItem[1].Price;
                                        Console.Clear();
                                        Console.WriteLine("구매를 완료했습니다.");
                                        Console.WriteLine();
                                        break;
                                    }
                                    else if (itemManager.attackItem[1].Price > player.Gold)
                                    {
                                        Console.WriteLine("Gold가 부족합니다.");
                                        Console.WriteLine();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("이미 구매한 아이템입니다.");
                                        Console.WriteLine();
                                        break;
                                    }
                                case "3":
                                    if (itemManager.attackItem[2].BuyCheck == false && itemManager.attackItem[2].Price <= player.Gold)
                                    {
                                        itemManager.attackItem[2].BuyCheck = true;
                                        player.Gold -= itemManager.attackItem[2].Price;
                                        Console.Clear();
                                        Console.WriteLine("구매를 완료했습니다.");
                                        Console.WriteLine();
                                        break;
                                    }
                                    else if (itemManager.attackItem[2].Price > player.Gold)
                                    {
                                        Console.WriteLine("Gold가 부족합니다.");
                                        Console.WriteLine();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("이미 구매한 아이템입니다.");
                                        Console.WriteLine();
                                        break;
                                    }
                                case "4":
                                    if (itemManager.defenseItem[0].BuyCheck == false && itemManager.defenseItem[0].Price <= player.Gold)
                                    {
                                        itemManager.defenseItem[0].BuyCheck = true;
                                        player.Gold -= itemManager.defenseItem[0].Price;
                                        Console.Clear();
                                        Console.WriteLine("구매를 완료했습니다.");
                                        Console.WriteLine();
                                        break;
                                    }
                                    else if (itemManager.defenseItem[0].Price > player.Gold)
                                    {
                                        Console.WriteLine("Gold가 부족합니다.");
                                        Console.WriteLine();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("이미 구매한 아이템입니다.");
                                        Console.WriteLine();
                                        break;
                                    }
                                case "5":
                                    if (itemManager.defenseItem[1].BuyCheck == false && itemManager.defenseItem[1].Price <= player.Gold)
                                    {
                                        itemManager.defenseItem[1].BuyCheck = true;
                                        player.Gold -= itemManager.defenseItem[1].Price;
                                        Console.Clear();
                                        Console.WriteLine("구매를 완료했습니다.");
                                        Console.WriteLine();
                                        break;
                                    }
                                    else if (itemManager.defenseItem[1].Price > player.Gold)
                                    {
                                        Console.WriteLine("Gold가 부족합니다.");
                                        Console.WriteLine();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("이미 구매한 아이템입니다.");
                                        Console.WriteLine();
                                        break;
                                    }
                                case "6":
                                    if (itemManager.defenseItem[2].BuyCheck == false && itemManager.defenseItem[2].Price <= player.Gold)
                                    {
                                        itemManager.defenseItem[2].BuyCheck = true;
                                        player.Gold -= itemManager.defenseItem[2].Price;
                                        Console.Clear();
                                        Console.WriteLine("구매를 완료했습니다.");
                                        Console.WriteLine();
                                        break;
                                    }
                                    else if (itemManager.defenseItem[2].Price > player.Gold)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Gold가 부족합니다.");
                                        Console.WriteLine();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("이미 구매한 아이템입니다.");
                                        Console.WriteLine();
                                        break;
                                    }
                                case "0":
                                    caseCheck = false;
                                    Console.Clear();
                                    return;
                                default:
                                    Console.Clear();
                                    Console.WriteLine("잘못 입력하셨습니다. 뒤로 돌아갑니다.");
                                    break;
                            }
                            break;
                        case "0":
                            caseCheck = false;
                            Console.Clear();
                            return;
                        default:
                            Console.Clear();
                            Console.WriteLine("잘못 입력하셨습니다. 메인으로 돌아갑니다.");
                            break;
                    }
                }
           
            }

            

        }


        public class InGame
        {
            Player player = Player.Instance;
            Store store = new Store();
            Inventory inventory = new Inventory();
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
                            inventory.InventoryMenu();
                            break;
                        case "3":
                            store.StoreMenu();
                            break;
                        default:
                            Console.Clear();
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
