using System;
using System.Collections.Generic;
using System.Threading;

namespace ScreenSaver
{
	public class Vector2
	{
		public int x;
		public int y;

		public Vector2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

	}

	public class Character
	{
		public string symbol;
		public Vector2 pos;
		public string dir;

		public Character(string symbol)
		{
			this.symbol = symbol;
			this.pos = new Vector2(1, 1);
			this.dir = "bottom right";
		}

		public string getHDir()
		{
			return dir.Split(' ')[1];
		}

		public string getVDir()
		{
			return dir.Split(' ')[0];
		}

		public void toggleDir(char d)
		{
			string[] arrDir = this.dir.Split(' ');
			if (d == 'v')
			{
				arrDir[0] = arrDir[0] == "up" ? "bottom" : "up";
			}
			else
			{
				arrDir[1] = arrDir[1] == "left" ? "right" : "left";
			}
			this.dir = String.Join(' ', arrDir);
		}

	}

	public class Program
	{
		public static void Main()
		{
			int WIDTH = 24;
			int HEIGHT = 32;
			string canvas = "";
			List<Character> characters = new List<Character>();
			Random rnd = new Random();

			string symbols = "©®o0O@$#*%£¢€¥^°=~`|•√π÷×¶∆={}[™";
			int quantity = 1;

			Console.WriteLine("How many symbols do you want?");
			quantity = Convert.ToInt32(Console.ReadLine());
			Console.Clear();

			for (int i = 0; i < quantity; i++) characters.Add(new Character(symbols[rnd.Next(symbols.Length)] + ""));

			foreach (Character c in characters)
			{
				int rndX = rnd.Next(1, WIDTH);
				int rndY = rnd.Next(1, HEIGHT);
				string rndV = (new string[] { "up", "bottom" })[rnd.Next(2)];
				string rndH = (new string[] { "left", "right" })[rnd.Next(2)];
				c.pos = new Vector2(rndX, rndY);
				c.dir = String.Join(' ', new string[] { rndV, rndH });
			}




			while (true)
			{
				canvas = "";
				for (int i = 0; i < WIDTH; i++)
				{
					for (int j = 0; j < HEIGHT; j++)
					{
						Vector2 pos = new Vector2(i, j);
						bool hasCharacter = false;
						string symbol = "";
						foreach (Character c in characters)
						{
							if (pos.x == c.pos.x && pos.y == c.pos.y)
							{
								hasCharacter = true;
								symbol = c.symbol;
							}
						}
						if (isBorder(pos, WIDTH - 1, HEIGHT - 1)) canvas += "X";
						else if (hasCharacter) canvas += symbol;
						else canvas += " ";


					}
					canvas += "\n";
				}
				foreach (Character c in characters)
				{
					string HDir = c.getHDir();
					string VDir = c.getVDir();
					int x = getDir(HDir);
					int y = getDir(VDir);
					Vector2 nextPos = c.pos;
					nextPos.x += x;
					nextPos.y += y;
					if (isAny(new int[] { 0, WIDTH - 1 }, nextPos.x))
					{
						c.toggleDir('h');
						nextPos.x -= x * 2;
					}
					if (isAny(new int[] { 0, HEIGHT - 1 }, nextPos.y))
					{
						nextPos.y -= y * 2;
						c.toggleDir('v');
					}
				}
				Console.Clear();
				Console.Write(canvas);
				Thread.Sleep(60);

			}

		}

		public static bool isBorder(Vector2 pos, int width, int height)
		{
			if (isAny(new int[] { 0, width }, pos.x)) return true;
			if (isAny(new int[] { 0, height }, pos.y)) return true;

			return false;
		}

		public static bool isAny(int[] all, int val)
		{
			foreach (int n in all) if (val == n) return true;
			return false;
		}

		public static int getDir(string dir)
		{
			if (dir == "up" || dir == "left") return -1;
			return 1;
		}



	}
}
