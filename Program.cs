using System.Diagnostics;


int BatHøjde = 5;
int LaneX = 10;
int LaneXTop = 5;
int LaneXbund = LaneXTop;
int LaneDX = 50;
int LaneY = 3;
int LaneDY = 23;

int score1 = 0;
int score2 = 0;
string name1 = "";
string name2 = "";
bool runGame = true;
bool spilKlar = false;
char boldTile = 'O';
bool RamtNogetVertikal = false;
bool RamtNogetHorizontal = false;
ConsoleKey tast;

TimeSpan BallMoveDeltaTime = TimeSpan.FromMilliseconds(100);
TimeSpan BallNextMoveTime;
Stopwatch BallMoveTimer = new Stopwatch();

int BatX1 = LaneX + 3;
int BatX2 = LaneDX - 3;

int BatY = (LaneY + LaneDY - BatHøjde) / 2;
int BatY2 = (LaneY + LaneDY - BatHøjde) / 2;

Random random = new Random();

int FartX = random.Next(0, 2) == 0 ? -1 : 1;
int FartY = random.Next(0, 2) == 0 ? -1 : 1;

Random randomx = new Random();
int BallStartX = randomx.Next(BatX1 + 1, LaneDX - 1);

Random randomy = new Random();
int BallStartY = randomy.Next(LaneY + 1, LaneDY - 1);

int BallY = BallStartY;
int BallX = BallStartX;

void Score2Tegn()
{
    if (score2 < 10)
    {
        Console.SetCursorPosition((LaneDX - 13), 1);
        Console.WriteLine(name2);
        Console.SetCursorPosition((LaneDX - 13), 2);
        Console.WriteLine($"score: {score2}point");
    }

    else
    {
        Console.SetCursorPosition((LaneDX - 14), 1);
        Console.WriteLine(name2);
        Console.SetCursorPosition((LaneDX - 14), 2);
        Console.WriteLine($"score: {score2}point");
    }
}

void Score1Tegn()
{
    Console.SetCursorPosition((LaneX), 1);
    Console.SetCursorPosition(LaneX, 2);
    Console.WriteLine($"score: {score1}point");
}

void BallRandom()
{
    Random random = new Random();

    FartX = random.Next(0, 2) == 0 ? -1 : 1;
    FartY = random.Next(0, 2) == 0 ? -1 : 1;

    Random randomx = new Random();
    BallX = randomx.Next((BatX1 + 3), (BatX2 - 3));

    Random randomy = new Random();
    BallY = randomy.Next((LaneY + 3), (LaneDY - 3));
}

void BallRemove(int x, int y)
{
    Console.SetCursorPosition(x, y);
    Console.Write(" ");
}

void BallDraw(int x, int y)
{
    Console.SetCursorPosition(x, y);
    Console.Write(boldTile);
}

void BallUpdate()

{
    {
        if (RamtNogetVertikal)
        {
            FartX = -FartX;
        }

        if (RamtNogetHorizontal)
        {
            FartY = -FartY;
        }

        BallRemove(BallX, BallY);

        BallX += FartX;
        BallY += FartY;

        BallDraw(BallX, BallY);
    }
}

void Ballbaglæns()
{
    if (BallX <= LaneX + 1 || BallX >= LaneDX - 1)
    {
        FartX = -FartX;
    }

    if (BallY <= LaneY + 1 || BallY >= LaneDY - 1)
    {
        FartY = -FartY;
    }

    BallRemove(BallX, BallY);

    BallX -= FartX;
    BallY -= FartY;

    BallDraw(BallX, BallY);
}

Console.SetCursorPosition(0, 0);

Console.BackgroundColor = ConsoleColor.Black;//Skifter skrivefarven til konsollen
Console.ForegroundColor = ConsoleColor.Red; //Forgrundsfarve ændres til Sort

Console.WriteLine("Velkommen til Pong");
Console.WriteLine("Du står nu overfor dit første valg");
Console.Write("Skriv 1 for en spiller eller skriv 2 for to spillere: ");
string input = Console.ReadLine()!;
Console.Clear();

int SpillerAnt = Convert.ToInt32(input);
Thread.Sleep(200);

if (SpillerAnt == 1 || SpillerAnt == 3)
{
    Console.WriteLine("skriv dit navn her");
    name1 = Console.ReadLine()!;
    Console.Clear();
}

if (SpillerAnt == 2 || SpillerAnt == 4)
{
    Console.WriteLine("skriv spiller 1 navn her");
    name1 = Console.ReadLine()!;
    Console.WriteLine(name1);
    Console.WriteLine("\n");

    Console.WriteLine("skriv spiller 2 navn her");
    name2 = Console.ReadLine()!;
    Console.WriteLine(name2);
    Console.WriteLine("\n");
    Console.Clear();

}

while (!spilKlar)
{
    Console.WriteLine("tryk enter for at begynde");
    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
    {
        spilKlar = true;

    }
}

if (spilKlar)
{

    if (runGame)
    {
        Console.Clear();
        Console.CursorVisible = false;

        for (int i = 0; i < LaneDX - LaneX - 1; i++)
        {
            Console.SetCursorPosition(LaneX + 1 + i, LaneY);
            Console.Write("─");

            Console.SetCursorPosition(LaneX + 1 + i, LaneDY);
            Console.Write("─");
        }

        for (int i = 0; i < LaneDY - LaneY; i++)
        {
            Console.SetCursorPosition(LaneX, LaneY + 1 + i);
            Console.Write("│");

            if (SpillerAnt == 2 || SpillerAnt == 4)
            {

                Console.SetCursorPosition(LaneDX, LaneY + 1 + i);
                Console.Write("│");
            }
        }

        Console.SetCursorPosition(LaneX, LaneY);
        Console.WriteLine("┌");

        Console.SetCursorPosition(LaneX, LaneDY);
        Console.WriteLine("└");

        Console.SetCursorPosition(LaneDX, LaneDY);
        Console.WriteLine("┘");

        Console.SetCursorPosition(LaneDX, LaneY);
        Console.WriteLine("┐");

        Score1Tegn();

        if (SpillerAnt == 2 || SpillerAnt == 4)
        {
            Score2Tegn();
        }
    }

    do
    {
        BallNextMoveTime = BallMoveDeltaTime;
        BallMoveTimer = Stopwatch.StartNew();

        while (runGame)
        {

            if (SpillerAnt == 1 || SpillerAnt == 2)
            {
                if (BallMoveTimer.Elapsed >= BallNextMoveTime)
                {
                    BallNextMoveTime = BallMoveTimer.Elapsed + BallMoveDeltaTime;
                    BallUpdate();
                }
            }

            if (SpillerAnt == 1 || SpillerAnt == 3)
            {
                for (int i = 0; i < BatHøjde; i++)
                {
                    Console.SetCursorPosition(BatX1, BatY + i);
                    Console.Write("█");
                }


                if (BatY >= LaneY + 1)
                {
                    Console.SetCursorPosition(BatX1, BatY + BatHøjde);
                    Console.Write(" ");
                }


                if (BatY > LaneY + 1)
                {
                    Console.SetCursorPosition(BatX1, BatY - 1);
                    Console.Write(" ");
                }
            }


            if (SpillerAnt == 2 || SpillerAnt == 4)
            {
                // bat 1

                for (int i = 0; i < BatHøjde; i++)
                {

                    Console.SetCursorPosition(BatX1, BatY + i);
                    Console.Write("█");
                }



                if (BatY >= LaneY + 1)
                {
                    Console.SetCursorPosition(BatX1, BatY + BatHøjde);
                    Console.Write(" ");
                }


                if (BatY > LaneY + 1)
                {
                    Console.SetCursorPosition(BatX1, BatY - 1);
                    Console.Write(" ");
                }


                // bat 2
                for (int i = 0; i < BatHøjde; i++)
                {
                    Console.SetCursorPosition(BatX2, BatY2 + i);
                    Console.Write("█");
                }

                if (BatY2 >= LaneY + 1)
                {
                    Console.SetCursorPosition(BatX2, BatY2 + BatHøjde);
                    Console.Write(" ");
                }


                if (BatY2 > LaneY + 1)
                {
                    Console.SetCursorPosition(BatX2, BatY2 - 1);
                    Console.Write(" ");
                }
            }

            // hit left wall
            if (BallX == LaneX + 1)
            {
                if (SpillerAnt == 2 || SpillerAnt == 4)
                {
                    score2++;
                    Score2Tegn();
                    BallRemove(BallX, BallY);
                    BallRandom();
                    BallDraw(BallX, BallY);
                    Console.Beep(500, 100);
                }

                RamtNogetVertikal = true;
            }
            else
            {
                RamtNogetVertikal = false;
            }

            // hit right wall
            if (BallX == LaneDX - 1)
            {
                if (SpillerAnt == 2 || SpillerAnt == 4)
                {
                    Score1Tegn();
                    BallRemove(BallX, BallY);
                    BallRandom();
                    BallDraw(BallX, BallY);
                    Console.Beep(500, 100);

                }

                RamtNogetVertikal = true;
            }
            else
            {
                RamtNogetVertikal = false;
            }

            // Hit top wall
            if ((BallY - 1) == LaneY)
            {

                RamtNogetHorizontal = true;
            }
            else
            {
                RamtNogetHorizontal = false;
            }


            // Hit Bottom wall
            if (BallY == LaneDY - 1)
            {

                RamtNogetHorizontal = true;
            }
            else
            {
                RamtNogetHorizontal = false;
            }

            // hit bat 1
            if (BallY >= BatY && BallY < BatY + BatHøjde && BallX == BatX1)
            {

                RamtNogetVertikal = true;
            }

            else
            {
                RamtNogetVertikal = false;
            }

            // hit bat 2
            if (BallY >= BatY2 && BallY < BatY2 + BatHøjde && BallX == BatX2)
            {

                RamtNogetVertikal = true;
            }

            else
            {
                RamtNogetVertikal = false;
            }

            if (Console.KeyAvailable)
            {
                tast = Console.ReadKey(true).Key;
                switch (tast)
                {

                    case ConsoleKey.W:
                        if (BatY > LaneY + 1)
                        {
                            BatY--;
                        }
                        break;


                    case ConsoleKey.S:
                        if (BatY < LaneDY - BatHøjde)
                        {
                            BatY++;
                        }
                        break;

                    case ConsoleKey.UpArrow:
                        if ((BatY2 > LaneY + 1) && SpillerAnt == 2 || SpillerAnt == 3)
                        {
                            BatY2--;
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if ((BatY2 < LaneDY - BatHøjde) && SpillerAnt == 2 || SpillerAnt == 4)
                        {
                            BatY2++;
                        }
                        break;


                    case ConsoleKey.Escape:
                        runGame = false;
                        break;

                    case ConsoleKey.RightArrow:
                        if (SpillerAnt == 3 || SpillerAnt == 4)
                        {

                            BallUpdate();
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (SpillerAnt == 3 || SpillerAnt == 4)
                        {
                            Ballbaglæns();
                        }
                        break;
                }

            }
        }
    }
    while (runGame);

    if (!runGame)
    {
        Console.Clear();
    }
}