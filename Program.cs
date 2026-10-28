using System.Diagnostics;


int BatHøjde = 5;
int LaneX = 10;
int LaneXTop = 5;
int LaneXbund = LaneXTop;
int LaneDX = 50;
int LaneY = 3;
int LaneDY = 23;



int FartX = 1;
int FartY = 1;




int score = 0;
bool runGame = true;
char boldTile = 'O';
ConsoleKey tast;

TimeSpan BallMoveDeltaTime = TimeSpan.FromMilliseconds(100);
TimeSpan BallNextMoveTime;
Stopwatch BallMoveTimer = new Stopwatch();


int BatX = LaneX + 3;
int BatX2 = LaneDX - 3;

int BatY = (LaneY + LaneDY - BatHøjde) / 2;
int BatY2 = (LaneY + LaneDY - BatHøjde) / 2;


Random randomx = new Random();
int BallStartX = randomx.Next(BatX + 1, LaneDX - 1);

Random randomy = new Random();
int BallStartY = randomy.Next(LaneY + 1, LaneDY - 1);




int BallY = BallStartY;
int BallX = BallStartX;




Console.SetCursorPosition(0, 0);

Console.BackgroundColor = ConsoleColor.Black;//Skifter skrivefarven til konsollen
Console.ForegroundColor = ConsoleColor.Red; //Forgrundsfarve ændres til Sort
Console.WriteLine("skriv dit navn her");
string name = Console.ReadLine()!;
Console.Clear();
Console.Write($"hej {name} du har nu {score} point");
Console.WriteLine("\n");

Thread.Sleep(200);

Console.Write("Skriv 1 eller 2: ");
string input = Console.ReadLine()!;

int SpillerAnt = Convert.ToInt32(input);


if (runGame == true)
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
        if (SpillerAnt == 2)
        {
            Console.SetCursorPosition(LaneX, LaneY + 1 + i);
            Console.Write("│");
        }

        Console.SetCursorPosition(LaneDX, LaneY + 1 + i);
        Console.Write("│");

    }


}


do
{
    BallNextMoveTime = BallMoveDeltaTime;
    BallMoveTimer = Stopwatch.StartNew();

    while (runGame == true)
    {


        if (BallMoveTimer.Elapsed >= BallNextMoveTime)
        {
            BallNextMoveTime = BallMoveTimer.Elapsed + BallMoveDeltaTime;
            BallUpdate();
        }

        if (SpillerAnt == 1)
        {


            for (int i = 0; i < BatHøjde; i++)
            {

                Console.SetCursorPosition(BatX, BatY + i);
                Console.Write("█");


            }



            if (BatY >= LaneY + 1)
            {
                Console.SetCursorPosition(BatX, BatY + BatHøjde);
                Console.Write(" ");

            }


            if (BatY > LaneY + 1)
            {
                Console.SetCursorPosition(BatX, BatY - 1);
                Console.Write(" ");
            }
        }


        if (SpillerAnt == 2)
        {
            // bat 1

            for (int i = 0; i < BatHøjde; i++)
            {

                Console.SetCursorPosition(BatX, BatY + i);
                Console.Write("█");


            }



            if (BatY >= LaneY + 1)
            {
                Console.SetCursorPosition(BatX, BatY + BatHøjde);
                Console.Write(" ");

            }


            if (BatY > LaneY + 1)
            {
                Console.SetCursorPosition(BatX, BatY - 1);
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


        bool HitLeftWall()
        {
            if (BallX == LaneX + 1 || -BallX == LaneX + 1)
            {
                return true;
            }
            return false;
        }

        bool HitRightWall()
        {
            if (BallX == LaneDX - 1 || -BallX == LaneX + 1)
            {
                return true;
            }
            return false;

        }

        bool HitTopWall()
        {
            if (BallY == LaneY + 1)
            {
                return true;
            }
            return false;

        }

        bool HitBottomWall()
        {
            if (BallY == LaneDY - 1)
            {
                return true;
            }
            return false;

        }

        bool HitBat1()
        {
            if (BallY >= BatY && BallY < BatY + BatHøjde && BallX == BatX)
            {
                return true;
            }

            return false;

        }

        bool HitBat2()
        {
            if (BallY >= BatY2 && BallY < BatY2 + BatHøjde && BallX == BatX2)
            {
                return true;
            }

            return false;

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


            if (HitBottomWall() || HitTopWall())
            {
                BallRemove(BallX, BallY);
                FartY = -FartY;
                BallX += FartX;
                BallY += FartY;
                BallDraw(BallX, BallY);

            }



            if (HitRightWall() || HitLeftWall() || HitBat1() || HitBat2())
            {
                BallRemove(BallX, BallY);
                FartX = -FartX;
                BallY += FartY;
                BallX += FartX;
                BallDraw(BallX, BallY);

            }

            else
            {
                BallRemove(BallX, BallY);
                BallY += FartY;
                BallX += FartX;
                BallDraw(BallX, BallY);
            }


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
                    if ((BatY2 > LaneY + 1) && SpillerAnt == 2)
                    {
                        BatY2--;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if ((BatY2 < LaneDY - BatHøjde) && SpillerAnt == 2)
                    {
                        BatY2++;
                    }
                    break;


                case ConsoleKey.Escape:
                    runGame = false;
                    break;






            }



        }
    }
}






while (runGame == true);

if (runGame == false)
{
    Console.Clear();
}



