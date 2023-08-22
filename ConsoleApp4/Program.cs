using System;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleApp4
{
	internal class Program
	{
		class BankAccount
		{
			public string GeneratePassword(int passwordLength)
			{
				Random random = new Random();
				string[] letters = new string[] {"A","B","C","D","E","F","G","H",
				"I","J","K","L","M","N","O","P",
				"Q","R","S","T","U","V","W","X","Y","Z" };
				string password = "";
				for (int i = 0; i < passwordLength; i++)
				{
					password += random.Next(0, 9).ToString();
				}
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("!Ваш пароль, не кому не сообщайте: " + password);
				//password[2] = "u";
				return password;
			}
		}

		class UserAccount : BankAccount
		{
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

		static void Main(string[] args)
		{
			int createPassword()
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Какой длины вы хотите пароль:");
				int passwordLength = int.Parse(Console.ReadLine());
				if (passwordLength < 3)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Ваш пароль слишком короткий минимальная длина три символа");
					createPassword();
				}
				if (passwordLength >= 3 && passwordLength <= 5)
				{
					Console.WriteLine("Ваш пароль лёгкий");
				}
				if (passwordLength >= 6 && passwordLength <= 10)
				{
					Console.WriteLine("Ваш пароль средний");
				}
				if (passwordLength >= 11 && passwordLength <= 15)
				{
					Console.WriteLine("Ваш пароль тяжёлый");
				}
				return passwordLength;
			}

			UserAccount bank = new UserAccount("4325 5673 3647 9003", "Sasha", 23.57, createPassword());
			/*bank.AccountInfo();
			bank.Balance();*/
			while (true)
			{
				Console.WriteLine("Если вы хотите узнать информацию о банковском аккаунте введите info \nЕсли вы хотите узнать информацию о количестве денег на карте аккаунте введите money \nЕсли вы хотите выйти введите outp");
				string userAnswer = Console.ReadLine();
				if (userAnswer == "outp")
				{
					Console.WriteLine("Вы успешно вышли");
					break;
				}
				if (userAnswer == "info")
				{
					Console.WriteLine("Это ваша информация о аккаунте" + bank.AccountInfo());		
				}
				if (userAnswer == "money")
				{
					Console.WriteLine("Это ваши деньги на аккаунте " + bank.Balance());
				}
			}
			
		}
	}
}
/*Сделать меню 
bank.AccountInfo();
bank.Balance();*/
