using System;
using System.IO;

namespace ConsoleApp4
{
	internal class Program
	{
		class BankAccount
		{
			public string GeneratePassword(int passwordLength)
			{
				Random random = new Random();
				string password = "";
				for (int i = 0; i < passwordLength; i++)
				{
					password += random.Next(0, 9).ToString();
				}
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("!Ваш пароль, не кому не сообщайте: " + password);
				return password;
			}
		}

		class UserAccount : BankAccount
		{
			public string path = @"C:\BankAccount\cardInfo.txt";
			public string cardNumber = null;
			public string userName = null;
			public string password = null;
			public double money = 0;

			public UserAccount(string cardNumber, string userName, double money, int passwordLength)
			{
				this.cardNumber = cardNumber;
				this.userName = userName;
				this.password = this.GeneratePassword(passwordLength);
				this.money = money;
			}
			public string AccountInfo()
			{
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.WriteLine($"имя пользователя: {this.userName}\nномер карты: {this.cardNumber}");
				return "";
			}
			public void PutMoneyOnCard(int countMoney)
			{
				if (countMoney > 999)
				{
					Console.WriteLine("Привышен лимит пополнения");
					return;
				}
				if (countMoney < 0)
				{
					Console.WriteLine("Купюра несоотвествует подленности");
					return;
				}
				this.money += countMoney;
				
			}

			public void WithDrawMoneyFromCard(int countMoney2)
			{
				if (countMoney2 > this.money)
				{
					Console.WriteLine("У вас не достаточно средств");
					return;
				}
				if (countMoney2 < 0)
				{
					Console.WriteLine("Купюра несоотвествует подленности");
					return;
				}
				this.money -= countMoney2;
			}

			public void CreateFileInfo()
			{
				File.Create(path).Close();
				File.WriteAllText(path, ReturnFileInfo());

			}
			
			public string ReturnFileInfo()
			{
				return $"имя пользователя: {this.userName}\nномер карты: {this.cardNumber}";
			}

			public string Balance()
			{
				Console.ForegroundColor = ConsoleColor.Cyan;
				Console.Write("введите пароль карты чтобы посмотреть количество денег :");
				string answer = Console.ReadLine();
				if (answer == this.password)
				{
					Console.ForegroundColor = ConsoleColor.Cyan;
					Console.WriteLine($"ваши деньги на карточке {this.money}");
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Cyan;
					Console.WriteLine("пароль не верный ");
				}
				return "";
			}
		}

		static void Main()
		{
			int createPassword()
			{
				while (true)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Какой длины вы хотите пароль:");
					int passwordLength = int.Parse(Console.ReadLine());
					if (passwordLength < 3)
					{
						Console.ForegroundColor = ConsoleColor.Red;
					    Console.WriteLine("Ваш пароль слишком короткий минимальная длина три символа");						
					}
					if (passwordLength >= 3 && passwordLength <= 5)
					{
						Console.WriteLine("Ваш пароль лёгкий");
						return passwordLength;
					}
					if (passwordLength >= 6 && passwordLength <= 10)
					{
						Console.WriteLine("Ваш пароль средний");
						return passwordLength;
					}
					if (passwordLength >= 11)
					{
						Console.WriteLine("Ваш пароль тяжёлый");
						return passwordLength;
					}
				}
			}				

			UserAccount bank = new UserAccount("4325 5673 3647 9003", "Sasha", 23, createPassword());
			bank.CreateFileInfo();
			while (true)
			{
				Console.WriteLine("Если вы хотите узнать информацию о банковском аккаунте введите 1\n" +
					"Если вы хотите узнать информацию о количестве денег на карте аккаунте введите 2\n" +
					"Если вы хотите положить деньги на карточку то введите 3\n" +
					"Если вы хотите снять деньги с карточки то введите 4\n" +
					"Если вы хотите выйти введите 5");
				string userAnswer = Console.ReadLine();

				if (userAnswer == "1")
				{
					Console.WriteLine("Это ваша информация о аккаунте" + bank.AccountInfo());
				}

				if (userAnswer == "2")
				{
					Console.WriteLine("Это ваши деньги на аккаунте " + bank.Balance());
				}

				if (userAnswer == "3")
				{
					Console.WriteLine("Введите сколько денег вы хотите положить на карту");
					int putMoney = int.Parse(Console.ReadLine());
					bank.PutMoneyOnCard(putMoney);
				}

				if (userAnswer == "4")
				{
					Console.WriteLine("Введите сколько денег вы хотите снять с карты");
					int withdrawMoney = int.Parse(Console.ReadLine());
					bank.WithDrawMoneyFromCard(withdrawMoney);
				}

				if (userAnswer == "5")
				{
					Console.WriteLine("Вы успешно вышли");
					break;
				}
			}
		}
	}
}