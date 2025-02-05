using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using static TextRPG.Program;

namespace TextRPG
{
    internal class Program
    {
        public interface ICharacter // 플레이어 캐릭터 정의 인터페이스
        {
            int Level { get; set; } // 플레이어의 레벨
            string Name { get; set; } // 플레이어의 이름
            string Job { get; set; } // 플레이어의 직업
            int Attack { get; set; } // 플레이어의 공격력
            int Defense { get; set; } // 플레이어의 방어력
            int Health { get; set; } // 플레이어의 체력
            int Gold { get; set; } // 소지금
        }

        public class Player : ICharacter
        {
            private static Player instance;

            public static Player Instance // 싱글톤 선언
            {
                get
                {
                    if (instance == null)
                        instance = new Player();
                    return instance;
                }
            }

            // Player 인터페이스 재정의. 플레이어의 기본 스탯 설정
            public int Level { get; set; } = 1; // 기본 레벨은 1부터 시작
            public string Name { get; set; } = "Chad"; // 이름
            public string Job { get; set; } = "전사"; // 직업
            public int Attack { get; set; } = 10; // 기본 공격력 10
            public int Defense { get; set; } = 5; // 기본 방어력 5
            public int Health { get; set; } = 100; // 기본 체력 100
            public int Gold { get; set; } = 3000; // 기본 소지금 3000
            public int DungeonClear { get; set; } // 던전 클리어 횟수
            public AttackItem WeaponItem { get; set; } // 무기 장착 슬롯
            public DefenseItem ArmorItem{ get; set; } // 방어구 장착 슬롯
            public AccessoryItem AccessoryItem { get; set; } // 액세서리 장착 슬롯

            public Player() // 플레이어 생성자. 아무것도 적지않아 기본 생성자 생성
            {
                
            }

            public void PlayerStat() // 플레이어의 현재 상태 확인
            {
                Console.WriteLine();
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine("LV. " + (Level + (DungeonClear / 2))); // 던전 클리어 횟수 2회당 1레벨 증가
                Console.WriteLine(Name + " " + "({0})", Job); // 이름과 직업
                // 각각 공격력, 방어력, 체력 출력. 각 스탯은 현재 레벨에 따라 공격력 0.5, 방어력 1, 체력 10 증가.
                Console.WriteLine("공격력 : " + (Attack + ((Level + (DungeonClear / 2)) * 0.5f - 0.5f)) + $"{PlusATK(Attack)}");
                Console.WriteLine("방어력 : " + (Defense + ((Level + (DungeonClear / 2)) * 1 - 1)) + $"{PlusDFS(Defense)}");
                Console.WriteLine("체 력 : " + (Health + (Level + (DungeonClear / 2)) * 10 - 10));
                Console.WriteLine("Gold : " + Gold); // 소지금
                Console.WriteLine();
                Console.WriteLine("0. 나가기"); // 상태창 나가기 안내
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                string input = Console.ReadLine();
                switch (input) 
                {
                    case "0":
                        Console.Clear();
                        return; // 상태창 나가기
                    default:
                        Console.Clear();
                        Console.WriteLine("잘못입력하셨습니다. 메인화면으로 돌아갑니다.");
                        break;
                }
            }

            private string PlusATK(int num) // 장비 장착 시 공격력 변화치 개별 표기, 레벨에 따른 스탯변화는 반영하지 않음
            {
                float defAtk = 10 + ((Level + (DungeonClear / 2)) * 0.5f - 0.5f);
                return num > defAtk ? $"(+{num - defAtk})" : ""; // 기본 공격력보다 높을 시 추가 공격력 표기, 그렇지 않을 시 표기하지 않음
            }

            private string PlusDFS(int num) // 장비 장착 시 방어력 변화치 개별 표기, 레벨에 따른 스탯변화는 반영하지 않음
            {
                int defDfs = 5 + ((Level + (DungeonClear / 2)) * 1 - 1);
                return num > defDfs ? $"(+{num - defDfs})" : ""; // 기본 방어력보다 높을 시 추가 방어력 표기, 그렇지 않을 시 표기하지 않음
            }

            

            // 장비 판매 메서드. 3가지 전부 작동방식이 같으나 판매하는 장비가 장착중일 시 장비의 스탯만큼 차감하는 스탯의 종류만 다르다.

            public void SellWeapon(AttackItem item) // 무기 판매
            {
                float sellPrice = 0.85f; // 장비 판매 시 구매한 가격의 85%을 돌려받는다

                if (item.BuyCheck == true) // 예전에 샀던 장비일때
                {
                    item.BuyCheck = false; // 구매 표시 해제
                    if(item.Equip == true) // 장착 중인 장비일때
                    {
                        item.Equip = false; //장착 해제
                        Attack -= item.Attack; // 장비의 공격력을 차감
                    }
                    Gold += (int)(item.Price * sellPrice); // 판매한 금액을 소지금에 추가
                    Console.Clear();
                    Console.WriteLine("판매를 완료했습니다.");
                    Console.WriteLine();
                }
                else // 아직 없는 장비일 때
                {
                    Console.Clear();
                    Console.WriteLine("이미 판매하거나 구매하지 않은 아이템입니다.");
                    Console.WriteLine();
                }
            }

            public void SellArmor(DefenseItem item) // 방어구 판매
            {
                float sellPrice = 0.85f;

                if (item.BuyCheck == true)
                {
                    item.BuyCheck = false;
                    if(item.Equip == true)
                    {
                        item.Equip = false;
                        Defense -= item.Defense;
                    }
                    Gold += (int)(item.Price * sellPrice);
                    Console.Clear();
                    Console.WriteLine("판매를 완료했습니다.");
                    Console.WriteLine();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("이미 판매하거나 구매하지 않은 아이템입니다.");
                    Console.WriteLine();
                }
            }

            public void SellAccessory(AccessoryItem item) // 액세서리 판매
            {
                float sellPrice = 0.85f;

                if (item.BuyCheck == true)
                {
                    item.BuyCheck = false;
                    if(item.Equip == true)
                    {
                        item.Equip = false;
                        Attack -= item.Attack;
                        Defense -= item.Defense;
                    }
                    Gold += (int)(item.Price * sellPrice);
                    Console.Clear();
                    Console.WriteLine("판매를 완료했습니다.");
                    Console.WriteLine();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("이미 판매하거나 구매하지 않은 아이템입니다.");
                    Console.WriteLine();
                }
            }

            // 장비 구매 메서드. 구매하지 않은 장비이거나 소지금이 가격보다 높거나 같을 때 장비 구매 후 소지금 차감.

            public void BuyWeapon(AttackItem item) // 장비 구매. 무기 구매
            {
                if (item.BuyCheck == false && item.Price <= Gold) // 아직 구매하지 않음 and 현재 소지금이 가격보다 높거나 같을 시
                {
                    item.BuyCheck = true; // 구매 표시
                    Gold -= item.Price; // 소지금 차감
                    Console.Clear();
                    Console.WriteLine("구매를 완료했습니다.");
                    Console.WriteLine();
                }
                else if (item.Price > Gold) // 소지금이 부족할 시
                {
                    Console.WriteLine("Gold가 부족합니다.");
                    Console.WriteLine();
                }
                else // 구매한 상품일 시
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                    Console.WriteLine();
                }
            }

            public void BuyArmor(DefenseItem item) // 방어구 구매
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

            public void BuyAccessory(AccessoryItem item) // 액세서리 구매
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

            // 장비 장착 메서드. 장비가 구매한 장비인지 체크하고, 맞다면 장비 장착. 각 분류(무기, 방어구, 장신구)마다 하나의 장비만 장착가능

            public void EquipWeapon(AttackItem newItem)
            {
                if(newItem.BuyCheck)
                {
                    if (WeaponItem != null) // 현재 장비 장착 중일 시
                    {
                        Attack -= WeaponItem.Attack; // 장착중인 장비의 스탯만큼 감소
                        WeaponItem.Equip = false; // 장착중인 장비 해제
                    }

                    WeaponItem = newItem; // 선택한 장비
                    Attack += WeaponItem.Attack; // 장비의 스탯만큼 증가
                    WeaponItem.Equip = true; // 장비 장착
                    Console.Clear();
                    Console.WriteLine("무기를 장착했습니다.");
                    Console.WriteLine();
                }
                else // 현재 지니고 있지않은 장비일 떄
                {
                    Console.Clear();
                    Console.WriteLine("없는 장비입니다.");
                    Console.WriteLine();
                }
                
            }

            public void EquipArmor(DefenseItem newItem) // 방어구 장착
            {
                if(newItem.BuyCheck)
                {
                    if (ArmorItem != null)
                    {
                        Defense -= ArmorItem.Defense;
                        ArmorItem.Equip = false;
                    }

                    ArmorItem = newItem;
                    Defense += ArmorItem.Defense;
                    ArmorItem.Equip = true;
                    Console.Clear();
                    Console.WriteLine("방어구를 장착했습니다.");
                    Console.WriteLine();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("없는 장비입니다.");
                    Console.WriteLine();
                }
            }

            public void EquipAccessory(AccessoryItem newItem) // 액세서리 장착
            {
                if(newItem.BuyCheck)
                {
                    if (AccessoryItem != null)
                    {
                        Attack -= AccessoryItem.Attack;
                        Defense -= AccessoryItem.Defense;
                        AccessoryItem.Equip = false;
                    }

                    AccessoryItem = newItem;
                    Attack += AccessoryItem.Attack;
                    Defense += AccessoryItem.Defense;
                    AccessoryItem.Equip = true;
                    Console.Clear();
                    Console.WriteLine("장신구를 장착했습니다.");
                    Console.WriteLine();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("없는 장비입니다.");
                    Console.WriteLine();
                }
            }
        }

        // 장비 인터페이스
        
        public interface IAttackItem // 공격력 장비 인터페이스
        {
            string Name { get; set; } // 이름
            int Price { get; set; } // 가격
            int Attack { get; set; } // 스탯
            string Txt { get; set; } // 장비 설명
            bool BuyCheck {  get; set; } // 구매 확인
            bool Equip { get; set; } // 장착 확인
        }

        public interface IDefenseItem // 방어구 장비 인터페이스
        {
            string Name { get; set; }
            int Price { get; set; }
            int Defense { get; set; }
            string Txt { get; set; }
            bool BuyCheck { get; set; }
            bool Equip { get; set; }
        }

        public interface IAccessory // 액세서리 인터페이스
        {
            string Name { get; set; }
            int Price { get; set; }
            int Attack { get; set; }
            int Defense { get; set; }
            string Txt { get; set; }
            bool BuyCheck { get; set; }
            bool Equip { get; set; }
        }

        // 장비 클래스. 무기, 방어구, 액세서리가 존재한다

        public class AttackItem : IAttackItem // 무기. 공격력 스탯을 지닌다
        {
            public string Name { get; set; } // 이름
            public int Price { get; set; } // 가격
            public int Attack { get; set; } // 공격력
            public string Txt { get; set; } // 장비 설명
            public bool BuyCheck { get; set; } // 구매 확인
            public bool Equip { get; set; } // 장착 확인


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

        public class DefenseItem : IDefenseItem // 방어구. 방어력 스탯을 지닌다
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

        public class AccessoryItem : IAccessory // 액세서리는 공격력, 방어력 둘 다 가지고 있다
        {
            public string Name { get; set; }
            public int Price { get; set; }
            public int Attack { get; set; }
            public int Defense { get; set; }
            public string Txt { get; set; }
            public bool BuyCheck { get; set; }
            public bool Equip { get; set; }


            public AccessoryItem(string name, int attack, int defense, string txt, int price, bool buyCheck, bool equipCheck)
            {
                Name = name;
                Price = price;
                Attack = attack;
                Defense = defense;
                Txt = txt;
                BuyCheck = buyCheck;
                Equip = equipCheck;
            }
        }

        // ItemManager 클래스. 장비들의 리스트 관리 및 장비 인터페이스를 관리한다

        public class ItemManager
        {
            private static ItemManager instance;

            public List<AttackItem> attackItem = new List<AttackItem>(); // 무기 리스트
            public List<DefenseItem> defenseItem = new List<DefenseItem>(); // 방어구 리스트
            public List<AccessoryItem> accessoryItem = new List<AccessoryItem>(); // 액세서리 리스트

            private ItemManager() // 장비 생성. 이름, 스탯, 설명, 가격, 구매 확인, 장착 확인 순으로 되어있다.
            {
                attackItem.Add(new AttackItem("1 낡은 검", 2, "쉽게 볼 수 있는 낡은 검 입니다.", 600, false, false));
                attackItem.Add(new AttackItem("2 청동 도끼", 5, "어디선가 사용됐던 거 같은 도끼입니다.", 1500, false, false));
                attackItem.Add(new AttackItem("3 스파르타의 창", 7, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 2500, false, false));

                defenseItem.Add(new DefenseItem("4 수련자 갑옷", 5, "수련에 도움을 주는 갑옷입니다.", 1000, false, false));
                defenseItem.Add(new DefenseItem("5 무쇠 갑옷", 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000, false, false));
                defenseItem.Add(new DefenseItem("6 스파르타의 갑옷", 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, false, false));

                accessoryItem.Add(new AccessoryItem("7 철 반지", 1, 1, "아무 특색없는 반지입니다.", 500, false, false));
                accessoryItem.Add(new AccessoryItem("8 오래된 반지", 5, 5, "왕의 반지입니다.", 2500, false, false));
            }

            // 싱글톤 화로 다른 클래스에서 사용 시 장착 또는 구매 확인의 변경점이 그대로 적용된다
            public static ItemManager Instance // 싱글톤 화
            {
                get
                {
                    if (instance == null)
                        instance = new ItemManager();
                    return instance;
                }
            }

            public void ShowItem() // 상점에 아이템 표시
            {
                // foreach로 리스트의 모든 요소 출력
                foreach (var item in attackItem) // 무기
                {
                    Console.WriteLine("- {0} | 공격력 +{1} | {2} | {3}", item.Name, item.Attack, item.Txt, BuyOrNot(item.Price, item.BuyCheck));
                }
                foreach (var item in defenseItem) // 방어구
                {
                    Console.WriteLine("- {0} | 방어력 +{1} | {2} | {3}", item.Name, item.Defense, item.Txt, BuyOrNot(item.Price, item.BuyCheck));
                }
                foreach (var item in accessoryItem) // 액세서리
                {
                    Console.WriteLine("- {0} | 공격력 +{1} | 방어력 +{2} | {3} | {4}", item.Name, item.Attack ,item.Defense, item.Txt, BuyOrNot(item.Price, item.BuyCheck));
                }
            }

            public void BuyItem() // 구입한 아이템 표시
            {
                foreach (var item in attackItem) // 무기 리스트를 전부 출력, 그러나 안에 조건문을 두어 특정 리스트만 출력되게 한다
                {
                    if(item.BuyCheck == true) // 구매가 확인돠었을 경우
                        Console.WriteLine("- {0} | 공격력 +{1} | {2}", IsEquip(item.Name, item.Equip), item.Attack, item.Txt);
                }
                foreach (var item in defenseItem) // 방어구
                {
                    if (item.BuyCheck == true)
                        Console.WriteLine("- {0} | 방어력 +{1} | {2}", IsEquip(item.Name, item.Equip), item.Defense, item.Txt);
                }
                foreach (var item in accessoryItem) // 액세서리
                {
                    if (item.BuyCheck == true)
                        Console.WriteLine("- {0} | 공격력 +{1} | 방어력 +{2} | {3} | {4}", item.Name, item.Attack, item.Defense, item.Txt, BuyOrNot(item.Price, item.BuyCheck));
                }
            }

            private string BuyOrNot(int price, bool buy) // 구매된 아이템일 경우 구매완료 표기. 아닐 시 가격 표기
            {
                return buy == true ? "구매완료" : $"{price} G";
            }

            private string IsEquip(string name, bool eq) // 아이템을 장착했는지 판단 후 장착 했을시 장착 표시
            {
                return eq == true ? $"[E]{name}" : name; // 장착 시 아이템 이름 왼쪽에 [E] 표시가 생긴다
            }


        }

        // Inventory 클래스. 인벤토리 선택지를 선택했을 때의 결과 관리
        public class Inventory
        {
            Player player = Player.Instance; // 플레이어 인스턴스
            ItemManager itemManager = ItemManager.Instance; // 아이템 인스턴스

            bool caseCheck; // 선택지 bool 변수. 장착 관리로 들어 갔을 시 true로 변경.

            public void InventoryMenu()
            {
                caseCheck = false; // 기본설정
                Console.WriteLine();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                itemManager.BuyItem(); // 보유중인 아이템 목록 출력
                Console.WriteLine();
                if(caseCheck == false) // 장착관리를 아직 들어가지 않은 상태일 때
                    Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                string input = Console.ReadLine();
                switch (input) // 장착 관리 또는 나가기 선택지
                {
                    case "1": // 장착 관리 선택
                        caseCheck = true;
                        Console.Write("장착할 아이템의 번호를 입력하세요 >> ");
                        input = Console.ReadLine();
                        switch (input) // 장착할 아이템 선택지
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
                            case "7":
                                player.EquipAccessory(itemManager.accessoryItem[0]);
                                break;
                            case "8":
                                player.EquipAccessory(itemManager.accessoryItem[1]);
                                break;
                            case "0": // 나가기
                                caseCheck = false;
                                Console.Clear();
                                return;
                            default: // 선택지에 없는 문자 입력
                                Console.Clear();
                                Console.WriteLine("잘못 입력하셨습니다. 뒤로 돌아갑니다.");
                                break;
                        }
                        break;
                    case "0": // 나가기
                        caseCheck = false;
                        Console.Clear();
                        return;
                    default: // 선택지에 없는 문자 입력
                        Console.Clear();
                        Console.WriteLine("잘못 입력하셨습니다. 메인으로 돌아갑니다.");
                        break;
                }
            }


        }

        // Store 클래스. 상점 선택지를 선택했을 때의 결과 관리
        public class Store
        {
            Player player = Player.Instance;
            ItemManager itemManager = ItemManager.Instance;

            bool caseCheck; // 아이템 구매 또는 아이템 판매를 선택했을 때 true로 변경
            bool sell; // 판매창으로 들어갔을 때 보유하고 있는 아이템만 출력 (미구현)
            public void StoreMenu()
            {
                sell = false; // 기본설정
                caseCheck = false; // 기본설정
                Console.WriteLine();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
                Console.WriteLine();
                Console.WriteLine("[보유 골드]\n" + player.Gold); // 현재 소지금 표시
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                if(sell == true) // 판매창으로 들어갔을 때
                {
                    itemManager.BuyItem(); // 보유한 장비만 출력
                }
                else // 기본 상점 선택지 또는 구매창일 때
                {
                    itemManager.ShowItem(); // 모든 장비 출력
                }

                Console.WriteLine();
                if (caseCheck == false) // 구매 or 판매창일때 선택지 가리기
                {
                    Console.WriteLine("1. 아이템 구매");
                    Console.WriteLine("2. 아이템 판매");
                }
                else
                    Console.WriteLine("구매(판매)할 아이템 번호를 눌러주세요");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                string input = Console.ReadLine();

                if (caseCheck == false)
                {
                    switch (input)
                    {
                        case "1": // 구매
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
                                case "7":
                                    player.BuyAccessory(itemManager.accessoryItem[0]);
                                    break;
                                case "8":
                                    player.BuyAccessory(itemManager.accessoryItem[1]);
                                    break;
                                case "0": // 나가기
                                    caseCheck = false;
                                    Console.Clear();
                                    return;
                                default: // 선택지에 없는 문자 입력시
                                    Console.Clear();
                                    Console.WriteLine("잘못 입력하셨습니다. 뒤로 돌아갑니다.");
                                    break;
                            }
                            break;
                        case "2": // 판매
                            sell = true; // 판매가 가능한 물품만 출력
                            caseCheck = true;
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
                                case "7":
                                    player.SellAccessory(itemManager.accessoryItem[0]);
                                    break;
                                case "8":
                                    player.SellAccessory(itemManager.accessoryItem[1]);
                                    break;
                                case "0": // 나가기
                                    caseCheck = false;
                                    Console.Clear();
                                    return;
                                default: // 선택지에 없는 문자 입력시
                                    Console.Clear();
                                    Console.WriteLine("잘못 입력하셨습니다. 뒤로 돌아갑니다.");
                                    break;
                            }
                            break;
                        case "0": // 나가기
                            caseCheck = false;
                            Console.Clear();
                            return;
                        default: // 선택지에 없는 문자 입력시
                            Console.Clear();
                            Console.WriteLine("잘못 입력하셨습니다. 메인으로 돌아갑니다.");
                            break;
                    }
                }
           
            }
        }

        // Sleep 클래스. 휴식 메뉴 관리
        public class Sleep
        {
            Player player = Player.Instance; // 플레이어 인스턴스

            public void Healing() // 가격을 지불하고 휴식을 해 체력을 채운다.
            {
                Console.WriteLine();
                Console.WriteLine("휴식하기");
                Console.WriteLine($"500 G 를 내면 체력을 회복할 수 있습니다. \n(현재 체력 : {player.Health}\n(보유 골드 : {player.Gold}");
                Console.WriteLine("1. 휴식하기");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        if(player.Gold >= 500) // 소지금 500 G 이상 보유하고 있을때
                        {
                            player.Gold -= 500; // 소지금 차감
                            player.Health += 80; // 체력 회복
                            if(player.Health > (player.Health + (player.Level + (player.DungeonClear / 2)) * 10 - 10)) // 만약 최대 체력보다 많이 회복했을 시
                                player.Health = (player.Health + (player.Level + (player.DungeonClear / 2)) * 10 - 10); // 체력을 최대체력으로 변경
                            Console.Clear();
                            Console.WriteLine("500 G 를 지불하고 휴식을 취했습니다.");
                            Console.WriteLine();
                            break;
                        }
                        else // 금액 부족 시
                        {
                            Console.Clear();
                            Console.WriteLine("보유한 금액이 부족합니다.");
                            Console.WriteLine();
                            break;
                        }
                    case "0": // 나가기
                        Console.Clear();
                        return;
                    default: // 선택지에 없는 문자 입력시
                        Console.Clear();
                        Console.WriteLine("잘못 입력하셨습니다. 메인 화면으로 돌아갑니다");
                        break;
                }
            }
        }

        // Dungeon 클래스. 던전 메뉴 기능 관리 (일부 기능만 동작. 던전 미구현)
        public class Dungeon
        {
            Player player = Player.Instance; // 플레이어 인스턴스
            public void DungeonClear() // 던전 클리어
            {
                player.DungeonClear++; // 플레이어의 던전 클리어 횟수 증가 > 경험치 증가
                player.Gold += 1000; // 소지금 추가
                player.Health -= 30;// 플레이어 체력 차감
                if(player.Health < 0) // 플레이어 체력이 0 보다 떨어졌을 경우
                {
                    player.Health = 0; // 체력 0으로 변경
                    Console.Clear();
                    Console.WriteLine("던전 클리어 실패"); // 클리어 실패 문구 출력
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"던전 클리어! (클리어 횟수 : {player.DungeonClear})\n현재 체력 : {player.Health}"); // 던전 클리어 문구 및 현재 체력 표시
                    Console.WriteLine();
                }
            }
        }

        public class InGame // 인게임 메뉴 관리 클래스
        {
            Player player = Player.Instance;
            Store store = new Store();
            Inventory inventory = new Inventory();
            Sleep sleep = new Sleep();
            Dungeon dungeon = new Dungeon();
            public void InGameMenu()
            {
                while (true)
                {
                    if(player.Health == 0) // 플레이어의 체력이 0이 되었을 때
                    {
                        Console.WriteLine("사망했습니다."); // 사망 문구 출력
                        break; // 게임 종료
                    }
                    // 메뉴
                    Console.WriteLine("1. 상태보기");
                    Console.WriteLine("2. 인벤토리");
                    Console.WriteLine("3. 상점");
                    Console.WriteLine("4. 휴식하기");
                    Console.WriteLine("5. 던전");
                    Console.WriteLine();
                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    Console.Write(">> ");
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            player.PlayerStat(); // 상태보기
                            break;
                        case "2":
                            inventory.InventoryMenu(); // 인벤토리
                            break;
                        case "3":
                            store.StoreMenu(); // 상점
                            break;
                        case "4":
                            sleep.Healing(); // 휴식
                            break;
                        case "5":
                            dungeon.DungeonClear(); // 던전
                            break;
                        default: // 선택지에 없는 문자일시
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
            game.InGameMenu(); // 게임 메뉴 출력
        }
    }
}
