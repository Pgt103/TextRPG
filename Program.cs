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
            public int Gold { get; set; } = 3000;
            public AttackItem WeaponItem { get; set; }
            public DefenseItem ArmorItem{ get; set; }

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
                Console.WriteLine("공격력 : " + Attack + $"{PlusATK(Attack)}");
                Console.WriteLine("방어력 : " + Defense + $"{PlusDFS(Defense)}");
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

            private string PlusATK(int num)
            {
                int defAtk = 10;
                return num > defAtk ? $"(+{num - defAtk})" : "";
            }

            private string PlusDFS(int num)
            {
                int defDfs = 5;
                return num > defDfs ? $"(+{num - defDfs})" : "";
            }

            public void BuyWeapon(AttackItem item)
            {
                if (item.BuyCheck == false && item.Price <= Gold)
                {
                    item.BuyCheck = true;
                    Gold -= item.Price;
                    Console.Clear();
                    Console.WriteLine("구매를 완료했습니다.");
                    Console.WriteLine();
                }
                else if (item.Price > Gold)
                {
                    Console.WriteLine("Gold가 부족합니다.");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                    Console.WriteLine();
                }
            }

            public void SellWeapon(AttackItem item)
            {
                float sellPrice = 0.85f;

                if (item.BuyCheck == true)
                {
                    item.BuyCheck = false;
                    item.Equip = false;
                    Attack -= item.Attack;
                    Gold += (int)(item.Price * sellPrice);
                    Console.Clear();
                    Console.WriteLine("판매를 완료했습니다.");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("이미 판매하거나 구매하지 않은 아이템입니다.");
                    Console.WriteLine();
                }
            }

            public void SellArmor(DefenseItem item)
            {
                float sellPrice = 0.85f;

                if (item.BuyCheck == true)
                {
                    item.BuyCheck = false;
                    item.Equip = false;
                    Defense -= item.Defense;
                    Gold += (int)(item.Price * sellPrice);
                    Console.Clear();
                    Console.WriteLine("판매를 완료했습니다.");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("이미 판매하거나 구매하지 않은 아이템입니다.");
                    Console.WriteLine();
                }
            }

            public void BuyArmor(DefenseItem item)
            {
                if (item.BuyCheck == false && item.Price <= Gold)
                {
                    item.BuyCheck = true;
                    Gold -= item.Price;
                    Console.Clear();
                    Console.WriteLine("구매를 완료했습니다.");
                    Console.WriteLine();
                }
                else if (item.Price > Gold)
                {
                    Console.WriteLine("Gold가 부족합니다.");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                    Console.WriteLine();
                }
            }

            public void EquipWeapon(AttackItem newItem)
            {
                if (WeaponItem != null)
                {
                    Attack -= WeaponItem.Attack;
                    WeaponItem.Equip = false;
                }

                WeaponItem = newItem;
                Attack += WeaponItem.Attack;
                WeaponItem.Equip = true;
            }

            public void EquipArmor(DefenseItem newItem)
            {
                if (ArmorItem != null)
                {
                    Defense -= ArmorItem.Defense;
                    ArmorItem.Equip = false;
                }

                ArmorItem = newItem;
                Defense += ArmorItem.Defense;
                ArmorItem.Equip = true;
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

            public static ItemManager Instance // 싱글톤 화
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

            public void BuyItem() // 구입한 아이템을 인벤토리에 표시해주는 역할
            {
                foreach (var item in attackItem) // 리스트를 전부 출력, 그러나 안에 조건문을 두어 특정 리스트만 출력되게 한다
                {
                    if(item.BuyCheck == true) // 구매가 확인돠었을 경우
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

            private string IsEquip(string name, bool eq) // 아이템을 장착했는지 판단 후 장착 했을시 장착 표시
            {
                return eq == true ? $"[E]{name}" : name; // 장착 시 아이템 이름 왼쪽에 [E] 표시가 생긴다
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
                                player.EquipWeapon(itemManager.attackItem[0]);
                                break;
                            case "2":
                                player.EquipWeapon(itemManager.attackItem[1]);
                                break;
                            case "3":
                                player.EquipWeapon(itemManager.attackItem[2]);
                                break;
                            case "4":
                                player.EquipArmor(itemManager.defenseItem[0]);
                                break;
                            case "5":
                                player.EquipArmor(itemManager.defenseItem[1]);
                                break;
                            case "6":
                                player.EquipArmor(itemManager.defenseItem[2]);
                                break;
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
            bool sell;
            public void StoreMenu()
            {
                caseCheck = false;
                sell = false;
                Console.WriteLine();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]\n" + player.Gold);
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                if(sell == true)
                {
                    itemManager.BuyItem();
                }
                else
                {
                    itemManager.ShowItem();
                }

                Console.WriteLine();
                if (caseCheck == false)
                {
                    Console.WriteLine("1. 아이템 구매");
                    Console.WriteLine("2. 아이템 판매");
                }
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
                                    player.BuyWeapon(itemManager.attackItem[0]);
                                    break;
                                case "2":
                                    player.BuyWeapon(itemManager.attackItem[1]);
                                    break;
                                case "3":
                                    player.BuyWeapon(itemManager.attackItem[2]);
                                    break;
                                case "4":
                                    player.BuyArmor(itemManager.defenseItem[0]);
                                    break;
                                case "5":
                                    player.BuyArmor(itemManager.defenseItem[1]);
                                    break;
                                case "6":
                                    player.BuyArmor(itemManager.defenseItem[2]);
                                    break;
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
                        case "2":
                            caseCheck = true;
                            sell = true;
                            Console.Write("판매할 아이템의 번호를 입력하세요 >> ");
                            input = Console.ReadLine();
                            switch (input)
                            {
                                case "1":
                                    player.SellWeapon(itemManager.attackItem[0]);
                                    break;
                                case "2":
                                    player.SellWeapon(itemManager.attackItem[1]);
                                    break;
                                case "3":
                                    player.SellWeapon(itemManager.attackItem[2]);
                                    break;
                                case "4":
                                    player.SellArmor(itemManager.defenseItem[0]);
                                    break;
                                case "5":
                                    player.SellArmor(itemManager.defenseItem[1]);
                                    break;
                                case "6":
                                    player.SellArmor(itemManager.defenseItem[2]);
                                    break;
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

        public class Sleep
        {
            Player player = Player.Instance;

            public void Healing()
            {
                Console.WriteLine();
                Console.WriteLine("휴식하기");
                Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. (보유 골드 : {player.Gold}");
                Console.WriteLine("1. 휴식하기");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        if(player.Gold >= 500)
                        {
                            player.Gold -= 500;
                            player.Health = 100;
                            Console.Clear();
                            Console.WriteLine("500 G 를 지불하고 휴식을 취했습니다.");
                            Console.WriteLine();
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("보유한 금액이 부족합니다.");
                            Console.WriteLine();
                            break;
                        }
                    case "0":
                        Console.Clear();
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못 입력하셨습니다. 메인 화면으로 돌아갑니다");
                        break;
                }
            }
        }

        public class InGame
        {
            Player player = Player.Instance;
            Store store = new Store();
            Inventory inventory = new Inventory();
            Sleep sleep = new Sleep();
            public void InGameMenu()
            {
                while (true)
                {
                    Console.WriteLine("1. 상태보기");
                    Console.WriteLine("2. 인벤토리");
                    Console.WriteLine("3. 상점");
                    Console.WriteLine("4. 휴식하기");
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
                        case "4":
                            sleep.Healing();
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
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            game.InGameMenu();
        }
    }
}
